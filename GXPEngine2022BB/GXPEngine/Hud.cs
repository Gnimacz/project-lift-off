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

        public Hud() : base(1366, 768, false)
        {
            for (int i = 0; i < 4; i++)
            {
                sprite = new Sprite("Player.png");
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
            if (healthSprites.Count > 0)
            {
                for (int i = 0; i < healthAmount; i++)
                {
                    healthSprites.Last().LateRemove();
                    healthSprites.RemoveAt(healthSprites.Count - 1);
                }
            }
        }

        void Update()
        {
            if (Input.GetKeyUp(Key.ENTER))
            {
                RemoveHealth();
            }
        }
        
    }
}
