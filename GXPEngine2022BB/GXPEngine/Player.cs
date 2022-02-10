using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Player : Sprite
    {
        private float speed = 200;
        private Vector2 velocity = new Vector2(0, 0);

        private Vector2 bulletSpawnPoints = new Vector2(0, 0);

        private Pivot bulletSpawnPoint = new Pivot();
        private float lastRotation = 0;
        private bool canShoot = true;
        private int shootDelay = 100;       //delay between bullet shots in ms


        public Player() : base("triangle.png")
        {
            SetOrigin(width / 2, height / 2);

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
            if (ControllerInput.Button3 == 1)
            {
                velocity.x = -1;
            }
            if (ControllerInput.Button6 == 1)
            {
                velocity.x = 1;
            }
            if (ControllerInput.Button4 == 1)
            {
                velocity.y = -1;
            }
            if (ControllerInput.Button5 == 1)
            {
                velocity.y = 1;
            }

            //rotation
            if(ControllerInput.joystickX <= 0.03 && ControllerInput.joystickX >= -0.03)
            {
                if(ControllerInput.joystickY <= 0.03 && ControllerInput.joystickY >= -0.03)
                {
                    rotation = lastRotation;
                }
            }
            else
            {
                rotation = CalculateRotation(new Vector2(x - ControllerInput.joystickX, y - ControllerInput.joystickY));
                lastRotation = rotation;
            }

            
            //shooting
            if (ControllerInput.Button7 == 1)
            {
                if (canShoot)
                {
                    Shoot();
                    canShoot = false;
                    ShootTimer(shootDelay);
                }
                
            }

            Move(velocity.x * speed * Time.deltaTime/1000f, velocity.y * speed * Time.deltaTime/1000f);
            
        }


        void Shoot()
        {
            Bullet projectile = new Bullet("circle.png", 500, 0, -1);
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
            Console.WriteLine(rotation);
            return rotation;
        }
    }
}
