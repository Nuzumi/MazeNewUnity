using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/TrapComponents/DamageComponent")]
public class DamageTrapComponent : BaseTrapComponent
{
    protected override void ActivateTrapComponent(GameObject enemy)
    {
        Damagable damagable = enemy.GetComponent<Damagable>();
        if (damagable != null)
            damagable.DealDamage(effectValue[0]);
    }
}
