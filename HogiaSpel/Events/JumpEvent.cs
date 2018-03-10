using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HogiaSpel.Events
{
    public class JumpEvent : IEvent
    {
        public Guid Id { get; private set; }


        public JumpEvent()
        {
            Id = Guid.NewGuid();
        }
    }
}
