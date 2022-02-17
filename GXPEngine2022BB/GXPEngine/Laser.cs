using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;
using GXPEngine;

    public class Laser : Sprite {
        public int damage = 3;
        private int startLiveTime = 0;
        private int currentLiveTime = 0;
        public static int maxLifeTime = 20000;
        private Sniper owner;
        
        
        public Laser(Sniper owner) : base("Laser.png", true) {
            SetScaleXY(1f, 2.5f);
            SetOrigin(0, 0);
            //Console.WriteLine("1");
            startLiveTime = Time.now;
            this.owner = owner;
        }

        void Update() {
            //Console.WriteLine("2");
            LiveTimer();
            //Console.WriteLine("LazerTimer {0}, {1}", currentLiveTime, maxLifeTime);
            Console.WriteLine("Laser x, y : {0}, {1}", x, y);
        }

        void LiveTimer() {
            currentLiveTime = Time.now - startLiveTime;
            if (currentLiveTime >= maxLifeTime) {
                owner.AllowRotation();
                Destroy();
            }
        }
    }
