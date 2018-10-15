using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour {

    private float lastTimeHit;
    private ActualUnitStatistic actualUnitStatistic;

    private void Start()
    {
        actualUnitStatistic = GetComponent<ActualUnitStatistic>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            DealDamage(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            DealDamage(collision.gameObject);
    }

    private void DealDamage(GameObject toDealDamage)
    {
        if (Time.timeSinceLevelLoad > lastTimeHit + actualUnitStatistic.MinTimeBetweenAttacks)
        {
            Damagable damagable = toDealDamage.GetComponent<Damagable>();
            if (damagable != null)
            {
                damagable.DealDamage(actualUnitStatistic.Damage);
                lastTimeHit = Time.timeSinceLevelLoad;
            }
        }
    }

}
