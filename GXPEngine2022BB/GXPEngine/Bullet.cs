using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;
using GXPEngine;

namespace GXPEngine
{
    public class Bullet : Sprite
    {
        private float movementSpeed;
        private Vector2 movementDir;


        public Bullet(String bulletImage, float bulletSpeed, float xDirection, float yDirection) : base(bulletImage, true)
        {
            movementSpeed = bulletSpeed;
            movementDir = new Vector2(xDirection, yDirection);
        }

        void Update()
        {
            MoveBullet();
        }

        void MoveBullet()
        {
            Move(movementDir.x * movementSpeed * Time.deltaTime / 1000f, movementDir.y * movementSpeed * Time.deltaTime / 1000f);
        }
    }
}
