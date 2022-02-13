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

namespace GXPEngine
{
    public class Player : Sprite
    {
        private float speed = 300;
        private Vector2 velocity = new Vector2(0, 0);

        private Vector2 bulletSpawnPoints = new Vector2(0, 0);

        private Pivot bulletSpawnPoint = new Pivot();
        
        private float lastRotation = 0;
        private float desiredRotation = 0;

        private bool canShoot = true;
        private int shootDelay = 100;       //delay between bullet shots in ms


        public Player() : base("triangle.png")
        {
            SetOrigin(width / 2, height / 2);
            SetScaleXY(0.5f, 0.5f);

            bulletSpawnPoint.SetXY(x, y);

            LateAddChild(bulletSpawnPoint);

            Thread thread = new Thread(ControllerInput.GetControllerState);
            thread.Start();

        }

        void Update()
        { 
            Movement();
        }

        private void Movement()
        {
            
            velocity = new Vector2(0, 0);
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
                if(ControllerInput.secondaryJoystickY <= 0.19 && ControllerInput.secondaryJoystickY >= -0.19)
                {
                    rotation = lastRotation;
                }
            }
            else
            {
                desiredRotation = CalculateRotation(new Vector2(x - ControllerInput.secondaryJoystickX, y - ControllerInput.secondaryJoystickY));
                SetRotationBetween360();
                SlowRotation(0.2f);
                lastRotation = rotation;

                //make sure cardinal directions work with joystick
                if (ControllerInput.secondaryJoystickX <= -0.98 && ControllerInput.secondaryJoystickY <= 0.2 && ControllerInput.secondaryJoystickY >= -0.2) { rotation = 90; }
                else if (ControllerInput.secondaryJoystickX >= 0.98 && ControllerInput.secondaryJoystickY <= 0.2 && ControllerInput.secondaryJoystickY >= -0.2) { rotation = -90; }
                else if (ControllerInput.secondaryJoystickY >= 0.98 && ControllerInput.secondaryJoystickY > 0.2 && ControllerInput.secondaryJoystickX <= 0.2 && ControllerInput.secondaryJoystickX >= -0.2) { rotation = 0; }
                else if (ControllerInput.secondaryJoystickY <= -0.98 && ControllerInput.secondaryJoystickY < 0.2 && ControllerInput.secondaryJoystickX <= 0.2 && ControllerInput.secondaryJoystickX >= -0.2) { rotation = 180; }
                else { rotation = lastRotation; }
            }

            


            //shooting
            if (ControllerInput.Button2 == 1)
            {
                if (canShoot)
                {
                    Shoot();
                    canShoot = false;
                    ShootTimer(shootDelay);
                }
                
            }

            Translate(velocity.x * speed * Time.deltaTime/1000f, velocity.y * speed * Time.deltaTime/1000f);
            
        }


        void Shoot()
        {
            Bullet projectile = new Bullet("circle.png", 500, 0, -1, false);
            EasyDraw canvas = parent.FindObjectOfType<EasyDraw>();
            projectile.SetOrigin(projectile.width/ 2, projectile.height / 2);
            projectile.rotation = rotation;
            projectile.SetXY(x, y);
            canvas.AddChildAt(projectile, canvas.GetChildCount() - 1);
            
            
            
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
            Console.WriteLine("Done!");
        }

        //thanks Dan :)
        float CalculateRotation(Vector2 target)
        {
            float diffX = target.x - x;
            float diffY = target.y - y;
            float cos = Mathf.Abs(diffX) / Mathf.Sqrt(Mathf.Pow(Mathf.Abs(diffX), 2) + Mathf.Pow(Mathf.Abs(diffY), 2)); // calculate cos of desired angle
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

    }
}
