using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public enum Decision
    {
        end_turn,
        play_card
    }

    public abstract class Agent
    {
        public class DecisionData
        {
            public Decision type;
            public List<CardInstance> arguments;
        }

        public struct CharData
        {
            public DeckData deckData;
            public int maxHP;
            public int maxMana;
            public int stackSize;
            public int handSize;
        }

        protected CharData chardata;

        public abstract DecisionData PromptAgent(GameInstance game);
    }

    public class Player : Agent
    {
        public override DecisionData PromptAgent(GameInstance game)
        {
            // TODO: give the user control of the game - the user can choose to play a card or to pass their turn.
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Repreesentation of a player playing synchronously using a lan connection
    /// </summary>
    public class LanPlayer : Agent
    {
        public override DecisionData PromptAgent(GameInstance game)
        {
            throw new NotImplementedException();
        }
    }

    public class Opponent : Agent
    {
        /// <summary>
        /// The signature for a decision function.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public delegate DecisionData Decider(GameInstance game);

        public Decider decider;

        public override DecisionData PromptAgent(GameInstance game)
        {
            return decider.Invoke(game);
        }

        public Opponent(Decider decider)
        {
            this.decider = decider;
        }
    }
}