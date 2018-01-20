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
        int healingModifier = Convert.ToInt32(0.365 * RaidSimulation.hero[k].power);

        healingValue = Convert.ToInt32(UnityEngine.Random.Range(0, healingModifier) + 0.73 * RaidSimulation.hero[k].power);

        bool critRoll = Logic.RNGroll(RaidSimulation.hero[k].critChance);
        if (critRoll)
        {
            healingValue = Convert.ToInt32(healingValue * RaidSimulation.hero[k].critDamage);
        }

        for (i = 0; i < healingValue; i++)
        {
            target = Logic.HealLogic();
            RaidSimulation.hero[target].hp++;
            if (RaidSimulation.hero[target].hp > RaidSimulation.hero[target].maxHp)
            {
                RaidSimulation.hero[target].hp = RaidSimulation.hero[target].maxHp;
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
                RaidSimulation.hero[k].sp -= 2;
                attackValue = SkillList.SwTarget_1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SwTarget_1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 15)
            {
                RaidSimulation.hero[k].sp -= 2;
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
                RaidSimulation.hero[k].sp -= 2;
                attackValue = SkillList.SwTarget_1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SwTarget_1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 15 && skillRoll < 45)
            {
                RaidSimulation.hero[k].sp -= 6;
                RaidSimulation.hero[k].drain = true;
                attackValue = SkillList.SwClosest3sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SwClosest3sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
                RaidSimulation.hero[k].drain = false;
            }
            else if (skillRoll >= 45)
            {
                RaidSimulation.hero[k].sp -= 2;
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
                RaidSimulation.hero[k].sp -= 2;
                attackValue = SkillList.SpBack1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpBack1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 25)
            {
                RaidSimulation.hero[k].sp -= 2;
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
                RaidSimulation.hero[k].sp -= 2;
                attackValue = SkillList.SpBack1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpBack1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 25 && skillRoll > 55)
            {
                RaidSimulation.hero[k].sp -= 4;
                attackValue = SkillList.SpTarget2sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpTarget2sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 55)
            {
                RaidSimulation.hero[k].sp -= 2;
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
                RaidSimulation.hero[k].sp -= 2;
                attackValue = SkillList.SpBack1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpBack1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 25 && skillRoll < 55)
            {
                RaidSimulation.hero[k].sp -= 4;
                attackValue = SkillList.SpTarget2sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpTarget2sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 55 && skillRoll < 85)
            {
                RaidSimulation.hero[k].sp -= 6;
                attackValue = SkillList.SpClosest3sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpClosest3sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 85)
            {
                RaidSimulation.hero[k].sp -= 2;
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
                RaidSimulation.hero[k].sp -= 2;
                attackValue = SkillList.BTarget1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.BTarget1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 25)
            {
                RaidSimulation.hero[k].sp -= 2;
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
                RaidSimulation.hero[k].sp -= 2;
                attackValue = SkillList.BTarget1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.BTarget1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 25 && skillRoll < 55)
            {
                RaidSimulation.hero[k].sp -= 4;
                attackValue = SkillList.BBack2sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.BBack2sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 55)
            {
                RaidSimulation.hero[k].sp -= 2;
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
                RaidSimulation.hero[k].sp -= 2;
                attackValue = SkillList.StClosest1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.StClosest1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }

            }
            else if (skillRoll >= 20 && skillRoll < 80)
            {
                RaidSimulation.hero[k].sp -= 2;
                SkillList.StHeal1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                if (DS)
                {
                    SkillList.StHeal1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                }
            }
            else if (skillRoll >= 80)
            {
                RaidSimulation.hero[k].sp -= 2;
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
                RaidSimulation.hero[k].sp -= 2;
                attackValue = SkillList.StClosest1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.StClosest1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 20 && skillRoll < 80)
            {
                RaidSimulation.hero[k].sp -= 2;
                SkillList.StHeal1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                if (DS)
                {
                    SkillList.StHeal1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                }
            }
            else if (skillRoll >= 80 && skillRoll < 90)
            {
                RaidSimulation.hero[k].sp -= 6;
                attackValue = SkillList.StTarget3sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.StTarget3sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 90)
            {
                RaidSimulation.hero[k].sp -= 2;
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
                RaidSimulation.hero[k].sp -= 2;
                attackValue = SkillList.AClosest_1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.AClosest_1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 20)
            {
                RaidSimulation.hero[k].sp -= 2;
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
                RaidSimulation.hero[k].sp -= 2;
                attackValue = SkillList.AClosest_1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.AClosest_1sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 20 && skillRoll < 80)
            {
                RaidSimulation.hero[k].sp -= 4;
                attackValue = SkillList.ATarget2sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.ATarget2sp(RaidSimulation.hero[k].power, RaidSimulation.hero[k].critChance, RaidSimulation.hero[k].critDamage, RaidSimulation.hero[k].empowerChance);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 80)
            {
                RaidSimulation.hero[k].sp -= 2;
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
        int weapon = (int)RaidSimulation.hero[k].weapon;
        switch (weapon)
        {
            case 1:
                BowSkillSelection(k, RaidSimulation.hero[k].sp, RaidSimulation.hero[k].unity, DS);
                break;
            case 2:
                SpearSkillSelection(k, RaidSimulation.hero[k].sp, RaidSimulation.hero[k].unity, DS);
                break;
            case 3:
                SwordSkillSelection(k, RaidSimulation.hero[k].sp, RaidSimulation.hero[k].unity, DS);
                break;
            case 4:
                StaffSkillSelection(k, RaidSimulation.hero[k].sp, RaidSimulation.hero[k].unity, DS);
                break;
            case 5:
                AxeSkillSelection(k, RaidSimulation.hero[k].sp, RaidSimulation.hero[k].unity, DS);
                break;
            default:
                break;
        }
    }


    #region New Code
    public static void WeaponSelection(Character hero, Character[] heroes, Character[] enemies)
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

    public static void SpreadHealingSkill(Character hero, Character[] heroes)
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
            target = Logic.HealFindWeakestPerc(heroes);
            heroes[target].hp++;
            if (heroes[target].hp > heroes[target].maxHp)
            {
                heroes[target].hp = heroes[target].maxHp;
            }
        }
    }

    public static void SwordSkillSelection(Character hero, Character[] heroes, Character[] enemies)
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
            bool isCrit = Logic.RNGroll(hero.critChance);
            bool isEmp = Logic.RNGroll(hero.empowerChance);
            hero.sp -= 2;
            int target = Logic.SelectPierce(enemies);
            for (int i = 0; i < 3; i++)
            {
                attackValue = SkillList.SwPierce3_1sp(hero);
                if (isCrit)
                {
                    attackValue = Convert.ToInt32(attackValue * hero.critDamage);
                }
                if (isEmp) attackValue *= 2;
                if (target < enemies.Length && enemies[target].hp > 0)
                {
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, enemies[target]);
                }
                target++;
            }
            if (Logic.RNGroll(hero.dsChance))
            {
                isCrit = Logic.RNGroll(hero.critChance);
                isEmp = Logic.RNGroll(hero.empowerChance);
                target = Logic.SelectPierce(enemies);
                for (int i = 0; i < 3; i++)
                {
                    attackValue = SkillList.SwPierce3_1sp(hero);
                    if (isCrit)
                    {
                        attackValue = Convert.ToInt32(attackValue * hero.critDamage);
                    }
                    if (isEmp) attackValue *= 2;
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
            bool isCrit = Logic.RNGroll(hero.critChance);
            bool isEmp = Logic.RNGroll(hero.empowerChance);
            hero.sp -= 4;
            for (int i = 0; i < enemies.Length; i++)
            {
                attackValue = SkillList.SwAoe2sp(hero);
                if (isCrit)
                {
                    attackValue = Convert.ToInt32(attackValue * hero.critDamage);
                }
                if (isEmp) attackValue *= 2;
                if (enemies[i].hp > 0)
                {
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, enemies[i]);
                }
            }
            if (Logic.RNGroll(hero.dsChance))
            {
                isCrit = Logic.RNGroll(hero.critChance);
                isEmp = Logic.RNGroll(hero.empowerChance);
                for (int i = 0; i < enemies.Length; i++)
                {
                    attackValue = SkillList.SwAoe2sp(hero);
                    if (isCrit)
                    {
                        attackValue = Convert.ToInt32(attackValue * hero.critDamage);
                    }
                    if (isEmp) attackValue *= 2;
                    if (enemies[i].hp > 0)
                    {
                        Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, enemies[i]);
                    }
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
    }

    public static void SpearSkillSelection(Character hero, Character[] heroes, Character[] enemies)
    {
        
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;
        if (hero.unity)
        {
            range = 10;
        }
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
                attackValue = SkillList.SpBack1sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectBack(enemies));
            }
        }
        else if (skillRoll >= 25 && skillRoll < 40)
        {
            bool isCrit = Logic.RNGroll(hero.critChance);
            bool isEmp = Logic.RNGroll(hero.empowerChance);
            hero.sp -= 2;
            int target = Logic.SelectPierce(enemies);
            for (int i = 0; i < 3; i++)
            {
                attackValue = SkillList.SpPierce3_1sp(hero);
                if (isCrit)
                {
                    attackValue = Convert.ToInt32(attackValue * hero.critDamage);
                }
                if (isEmp) attackValue *= 2;
                if (target < enemies.Length && enemies[target].hp > 0)
                {
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, enemies[target]);
                }
                target++;
            }
            if (Logic.RNGroll(hero.dsChance))
            {
                isCrit = Logic.RNGroll(hero.critChance);
                isEmp = Logic.RNGroll(hero.empowerChance);
                target = Logic.SelectPierce(enemies);
                for (int i = 0; i < 3; i++)
                {
                    attackValue = SkillList.SpPierce3_1sp(hero);
                    if (isCrit)
                    {
                        attackValue = Convert.ToInt32(attackValue * hero.critDamage);
                    }
                    if (isEmp) attackValue *= 2;
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

    public static void BowSkillSelection(Character hero, Character[] heroes, Character[] enemies)
    {
        
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;


        if (hero.unity)
        {
            range = 10;
        }
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
        {
            bool isCrit = Logic.RNGroll(hero.critChance);
            bool isEmp = Logic.RNGroll(hero.empowerChance);
            hero.sp -= 2;
            for (int i = 0; i < enemies.Length; i++)
            {
                attackValue = SkillList.BAoe1sp(hero);
                if (isCrit)
                {
                    attackValue = Convert.ToInt32(attackValue * hero.critDamage);
                }
                if (isEmp) attackValue *= 2;

                if (enemies[i].hp > 0)
                {
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, enemies[i]);
                }
            }
            if (Logic.RNGroll(hero.dsChance))
            {
                isCrit = Logic.RNGroll(hero.critChance);
                isEmp = Logic.RNGroll(hero.empowerChance);
                for (int i = 0; i < 3; i++)
                {
                    attackValue = SkillList.SpPierce3_1sp(hero);
                    if (isCrit)
                    {
                        attackValue = Convert.ToInt32(attackValue * hero.critDamage);
                    }
                    if (isEmp) attackValue *= 2;
                    if (enemies[i].hp > 0)
                    {
                        Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, enemies[i]);
                    }
                }
            }
        }
        else if (skillRoll >= 40 && skillRoll < 70)
        {
            hero.sp -= 4;
            attackValue = SkillList.BBack2sp(hero);
            Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectBack(enemies));
            if (Logic.RNGroll(hero.dsChance))
            {
                attackValue = SkillList.BBack2sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectBack(enemies));
            }
        }
        else if (skillRoll >= 70 && skillRoll < 100)
        {
            bool isCrit = Logic.RNGroll(hero.critChance);
            bool isEmp = Logic.RNGroll(hero.empowerChance);
            hero.sp -= 6;
            hero.drain = true;
            for (int i = 0; i < enemies.Length; i++)
            {
                attackValue = SkillList.BAoeDraint3sp(hero);
                if (isCrit)
                {
                    attackValue = Convert.ToInt32(attackValue * hero.critDamage);
                }
                if (isEmp) attackValue *= 2;

                if (enemies[i].hp > 0)
                {
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, enemies[i]);
                }

            }
            if (Logic.RNGroll(hero.dsChance))
            {
                isCrit = Logic.RNGroll(hero.critChance);
                isEmp = Logic.RNGroll(hero.empowerChance);
                for (int i = 0; i < enemies.Length; i++)
                {
                    attackValue = SkillList.BAoeDraint3sp(hero);
                    if (isCrit)
                    {
                        attackValue = Convert.ToInt32(attackValue * hero.critDamage);
                    }
                    if (isEmp) attackValue *= 2;
                    if (enemies[i].hp > 0)
                    {
                        Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, enemies[i]);
                    }
                }
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
    }

    public static void StaffSkillSelection(Character hero, Character[] heroes, Character[] enemies)
    {
        
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;

        if (hero.unity)
        {
            range = 10;
        }
        if (hero.sp < 2) range += 10;
        else if (hero.sp < 4) range += 80;
        else if (hero.sp < 6) range += 90;
        else range += 100;

        skillRoll = rnd.Next(skillRoll);

        if (skillRoll < 10)
        {
            Logic.HeroAttak0SP(hero, heroes, enemies);
        }
        else if (skillRoll >= 10 && skillRoll < 15)
        {
            hero.sp -= 2;
            attackValue = SkillList.StClosest1sp(hero);
            Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectBack(enemies));
            if (Logic.RNGroll(hero.dsChance))
            {
                attackValue = SkillList.StClosest1sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectBack(enemies));
            }
        }
        else if (skillRoll >= 15 && skillRoll < 80)
        {
            hero.sp -= 2;
            attackValue = SkillList.StHeal1sp(hero);
            int target = Logic.HealFindWeakestPerc(heroes);
            heroes[target].hp += attackValue;
            if (heroes[target].hp > heroes[target].maxHp) heroes[target].hp = heroes[target].maxHp;

            if (Logic.RNGroll(hero.dsChance))
            {
                attackValue = SkillList.StHeal1sp(hero);
                target = Logic.HealFindWeakestPerc(heroes);
                heroes[target].hp += attackValue;
                if (heroes[target].hp > heroes[target].maxHp) heroes[target].hp = heroes[target].maxHp;
            }
        }
        else if (skillRoll >= 80 && skillRoll < 90)
        {
            bool isCrit = Logic.RNGroll(hero.critChance);
            bool isEmp = Logic.RNGroll(hero.empowerChance);
            hero.sp -= 4;
            for (int i = 0; i < enemies.Length; i++)
            {
                attackValue = SkillList.StAOE2sp(hero);
                if (isCrit)
                {
                    attackValue = Convert.ToInt32(attackValue * hero.critDamage);
                }
                if (isEmp) attackValue *= 2;

                if (enemies[i].hp > 0)
                {
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, enemies[i]);
                }
            }
            if (Logic.RNGroll(hero.dsChance))
            {
                isCrit = Logic.RNGroll(hero.critChance);
                isEmp = Logic.RNGroll(hero.empowerChance);
                for (int i = 0; i < 3; i++)
                {
                    attackValue = SkillList.StAOE2sp(hero);
                    if (isCrit)
                    {
                        attackValue = Convert.ToInt32(attackValue * hero.critDamage);
                    }
                    if (isEmp) attackValue *= 2;
                    if (enemies[i].hp > 0)
                    {
                        Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, enemies[i]);
                    }
                }
            }
        }
        else if (skillRoll >= 90 && skillRoll < 100)
        {
            hero.sp -= 6;
            attackValue = SkillList.StTarget3sp(hero);
            Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
            if (Logic.RNGroll(hero.dsChance))
            {
                attackValue = SkillList.StTarget3sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
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

    public static void AxeSkillSelection(Character hero, Character[] heroes, Character[] enemies)
    {
        
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;

        if (hero.unity)
        {
            range = 10;
        }
        if (hero.sp < 2) range += 10;
        else if (hero.sp < 4) range += 40;
        else if (hero.sp < 6) range += 100;
        else range += 150;

        skillRoll = rnd.Next(skillRoll);

        if (skillRoll < 10)
        {
            Logic.HeroAttak0SP(hero, heroes, enemies);
        }
        else if (skillRoll >= 10 && skillRoll < 30)
        {
            bool isCrit = Logic.RNGroll(hero.critChance);
            bool isEmp = Logic.RNGroll(hero.empowerChance);
            hero.sp -= 2;
            hero.drain = true;
            for (int i = 0; i < enemies.Length; i++)
            {
                attackValue = SkillList.AAoeDrain1sp(hero);
                if (isCrit)
                {
                    attackValue = Convert.ToInt32(attackValue * hero.critDamage);
                }
                if (isEmp) attackValue *= 2;

                if (enemies[i].hp > 0)
                {
                    Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, enemies[i]);
                }

            }
            if (Logic.RNGroll(hero.dsChance))
            {
                isCrit = Logic.RNGroll(hero.critChance);
                isEmp = Logic.RNGroll(hero.empowerChance);
                for (int i = 0; i < enemies.Length; i++)
                {
                    attackValue = SkillList.AAoeDrain1sp(hero);
                    if (isCrit)
                    {
                        attackValue = Convert.ToInt32(attackValue * hero.critDamage);
                    }
                    if (isEmp) attackValue *= 2;
                    if (enemies[i].hp > 0)
                    {
                        Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, enemies[i]);
                    }
                }
            }
            hero.drain = false;
        }
        else if (skillRoll >= 30 && skillRoll < 40)
        {
            hero.sp -= 2;

            attackValue = SkillList.AClosest_1sp(hero);
            Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectFront(enemies));
            if (Logic.RNGroll(hero.dsChance))
            {
                attackValue = SkillList.AClosest_1sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectFront(enemies));
            }
        }
        else if (skillRoll >= 40 && skillRoll < 100)
        {
            hero.sp -= 4;
            attackValue = SkillList.ATarget2sp(hero);
            Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
            if (Logic.RNGroll(hero.dsChance))
            {
                attackValue = SkillList.ATarget2sp(hero);
                Logic.HeroDamageApplication(hero, heroes, enemies, attackValue, Logic.SelectTarget(enemies));
            }
        }
        else if (skillRoll >= 100 && skillRoll < 150)
        {
            hero.sp -= 6;
            attackValue = SkillList.ASpreadHeal3sp(hero);
            for (int i = 0; i < attackValue; i++)
            {
                int target = Logic.HealFindWeakestPerc(heroes);
                heroes[target].hp++;
                if (heroes[target].hp > heroes[target].maxHp)
                {
                    heroes[target].hp = heroes[target].maxHp;
                }
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

    #endregion


}
