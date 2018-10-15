using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Statistic{MaxHp,Damage,SpeedModifier,LostounterMax,PlayerFollowerCount,MinTimeBetweenAttacks}

public class ActualUnitStatistic : MonoBehaviour {

    [SerializeField]
    private UnitStatistic unitStatistic;
    [SerializeField]
    private IntGameObjectEvent timerResponse;
    [SerializeField]
    private FloatIntGameObjectEvent addTimer;
    [SerializeField]
    private IntGameObjectEvent removeTimer;

    public int MaxHP{ get; set; }
    public float Damage { get; set; }
    public float SpeedModifier { get; set; }
    public int LostCounterMax { get; set; }
    public int PlayerFollowerCount { get; set; }
    public float MinTimeBetweenAttacks { get; set; }

    private int oldMaxHP;
    private float oldDamage;
    private float oldSpeedModifier;
    private int oldLostCounterMax;
    private int oldPlayerFollowerCount;
    private float oldMinTimeBetweenAttacks;

    private void Start()
    {
        MaxHP = unitStatistic.maxHpPoints;
        oldMaxHP = MaxHP;
        Damage = unitStatistic.damage;
        oldDamage = Damage;
        SpeedModifier = unitStatistic.speedModifier;
        oldSpeedModifier = SpeedModifier;
        LostCounterMax = unitStatistic.lostCounterMax;
        oldLostCounterMax = LostCounterMax;
        PlayerFollowerCount = unitStatistic.playerFollowersCount;
        oldPlayerFollowerCount = PlayerFollowerCount;
        MinTimeBetweenAttacks = unitStatistic.minTimeBetweenAttacks;
        oldMinTimeBetweenAttacks = MinTimeBetweenAttacks;
    }

    public void ModifyPropertyForTime(Statistic statistic, float newValue, float time)
    {
        switch(statistic)
        {
            case Statistic.Damage:

                if(!Mathf.Approximately(Damage,oldDamage))
                    removeTimer.Invoke((int)statistic, gameObject);

                Damage = newValue;
                break;

            case Statistic.LostounterMax:

                if (LostCounterMax == oldLostCounterMax)
                    removeTimer.Invoke((int)statistic, gameObject);

                LostCounterMax = (int)newValue;
                break;

            case Statistic.MaxHp:

                if (!Mathf.Approximately(MaxHP, oldMaxHP))
                    removeTimer.Invoke((int)statistic, gameObject);
                MaxHP = (int)newValue;
                break;

            case Statistic.MinTimeBetweenAttacks:

                if (Mathf.Approximately(MinTimeBetweenAttacks, oldMinTimeBetweenAttacks))
                    removeTimer.Invoke((int)statistic, gameObject);
                MinTimeBetweenAttacks = newValue;
                break;

            case Statistic.PlayerFollowerCount:

                if (!Mathf.Approximately(PlayerFollowerCount, oldPlayerFollowerCount))
                    removeTimer.Invoke((int)statistic, gameObject);
                break;

            case Statistic.SpeedModifier:

                if (!Mathf.Approximately(SpeedModifier, oldSpeedModifier))
                    removeTimer.Invoke((int)statistic, gameObject);
                SpeedModifier = newValue;
                break;

        }

        addTimer.Invoke(time, (int)statistic, gameObject);
    }


}
