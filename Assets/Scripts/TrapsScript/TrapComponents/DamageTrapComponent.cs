using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/TrapComponents/DamageComponent")]
public class DamageTrapComponent : BaseTrapComponent
{
    [SerializeField]
    private float damage;

    protected override void ActivateTrapComponent(GameObject enemy)
    {
        Damagable damagable = enemy.GetComponent<Damagable>();
        if (damagable != null)
            damagable.DealDamage(damage);
        Debug.Log("boom");
    }
}
