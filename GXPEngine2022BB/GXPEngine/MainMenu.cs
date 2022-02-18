using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GXPEngine;

namespace GXPEngine
{
    class MainMenu : GameObject
    {
        EasyDraw text;
        private bool canBlink = true;
        private int blinkDelay = 1000;
        private SoundChannel MenuIntro;
        private bool shouldBePlaying = true;

        public MainMenu() : base()
        {
            Sprite menuSprite = new Sprite("StartMenu.png");
            AddChild(menuSprite);
            text = new EasyDraw(game.width,game.height);
            AddChild(text);
            text.TextAlign(CenterMode.Center, CenterMode.Center);
            text.TextFont(Utils.LoadFont("nove.ttf", 20));
            text.Text("Shoot To Start", game.width / 2, game.height - 256);
            MenuIntro = new Sound("MenuIntro.wav", false).Play(true, 0);
            MenuIntro.Volume = 0.4f;
            MenuIntro.IsPaused = false;
        }

        void Update()
        {
            if (canBlink)
            {
                Blink();
                canBlink = false;
            }
            if (ControllerInput.Button9 == 1 || Input.AnyKeyDown())
            {
                sceneSwap();
            }

            StartMusicLoop();
        }
            
        async void Blink()
        {
            text.visible = true;
            await Task.Delay(blinkDelay);
            Console.WriteLine("OKAY?");
            text.visible = false;
            await Task.Delay(blinkDelay);
            text.visible = false;
            canBlink = true;
        }


        void StartMusicLoop() {
            if (!MenuIntro.IsPlaying && shouldBePlaying) {
                MenuIntro = new Sound("MenuLoop.wav", true).Play(true, 0);
                MenuIntro.Volume = 1f;
                MenuIntro.IsPaused = false;
            }
            
        }

        async void sceneSwap() {
            text.Clear(Color.Empty);
            Console.WriteLine("LEVEL");
            text.LateRemove();
            shouldBePlaying = false;
            game.RemoveChild(this);
            LateRemove();
            
            MenuIntro.Stop();
            Hud hud = new Hud();
            game.AddChild(hud);
            Level level = new Level();
            game.LateAddChildAt(level, 0);
        }
    }
}
