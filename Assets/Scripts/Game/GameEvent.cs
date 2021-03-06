using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameEvent
    {
        enum EventTy
        { 
            ConstructCard,  //called at the start of the game to construct card instances

            CardMove, // Card Moved Locations - can use for unity animations. Has to pass in an object that knows the two locations that it moved from
            CardResolve, // Card is resolved from the stack.
            PlayerDraw, // a player draws a card
            DamageRecieved, // a player takes damage from a source
            SIZE,   // Number of events
        }

        // queueing order for an event can possibly be important
        // data could mean literally whatever you want it to mean
        public delegate void EventObserver(GameInstance state, object data);

        private List<EventObserver> _mapping;

        void Attach(EventTy type, EventObserver observer)
        {
            _mapping[(int)type] += observer;
        }
        void Detach(EventTy type, EventObserver observer)
        {
            _mapping[(int)type] += observer;
        }
        void Notify(EventTy type, GameInstance state, object data)
        {
            _mapping[(int)type].Invoke(state, data);
        }

        public GameEvent()
        {
            _mapping = new List<EventObserver>((int)EventTy.SIZE);

            for(int i = 0; i < _mapping.Count; i++)
            {
                _mapping[i] = delegate { };
            }
        }
    }
}
