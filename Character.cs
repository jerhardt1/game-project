using UnityEngine;
using System;

public abstract class Character : MonoBehaviour
{
    public Stat StatHealth;
    public Stat StatAttack;
    public Stat StatAttackSpeed;
    public Stat StatCritChance;

    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public bool isAlive = true;
    [HideInInspector]
    public string attackType = "normal";

    [HideInInspector]
    public event Action<float> OnHealthChange = delegate { };

    public virtual void Awake()
    {
        currentHealth = StatHealth.Value;
    }

    public virtual void ApplyBuff(float value, StatModType type)
    {
        StatModifier mod = new StatModifier(value, type);
    }

    public virtual void ModifyHealth(float amount)
    {
        currentHealth += amount;

        float currentHealthPct = currentHealth / StatHealth.Value;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            currentHealthPct = 0;
            isAlive = false;
        }
        OnHealthChange(currentHealthPct);
    }

    public Quaternion LookVector(Transform ToVector, Transform FromVector)
    {
        Vector3 lookVector = ToVector.position - FromVector.position;
        lookVector.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(lookVector);
        return rotation;
    }

    public float finalDamage()
    {
        float dmg = StatAttack.Value;

        int randValue = UnityEngine.Random.Range(0, 100);

        if (randValue <= StatCritChance.Value)
        {
            dmg *= 2;
            attackType = "crit";

        }
        else
        {
            attackType = "normal";
        }
        return dmg
        ;
    }

    public void Revive()
    {
        isAlive = true;
        currentHealth = StatHealth.Value;
    }

}
