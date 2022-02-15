using System;
using System.Collections.Generic; // System contains a lot of default C# libraries 
using GXPEngine; // GXPEngine contains the engine
using System.Drawing;

    public class Background : Sprite{

        public Background(string backgroundName) : base(backgroundName, true, false) {
            SetOrigin(width / 2, height / 2);
        }
    }
