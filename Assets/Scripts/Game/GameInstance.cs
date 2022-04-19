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

        PlayingField leftField;
        PlayingField rightField;
        // true = left, false = right
        bool turn;

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
            while(true) // primitivity
            {
                turn = false;
                Agent.DecisionData decision = null;
                IEnumerator cycle = null;
                if(turn == true)
                {
                    cycle = leftField.owner.PromptAgent(this, ref decision);
                    while(cycle != null)
                    {
                        yield return cycle;
                        cycle = leftField.owner.PromptAgent(this, ref decision);
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
                    cycle = rightField.owner.PromptAgent(this, ref decision);
                    while(cycle != null)
                    {
                        yield return cycle;
                        cycle = rightField.owner.PromptAgent(this, ref decision);
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
        List<CardInstance> collection = new List<CardInstance>();

        /// <summary>
        /// Initializes the Card List using a DeckData.
        /// </summary>
        /// <param name="deckData"> Contains the set of cards to be used in the deck.</param>
        public CardList(DeckData deckData) {
            foreach (KeyValuePair<CardInstance, uint> pair in deckData.cards)
            {
                for(uint i = pair.Value; i > 0; i--)
                    collection.Add(pair.Key);
            }
        }

        /// <summary>
        /// Initializes the Card List to an empty list.
        /// </summary>
        public CardList()
        {
            // lol nothing
        }

        public int Count { get { return collection.Count; } }

        public void Shuffle(System.Random rng)
        {
            var shuffled = collection.OrderBy(item => rng.Next());
            collection = shuffled.ToList<CardInstance>();
        }

        public int FindCardLocation(CardInstance card)
        {
            return collection.FindIndex(x => x == card);
        }
        public CardInstance GetCard(int index)
        {
            CardInstance card = collection[index];
            return card;
        }

        // Check for null if empty
        // Remove also returns card removed, can be used for draw
        public CardInstance RemoveCardTop()
        {
            CardInstance topCard;

            if(collection.Count == 0)
                return null;

            topCard = collection[collection.Count - 1];
            collection.RemoveAt(collection.Count - 1);

            return topCard;
        }

        // If index = 0, bottom of deck
        public CardInstance RemoveCardAt (int index)
        {
            CardInstance card;
            card = collection[index];
            collection.RemoveAt(index);
            return card;
        }

        public void AddCardTop (CardInstance card)
        {
            collection.Add(card);
        }

        public void AddCardAt (CardInstance card, int index)
        {
            collection.Insert(index, card);
        }
    }

    public class DeckData
    {
        public Multiset<Card> cards;
    }


}
