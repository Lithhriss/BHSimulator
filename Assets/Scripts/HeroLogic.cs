using System.Collections;
using System.Collections.Generic;
using System;

class HeroLogic
{
    private static Random rnd = new Random(Guid.NewGuid().GetHashCode());

    public static void SpreadHealingSkill(int k)
    {
        int i;
        int target = 0;
        int healingValue = 0;
        int healingModifier = Convert.ToInt32(0.365 * Simulation.hero[k].power);

        healingValue = Convert.ToInt32(UnityEngine.Random.Range(0, healingModifier) + 0.73 * Simulation.hero[k].power);

        bool critRoll = Logic.RNGroll(Simulation.hero[k].critChance);
        if (critRoll)
        {
            healingValue = Convert.ToInt32(healingValue * Simulation.hero[k].critDamage);
        }

        for (i = 0; i < healingValue; i++)
        {
            target = Logic.HealLogic();
            Simulation.hero[target].hp++;
            if (Simulation.hero[target].hp > Simulation.hero[target].maxHp)
            {
                Simulation.hero[target].hp = Simulation.hero[target].maxHp;
            }
        }
    }


    public static void SwordSkillSelection(int k, int sp, bool unity, bool DS)
    {
        
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;
        //int targetMethod = 0;

        if (sp < 2)
        {
            Logic.HeroAttak0SP(k, DS);
        }
        else if (sp < 6)
        {
            // 1 sp skill AI
            range = 15;
            if (unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                Logic.HeroAttak0SP(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 15)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.SwTarget_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SwTarget_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 15)
            {
                Simulation.hero[k].sp -= 2;
                SpreadHealingSkill(k);
                if (DS)
                {
                    SpreadHealingSkill(k);
                }
            }
        }
        else if (sp >= 6)
        {
            // 1 - 2 sp skill AI
            range = 45;
            if (unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                Logic.HeroAttak0SP(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 15)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.SwTarget_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SwTarget_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 15 && skillRoll < 45)
            {
                Simulation.hero[k].sp -= 6;
                Simulation.hero[k].drain = true;
                attackValue = SkillList.SwClosest3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SwClosest3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
                Simulation.hero[k].drain = false;
            }
            else if (skillRoll >= 45)
            {
                Simulation.hero[k].sp -= 2;
                SpreadHealingSkill(k);
                if (DS)
                {
                    SpreadHealingSkill(k);
                }
            }
        }
    }

    public static void SpearSkillSelection(int k, int sp, bool unity, bool DS)
    {
        
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;
        //int targetMethod = 0;

        if (sp < 2)
        {
            Logic.HeroAttak0SP(k, DS);
        }
        else if (sp < 4)
        {
            // 1 sp skill AI
            range = 25;
            if (unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                Logic.HeroAttak0SP(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.SpBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 25)
            {
                Simulation.hero[k].sp -= 2;
                SpreadHealingSkill(k);
                if (DS)
                {
                    SpreadHealingSkill(k);
                }
            }
        }
        else if (sp < 6)
        {
            // 1 - 2 sp skill AI
            range = 55;
            if (unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                Logic.HeroAttak0SP(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.SpBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 25 && skillRoll > 55)
            {
                Simulation.hero[k].sp -= 4;
                attackValue = SkillList.SpTarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpTarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 55)
            {
                Simulation.hero[k].sp -= 2;
                SpreadHealingSkill(k);
                if (DS)
                {
                    SpreadHealingSkill(k);
                }
            }
        }
        else if (sp >= 6)
        {
            // 1 - 2 sp skill AI
            range = 85;
            if (unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                Logic.HeroAttak0SP(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.SpBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 25 && skillRoll < 55)
            {
                Simulation.hero[k].sp -= 4;
                attackValue = SkillList.SpTarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpTarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 55 && skillRoll < 85)
            {
                Simulation.hero[k].sp -= 6;
                attackValue = SkillList.SpClosest3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpClosest3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 85)
            {
                Simulation.hero[k].sp -= 2;
                SpreadHealingSkill(k);
                if (DS)
                {
                    SpreadHealingSkill(k);
                }
            }
        }
    }

    public static void BowSkillSelection(int k, int sp, bool unity, bool DS)
    {
        
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;
        //int targetMethod = 0;
        if (sp < 2)
        {
            Logic.HeroAttak0SP(k, DS);
        }
        else if (sp < 4)
        {
            // 1 sp skill AI
            range = 25;
            if (unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                Logic.HeroAttak0SP(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.BTarget1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.BTarget1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 25)
            {
                Simulation.hero[k].sp -= 2;
                SpreadHealingSkill(k);
                if (DS)
                {
                    SpreadHealingSkill(k);
                }
            }

        }
        else if (sp >= 4)
        {
            // 1 - 2 sp skill AI
            range = 55;
            if (unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                Logic.HeroAttak0SP(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.BTarget1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.BTarget1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 25 && skillRoll < 55)
            {
                Simulation.hero[k].sp -= 4;
                attackValue = SkillList.BBack2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.BBack2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 55)
            {
                Simulation.hero[k].sp -= 2;
                SpreadHealingSkill(k);
                if (DS)
                {
                    SpreadHealingSkill(k);
                }
            }
        }
    }

    public static void StaffSkillSelection(int k, int sp, bool unity, bool DS)
    {
        
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;
        //int targetMethod = 0;

        if (sp < 2)
        {
            Logic.HeroAttak0SP(k, DS);
        }
        else if (sp < 6)
        {
            // 1 sp skill AI
            range = 80;
            if (unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                Logic.HeroAttak0SP(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 20)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.StClosest1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.StClosest1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }

            }
            else if (skillRoll >= 20 && skillRoll < 80)
            {
                Simulation.hero[k].sp -= 2;
                SkillList.StHeal1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                if (DS)
                {
                    SkillList.StHeal1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                }
            }
            else if (skillRoll >= 80)
            {
                Simulation.hero[k].sp -= 2;
                SpreadHealingSkill(k);
                if (DS)
                {
                    SpreadHealingSkill(k);
                }
            }
        }
        else if (sp >= 6)
        {
            // 1 - 2 sp skill AI
            range = 90;
            if (unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                Logic.HeroAttak0SP(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 20)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.StClosest1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.StClosest1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 20 && skillRoll < 80)
            {
                Simulation.hero[k].sp -= 2;
                SkillList.StHeal1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                if (DS)
                {
                    SkillList.StHeal1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                }
            }
            else if (skillRoll >= 80 && skillRoll < 90)
            {
                Simulation.hero[k].sp -= 6;
                attackValue = SkillList.StTarget3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.StTarget3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 90)
            {
                Simulation.hero[k].sp -= 2;
                SpreadHealingSkill(k);
                if (DS)
                {
                    SpreadHealingSkill(k);
                }
            }
        }
    }

    public static void AxeSkillSelection(int k, int sp, bool unity, bool DS)
    {
        
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;
        //int targetMethod = 0;

        if (sp < 2)
        {
            Logic.HeroAttak0SP(k, DS);
        }
        else if (sp < 4)
        {
            // 1 sp skill AI
            range = 20;
            if (unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                Logic.HeroAttak0SP(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 20)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.AClosest_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.AClosest_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 20)
            {
                Simulation.hero[k].sp -= 2;
                SpreadHealingSkill(k);
                if (DS)
                {
                    SpreadHealingSkill(k);
                }
            }
        }
        else if (sp >= 4)
        {
            // 1 - 2 sp skill AI
            range = 80;
            if (unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                Logic.HeroAttak0SP(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 20)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.AClosest_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.AClosest_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 20 && skillRoll < 80)
            {
                Simulation.hero[k].sp -= 4;
                attackValue = SkillList.ATarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.ATarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage, Simulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 80)
            {
                Simulation.hero[k].sp -= 2;
                SpreadHealingSkill(k);
                if (DS)
                {
                    SpreadHealingSkill(k);
                }
            }
        }
    }

    public static void WeaponSelection(int k, bool DS)
    {
        int weapon = (int)Simulation.hero[k].weapon;
        switch (weapon)
        {
            case 1:
                BowSkillSelection(k, Simulation.hero[k].sp, Simulation.hero[k].unity, DS);
                break;
            case 2:
                SpearSkillSelection(k, Simulation.hero[k].sp, Simulation.hero[k].unity, DS);
                break;
            case 3:
                SwordSkillSelection(k, Simulation.hero[k].sp, Simulation.hero[k].unity, DS);
                break;
            case 4:
                StaffSkillSelection(k, Simulation.hero[k].sp, Simulation.hero[k].unity, DS);
                break;
            case 5:
                AxeSkillSelection(k, Simulation.hero[k].sp, Simulation.hero[k].unity, DS);
                break;
            default:
                break;
        }
    }


    #region New Code
    public static void WeaponSelection(Hero hero, Hero[] heroes, Enemy[] enemies)
    {
        int weapon = (int)hero.weapon;
        switch (weapon)
        {
            case 1:
                BowSkillSelection(hero, heroes, enemies);
                break;
            case 2:
                SpearSkillSelection(hero, heroes, enemies);
                break;
            case 3:
                SwordSkillSelection(hero, heroes, enemies);
                break;
            case 4:
                StaffSkillSelection(hero, heroes, enemies);
                break;
            case 5:
                AxeSkillSelection(hero, heroes, enemies);
                break;
            default:
                break;
        }
    }

    public static void SpreadHealingSkill(Hero hero, Hero[] heroes)
    {
        int i;
        int target = 0;
        int healingValue = 0;
        int healingModifier = Convert.ToInt32(0.365 * hero.power);

        healingValue = Convert.ToInt32(UnityEngine.Random.Range(0, healingModifier) + 0.73 * hero.power);

        bool critRoll = Logic.RNGroll(hero.critChance);
        if (critRoll)
        {
            healingValue = Convert.ToInt32(healingValue * hero.critDamage);
        }

        for (i = 0; i < healingValue; i++)
        {
            target = Logic.HealLogic(heroes);
            heroes[target].hp++;
            if (heroes[target].hp > heroes[target].maxHp)
            {
                heroes[target].hp = heroes[target].maxHp;
            }
        }
    }

    public static void SwordSkillSelection(Hero hero, Hero[] heroes, Enemy[] enemies)
    {
        
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;
        if (hero.unity)
        {
            range = 10;
        }
        //int targetMethod = 0;
        if (hero.sp < 2) range += 10;
        else if (hero.sp < 4) range += 40;
        else if (hero.sp < 6) range += 100;

        skillRoll = rnd.Next(skillRoll);

        if (skillRoll < 10)
        {
            Logic.HeroAttak0SP(hero, heroes, enemies);
        }
        else if (skillRoll >= 10 && skillRoll < 15)
        {
            hero.sp -= 2;
            attackValue = SkillList.SwTarget_1sp(hero);
            Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
            if (Logic.RNGroll(hero.dsChance))
            {
                attackValue = SkillList.SwTarget_1sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
            }
        }
        else if (skillRoll >= 15 && skillRoll < 40)
        {
            hero.sp -= 2;
            int target = Logic.SelectPierce(enemies);
            for (int i = 0; i < 3; i++)
            {
                attackValue = SkillList.SwPierce3_1sp(hero);
                if (target < enemies.Length && enemies[target].hp > 0)
                {
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, enemies[target]);
                }
                target++;
            }
            if (Logic.RNGroll(hero.dsChance))
            {
                target = Logic.SelectPierce(enemies);
                for (int i = 0; i < 3; i++)
                {
                    attackValue = SkillList.SwPierce3_1sp(hero);
                    if (target < enemies.Length && enemies[target].hp > 0)
                    {
                        Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, enemies[target]);
                    }
                    target++;
                }
            }
        }
        else if (skillRoll >= 40 && skillRoll < 70)
        {
            hero.sp -= 4;
            foreach (Enemy enemy in enemies)
            {
                if (enemy.hp > 0)
                {
                    attackValue = SkillList.SwAoe2sp(hero);
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, enemy);
                }
            }
        }
        else if (skillRoll >= 70 && skillRoll < 100)
        {
            hero.sp -= 6;
            hero.drain = true;
            attackValue = SkillList.SwClosest3sp(hero);
            Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectFront(enemies));
            if (Logic.RNGroll(hero.dsChance))
            {
                attackValue = SkillList.SwClosest3sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectFront(enemies));
            }
            hero.drain = false;
        }
        else
        {
            hero.sp -= 2;
            SpreadHealingSkill(hero, heroes);
            if (Logic.RNGroll(hero.dsChance))
            {
                SpreadHealingSkill(hero, heroes);
            }
        }

        /*
          
         if (hero.sp < 2)
        {
            Logic.HeroAttak0SP(hero, heroes, enemies);
        }
        else if (hero.sp < 4)
        {
            range = 40;
            if (hero.unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                Logic.HeroAttak0SP(hero, heroes, enemies);
            }
            else if (skillRoll >= 10 && skillRoll < 15)
            {
                hero.sp -= 2;
                attackValue = SkillList.SwTarget_1sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
                if (Logic.RNGroll(hero.dsChance))
                {
                    attackValue = SkillList.SwTarget_1sp(hero);
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
                }
            }
            else if (skillRoll >= 15 && skillRoll < 40)
            {
                hero.sp -= 2;
                int target = Logic.SelectPierce(enemies);
                for (int i = 0; i < 3; i++)
                {
                    attackValue = SkillList.SwPierce3_1sp(hero);
                    if (target > enemies.Length && enemies[target].hp > 0)
                    {
                        Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
                    }
                    target++;

                }
                if (Logic.RNGroll(hero.dsChance))
                {
                    target = Logic.SelectPierce(enemies);
                    for (int i = 0; i < 3; i++)
                    {
                        attackValue = SkillList.SwPierce3_1sp(hero);
                        if (target > enemies.Length && enemies[target].hp > 0)
                        {
                            Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
                        }
                        target++;

                    }
                }
            }
            else if (skillRoll >= 40)
            {
                hero.sp -= 2;
                SpreadHealingSkill(hero, heroes);
                if (Logic.RNGroll(hero.dsChance))
                {
                    SpreadHealingSkill(hero, heroes);
                }
            }
            
        }
        else if (hero.sp < 6)
        {
            // 1 sp skill AI
            range = 15;
            if (hero.unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                Logic.HeroAttak0SP(hero, heroes, enemies);
            }
            else if (skillRoll >= 10 && skillRoll < 15)
            {
                hero.sp -= 2;
                attackValue = SkillList.SwTarget_1sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
                if (Logic.RNGroll(hero.dsChance))
                {
                    attackValue = SkillList.SwTarget_1sp(hero);
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
                }
            }
            else if (true) //add skillroll
            {
                hero.sp -= 2;
                int target = Logic.SelectPierce(enemies);
                for (int i = 0; i < 3; i++)
                {
                    attackValue = SkillList.SwPierce3_1sp(hero);
                    if (target > enemies.Length &&enemies[target].hp > 0)
                    {
                        Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
                    }
                    target++;

                }
                if (Logic.RNGroll(hero.dsChance))
                {
                    target = Logic.SelectPierce(enemies);
                    for (int i = 0; i < 3; i++)
                    {
                        attackValue = SkillList.SwPierce3_1sp(hero);
                        if (target > enemies.Length && enemies[target].hp > 0)
                        {
                            Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
                        }
                        target++;

                    }
                }
            }
            else if (skillRoll >= 15)
            {
                hero.sp -= 2;
                SpreadHealingSkill(hero, heroes);
                if (Logic.RNGroll(hero.dsChance))
                {
                    SpreadHealingSkill(hero, heroes);
                }
            }
        }
        else if (hero.sp >= 6)
        {
            // 1 - 2 sp skill AI
            range = 45;
            if (hero.unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                Logic.HeroAttak0SP(hero, heroes, enemies);
            }
            else if (skillRoll >= 10 && skillRoll < 15)
            {
                hero.sp -= 2;
                attackValue = SkillList.SwTarget_1sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
                if (Logic.RNGroll(hero.dsChance))
                {
                    attackValue = SkillList.SwTarget_1sp(hero);
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
                }
            }
            else if (rnd.Next(5) > 2) //add skillroll
            {
                hero.sp -= 2;
                int target = Logic.SelectPierce(enemies);
                for (int i = 0; i < 3; i++)
                {
                    attackValue = SkillList.SwPierce3_1sp(hero);
                    if (target > enemies.Length && enemies[target].hp > 0)
                    {
                        Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
                    }
                    target++;

                }
                if (Logic.RNGroll(hero.dsChance))
                {
                    target = Logic.SelectPierce(enemies);
                    for (int i = 0; i < 3; i++)
                    {
                        attackValue = SkillList.SwPierce3_1sp(hero);
                        if (target > enemies.Length && enemies[target].hp > 0)
                        {
                            Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
                        }
                        target++;

                    }
                }
            }
            else if (skillRoll >= 15 && skillRoll < 45)
            {
                hero.sp -= 6;
                hero.drain = true;
                attackValue = SkillList.SwClosest3sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectFront(enemies));
                if (Logic.RNGroll(hero.dsChance))
                {
                    attackValue = SkillList.SwClosest3sp(hero);
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectFront(enemies));
                }
                hero.drain = false;
            }
            else if (skillRoll >= 45)
            {
                hero.sp -= 2;
                SpreadHealingSkill(hero, heroes);
                if (Logic.RNGroll(hero.dsChance))
                {
                    SpreadHealingSkill(hero, heroes);
                }
            }
        }
        */
    }

    public static void SpearSkillSelection(Hero hero, Hero[] heroes, Enemy[] enemies)
    {
        
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;
        //int targetMethod = 0;

        if (hero.unity)
        {
            range = 10;
        }
        //int targetMethod = 0;
        if (hero.sp < 2) range += 10;
        else if (hero.sp < 4) range += 40;
        else if (hero.sp < 6) range += 70;
        else range += 100;

        skillRoll = rnd.Next(skillRoll);

        if (skillRoll < 10)
        {
            Logic.HeroAttak0SP(hero, heroes, enemies);
        }
        else if (skillRoll >= 10 && skillRoll < 25)
        {
            hero.sp -= 2;
            attackValue = SkillList.SpBack1sp(hero);
            Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectBack(enemies));
            if (Logic.RNGroll(hero.dsChance))
            {
                attackValue = SkillList.SwTarget_1sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectBack(enemies));
            }
        }
        else if (skillRoll >= 25 && skillRoll < 40)
        {
            hero.sp -= 2;
            int target = Logic.SelectPierce(enemies);
            for (int i = 0; i < 3; i++)
            {
                attackValue = SkillList.SpPierce3_1sp(hero);
                if (target < enemies.Length && enemies[target].hp > 0)
                {
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, enemies[target]);
                }
                target++;
            }
            if (Logic.RNGroll(hero.dsChance))
            {
                target = Logic.SelectPierce(enemies);
                for (int i = 0; i < 3; i++)
                {
                    attackValue = SkillList.SpPierce3_1sp(hero);
                    if (target < enemies.Length && enemies[target].hp > 0)
                    {
                        Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, enemies[target]);
                    }
                    target++;
                }
            }
        }
        else if (skillRoll >= 40 && skillRoll > 70)
        {
            hero.sp -= 4;
            attackValue = SkillList.SpTarget2sp(hero);
            Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
            if (Logic.RNGroll(hero.dsChance))
            {
                attackValue = SkillList.SpTarget2sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
            }
        }
        else if (skillRoll >= 70 && skillRoll < 100)
        {
            hero.sp -= 6;
            attackValue = SkillList.SpClosest3sp(hero);
            Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectFront(enemies));
            if (Logic.RNGroll(hero.dsChance))
            {
                attackValue = SkillList.SpClosest3sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectFront(enemies));
            }
        }
        else
        {
            hero.sp -= 2;
            SpreadHealingSkill(hero, heroes);
            if (Logic.RNGroll(hero.dsChance))
            {
                SpreadHealingSkill(hero, heroes);
            }
        }

    }

    public static void BowSkillSelection(Hero hero, Hero[] heroes, Enemy[] enemies)
    {
        
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;


        if (hero.unity)
        {
            range = 10;
        }
        //int targetMethod = 0;
        if (hero.sp < 2) range += 10;
        else if (hero.sp < 4) range += 40;
        else if (hero.sp < 6) range += 70;
        else range += 100;

        skillRoll = rnd.Next(skillRoll);

        if (skillRoll < 10)
        {
            Logic.HeroAttak0SP(hero, heroes, enemies);
        }
        else if (skillRoll >= 10 && skillRoll < 25)
        {
            hero.sp -= 2;
            attackValue = SkillList.BTarget1sp(hero);
            Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
            if (Logic.RNGroll(hero.dsChance))
            {
                attackValue = SkillList.BTarget1sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
            }
        }
        else if (skillRoll >= 25 && skillRoll < 40)
        { }
        else if (skillRoll >= 40 && skillRoll < 70)
        { }
        else if (skillRoll >= 70 && skillRoll < 100)
        { }
        else { }

            if (hero.sp < 2)
        {
                Logic.HeroAttak0SP(hero, heroes, enemies);
        }
        else if (hero.sp < 4)
        {
            // 1 sp skill AI
            range = 25;
            if (hero.unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                    Logic.HeroAttak0SP(hero, heroes, enemies);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                hero.sp -= 2;
                attackValue = SkillList.BTarget1sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                if (Logic.RNGroll(hero.dsChance))
                {
                    attackValue = SkillList.BTarget1sp(hero);
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                }
            }
            else if (skillRoll >= 25)
            {
                hero.sp -= 2;
                SpreadHealingSkill(hero, heroes);
                if (Logic.RNGroll(hero.dsChance))
                {
                    SpreadHealingSkill(hero, heroes);
                }
            }

        }
        else if (hero.sp >= 4)
        {
            // 1 - 2 sp skill AI
            range = 55;
            if (hero.unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                    Logic.HeroAttak0SP(hero, heroes, enemies);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                hero.sp -= 2;
                attackValue = SkillList.BTarget1sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                if (Logic.RNGroll(hero.dsChance))
                {
                    attackValue = SkillList.BTarget1sp(hero);
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                }
            }
            else if (skillRoll >= 25 && skillRoll < 55)
            {
                hero.sp -= 4;
                attackValue = SkillList.BBack2sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                if (Logic.RNGroll(hero.dsChance))
                {
                    attackValue = SkillList.BBack2sp(hero);
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                }
            }
            else if (skillRoll >= 55)
            {
                hero.sp -= 2;
                SpreadHealingSkill(hero, heroes);
                if (Logic.RNGroll(hero.dsChance))
                {
                    SpreadHealingSkill(hero, heroes);
                }
            }
        }
    }

    public static void StaffSkillSelection(Hero hero, Hero[] heroes, Enemy[] enemies)
    {
        
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;
        //int targetMethod = 0;

        if (hero.sp < 2)
        {
                Logic.HeroAttak0SP(hero, heroes, enemies);
        }
        else if (hero.sp < 6)
        {
            // 1 sp skill AI
            range = 80;
            if (hero.unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                    Logic.HeroAttak0SP(hero, heroes, enemies);
            }
            else if (skillRoll >= 10 && skillRoll < 20)
            {
                hero.sp -= 2;
                attackValue = SkillList.StClosest1sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                if (Logic.RNGroll(hero.dsChance))
                {
                    attackValue = SkillList.StClosest1sp(hero);
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                }

            }
            else if (skillRoll >= 20 && skillRoll < 80)
            {
                hero.sp -= 2;
                SkillList.StHeal1sp(hero);
                if (Logic.RNGroll(hero.dsChance))
                {
                    SkillList.StHeal1sp(hero);
                }
            }
            else if (skillRoll >= 80)
            {
                hero.sp -= 2;
                SpreadHealingSkill(hero, heroes);
                if (Logic.RNGroll(hero.dsChance))
                {
                    SpreadHealingSkill(hero, heroes);
                }
            }
        }
        else if (hero.sp >= 6)
        {
            // 1 - 2 sp skill AI
            range = 90;
            if (hero.unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                    Logic.HeroAttak0SP(hero, heroes, enemies);
            }
            else if (skillRoll >= 10 && skillRoll < 20)
            {
                hero.sp -= 2;
                attackValue = SkillList.StClosest1sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                if (Logic.RNGroll(hero.dsChance))
                {
                    attackValue = SkillList.StClosest1sp(hero);
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                }
            }
            else if (skillRoll >= 20 && skillRoll < 80)
            {
                hero.sp -= 2;
                SkillList.StHeal1sp(hero);
                if (Logic.RNGroll(hero.dsChance))
                {
                    SkillList.StHeal1sp(hero);
                }
            }
            else if (skillRoll >= 80 && skillRoll < 90)
            {
                hero.sp -= 6;
                attackValue = SkillList.StTarget3sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                if (Logic.RNGroll(hero.dsChance))
                {
                    attackValue = SkillList.StTarget3sp(hero);
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                }
            }
            else if (skillRoll >= 90)
            {
                hero.sp -= 2;
                SpreadHealingSkill(hero, heroes);
                if (Logic.RNGroll(hero.dsChance))
                {
                    SpreadHealingSkill(hero, heroes);
                }
            }
        }
    }

    public static void AxeSkillSelection(Hero hero, Hero[] heroes, Enemy[] enemies)
    {
        
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;
        //int targetMethod = 0;

        if (hero.sp < 2)
        {
                Logic.HeroAttak0SP(hero, heroes, enemies);
        }
        else if (hero.sp < 4)
        {
            // 1 sp skill AI
            range = 20;
            if (hero.unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                    Logic.HeroAttak0SP(hero, heroes, enemies);
            }
            else if (skillRoll >= 10 && skillRoll < 20)
            {
                hero.sp -= 2;
                attackValue = SkillList.AClosest_1sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                if (Logic.RNGroll(hero.dsChance))
                {
                    attackValue = SkillList.AClosest_1sp(hero);
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                }
            }
            else if (skillRoll >= 20)
            {
                hero.sp -= 2;
                SpreadHealingSkill(hero, heroes);
                if (Logic.RNGroll(hero.dsChance))
                {
                    SpreadHealingSkill(hero, heroes);
                }
            }
        }
        else if (hero.sp >= 4)
        {
            // 1 - 2 sp skill AI
            range = 80;
            if (hero.unity)
            {
                range += 10;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                    Logic.HeroAttak0SP(hero, heroes, enemies);
            }
            else if (skillRoll >= 10 && skillRoll < 20)
            {
                hero.sp -= 2;
                attackValue = SkillList.AClosest_1sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                if (Logic.RNGroll(hero.dsChance))
                {
                    attackValue = SkillList.AClosest_1sp(hero);
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                }
            }
            else if (skillRoll >= 20 && skillRoll < 80)
            {
                hero.sp -= 4;
                attackValue = SkillList.ATarget2sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                if (Logic.RNGroll(hero.dsChance))
                {
                    attackValue = SkillList.ATarget2sp(hero);
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue);
                }
            }
            else if (skillRoll >= 80)
            {
                hero.sp -= 2;
                SpreadHealingSkill(hero, heroes);
                if (Logic.RNGroll(hero.dsChance))
                {
                    SpreadHealingSkill(hero, heroes);
                }
            }
        }
    }

    #endregion


}
