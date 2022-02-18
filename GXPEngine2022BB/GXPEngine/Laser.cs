using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GXPEngine.Core;
using GXPEngine;

    public class Laser : Sprite {
        public int damage = 3;
        public bool haveDealtDamage = false;
        private int startLiveTime = 0;
        private int currentLiveTime = 0;
        public static int maxLifeTime = 20000;
        private Sniper owner;
        
        
        public Laser(Sniper owner) : base("Laser.png", true) {
            SetOrigin(width / 2, 0);
            SetScaleXY(0.5f, 1.5f);
            //Console.WriteLine("1");
            startLiveTime = Time.now;
            this.owner = owner;
        }

        void Update() {
            //Console.WriteLine("2");
            LiveTimer();
            //Console.WriteLine("LazerTimer {0}, {1}", currentLiveTime, maxLifeTime);
            //Console.WriteLine("Laser x, y : {0}, {1}", x, y);
        }

        void LiveTimer() {
            currentLiveTime = Time.now - startLiveTime;
            if (currentLiveTime >= maxLifeTime) {
                owner.AllowRotation();
                LateRemove();
            }
        }
    }
