using HogiaSpel.Entities;
using HogiaSpel.GlobalLists;
using Microsoft.Xna.Framework;

namespace HogiaSpel
{
    public static class EntityFactory
    {
        public static void CreateEntity(IEntity entity, Vector2 position)
        {
            entity.Initialize(position);
            var entities = EntityList.Instance;
            entities.AddEntity(entity);
        }
    }
}
