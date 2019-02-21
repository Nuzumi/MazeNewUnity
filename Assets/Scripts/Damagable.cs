using System;
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
    [SerializeField]
    private NativeEvent playerDied;
    [SerializeField]
    private EnemyLootController enemyLootController;
    [SerializeField]
    private AudioClip getDamagaSound;
    [SerializeField]
    private AudioClip dieSound;
    [SerializeField]
    private AudioClip healSound;
    [SerializeField]
    private NativeEvent levelPassed;

    private AudioSource audioSource;
    private ActualUnitStatistic actualUnitStatistic;
    private bool dead;
    private int maxHealth;
    public float ActualHealth { get; set; }
    private bool active = true;

    private void OnEnable()
    {
        levelPassed.AddListener(() => active = false);
    }

    private void OnDisable()
    {
        levelPassed.RemoveListener(() => active = false);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        actualUnitStatistic = GetComponent<ActualUnitStatistic>();
        maxHealth = actualUnitStatistic.MaxHP;
        ActualHealth = maxHealth;
        if (hpBarController != null)
            hpBarController.MaxHealthPoint = actualUnitStatistic.MaxHP;
    }

    public bool DealDamage(float damage)
    {
        if (active)
        {
            bool result = true;
            if (!dead)
            {
                if (damage > 0)
                {

                    ActualHealth -= damage;
                    if (animator != null)
                    {
                        animator.SetTrigger("GetHurt");
                    }
                    audioSource.clip = getDamagaSound;
                    audioSource.Play();
                }
                else
                {
                    if (ActualHealth != maxHealth)
                    {
                        ActualHealth -= damage;
                        if (ActualHealth > maxHealth)
                            ActualHealth = maxHealth;
                        animator.SetTrigger("GetHeal");
                        audioSource.clip = healSound;
                        audioSource.Play();
                    }
                    else
                    {
                        result = false;
                    }
                }

                ActualHealth = (float)Math.Round(ActualHealth, 4);

                if (hpBarController != null)
                    hpBarController.ActualHealthPoint = ActualHealth;
            }
            else
                result = false;

            if (ActualHealth <= 0 && !dead)
            {
                Die();
                result = false;
            }

            return result;
        }
        return false;
    }

    private void Die()
    {
        audioSource.clip = dieSound;
        audioSource.Play();
        if (addPoints != null)
            addPoints.Invoke(Mathf.CeilToInt(actualUnitStatistic.MaxHP / 2f));

        if (playerDied != null)
        {
            playerDied.Invoke();
            SaveLoadDataController.ClearData();
        }

        DropLoot();

        dead = true;
        gameObject.SetActive(false);
    }

    private void DropLoot()
    {
        if(enemyLootController != null)
        {
            GameObject loot = enemyLootController.GetLootToDrop();
            if(loot != null)
            {
                Instantiate(loot, transform.position, Quaternion.identity);
            }
        }
    }
}
