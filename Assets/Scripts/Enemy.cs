using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy {

    // Base Stats
    public int power;
    public int stamina;
    public int agility;

    // Combat Stats
    public int hp;
    public int maxHp;
    public int sp;
    public int shield;
    public int maxShield;
    public float hpPerc;
    public float turnRate;
    public float interval;
    public float counter;

    // Specials
    public float critChance;
    public float critDamage;
    public float empowerChance;
    public float dsChance;
    public float blockChance;
    public float evadeChance;
    public float deflectChance;
    public float absorbChance;

    // Runes
    public float powerRunes;
    public float staminaRunes;
    public float agilityRunes;
    public float damageReduction;

    public double priority;

    // state
    public bool alive;

    public Enemy(float pow, float sta, float agi, float crit, float critdmg, float emp, float ds, float block, float evade, float deflect, float absorb, float prunes, float starunes, float agirunes, float redrunes, int diffMod, double prior)
    {
        power = Convert.ToInt32(pow * diffMod);
        stamina = Convert.ToInt32(sta * diffMod);
        agility = Convert.ToInt32(agi * diffMod);
        critChance = crit;
        critDamage = (100f + critdmg) / 100f;
        empowerChance = emp;
        dsChance = ds;
        blockChance = block;
        evadeChance = evade;
        deflectChance = deflect;
        absorbChance = absorb;
        powerRunes = (100f + prunes) / 100f;
        staminaRunes = (100f + starunes) / 100f;
        agilityRunes = (100f + agirunes) / 100f;
        damageReduction =(100f - redrunes) / 100f;
        priority = prior;
    }

    public void Initialise()
    {
        hp = stamina * 10;
        maxHp = hp;
        sp = 0;
        shield = 0;
        maxShield = maxHp / 2;
        turnRate = Logic.TurnRate(power, agility);
        interval = 100 / turnRate;
        counter = 0;
        alive = true;
    }
    public void IncrementCounter()
    {
        counter++;
    }

}
