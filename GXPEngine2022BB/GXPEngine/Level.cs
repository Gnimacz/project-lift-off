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
using System.Drawing;
using GXPEngine.Managers;

public class Level : GameObject{
        
    private int enemiesLeft = 55;
    public static int currentNumberOfEnemies = 0;
    private int screenWidth = 1366;
    private int screenHeight = 768;
    
    public Level() {
        LevelSpawn();
    }

    void Update() {
        LevelOne();
    }

    void LevelSpawn() {
        EasyDraw canvas = new EasyDraw(1366, 768);
        canvas.Clear(Color.MediumPurple);

        Player player = new Player();
        player.SetXY(screenWidth / 2, screenHeight/2);
        canvas.AddChild(player);
        
        AddChild(canvas);
        Console.WriteLine("MyGame initialized");
        enemiesLeft = 55;
    }
    
    void SpawnGrunt(float xPos, float yPos, EasyDraw canvas, Player player) {
        Grunt grunt = new Grunt();
        grunt.SetXY(xPos, yPos);
        canvas.AddChild(grunt);
        grunt.SetTarget(player);
        currentNumberOfEnemies++;
        enemiesLeft--;
    }
    
    void SpawnKamikazee(float xPos, float yPos, EasyDraw canvas, Player player) {
        SuicideBoi suicideBoi = new SuicideBoi();
        suicideBoi.SetXY(xPos, yPos);
        canvas.AddChild(suicideBoi);
        suicideBoi.SetTarget(player);
        currentNumberOfEnemies++;
        enemiesLeft--;
    }

    void SpawnSniper(float xPos, float yPos, EasyDraw canvas, Player player) {
        Sniper sniper = new Sniper();
        sniper.SetXY(xPos, yPos);
        canvas.AddChild(sniper);
        sniper.SetTarget(player);
        currentNumberOfEnemies++;
        enemiesLeft--;
    }
    
    void ClearEnemies(EasyDraw canvas, Player player) {
        List<GameObject> CurrentObjectsONScreen = canvas.GetChildren();
        CurrentObjectsONScreen.Remove(player);
        foreach (GameObject gameobject in CurrentObjectsONScreen) {
            gameobject.LateDestroy();
        }
    }
    
    void LevelOne() {
        EasyDraw canvas = FindObjectOfType<EasyDraw>();
        Player player = FindObjectOfType<Player>();
        
        Console.WriteLine("{0}, {1}", currentNumberOfEnemies, enemiesLeft);
        
        if (currentNumberOfEnemies == 0 && enemiesLeft == 55) {
            SpawnGrunt(screenWidth/2, 153, canvas, player);
        }
        if (currentNumberOfEnemies == 0 && enemiesLeft == 54) {
            SpawnGrunt(screenWidth / 3, 153, canvas, player);
            SpawnGrunt(screenWidth*2 / 3, 153, canvas, player);
        }

        if (currentNumberOfEnemies == 0 && enemiesLeft == 52)
        {
            SpawnGrunt(227, 153, canvas, player);
            SpawnGrunt(227* 2 ,153 , canvas, player);
            SpawnGrunt(227 * 3,153 , canvas, player);
            SpawnGrunt(1400, -150, canvas, player);
        }
        if (currentNumberOfEnemies == 0 && enemiesLeft == 48)
        {
            SpawnGrunt(227, 153, canvas, player);
            SpawnGrunt(227 * 5, 153, canvas, player);
            SpawnKamikazee(screenWidth / 2, 0, canvas, player);

        }
        if (currentNumberOfEnemies == 0 && enemiesLeft == 45)
        {
            SpawnGrunt(227, 153, canvas, player);
            SpawnGrunt(227 * 5, 153, canvas, player);
            SpawnKamikazee(screenWidth / 2, 0, canvas, player);
            SpawnKamikazee(screenWidth / 3, -767/2, canvas, player);
            SpawnKamikazee(screenWidth *2/ 3, -767 *2/ 3, canvas, player);
            SpawnGrunt(-150, 0, canvas, player);
            SpawnGrunt(1500, 0, canvas, player);
        }
        if (currentNumberOfEnemies == 0 && enemiesLeft == 38)
        {
            SpawnGrunt(227, 153, canvas, player);
            SpawnGrunt(227 * 5, 153, canvas, player);
            SpawnKamikazee(screenWidth / 4, 0, canvas, player);
            SpawnKamikazee(screenWidth * 3 / 4, 0, canvas, player);
            SpawnKamikazee(screenWidth / 2, -767 /2, canvas, player);
            SpawnGrunt(250, 50, canvas, player);
            SpawnGrunt(250 * 5, 50, canvas, player);
            SpawnGrunt(250, -200, canvas, player);
            SpawnGrunt(250 * 5, -200, canvas, player);
            SpawnGrunt(250*2, -200, canvas, player);
            SpawnGrunt(250 * 4, -200, canvas, player);
        }
        if (currentNumberOfEnemies == 0 && enemiesLeft == 27)//enemiesLeft does not account for sniper
        {
            SpawnGrunt(227, 153, canvas, player);
            SpawnGrunt(227 * 5, 153, canvas, player);
            SpawnKamikazee(screenWidth / 4, 0, canvas, player);
            SpawnKamikazee(screenWidth * 3 / 4, 0, canvas, player);
            SpawnKamikazee(screenWidth / 2, -767 / 2, canvas, player);
            SpawnGrunt(250, 50, canvas, player);
            SpawnGrunt(250 * 5, 50, canvas, player);
            SpawnSniper(screenWidth/2, -120, canvas, player);
        }
        if (currentNumberOfEnemies == 0 && enemiesLeft == 20)
        {
            SpawnSniper(screenWidth / 2, 100, canvas, player);
            SpawnSniper(screenWidth / 2, 660, canvas, player);
            SpawnSniper(1200, screenHeight/2, canvas, player);
            SpawnSniper(100, screenHeight/2, canvas, player);
            SpawnKamikazee(700, 0, canvas, player);
            SpawnKamikazee(750, 0, canvas, player);
            SpawnKamikazee(800, 0, canvas, player);
            SpawnKamikazee(700, 900, canvas, player);
            SpawnKamikazee(750, 900, canvas, player);
            SpawnKamikazee(800, 900, canvas, player);
        }
        if (currentNumberOfEnemies == 0 && enemiesLeft == 14)
        {
            //set grunt shoot level 2 or spawn grunt level 2
            SpawnGrunt(227, 153, canvas, player);
            SpawnGrunt(227 * 5, 153, canvas, player);
            SpawnGrunt(250, 50, canvas, player);
            SpawnGrunt(250 * 5, 50, canvas, player);
        }
        if (currentNumberOfEnemies == 0 && enemiesLeft == 10)
        {
            //set grunt shoot level 2 or spawn grunt level 2
            SpawnGrunt(227, 153, canvas, player);
            SpawnGrunt(227 * 5, 153, canvas, player);
            SpawnGrunt(250, 50, canvas, player);
            SpawnGrunt(250 * 5, 50, canvas, player);
            // SpawnSniper(screenWidth / 2, 660, canvas, player);
            SpawnGrunt(-227, 153, canvas, player);
            SpawnGrunt(1600, 153, canvas, player);
            SpawnGrunt(-227, 50, canvas, player);
            SpawnGrunt(1600, 50, canvas, player);
        }


        if (Input.GetKeyDown(Key.Q)) {
            ClearEnemies(canvas, player);
            currentNumberOfEnemies = 0;
        }

    } 
}
