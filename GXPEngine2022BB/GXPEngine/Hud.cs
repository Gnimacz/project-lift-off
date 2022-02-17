using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GXPEngine
{
    class Hud : Canvas
    {
        List<Sprite> healthSprites = new List<Sprite>();
        Sprite sprite;
        EasyDraw textRenderer;
        public bool showHighScoreText = false;
        public bool showHighScore = false;

        public Hud() : base(1366, 768, false)
        {
            ResetSprites(5);

            textRenderer = new EasyDraw(1366, 768, false);
            textRenderer.TextAlign(CenterMode.Center, CenterMode.Center);
            //TODO(pick a font and use that instead of default windows font);
            textRenderer.TextFont(Utils.LoadFont("nove.ttf", 16));
            AddChild(textRenderer);
        }
        public void ResetSprites(int amount) {

            foreach (Sprite item in healthSprites)
            {
                item.LateRemove();
            }

            healthSprites = new List<Sprite>();

            for (int i = 0; i < amount; i++)
            {
                sprite = new Sprite("PlayerSingle.png");
                healthSprites.Add(sprite);
                sprite.scale = 0.15f;
                sprite.x = i * sprite.width;
                sprite.y = 20;
                AddChild(sprite);
            }
        }

        public void RemoveHealth()
        {
            if(healthSprites.Count > 0)
            {
                healthSprites.Last().Remove();
                healthSprites.RemoveAt(healthSprites.Count - 1);
            }
        }
        public void RemoveHealth(int healthAmount)
        {
            if (healthSprites.Count > healthAmount)
            {
                for (int i = 0; i < healthAmount; i++)
                {
                    healthSprites.Last().LateRemove();
                    healthSprites.RemoveAt(healthSprites.Count - 1);
                }
            }else {
                for (int i = healthSprites.Count - 1; i > 0; i--)
                {
                    healthSprites.Last().LateRemove();
                    healthSprites.RemoveAt(i);
                }
            }
        }

        void Update()
        {
            textRenderer.Clear(Color.Empty);
            
            if (!showHighScore)
            {
                textRenderer.Text($"Enemies left: {Level.currentNumberOfEnemies}", game.width - 90, 20);
                
            }

            if (showHighScore && !showHighScoreText)
            {
                foreach (Sprite item in healthSprites)
                {
                    item.LateRemove();
                }
                ShowScores();
            }
            else if (showHighScoreText && showHighScore)
            {
                foreach (Sprite item in healthSprites)
                {
                    item.LateRemove();
                }
                textRenderer.Text("No Scores yet");
            }

        }

        public void ShowScores()
        {
            textRenderer.Text($"Current High Score\n{Scoreboard.scores}");
        }
        
    }
}
