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

public class Level : GameObject {
    private int enemiesLeft = 55;
    public static int currentNumberOfEnemies = 0;
    public static int screenWidth = 1366;
    public static int screenHeight = 768;
    private float backgroundSpeed = 0.05f;
    private string[] backgroundNames = new string[2];
    private SoundChannel levelMusic;
    private bool shouldBePlaying = true;
    Hud hudRef;
    Player player;


    public Level() {
        StartLevel();
        hudRef = game.FindObjectOfType<Hud>();
    }

    void Update() {
        LevelOne();
        LoopBackground();
        if(player.health <= 0)
        {
            shouldBePlaying = false;
            levelMusic.Stop();
        }
        StartMusicLoop();

    }
    void StartMusicLoop() {
        if (!levelMusic.IsPlaying && shouldBePlaying) {
            levelMusic = new Sound("GameLoop.wav", true).Play(true, 0);
            levelMusic.Volume = 1f;
            levelMusic.IsPaused = false;
        }
            
    }

    void StartLevel() {
        EasyDraw canvas = new EasyDraw(1366, 768);

        SpawnBackground(screenWidth / 2, screenHeight / 2, canvas, "Background1.png");
        SpawnBackground(screenWidth / 2, -screenHeight / 2, canvas, "Background1.png");
        backgroundNames[0] = "Background1";
        backgroundNames[1] = "Background2";

        player = new Player();
        player.SetXY(screenWidth / 2, screenHeight / 2);
        canvas.AddChild(player);

        AddChild(canvas);
        Console.WriteLine("MyGame initialized");
        enemiesLeft = 55;
        
        levelMusic = new Sound("GameIntro.wav", false).Play(true, 0);
        levelMusic.Volume = 1f;
        levelMusic.IsPaused = false;
    }

    void SpawnGrunt(float xPos, float yPos, EasyDraw canvas, Player player, int shootLevel) {
        Grunt grunt = new Grunt(shootLevel);
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
        List<GameObject> CurrentObjectsOnScreen = canvas.GetChildren();
        CurrentObjectsOnScreen.Remove(player);
        foreach (GameObject gameobject in CurrentObjectsOnScreen) {
            gameobject.LateRemove();
        }
    }

    void LevelOne() {
        EasyDraw canvas = FindObjectOfType<EasyDraw>();
        Player player = FindObjectOfType<Player>();

        //Console.WriteLine("{0}, {1}", currentNumberOfEnemies, enemiesLeft);

        if (currentNumberOfEnemies == 0 && enemiesLeft == 55) {
            SpawnGrunt(screenWidth / 2, 153, canvas, player, 1);
            }

        if (currentNumberOfEnemies == 0 && enemiesLeft == 54) {
            player.health = 5;
            hudRef.ResetSprites(player.health); 
            SpawnGrunt(screenWidth / 3, -50, canvas, player, 1);
            SpawnGrunt(screenWidth * 2 / 3, -50, canvas, player, 1);
        }

        if (currentNumberOfEnemies == 0 && enemiesLeft == 52) {
            player.health = 5;
            hudRef.ResetSprites(player.health);
            SpawnGrunt(227, -50, canvas, player, 1);
            SpawnGrunt(227 * 2, -50, canvas, player, 1);
            SpawnGrunt(227 * 3, -150, canvas, player, 1);
            SpawnGrunt(227, -400, canvas, player, 1);
        }

        if (currentNumberOfEnemies == 0 && enemiesLeft == 48) {
            player.health = 5;
            hudRef.ResetSprites(player.health);
            SpawnGrunt(227, -50, canvas, player, 1);
            SpawnGrunt(227 * 5, -50, canvas, player, 1);
            SpawnKamikazee(screenWidth / 2, -50, canvas, player);
        }

        if (currentNumberOfEnemies == 0 && enemiesLeft == 45) {
            player.health = 5;
            hudRef.ResetSprites(player.health);
            SpawnGrunt(227, -50, canvas, player, 1);
            SpawnGrunt(227 * 5, -50, canvas, player, 1);
            SpawnKamikazee(screenWidth / 2, 0, canvas, player);
            SpawnKamikazee(screenWidth / 3, -767 / 2, canvas, player);
            SpawnKamikazee(screenWidth * 2 / 3, -767 * 2 / 3, canvas, player);
            SpawnGrunt(-300, 0, canvas, player, 1);
            SpawnGrunt(1800, 0, canvas, player, 1);
        }

        if (currentNumberOfEnemies == 0 && enemiesLeft == 38) {
            player.health = 5;
            hudRef.ResetSprites(player.health);
            SpawnGrunt(227, 153, canvas, player, 1);
            SpawnGrunt(227 * 5, 153, canvas, player, 1);
            SpawnKamikazee(screenWidth / 4, 0, canvas, player);
            SpawnKamikazee(screenWidth * 3 / 4, 0, canvas, player);
            SpawnKamikazee(screenWidth / 2, -767 / 2, canvas, player);
            SpawnGrunt(250, -100, canvas, player, 1);
            SpawnGrunt(250 * 5, -150, canvas, player, 1);
            SpawnGrunt(250, -500, canvas, player, 1);
            SpawnGrunt(250 * 5, -700, canvas, player, 1);
            }

        if (currentNumberOfEnemies == 0 && enemiesLeft == 29) //enemiesLeft does not account for sniper
        {
            player.health = 5;
            hudRef.ResetSprites(player.health);
            SpawnGrunt(227, 153, canvas, player, 1);
            SpawnGrunt(227 * 5, 153, canvas, player, 1);
            SpawnKamikazee(screenWidth / 4, 0, canvas, player);
            SpawnKamikazee(screenWidth * 3 / 4, 0, canvas, player);
            SpawnKamikazee(screenWidth / 2, -767 / 2, canvas, player);
            SpawnGrunt(250, 500, canvas, player, 1);
            SpawnGrunt(250 * 5, 500, canvas, player, 1);
        }

        if (currentNumberOfEnemies == 0 && enemiesLeft == 22) {
            player.health = 5;
            hudRef.ResetSprites(player.health);
            SpawnSniper(screenWidth / 2, 100, canvas, player);
            SpawnGrunt(227, -50, canvas, player, 1);
            SpawnGrunt(227 * 5, -50, canvas, player, 1);
        }
        if (currentNumberOfEnemies == 0 && enemiesLeft == 19)
        {
            player.health = 5;
            hudRef.ResetSprites(player.health);
            SpawnSniper(screenWidth / 2, 100, canvas, player);
            SpawnSniper(screenWidth / 2, 100, canvas, player);
            SpawnGrunt(227, -50, canvas, player, 1);
            SpawnGrunt(227 * 5, -50, canvas, player, 1);
        }

        if (currentNumberOfEnemies == 0 && enemiesLeft == 15) {
            player.health = 5;
            hudRef.ResetSprites(player.health);
            SpawnGrunt(227, -50, canvas, player, 2);
            SpawnGrunt(227 * 5, -50, canvas, player, 2);
            }

        if (currentNumberOfEnemies == 0 && enemiesLeft == 13) {
            //set grunt shoot level 2 or spawn grunt level 2
            player.health = 5;
            hudRef.ResetSprites(player.health);
            SpawnGrunt(227, -50, canvas, player, 2);
            SpawnGrunt(227 * 5, -50, canvas, player, 2);
            SpawnSniper(screenWidth / 2, 660, canvas, player);
            SpawnGrunt(-300, 300, canvas, player, 2);
            SpawnGrunt(1700, 300, canvas, player, 2);
        }
        if (currentNumberOfEnemies == 0 && enemiesLeft == 8)
        {
            //set grunt shoot level 2 or spawn grunt level 2
            player.health = 5;
            hudRef.ResetSprites(player.health);
            SpawnGrunt(227, -50, canvas, player, 2);
            SpawnGrunt(227 * 5, -50, canvas, player, 2);
            SpawnGrunt(227, -350, canvas, player, 2);
            SpawnGrunt(227 * 5, -350, canvas, player, 2);
            SpawnGrunt(227, -650, canvas, player, 2);
            SpawnGrunt(227 * 5, -650, canvas, player, 2);

            SpawnGrunt(-300, 300, canvas, player, 2);
            SpawnGrunt(1700, 300, canvas, player, 2);
            }

        if (Input.GetKeyDown(Key.Q)) {
            ClearEnemies(canvas, player);
            currentNumberOfEnemies = 0;
        }
    }

    void SpawnBackground(float xPos, float yPos, EasyDraw canvas, string backgroundName) {
        Background background = new Background(backgroundName);
        background.SetXY(xPos, yPos);
        canvas.AddChildAt(background, 0);
    }

    void LoopBackground() {
        EasyDraw canvas = FindObjectOfType<EasyDraw>();
        GameObject[] backgrounds = FindObjectsOfType(typeof(Background));
        for (int i = 0; i < backgrounds.Length; i++) {
            if (backgrounds[i].y - 1.5 * screenHeight >= float.Epsilon) {
                backgrounds[i].LateRemove();
                
                switch (backgroundNames[i]) {
                    case "Background1":
                        SpawnBackground(screenWidth / 2, -screenHeight / 2, canvas, "Background3.png");
                        backgroundNames[i] = backgroundNames[i - 1];
                        backgroundNames[i - 1] = "Background3";
                        break;
                    case "Background2":
                        SpawnBackground(screenWidth / 2, -screenHeight / 2, canvas, "Background1.png");
                        backgroundNames[i] = backgroundNames[i - 1];
                        backgroundNames[i - 1] = "Background1";
                        break;
                    case "Background3":
                        SpawnBackground(screenWidth / 2, -screenHeight / 2, canvas, "Background2.png");
                        backgroundNames[i] = backgroundNames[i - 1];
                        backgroundNames[i - 1] = "Background2";
                        break;
                    default:
                        Console.WriteLine("Backgrounds broke");
                        break;
                }
                
            }

            backgrounds[i].y += backgroundSpeed * Time.deltaTime;
        }
    }
    async void sceneSwap() {
        await Task.Delay(4000);
        foreach (var item in game.GetChildren())
        {
            item.LateRemove();
        }
        enemiesLeft = 55;
        currentNumberOfEnemies = 0;

        Sprite sprite = new Sprite("PlayerSingle.png");
        game.AddChild(sprite);
        MainMenu main = new MainMenu();
        game.AddChild(main);

    }

}
