using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//observes a certain cardlist in a game
public abstract class AbstractContainer : MonoBehaviour
{
    public abstract void emplace(GenericCard card);
    public abstract void remove(GenericCard card);
}
