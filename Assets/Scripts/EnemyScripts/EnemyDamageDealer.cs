using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour {

    [SerializeField]
    private UnitStatistic unitStatistic;

    private float lastTimeHit;

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
        if (Time.timeSinceLevelLoad > lastTimeHit + unitStatistic.minTimeBetweenAttacks)
        {
            Damagable damagable = toDealDamage.GetComponent<Damagable>();
            if (damagable != null)
            {
                damagable.DealDamage(unitStatistic.damage);
                lastTimeHit = Time.timeSinceLevelLoad;
            }
        }
    }

}
