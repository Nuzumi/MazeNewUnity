using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TrapComponents/SlowDownComponent")]
public class SlowDownTrapComponent : BaseTrapComponent
{
    protected override void ActivateTrapComponent(GameObject enemy)
    {
        var actualStatistic = enemy.GetComponent<ActualUnitStatistic>();
        if(actualStatistic != null)
        {
            float speedModifier = actualStatistic.SpeedModifier;
            float newSpeedValue = speedModifier - speedModifier * effectValue[0];
            actualStatistic.ModifyPropertyForTime(Statistic.SpeedModifier, newSpeedValue, effectValue[1]);
        }
    }
}
