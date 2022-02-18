using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;
using GXPEngine;

namespace GXPEngine {
    public class Bullet : Sprite {
        private float movementSpeed;
        private Vector2 movementDir;
        public int damage = 1;
        public bool canDamage = true;

        public Bullet(String bulletImage, float bulletSpeed, float xDirection, float yDirection, bool canHarmPlayer) :
            base(bulletImage, true) {
            movementSpeed = bulletSpeed;
            movementDir = new Vector2(xDirection, yDirection);
            SetScaleXY(0.3f, 0.3f);
            canDamage = canHarmPlayer;
        }

        public Bullet(String bulletImage, float bulletSpeed, float xDirection, float yDirection, bool canHarmPlayer,
            int damage, float bulletSize) : base(bulletImage, true) {
            movementSpeed = bulletSpeed;
            movementDir = new Vector2(xDirection, yDirection);
            SetScaleXY(bulletSize, bulletSize);
            canDamage = canHarmPlayer;
            this.damage = damage;
        }

        void Update() {
            MoveBullet();
            OutOfBounds();
        }

        void MoveBullet() {
            float movementLength = Mathf.Sqrt(Mathf.Pow(movementDir.x, 2) + Mathf.Pow(movementDir.y, 2));
            Move(movementDir.x / movementLength * movementSpeed * Time.deltaTime,
                movementDir.y / movementLength * movementSpeed * Time.deltaTime);
        }

        private void OutOfBounds() {
            if (x - Level.screenWidth > float.Epsilon || x < float.Epsilon)
                LateRemove();
            if (y - Level.screenHeight > float.Epsilon || y < float.Epsilon)
                LateRemove();
        }
    }
}