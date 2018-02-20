using HogiaSpel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HogiaSpel.GlobalLists
{
    public sealed class EntityList
    {
        public static EntityList Instance { get { return _lazy.Value; } }

        private static readonly Lazy<EntityList> _lazy = new Lazy<EntityList>(() => new EntityList());
        private List<IEntity> _entities;

        private EntityList()
        {
            _entities = new List<IEntity>();
        }

        public int Count()
        {
            return _entities.Count;
        }

        public void AddEntity(IEntity entity)
        {
            _entities.Add(entity);
        }

        public IEntity GetEntity(int index)
        {
            return _entities.ElementAtOrDefault(index);
        }

    }
}
