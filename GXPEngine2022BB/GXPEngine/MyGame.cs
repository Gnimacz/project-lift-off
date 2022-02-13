using System; // System contains a lot of default C# libraries 
using GXPEngine; // GXPEngine contains the engine
using System.Drawing; // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{
    public MyGame() : base(1366, 768, false)
    {// Create a window that's 800x600 and NOT fullscreen
        // Draw some things on a canvas:
        EasyDraw canvas = new EasyDraw(1366, 768);
        canvas.Clear(Color.MediumPurple);

        Player player = new Player();
        player.SetXY(width / 2, height/2);
        canvas.AddChild(player);

        //Grunt grunt = new Grunt();
        //grunt.SetXY(width / 2, height / 2);
        //canvas.AddChild(grunt);
        //grunt.SetTarget(player);

        //SuicideBoi suicideBoi = new SuicideBoi();
        //suicideBoi.SetXY(width / 3, height);
        //canvas.AddChild(suicideBoi);
        //suicideBoi.SetTarget(player);

        //Sniper sniper = new Sniper();
        //sniper.SetXY(width / 5, height / 5);
        //canvas.AddChild(sniper);
        //sniper.SetTarget(player);

        // Add the canvas to the engine to display it:
        AddChild(canvas);
        Console.WriteLine("MyGame initialized");
    }

    // For every game object, Update is called every frame, by the engine:
    void Update()
    {
        // Empty
    }

    static void Main() // Main() is the first method that's called when the program is run
    {
        new MyGame().Start(); // Create a "MyGame" and start it
    }
}