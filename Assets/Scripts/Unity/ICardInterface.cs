using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;

// called when player drops a card
public interface ICardInterface
{
    void DropCard(GenericCard card);
}

