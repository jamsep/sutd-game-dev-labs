using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voidd { } // dummy class
// no arguments
[CreateAssetMenu(fileName = "SimpleGameEvent", menuName = "ScriptableObjects/SimpleGameEvent", order = 3)]
public class SimpleGameEvent : GameEvent<Voidd>
{
    // create new method that doesn't accept any argument
    // calls base' Raise with Void arg
    public void Raise() => Raise(new Voidd()); // automatically create new Void data instead
}