using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// no arguments
[CreateAssetMenu(fileName = "IntGameEvent", menuName = "ScriptableObjects/IntGameEvent", order = 4)]
public class IntGameEvent : GameEvent<int>
{
    // create new method that doesn't accept any argument
    // calls base' Raise with Void arg
    public void Raise() => Raise(new int()); // automatically create new Void data instead
}