using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using Elementary;
using Elementary.Forms;

namespace GXPEngine {
    public class Player : Sprite
    {
        private float speed = 300;
        private Vector2 velocity = new Vector2(0, 0);

        private Pivot bulletSpawnPoint = new Pivot();

        private float lastRotation = 0;
        private float desiredRotation = 0;

        private bool canShoot = true;
        private int shootDelay = 150; //delay between bullet shots in ms
        private static int health = 5;

        private Hud hudRef;
        public int score;

        public Player() : base("Player.png")
        {
            SetOrigin(width / 2, height / 2);
            SetScaleXY(0.2f, 0.2f);

            bulletSpawnPoint.SetXY(x, y);

            LateAddChild(bulletSpawnPoint);

            Thread thread = new Thread(ControllerInput.GetControllerState);
            thread.Start();

            hudRef = game.FindObjectOfType<Hud>();

        }

        void Update()
        {
            Movement();

            //handle dying
            if (health <= 0)
            {
                if(Scoreboard.oldScore != "No scores yet")
                {
                    int oldScoreInt = int.Parse(Scoreboard.oldScore);
                    if(oldScoreInt < score)
                    {
                        Scoreboard.WriteScore(score);
                        hudRef.showHighScore = true;
                    }
                    else
                    {
                        Scoreboard.WriteScore(oldScoreInt);
                        hudRef.showHighScore = true;
                    }

                    
                }
                else if(Scoreboard.oldScore == "No scores yet")
                {
                    Scoreboard.WriteScore(score);
                    hudRef.showHighScore = true;
                    hudRef.showHighScoreText = false;
                }
                
                LateDestroy();
                Scoreboard.ReadScores();
            }

            score = Time.time;
            
        }

        private void Movement()
        {
            velocity = new Vector2(0, 0);
            if (ControllerInput.controllerConnected)
            {
                if (ControllerInput.joystickX > 0.04 || ControllerInput.joystickX < -0.04)
                {
                    velocity.x = ControllerInput.joystickX;
                }

                if (ControllerInput.joystickY > 0.04 || ControllerInput.joystickY < -0.04)
                {
                    velocity.y = ControllerInput.joystickY;
                }

                //rotation
                if (ControllerInput.secondaryJoystickX <= 0.19 && ControllerInput.secondaryJoystickX >= -0.19)
                {
                    if (ControllerInput.secondaryJoystickY <= 0.19 && ControllerInput.secondaryJoystickY >= -0.19)
                    {
                        rotation = lastRotation;
                    }
                }
                else
                {
                    desiredRotation = CalculateRotation(new Vector2(x - ControllerInput.secondaryJoystickX,
                        y - ControllerInput.secondaryJoystickY));
                    SetRotationBetween360();
                    SlowRotation(0.2f);
                    lastRotation = rotation;

                    //make sure cardinal directions work with joystick
                    if (ControllerInput.secondaryJoystickX <= -0.98 && ControllerInput.secondaryJoystickY <= 0.2 &&
                        ControllerInput.secondaryJoystickY >= -0.2)
                    {
                        rotation = 90;
                    }
                    else if (ControllerInput.secondaryJoystickX >= 0.98 && ControllerInput.secondaryJoystickY <= 0.2 &&
                             ControllerInput.secondaryJoystickY >= -0.2)
                    {
                        rotation = -90;
                    }
                    else if (ControllerInput.secondaryJoystickY >= 0.98 && ControllerInput.secondaryJoystickY > 0.2 &&
                             ControllerInput.secondaryJoystickX <= 0.2 && ControllerInput.secondaryJoystickX >= -0.2)
                    {
                        rotation = 0;
                    }
                    else if (ControllerInput.secondaryJoystickY <= -0.98 && ControllerInput.secondaryJoystickY < 0.2 &&
                             ControllerInput.secondaryJoystickX <= 0.2 && ControllerInput.secondaryJoystickX >= -0.2)
                    {
                        rotation = 180;
                    }
                    else
                    {
                        rotation = lastRotation;
                    }
                }
            }
            else if (!ControllerInput.controllerConnected)
            {
                if (Input.GetKey(Key.A))
                {
                    velocity.x = -1;
                }

                if (Input.GetKey(Key.D))
                {
                    velocity.x = 1;
                }

                if (Input.GetKey(Key.W))
                {
                    velocity.y = -1;
                }

                if (Input.GetKey(Key.S))
                {
                    velocity.y = 1;
                }

                desiredRotation = CalculateRotation(new Vector2(Input.mouseX, Input.mouseY));
                SetRotationBetween360();
                SlowRotation(0.2f);
                lastRotation = rotation;
            }


            //shooting
            if (ControllerInput.Button9 == 1 && canShoot || Input.GetKey(Key.SPACE) && canShoot)
            {
                Shoot();
                canShoot = false;
                ShootTimer(shootDelay);
            }

            Translate(velocity.x * speed * Time.deltaTime / 1000f, velocity.y * speed * Time.deltaTime / 1000f);
        }


        void Shoot()
        {
            Bullet projectile = new Bullet("circle.png", 1.5f, 0, -2, false);
            EasyDraw canvas = parent.FindObjectOfType<EasyDraw>();
            projectile.SetOrigin(projectile.width / 2, projectile.height / 2);
            projectile.rotation = rotation;
            projectile.SetXY(x, y);
            projectile.SetColor(255, 0, 255);
            canvas.AddChildAt(projectile, canvas.GetChildCount() - 2);
        }


        // <summary>
        // Waits for a certain amount of time before allowing the player to shoot again.
        // </summary>
        // <param name="timeToWait">
        // The time to wait in Miliseconds</param>
        // <returns>void</returns>
        async void ShootTimer(int timeToWait)
        {
            await Task.Delay(timeToWait);
            canShoot = true;
        }

        //thanks Dan :)
        //you are welcome)
        float CalculateRotation(Vector2 target)
        {
            float diffX = target.x - x;
            float diffY = target.y - y;
            float cos = Mathf.Abs(diffX) /
                        Mathf.Sqrt(Mathf.Pow(Mathf.Abs(diffX), 2) +
                                   Mathf.Pow(Mathf.Abs(diffY), 2)); // calculate cos of desired angle
            if (diffX > float.Epsilon && diffY < float.Epsilon)
            {
                rotation = 90 - Mathf.Acos(cos) * 360 / (Mathf.PI * 2); // set rotation in degrees
            }
            else if (diffX > float.Epsilon && diffY > float.Epsilon)
            {
                rotation = 90 + Mathf.Acos(cos) * 360 / (Mathf.PI * 2);
            }
            else if (diffX < float.Epsilon && diffY > float.Epsilon)
            {
                rotation = 270 - Mathf.Acos(cos) * 360 / (Mathf.PI * 2);
            }
            else
            {
                rotation = 270 + Mathf.Acos(cos) * 360 / (Mathf.PI * 2);
            }

            return rotation;
        }

        void SetRotationBetween360()
        {
            if (rotation < float.Epsilon)
                rotation = 360 + rotation;
            else
                rotation = rotation % 360;
        }

        void SlowRotation(float rotationSpeed)
        {
            float positiveTurn = desiredRotation - rotation;
            float degreeToTurn = 0f;
            if (positiveTurn > float.Epsilon)
            {
                if (positiveTurn - 180 < float.Epsilon) degreeToTurn = positiveTurn;
                else degreeToTurn = -positiveTurn;
            }
            else
            {
                if (Mathf.Abs(positiveTurn) - 180 > float.Epsilon) degreeToTurn = -positiveTurn;
                else degreeToTurn = positiveTurn;
            }

            if (Mathf.Abs(degreeToTurn) - rotationSpeed * Time.deltaTime < float.Epsilon)
                rotation = desiredRotation;
            else
            {
                rotation += Mathf.Sign(degreeToTurn) * rotationSpeed * Time.deltaTime;
            }
        }


        //Bullet and health logic
        void OnCollision(GameObject other)
        {
            if (other is Bullet)
            {
                Bullet bullet = other.FindObjectOfType<Bullet>();
                if (bullet.canDamage)
                {
                    TakeDamage(bullet.damage);


                    other.LateDestroy();
                }
            }

            if (other is SuicideBoi)
            {
                SuicideBoi damager = other.FindObjectOfType<SuicideBoi>();
                TakeDamage(damager.damage);
                other.LateRemove();
                Level.currentNumberOfEnemies--;
            }

            if (other is Laser) {
                Laser laser = other.FindObjectOfType<Laser>();
                TakeDamage(laser.damage);
            }

        }

        private void TakeDamage(int damageAmount)
        {
            if(hudRef == null) { hudRef = game.FindObjectOfType<Hud>(); }
            hudRef.RemoveHealth(damageAmount);
            if (health < damageAmount) {
                health = 0;
            }
            else {
                health -= damageAmount;
            }
        }
    }
}