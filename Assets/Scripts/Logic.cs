using System.Collections;
using System.Collections.Generic;
using System;

class Logic
{
    //methods for game logic
    public static bool RNGroll(float a)
    {
        bool outcome;
        float chance = a * 10f;
        float roll = UnityEngine.Random.Range(0, 999);
        if (roll <= chance)
        {
            outcome = true;
        }
        else
        {
            outcome = false;
        }
        return outcome;
    }

    public static float TurnRate(int b, int a)
    {
        float tr = 0f;
        tr = ((a + b) / 2f);
        tr = (float)Math.Pow(tr, 2);
        tr = tr / (100f * b);
        return tr;
    }
		

    public static void HpPerc()
    {
        int i;
        for (i = 0; i < 5; i++)
        {
            if (Simulation.hero[i].alive)
            {
                Simulation.hero[i].hpPerc = (float)(Simulation.hero[i].hp) / (float)(Simulation.hero[i].maxHp);
            }
            else
            {
                Simulation.hero[i].hpPerc = 10;
            }
        }
    }

    public static int HealLogic()
    {
        int i;
        int lowest = 0;
        HpPerc();
        for (i = 0; i < 4; i++)
        {
            if (Simulation.hero[lowest].hpPerc >= Simulation.hero[i + 1].hpPerc)
            {
                if (Simulation.hero[i + 1].alive)
                {
                    lowest = i + 1;
                }
                else
                {
                    if (!Simulation.hero[lowest].alive)
                    {
                        lowest = i + 1;
                    }
                }
            }
        }
        return lowest;
    }

	public static int FindLowestHealth()
	{
		int lowest = 0;
		for (int i = 0; i < 4; i++)
		{
			if (Simulation.hero[i].hp >= Simulation.hero[i + 1].hp)
			{
				if (Simulation.hero[i + 1].alive)
				{
					lowest = i + 1;
				}
				else
				{
					if (!Simulation.hero[lowest].alive)
					{
						lowest = i + 1;
					}
				}
			}
		}
		return lowest;
	}

    public static int TargetSelection(int method)
    {
        int target = 0;
        int i = 0;
        bool targetLocked = false;

        switch (method)
        {
            case 1:
                while (!targetLocked)
                {
                    if (Simulation.hero[i].alive)
                    {
                        target = i;
                        targetLocked = true;
                    }
                    i++;
                }
                break;
            case 2:
                i = 4;
                while (!targetLocked)
                {
                    if (Simulation.hero[i].alive)
                    {
                        target = i;
                        targetLocked = true;
                    }
                    i--;
                }
                break;
            case 3:
                while (!targetLocked)
                {
                    i = UnityEngine.Random.Range(0, 5);
                    if (Simulation.hero[i].alive)
                    {
                        target = i;
                        targetLocked = true;
                    }
                }
                break;
            default:
                break;
        }
        return target;
    }

    // method for hero to deal damage
    public static void HeroDamageApplication(int k, int attackValue)
    {
        bool bossEvade = RNGroll(2.5f);
        if (!bossEvade) {
            PetLogic.PetSelection(k);
            if ((int)Simulation.hero[k].weapon == 3)
            {
                switch ((int)Simulation.hero[k].divinityBonus)
                {
                    case 1:
                        attackValue = Convert.ToInt32(attackValue * 1.05);
                        break;
                    case 2:
                        attackValue = Convert.ToInt32(attackValue * 1.05);
                        if (Simulation.hpDummy < Convert.ToInt32(Simulation.maxHpDummy / 4))
                        {
                            attackValue = Convert.ToInt32(attackValue * 1.30);
                        }
                        break;
                    default:
                        break;
                }
            }
            if (Simulation.hero[k].bushidoBonus)
            {
                attackValue = Convert.ToInt32(attackValue * 1.10);
            }
            Simulation.hpDummy -= attackValue;
            if (Simulation.hero[k].drain)
            {
                Simulation.hero[k].hp += attackValue;
                if (Simulation.hero[k].hp > Simulation.hero[k].maxHp)
                {
                    Simulation.hero[k].hp = Simulation.hero[k].maxHp;
                }
            }
            if (Simulation.hero[k].lifeSteal > 0f)
            {
                Simulation.hero[k].hp = Simulation.hero[k].hp + Convert.ToInt32(attackValue * Simulation.hero[k].lifeSteal);
            }
        }

    }

    // following statements to choose a def proc and to select the redirected target
    public static int DefensiveProcCase(int k) {
        int scenario = 10;
        if (RNGroll(Simulation.hero[k].blockChance))   { scenario = 3; }
        if (RNGroll(Simulation.hero[k].evadeChance))   { scenario = 2; }
        if (RNGroll(Simulation.hero[k].deflectChance)) { scenario = 1; }
        if (RNGroll(Simulation.hero[k].absorbChance))  { scenario = 0; }
        return scenario;
    }

    public static int RedirectSelection(int k)
    {
        int redirectCountLive = Simulation.redirectCount;
        while (redirectCountLive > 0)
        {//redirect loop will run only if at least one member has the rune
            for (int i = 0; i < 5; i++)
            {
                if (Simulation.hero[i].metaRune == Hero.MetaRune.Redirect && Simulation.hero[i].redirect && Simulation.hero[i].alive)
                { //3 part condition, that they have rune, that their last redirect roll was successful and alive
                    Simulation.hero[i].redirect = RNGroll(25f);
                    if (!Simulation.hero[i].redirect)
                    {
                        redirectCountLive--;
                    }
                    else
                    {
                        k = i;
                        if (redirectCountLive == 1)
                        {//if only one member has the rune. will stop the loop to lock itself as target
                            redirectCountLive = 0;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < 5; i++)
        { //reset redirect rolls to true
            if (Simulation.hero[i].metaRune == Hero.MetaRune.Redirect)
            {
                Simulation.hero[i].redirect = true;
            }
        }
        return k;
    }


    // following methods used when defensiveproc is successful in boss' damage application method
    public static void HeroAbsorb (int attackValue, int k)
    {
        Simulation.hero[k].shield += attackValue;
        if (Simulation.hero[k].shield > Simulation.hero[k].maxShield)
        {
            Simulation.hero[k].shield = Simulation.hero[k].maxShield;
        }
    }

    public static void HeroDeflect (int attackValue, int k)
    {
        Simulation.hpDummy -= attackValue;
        if (Simulation.dummyDrain)
        {
            Simulation.hpDummy += attackValue;
        }
        if (Simulation.dummySelfInjure)
        {
            Simulation.hpDummy -= Convert.ToInt32(attackValue * 0.10);
        }
    }

    public static void HeroBlock (int attackValue, int k)
    {
		float reductionModifier;
		reductionModifier = 1f - (Simulation.hero[k].damageReduction / 100f);
        if (Simulation.hero[k].bushidoBonus)
        {
            attackValue = Convert.ToInt32(attackValue * 1.10);
        }
        attackValue = Convert.ToInt32(0.5 * attackValue);
		attackValue = Convert.ToInt32(attackValue * reductionModifier);
        if (Simulation.dummyDrain)
        {
            Simulation.hpDummy += attackValue;
        }
        if (Simulation.dummySelfInjure)
        {
            Simulation.hpDummy -= Convert.ToInt32(attackValue * 0.10);
        }
        if (Simulation.hero[k].shield > 0)
        {
            if (attackValue > Simulation.hero[k].shield)
            {
                attackValue -= Simulation.hero[k].shield;
                Simulation.hero[k].shield = 0;
            }
            else
            {
                Simulation.hero[k].shield -= attackValue;
                attackValue = 0;
            }
        }
        Simulation.hero[k].hp -= attackValue;
        if (Simulation.hero[k].hp <= 0)
        {
            Simulation.hero[k].alive = false;
            Simulation.aliveCount--;
        }
        else
        {
            PetLogic.PetSelection(k);
        }
    }

    public static void HeroNormal(int attackValue, int k)
    {
		if (Simulation.hero[k].bushidoBonus)
		{
			attackValue = Convert.ToInt32(attackValue * 1.10);
		}
        if (Simulation.dummySelfInjure)
        {
            Simulation.hpDummy -= Convert.ToInt32(attackValue * 0.10);
		}

		float reductionModifier = 1f - (Simulation.hero[k].damageReduction / 100f);
		attackValue = Convert.ToInt32(attackValue * reductionModifier);
		if (Simulation.dummyDrain)
		{
			Simulation.hpDummy += attackValue;
		}
        if (Simulation.hero[k].shield > 0)
        {
            if (attackValue > Simulation.hero[k].shield)
            {
                attackValue -= Simulation.hero[k].shield;
                Simulation.hero[k].shield = 0;
            }
            else
            {
                Simulation.hero[k].shield -= attackValue;
                attackValue = 0;
            }
        }
        Simulation.hero[k].hp -= attackValue;
        if (Simulation.hero[k].hp <= 0)
        {
            Simulation.hero[k].alive = false;
            Simulation.aliveCount--;
            if (Simulation.hero[k].metaRune == Hero.MetaRune.Redirect)
            {
                Simulation.redirectCount--;
            }
        }
        else
        {
            PetLogic.PetSelection(k);
        }
    }


    //Hero skills
    public static void HeroAttak0SP(int k, bool DS)
    {
        int attackValue = SkillList.NormalAttack(Simulation.hero[k].power);
        if (Logic.RNGroll(Simulation.hero[k].critChance))
        {
            attackValue = Convert.ToInt32(attackValue * Simulation.hero[k].critDamage);
        }
        Logic.HeroDamageApplication(k, attackValue);
        if (DS)
        {
            attackValue = SkillList.NormalAttack(Simulation.hero[k].power);
            if (Logic.RNGroll(Simulation.hero[k].critChance))
            {
                attackValue = Convert.ToInt32(attackValue * Simulation.hero[k].critDamage);
            }
            Logic.HeroDamageApplication(k, attackValue);
        }
    }
}
