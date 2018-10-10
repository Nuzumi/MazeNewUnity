using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {

    public int maxHealth;

    private float actualHealth;

    private void Start()
    {
        actualHealth = maxHealth;
    }

    public void DealDamage(float damage)
    {
        actualHealth -= damage;
        if (actualHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
