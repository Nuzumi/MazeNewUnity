using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName ="ScriptableObjects/UnitStatistic")]
public class UnitStatistic : ScriptableObject {

    public int maxHpPoints;
    public float damage;
    public float speedModifier;
    public int lostCounterMax;
    public int playerFollowersCount;
    public float minTimeBetweenAttacks;
}
