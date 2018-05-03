using Microsoft.Xna.Framework;

namespace HogiaSpel.Entities
{
    public interface IAbstractEntity : IEntity
    {
        void MoveUp(float speed, GameTime gameTime);

        void MoveDown(float speed, GameTime gameTime);

        void MoveRight(float speed, GameTime gameTime);

        void MoveLeft(float speed, GameTime gameTime);

        void SetPosition(float x, float y);
    }
}
