using System;
using System.Collections;
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
        public int currentHP;
        public int currentMana;

        protected CharData chardata;

        public abstract IEnumerator PromptAgent(GameInstance game, DecisionData decision);

        public void TakeDamage(int damage)
        {
            currentHP = System.Math.Max(0, currentHP - damage);

            // die if hp = 0
        }

        public void HealDamage(int heal)
        {
            currentHP = System.Math.Min(chardata.maxHP, currentHP + heal);
        }
    }

    

    /// <summary>
    /// Representation of a player playing synchronously using a lan connection
    /// </summary>
    public class LanPlayer : Agent
    {
        public override IEnumerator PromptAgent(GameInstance game, DecisionData decision)
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

        public override IEnumerator PromptAgent(GameInstance game, DecisionData decision)
        {
            decision = decider.Invoke(game);
            return null;
        }

        public Opponent(Decider decider)
        {
            this.decider = decider;
        }
    }
}