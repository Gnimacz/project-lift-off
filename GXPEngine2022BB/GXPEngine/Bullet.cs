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
        public int damage = 1;
        public bool canDamage = true;

        public Bullet(String bulletImage, float bulletSpeed, float xDirection, float yDirection, bool canHarmPlayer) : base(bulletImage, true)
        {
            movementSpeed = bulletSpeed;
            movementDir = new Vector2(xDirection, yDirection);
            SetScaleXY(0.5f, 0.5f);
            canDamage = canHarmPlayer;
        }

        public Bullet(String bulletImage, float bulletSpeed, float xDirection, float yDirection, bool canHarmPlayer, int damage) : base(bulletImage, true)
        {
            movementSpeed = bulletSpeed;
            movementDir = new Vector2(xDirection, yDirection);
            SetScaleXY(0.5f, 0.5f);
            canDamage = canHarmPlayer;
            this.damage = damage;
        }

        void Update()
        {
            MoveBullet();
            OutOfBounds();
        }

        void MoveBullet()
        {
            Move(movementDir.x * movementSpeed * Time.deltaTime / 1000f, movementDir.y * movementSpeed * Time.deltaTime / 1000f);
        }
        

        private void OutOfBounds()
        {
            if(x - Level.screenWidth > float.Epsilon || x < float.Epsilon)
                LateDestroy();
            if (y - Level.screenHeight > float.Epsilon || y < float.Epsilon)
                LateDestroy();
        }
    }
}