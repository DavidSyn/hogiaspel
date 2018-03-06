using Microsoft.Xna.Framework;

namespace HogiaSpel.Entities
{
    public interface IAbstractEntity : IEntity
    {
        void MoveUp(GameTime gameTime);

        void MoveDown(GameTime gameTime);

        void MoveRight(GameTime gameTime);

        void MoveLeft(GameTime gameTime);
    }
}
