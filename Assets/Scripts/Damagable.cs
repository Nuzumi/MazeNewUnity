using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {

    [SerializeField]
    private HPBarController hpBarController;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private IntEvent addPoints;

    private ActualUnitStatistic actualUnitStatistic;
    private bool dead;
    private int maxHealth;
    public float ActualHealth { get; set; }

    private void Start()
    {
        actualUnitStatistic = GetComponent<ActualUnitStatistic>();
        maxHealth = actualUnitStatistic.MaxHP;
        ActualHealth = maxHealth;
        if (hpBarController != null)
            hpBarController.MaxHealthPoint = actualUnitStatistic.MaxHP;
    }

    public void DealDamage(float damage)
    {
        ActualHealth -= damage;
        if (hpBarController != null)
            hpBarController.ActualHealthPoint = ActualHealth;

        if (animator != null)
            animator.SetTrigger("GetHurt");

        if (ActualHealth <= 0 && !dead)
        {
            Die();
        }
    }

    private void Die()
    {
        if (addPoints != null)
            addPoints.Invoke(actualUnitStatistic.MaxHP);
        dead = true;
        gameObject.SetActive(false);
    }
}
