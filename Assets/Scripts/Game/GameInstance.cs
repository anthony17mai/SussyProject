using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Util;

namespace Game {
    public class GameInstance
    {
        public struct PlayingField
        {
            public Agent owner;
            public CardList deck;
            public CardList hand;
            public CardList stack;
        }
        public struct RoundInstance
        {
            public int attack;
            public int def;
        }

        public System.Random rng = new System.Random();

        public PlayingField leftField;
        public PlayingField rightField;
        public GameEvent eventsystem;
        // true = left, false = right
        public bool turn;

        public void PlayCard(int cardLocation, bool owner)
        {
            ref PlayingField field = ref leftField;
            if(owner == true)
            {
                field = leftField;
            }
            else if(owner == false)
            {
                field = rightField;
            }
            CardInstance card = field.hand.RemoveCardAt(cardLocation);
            field.stack.AddCardTop(card);
        }

        public void EndTurn(bool owner)
        {
            if(owner == true)
            {
                ref PlayingField max = ref rightField.stack.Count > leftField.stack.Count ?  ref rightField :  ref leftField;
                int min = System.Math.Min(leftField.stack.Count, rightField.stack.Count);
                for(int i = 0; i < min; i++)
                {
                    CardInstance leftCard = leftField.stack.GetCard(i);
                    CardInstance rightCard = rightField.stack.GetCard(i);
                    RoundInstance leftRound;
                    RoundInstance rightRound;
                    leftCard.Behavior(this, out leftRound);
                    rightCard.Behavior(this, out rightRound);

                    int leftTotal = System.Math.Max(0, leftRound.attack - rightRound.def);
                    int rightTotal = System.Math.Max(0, rightRound.attack - leftRound.def);

                    leftField.owner.TakeDamage(rightTotal);
                    rightField.owner.TakeDamage(leftTotal);
                }
                turn = false;
            }
            else if(owner == false)
            {
                turn = true;
            }
        }

        public IEnumerator Cycle()
        {
            //instantiate each player's deck

            while(true) // primitivity
            {
                turn = false;
                Agent.DecisionData decision = new Agent.DecisionData();
                IEnumerator cycle = null;
                if(turn == true)
                {
                    cycle = leftField.owner.PromptAgent(this, decision);
                    while(cycle != null)
                    {
                        yield return cycle;
                        cycle = leftField.owner.PromptAgent(this, decision);
                    }

                    if(decision.type == Decision.play_card)
                    {
                        int location = leftField.hand.FindCardLocation(decision.arguments[0]);
                        PlayCard(location, turn);
                    }
                    else if(decision.type == Decision.end_turn)
                    {
                        EndTurn(turn);
                        turn = !turn;
                    }
                }
                else if(turn == false)
                {
                    cycle = rightField.owner.PromptAgent(this, decision);
                    while(cycle != null)
                    {
                        yield return cycle;
                        cycle = rightField.owner.PromptAgent(this, decision);
                    }

                    if(decision.type == Decision.play_card)
                    {
                        int location = rightField.hand.FindCardLocation(decision.arguments[0]);
                        PlayCard(location, turn);
                    }
                    else if(decision.type == Decision.end_turn)
                    {
                        EndTurn(turn);
                        turn = !turn;
                    }
                }
            }
        }
    }

    public abstract class CardList
    {
        public List<CardInstance> Collection { get; private set; } = new List<CardInstance>();

        /// <summary>
        /// Initializes the Card List using a DeckData.
        /// </summary>
        /// <param name="deckData"> Contains the set of cards to be used in the deck.</param>
        public CardList(DeckData deckData) {
            foreach (KeyValuePair<CardInstance, uint> pair in deckData.cards)
            {
                for(uint i = pair.Value; i > 0; i--)
                    Collection.Add(pair.Key);
            }
        }

        /// <summary>
        /// Initializes the Card List to an empty list.
        /// </summary>
        public CardList()
        {
            // lol nothing
        }

        public int Count { get { return Collection.Count; } }

        public void Shuffle(System.Random rng)
        {
            var shuffled = Collection.OrderBy(item => rng.Next());
            Collection = shuffled.ToList<CardInstance>();
        }

        public int FindCardLocation(CardInstance card)
        {
            return Collection.FindIndex(x => x == card);
        }
        public CardInstance GetCard(int index)
        {
            CardInstance card = Collection[index];
            return card;
        }

        // Check for null if empty
        // Remove also returns card removed, can be used for draw
        public CardInstance RemoveCardTop()
        {
            CardInstance topCard;

            if(Collection.Count == 0)
                return null;

            topCard = Collection[Collection.Count - 1];
            Collection.RemoveAt(Collection.Count - 1);

            return topCard;
        }

        // If index = 0, bottom of deck
        public CardInstance RemoveCardAt (int index)
        {
            CardInstance card;
            card = Collection[index];
            Collection.RemoveAt(index);
            return card;
        }

        public void AddCardTop (CardInstance card)
        {
            Collection.Add(card);
        }

        public void AddCardAt (CardInstance card, int index)
        {
            Collection.Insert(index, card);
        }
    }

    public class DeckData
    {
        public Multiset<Card> cards;
    }


}
