using HogiaSpel.Entities;
using HogiaSpel.Entities.Blocks;
using HogiaSpel.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HogiaSpel.Factories
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
            sprites.Load(SpriteKeys.Block.Stand, loader.Load<Texture2D>("sprites/block/block"));
            sprites.Load(SpriteKeys.DiamondBlock.Stand, loader.Load<Texture2D>("sprites/block/diamon-block"));
            sprites.Load(SpriteKeys.MetalBlock.Stand, loader.Load<Texture2D>("sprites/block/metal-block"));
            //sprites.Load(SpriteKeys.PowerUp.Stand, loader.Load<Texture2D>("sprites/powerup/powerup"));

            // --- CREATE THINGS --- //
            EntityFactory.CreateEntity(new PlayerAvatar(), new Vector2(400, 400));
            EntityFactory.CreateEntity(new Block(), new Vector2(336, 400));

            EntityFactory.CreateEntity(new DiamondBlock(), new Vector2(272, 464));
            EntityFactory.CreateEntity(new MetalBlock(), new Vector2(336, 464));
            EntityFactory.CreateEntity(new MetalBlock(), new Vector2(400, 464));
            EntityFactory.CreateEntity(new MetalBlock(), new Vector2(464, 464));
            EntityFactory.CreateEntity(new MetalBlock(), new Vector2(528, 464));
            EntityFactory.CreateEntity(new DiamondBlock(), new Vector2(592, 464));
            //EntityFactory.CreateEntity(new PowerUp(), new Vector2(592, 528));

            EntityFactory.CreateEntity(new DiamondBlock(), new Vector2(752, 400));
            EntityFactory.CreateEntity(new MetalBlock(), new Vector2(816, 400));
            EntityFactory.CreateEntity(new MetalBlock(), new Vector2(880, 400));
            EntityFactory.CreateEntity(new MetalBlock(), new Vector2(944, 400));
            EntityFactory.CreateEntity(new MetalBlock(), new Vector2(1008, 400));
            EntityFactory.CreateEntity(new MetalBlock(), new Vector2(1072, 400));
            EntityFactory.CreateEntity(new MetalBlock(), new Vector2(1136, 400));
            EntityFactory.CreateEntity(new DiamondBlock(), new Vector2(1200, 400));
        }
    }
}
