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

            BulletTimer();
        }

        public Bullet(String bulletImage, float bulletSpeed, float xDirection, float yDirection, bool canHarmPlayer, int damage) : base(bulletImage, true)
        {
            movementSpeed = bulletSpeed;
            movementDir = new Vector2(xDirection, yDirection);
            SetScaleXY(0.5f, 0.5f);
            canDamage = canHarmPlayer;
            this.damage = damage;

            BulletTimer();
        }

        void Update()
        {
            MoveBullet();
        }

        void MoveBullet()
        {
            if(x >= game.width || x <= 0)
            {
                movementDir.x *= -1;
            }
            if (y >= game.height || y <= 0)
            {
                movementDir.y *= -1;
            }
            Move(movementDir.x * movementSpeed * Time.deltaTime / 1000f, movementDir.y * movementSpeed * Time.deltaTime / 1000f);
        }
        

        private async void BulletTimer()
        {
            await Task.Delay(2000);
            LateRemove();
        }
    }
}