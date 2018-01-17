using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

class Logic
{
    #region Old Code
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

    public static float TurnRate(int power, int agility)
    {
        float tr = 0f;
        tr = ((agility + power) / 2f);
        tr = (float)Math.Pow(tr, 2);
        tr = tr / (100f * power);
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
		attackValue = Convert.ToInt32((float)attackValue * reductionModifier);
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
            if (Logic.RNGroll(Simulation.hero[k].empowerChance))
            {
                attackValue *= 2;
            }
            Logic.HeroDamageApplication(k, attackValue);
        }
    }

#endregion

    #region New Code
    public static void HpPerc(Hero[] heroes)
    {
        int i;
        for (i = 0; i < heroes.Length; i++)
        {
            if (heroes[i].alive)
            {
                heroes[i].hpPerc = (float)(heroes[i].hp) / (float)(heroes[i].maxHp);
            }
            else
            {
                heroes[i].hpPerc = 100;
            }
        }
    }
    public static int HealLogic(Hero[] heroes)
    {
        int i;
        int lowest = 0;
        HpPerc(heroes);
        for (i = 0; i < heroes.Length - 1; i++)
        {
            if (heroes[lowest].hpPerc >= heroes[i + 1].hpPerc)
            {
                if (heroes[i + 1].alive)
                {
                    lowest = i + 1;
                }
                else
                {
                    if (!heroes[lowest].alive)
                    {
                        lowest = i + 1;
                    }
                }
            }
        }
        return lowest;
    }

    public static Enemy SelectTarget(Enemy[] enemies)
    {
        while (true)
        {
            int target = UnityEngine.Random.Range(0, 4);
            if (enemies[target].hp > 0) return enemies[target];
        }
    }
    public static Enemy SelectBack(Enemy[] enemies)
    {
        int target = enemies.Length - 1;
        while (true)
        { 
            if (enemies[target].hp > 0) return enemies[target];
            target--;
        }
    }
    public static Enemy SelectFront(Enemy[] enemies)
    {
        int target = 0;
        while (true)
        {
            if (enemies[target].hp > 0) return enemies[target];
            target++;
        }
    }
    public static int SelectPierce(Enemy[] enemies)
    {
        int target = 0;
        while (true)
        {
            if (enemies[target].hp > 0) return target;
            target++;
        }
    }
    public static Hero SelectWeakest(Hero[] heroes)
    {
        return heroes.OrderBy(hero => hero.hp).First();
    }

    public static void HeroDamageApplication(Hero hero, Hero[] heroes, Enemy[] enemies, int attackValue, Enemy target)
    {
        bool bossEvade = RNGroll(2.5f);
        if (!bossEvade)
        {
            PetLogic.PetSelection(hero, heroes, enemies );
            if ((int)hero.weapon == 3)
            {
                switch ((int)hero.divinityBonus)
                {
                    case 1:
                        attackValue = Convert.ToInt32(attackValue * 1.05);
                        break;
                    case 2:
                        attackValue = Convert.ToInt32(attackValue * 1.05);
                        if (target.hp < Convert.ToInt32(target.maxHp / 4))
                        {
                            attackValue = Convert.ToInt32(attackValue * 1.30);
                        }
                        break;
                    default:
                        break;
                }
            }
            if (hero.bushidoBonus)
            {
                attackValue = Convert.ToInt32(attackValue * 1.10);
            }
            target.hp -= attackValue;
            if (hero.drain)
            {
                hero.hp += attackValue;
                if (hero.hp > hero.maxHp)
                {
                    hero.hp = hero.maxHp;
                }
            }
            if (hero.lifeSteal > 0f)
            {
                hero.hp = hero.hp + Convert.ToInt32(attackValue * hero.lifeSteal);
                if (hero.hp > hero.maxHp)
                {
                    hero.hp = hero.maxHp;
                }
            }
        }

    }


    public static void HeroAttak0SP(Hero hero, Hero[] heroes, Enemy[] enemies)
    {
        int attackValue = SkillList.NormalAttack(hero.power);
        if (RNGroll(hero.critChance))
        {
            attackValue = Convert.ToInt32(attackValue * hero.critDamage);
        }
        HeroDamageApplication(hero, heroes, enemies, attackValue);
        if (RNGroll(hero.dsChance))
        {
            attackValue = SkillList.NormalAttack(hero.power);
            if (RNGroll(hero.critChance))
            {
                attackValue = Convert.ToInt32(attackValue * hero.critDamage);
            }
            if (RNGroll(hero.empowerChance))
            {
                attackValue *= 2;
            }
            HeroDamageApplication(hero, heroes, enemies, attackValue);
        }
    }

    #endregion
}
