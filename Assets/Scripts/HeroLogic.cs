using System.Collections;
using System.Collections.Generic;
using System;

class HeroLogic
{

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
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
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
                attackValue = SkillList.SwTarget_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SwTarget_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
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
                attackValue = SkillList.SwTarget_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SwTarget_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 15 && skillRoll < 45)
            {
                Simulation.hero[k].sp -= 6;
                Simulation.hero[k].drain = true;
                attackValue = SkillList.SwClosest3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SwClosest3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
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
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
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
                attackValue = SkillList.SpBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
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
                attackValue = SkillList.SpBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 25 && skillRoll > 55)
            {
                Simulation.hero[k].sp -= 4;
                attackValue = SkillList.SpTarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpTarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
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
                attackValue = SkillList.SpBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 25 && skillRoll < 55)
            {
                Simulation.hero[k].sp -= 4;
                attackValue = SkillList.SpTarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpTarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 55 && skillRoll < 85)
            {
                Simulation.hero[k].sp -= 6;
                attackValue = SkillList.SpClosest3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.SpClosest3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
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
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
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
                attackValue = SkillList.BTarget1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.BTarget1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
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
                attackValue = SkillList.BTarget1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.BTarget1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 25 && skillRoll < 55)
            {
                Simulation.hero[k].sp -= 4;
                attackValue = SkillList.BBack2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.BBack2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
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
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
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
                attackValue = SkillList.StClosest1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.StClosest1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.HeroDamageApplication(k, attackValue);
                }

            }
            else if (skillRoll >= 20 && skillRoll < 80)
            {
                Simulation.hero[k].sp -= 2;
                SkillList.StHeal1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                if (DS)
                {
                    SkillList.StHeal1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
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
                attackValue = SkillList.StClosest1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.StClosest1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 20 && skillRoll < 80)
            {
                Simulation.hero[k].sp -= 2;
                SkillList.StHeal1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                if (DS)
                {
                    SkillList.StHeal1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                }
            }
            else if (skillRoll >= 80 && skillRoll < 90)
            {
                Simulation.hero[k].sp -= 6;
                attackValue = SkillList.StTarget3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.StTarget3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
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
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
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
                attackValue = SkillList.AClosest_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.AClosest_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
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
                attackValue = SkillList.AClosest_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.AClosest_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.HeroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 20 && skillRoll < 80)
            {
                Simulation.hero[k].sp -= 4;
                attackValue = SkillList.ATarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.HeroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.ATarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
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
}
