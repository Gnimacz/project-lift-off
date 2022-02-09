using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GXPEngine;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Player : Sprite
    {
        private float speed = 200;
        private Vector2 velocity = new Vector2(0, 0);
        private float rotationSpeed = 1.5f;

        private Vector2 bulletSpawnPoints = new Vector2(0, 0);

        private Pivot bulletSpawnPoint = new Pivot();

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

            //rotation
            if (Input.GetKey(Key.LEFT))
            {
                rotation -= rotationSpeed;
            }
            if (Input.GetKey(Key.RIGHT))
            {
                rotation += rotationSpeed;
            }

            //shooting
            if (ControllerInput.buttonPressed == 1)
            {
                Shoot();
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
    }
}
