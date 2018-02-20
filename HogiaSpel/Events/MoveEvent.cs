using HogiaSpel.Enums;
using System;

namespace HogiaSpel.Events
{
    public class MoveEvent : IEvent
    {
        public Guid Id { get; private set; }
        public DirectionEnum Direction { get; set; }

        public MoveEvent(DirectionEnum direction)
        {
            Id = Guid.NewGuid();
            Direction = direction;
        }
    }
}
