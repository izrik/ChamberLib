using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamberLib;

namespace ChamberLib
{
    public class AddEntityEvent : TimeEvent
    {
        public AddEntityEvent(float time, Entity entity, ICollection<Entity> collection)
        {
            Entity = entity;
            Collection = collection;

            StartTime = time;
            EndTime = time;
        }

        public Entity Entity { get; protected set; }
        public ICollection<Entity> Collection { get; protected set; }

        protected override void UpdateTimeEvent(float time)
        {
            Collection.Add(Entity);
        }
    }
}
