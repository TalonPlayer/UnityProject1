using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New unit", menuName = "Scriptable Unit")]
public class ScriptableUnits : ScriptableObject
{
    public Faction Faction;
    public BaseUnit UnitPrefab;
}

public enum Faction{
    Member = 0,
    Enemy = 1
}
