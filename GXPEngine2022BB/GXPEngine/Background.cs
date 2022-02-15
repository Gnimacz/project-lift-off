using System;
using System.Collections.Generic; // System contains a lot of default C# libraries 
using GXPEngine; // GXPEngine contains the engine
using System.Drawing;

    public class Background : Sprite{

        public Background() : base("Background.png", true, false) {
            SetOrigin(width / 2, height / 2);
        }
    }
