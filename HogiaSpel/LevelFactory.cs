using HogiaSpel.Entities;
using HogiaSpel.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HogiaSpel
{
    public static class LevelFactory
    {
        public static void LoadLevelOne(ContentManager loader)
        {
            var sprites = Sprites.Instance;

            // --- LOAD GRAPHICS --- //
            //Load player
            sprites.Load(SpriteKeys.Quote.StandRight, loader.Load<Texture2D>("sprites/quote/quote-stand-right"));
            sprites.Load(SpriteKeys.Quote.StandLeft, loader.Load<Texture2D>("sprites/quote/quote-stand-left"));
            sprites.Load(SpriteKeys.Quote.RunRight, loader.Load<Texture2D>("sprites/quote/quote-run-right"));
            sprites.Load(SpriteKeys.Quote.RunLeft, loader.Load<Texture2D>("sprites/quote/quote-run-left"));

            //Load environmental  entities
            sprites.Load(SpriteKeys.Block.Stand, loader.Load<Texture2D>("sprites/block"));

            // --- CREATE THINGS --- //
            EntityFactory.CreateEntity(new PlayerAvatar(), new Vector2(400, 400));
        }
    }
}
