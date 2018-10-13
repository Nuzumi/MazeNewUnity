using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {

    [SerializeField]
    private HPBarController hpBarController;
    [SerializeField]
    private UnitStatistic unitStatistic;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private IntEvent addPoints;
    
    private int maxHealth;
    public float ActualHealth { get; set; }

    private void Start()
    {
        maxHealth = unitStatistic.maxHpPoints;
        ActualHealth = maxHealth;
        if (hpBarController != null)
            hpBarController.MaxHealthPoint = unitStatistic.maxHpPoints;
    }

    public void DealDamage(float damage)
    {
        ActualHealth -= damage;
        if (hpBarController != null)
            hpBarController.ActualHealthPoint = ActualHealth;

        if (animator != null)
            animator.SetTrigger("GetHurt");

        if (ActualHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (addPoints != null)
            addPoints.Invoke(unitStatistic.maxHpPoints);
        gameObject.SetActive(false);
    }
}
