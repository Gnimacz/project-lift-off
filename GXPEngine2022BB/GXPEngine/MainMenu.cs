using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using GXPEngine;

namespace GXPEngine
{
    class MainMenu : GameObject
    {
        EasyDraw text;
        private bool canBlink = true;
        private int blinkDelay = 1000;

        public MainMenu() : base()
        {
            Sprite menuSprite = new Sprite("StartMenu.png");
            AddChild(menuSprite);
            text = new EasyDraw(game.width,game.height);
            AddChild(text);
            text.TextAlign(CenterMode.Center, CenterMode.Center);
            text.TextFont(Utils.LoadFont("nove.ttf", 20));
            text.Text("Shoot To Start", game.width / 2, game.height - 256);

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
        }
            
        async void Blink()
        {
            text.visible = true;
            await Task.Delay(blinkDelay);
            text.visible = false;
            await Task.Delay(blinkDelay);
            text.visible = false;
            canBlink = true;
        }

        public void sceneSwap()
        {

            Hud hud = new Hud();
            game.LateAddChild(hud);
            Level level = new Level();
            game.LateAddChildAt(level, game.GetChildCount() - 1);
            LateRemove();

        }
    }
}
