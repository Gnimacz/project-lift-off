using System;
using System.Collections.Generic; // System contains a lot of default C# libraries 
using GXPEngine; // GXPEngine contains the engine
using System.Drawing; // System.Drawing contains drawing tools such as Color definitions
using System.Threading.Tasks;
using System.Threading;

public class MyGame : Game {

    Sprite fake;
    public MyGame() : base(1366, 768, false) {
        Thread thread = new Thread(ControllerInput.GetControllerState);
        thread.Start();

        Setup();
        

        // Add the canvas to the engine to display it:
    }

    static void Main() // Main() is the first method that's called when the program is run
    {
        new MyGame().Start(); // Create a "MyGame" and start it
    }


    private void readscores()
    {
        Scoreboard.ReadScores();
    }

    //void Update()
    //{
    //    if (Globals.shouldReset)
    //    {
    //        Globals.shouldReset = false;
    //        Restart();
    //    }
    //}

    //not sure why but this seems to break the whole game. Not sure how to fix
    public async void Restart()
    {
        await Task.Delay(4000);
        foreach (GameObject item in game.GetChildren())
        {
            item.LateRemove();
        }
        await Task.Delay(1000);
        Setup();
    }

    private void Setup()
    {
        foreach (var item in GetChildren())
        {
            item.Remove();
        }

        //read the scoreboard file to get the current high score
        readscores();


        // Create a window that's 800x600 and NOT fullscreen
        // Draw some things on a canvas:
        MainMenu menu = new MainMenu();
        AddChild(menu);
    }


}