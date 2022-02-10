using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;
using GXPEngine;
using TiledMapParser;

public class Grunt : Sprite {
        private float speed = 0.3f;
        private Sprite target;
        private bool isMoving = false;
        private int lastShootTime = 0;
        private int shootIntervals = 700;
        private Vector2 movePoint;
        
        public Grunt() : base("square.png")
        {
            SetOrigin(width / 2, height / 2);
            ChoseMovement();
        }

        void Update() {
            Movement();
            SetRotation();
            if (Time.time > lastShootTime) {
               Shoot();
               lastShootTime = Time.time + shootIntervals;
             }
        }

        void ChoseMovement() {
            movePoint.x = Utils.Random(50, 1316);
            movePoint.y = Utils.Random(30, 738);
        }

        public void SetTarget(Sprite target) {
            this.target = target;
        }

        void Shoot() {
            Bullet projectile = new Bullet("circle.png", 500, 0, -1, true);
            EasyDraw canvas = parent.FindObjectOfType<EasyDraw>();
            projectile.SetOrigin(projectile.width/ 2, projectile.height / 2);
            projectile.rotation = rotation;
            projectile.SetXY(x, y);
            canvas.AddChildAt(projectile, canvas.GetChildCount() - 1);
        }
        
        void SetRotation() {
            float diffX = target.x - x;
            float diffY = target.y - y;
            float cos = Mathf.Abs(diffX) / Mathf.Sqrt(Mathf.Pow(Mathf.Abs(diffX), 2) + Mathf.Pow(Mathf.Abs(diffY), 2)); // calculate cos of desired angle
            if (diffX > float.Epsilon && diffY < float.Epsilon) {
                rotation = 90 - Mathf.Acos(cos) * 360 / (Mathf.PI * 2); // set rotation in degrees
            }
            else if (diffX > float.Epsilon && diffY > float.Epsilon) {
                rotation = 90 + Mathf.Acos(cos) * 360 / (Mathf.PI * 2);
            }
            else if (diffX < float.Epsilon && diffY > float.Epsilon) {
                rotation = 270 - Mathf.Acos(cos) * 360 / (Mathf.PI * 2);
            }
            else {
                rotation = 270 + Mathf.Acos(cos) * 360 / (Mathf.PI * 2);
            }
        }

        void Movement() {
            float lengthToPoint = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(movePoint.x - x), 2) + Mathf.Pow(Mathf.Abs(movePoint.y - y), 2));
            Console.WriteLine("{0}, {1}", Mathf.Sqrt(Mathf.Pow(Mathf.Abs(movePoint.x - x) / lengthToPoint, 2) +
                                                     Mathf.Pow(Mathf.Abs(movePoint.y - y) / lengthToPoint, 2)) * speed * Time.deltaTime, lengthToPoint);
            Console.WriteLine("x = {0}, y = {1}", movePoint.x, movePoint.y);
            if (Mathf.Sqrt(Mathf.Pow(Mathf.Abs(movePoint.x - x) / lengthToPoint, 2) +
                Mathf.Pow(Mathf.Abs(movePoint.y - y) / lengthToPoint, 2)) * speed * Time.deltaTime > lengthToPoint) {
                ChoseMovement();   
            }
            else {  
                Translate((movePoint.x - x) / lengthToPoint * speed * Time.deltaTime, (movePoint.y - y) / lengthToPoint * speed * Time.deltaTime);
            }

            Console.WriteLine(Utils.frameRate);

        }
    }
