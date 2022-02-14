using System;
using System.Collections.Generic; // System contains a lot of default C# libraries 
using GXPEngine; // GXPEngine contains the engine
using System.Drawing; // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {
    private int currentNumberOfEnemies = 0;
    private int EnemiesLeft = 40;
    public MyGame() : base(1366, 768, false)
    {// Create a window that's 800x600 and NOT fullscreen
        // Draw some things on a canvas:

        LevelSpawn();

        //Sniper sniper = new Sniper();
        //sniper.SetXY(width / 5, height / 5);
        //canvas.AddChild(sniper);
        //sniper.SetTarget(player);

        // Add the canvas to the engine to display it:

    }

    // For every game object, Update is called every frame, by the engine:
    void Update() {
        EasyDraw canvas = FindObjectOfType<EasyDraw>();
        Player player = FindObjectOfType<Player>();
        
        if (currentNumberOfEnemies == 0 && EnemiesLeft == 40) {
            SpawnGrunt(width/2, height/2, canvas, player);
        
            SpawnKamikazee(width/3, height, canvas, player);
        }

        if (currentNumberOfEnemies == 0 && EnemiesLeft == 38) {
            
        }

        if (Input.GetKeyDown(Key.Q)) {
            ClearEnemies(canvas, player);
        }
    }

    static void Main() // Main() is the first method that's called when the program is run
    {
        new MyGame().Start(); // Create a "MyGame" and start it
    }

    void SpawnGrunt(float xPos, float yPos, EasyDraw canvas, Player player) {
        Grunt grunt = new Grunt();
        grunt.SetXY(xPos, yPos);
        canvas.AddChild(grunt);
        grunt.SetTarget(player);
        currentNumberOfEnemies++;
        EnemiesLeft--;
    }
    
    void SpawnKamikazee(float xPos, float yPos, EasyDraw canvas, Player player) {
        SuicideBoi suicideBoi = new SuicideBoi();
        suicideBoi.SetXY(xPos, yPos);
        canvas.AddChild(suicideBoi);
        suicideBoi.SetTarget(player);
        currentNumberOfEnemies++;
        EnemiesLeft--;
    }

    void LevelSpawn() {
        EasyDraw canvas = new EasyDraw(1366, 768);
        canvas.Clear(Color.MediumPurple);

        Player player = new Player();
        player.SetXY(width / 2, height/2);
        canvas.AddChild(player);

        
        
        AddChild(canvas);
        Console.WriteLine("MyGame initialized");
    }

    void ClearEnemies(EasyDraw canvas, Player player) {
        List<GameObject> CurrentObjectsONScreen = canvas.GetChildren();
        CurrentObjectsONScreen.Remove(player);
        foreach (GameObject gameobject in CurrentObjectsONScreen) {
            gameobject.LateDestroy();
        }
        
        
    }
}