using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HogiaSpel.Entities
{
    public interface IEntity
    {
        void Initialize(Vector2 position);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
