using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[System.Serializable]
public enum FieldReference
{
    leftField,
    rightField,
}

public enum Notification
{ 
    moveHand,
    moveDeck,
    moveSomewhereElse,
}

public static class GameExtensions
{
    public static ref Game.GameInstance.PlayingField Reference(this Game.GameInstance inst, FieldReference refer)
    {
        if (refer == FieldReference.leftField) return ref inst.leftField;
        else return ref inst.rightField;
    }
}