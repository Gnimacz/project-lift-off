using System;
using System.Collections.Generic; // System contains a lot of default C# libraries 
using GXPEngine; // GXPEngine contains the engine
using System.Drawing; // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {
    public MyGame() : base(1366, 768, false) {
        // Create a window that's 800x600 and NOT fullscreen
        // Draw some things on a canvas:

        Level level = new Level();
        AddChild(level);

        Hud hud = new Hud();
        AddChild(hud);

        // Add the canvas to the engine to display it:
    }

    static void Main() // Main() is the first method that's called when the program is run
    {
        new MyGame().Start(); // Create a "MyGame" and start it
    }
}