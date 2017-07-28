using System.Collections;
using System.Collections.Generic;
using System;

class HeroLogic
{

    public static void spreadHealingSkill(int k)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        //UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
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
            target = Logic.healLogic();
            Simulation.hero[target].hp++;
            if (Simulation.hero[target].hp > Simulation.hero[target].maxHp)
            {
                Simulation.hero[target].hp = Simulation.hero[target].maxHp;
            }
        }
    }

    public static void heroAttack(int k, bool dual)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        //UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        int skillSelection;
        int attackValue = 0;
        bool hasHealed = false;
        int attackModifier = Convert.ToInt32(0.2 * Simulation.hero[k].power);
        attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + 0.9f * Simulation.hero[k].power);
        if (Simulation.hero[k].sp >= 2)
        {
            skillSelection = UnityEngine.Random.Range(0, 100);
            if (skillSelection < 20 && (Simulation.hero[0].hpPerc < 0.85f || Simulation.hero[4].hpPerc < 0.85f))
            {
                spreadHealingSkill(k);
                hasHealed = true;
                if (!dual)
                {
                    Simulation.hero[k].sp -= 2;
                }
            }
            else
            {
                float skillModifier = UnityEngine.Random.Range(0, 50f) + 110f;
                skillModifier /= 100;
                attackValue = Convert.ToInt32(Simulation.hero[k].power * skillModifier);
                if (!dual)
                {
                    Simulation.hero[k].sp -= 2;
                }
            }
        }
        bool critRoll = Logic.RNGroll(Simulation.hero[k].critChance);
        if (critRoll)
        {
            attackValue = Convert.ToInt32(attackValue * Simulation.hero[k].critDamage);
        }
        bool evadeRoll = Logic.RNGroll(2.5f);
        if (!evadeRoll && hasHealed == false)
        {
            Simulation.hpDummy -= attackValue;
            PetLogic.petSelection(k);
        }
    }

    public static void heroAttackTarget(int k, bool dual, int target)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int skillSelection;
        int attackValue = 0;
        bool hasHealed = false;
        int attackModifier = Convert.ToInt32(0.2 * Simulation.hero[k].power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.9f * Simulation.hero[k].power);
        if (Simulation.hero[k].sp >= 2)
        {
            skillSelection = rnd.Next(0, 100);
            if (skillSelection < 20 && (Simulation.hero[0].hpPerc < 0.85f || Simulation.hero[4].hpPerc < 0.85f))
            {
                spreadHealingSkill(k);
                hasHealed = true;
                if (!dual)
                {
                    Simulation.hero[k].sp -= 2;
                }
            }
            else
            {
                float skillModifier = rnd.Next(0, 50) + 110;
                skillModifier /= 100;
                attackValue = Convert.ToInt32(Simulation.hero[k].power * skillModifier);
                if (!dual)
                {
                    Simulation.hero[k].sp -= 2;
                }
            }
        }
        bool critRoll = Logic.RNGroll(Simulation.hero[k].critChance);
        if (critRoll)
        {
            attackValue = Convert.ToInt32(attackValue * Simulation.hero[k].critDamage);
        }
        bool evadeRoll = Logic.RNGroll(2.5f);
        if (!evadeRoll && hasHealed == false)
        {
            target -= attackValue;
            //PetLogic.petSelection1v1(k);
        }
    }

    public static void swordSkillSelection(int k, int sp, bool unity, bool DS)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;
        //int targetMethod = 0;

        if (sp < 2)
        {
            Logic.heroNormalAttack(k, DS);
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
                Logic.heroNormalAttack(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.swTarget_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.swTarget_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 25)
            {
                spreadHealingSkill(k);
                if (DS)
                {
                    spreadHealingSkill(k);
                }
            }
        }
        else if (sp >=6)
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
                Logic.heroNormalAttack(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 15)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.swTarget_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.swTarget_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 15 && skillRoll < 45)
            {
                Simulation.hero[k].sp -= 6;
                Simulation.hero[k].drain = true;
                attackValue = SkillList.swClosest3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.swClosest3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }
                Simulation.hero[k].drain = false;
            }
            else if (skillRoll >= 45)
            {
                spreadHealingSkill(k);
                if (DS)
                {
                    spreadHealingSkill(k);
                }
            }
        }
    }

    public static void spearSkillSelection(int k, int sp, bool unity, bool DS)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;
        //int targetMethod = 0;

        if (sp < 2)
        {
            Logic.heroNormalAttack(k, DS);
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
                Logic.heroNormalAttack(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.spBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.spBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 25)
            {
                spreadHealingSkill(k);
                if (DS)
                {
                    spreadHealingSkill(k);
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
                Logic.heroNormalAttack(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.spBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.spBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 25 && skillRoll > 55)
            {
                Simulation.hero[k].sp -= 4;
                attackValue = SkillList.spTarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.spTarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 55)
            {
                spreadHealingSkill(k);
                if (DS)
                {
                    spreadHealingSkill(k);
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
                Logic.heroNormalAttack(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.spBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.spBack1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 25 && skillRoll < 55)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.spTarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.spTarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 55 && skillRoll <85)
            {
                Simulation.hero[k].sp -= 6;
                attackValue = SkillList.spClosest3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.spClosest3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 85)
            {
                spreadHealingSkill(k);
                if (DS)
                {
                    spreadHealingSkill(k);
                }
            }
        }      
    }

    public static void bowSkillSelection(int k, int sp, bool unity, bool DS)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;
        //int targetMethod = 0;
        if (sp < 2)
        {
            Logic.heroNormalAttack(k, DS);
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
                Logic.heroNormalAttack(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.bTarget1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.bTarget1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }
                Simulation.hero[k].sp -= 2;
            }
            else if (skillRoll >= 25)
            {
                spreadHealingSkill(k);
                if (DS)
                {
                    spreadHealingSkill(k);
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
                Logic.heroNormalAttack(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.bTarget1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.bTarget1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }
                Simulation.hero[k].sp -= 2;
            }
            else if (skillRoll >= 25 && skillRoll <55)
            {
                Simulation.hero[k].sp -= 4;
                attackValue = SkillList.bBack2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.bBack2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }
                Simulation.hero[k].sp -= 4;
            }
            else if (skillRoll >= 55)
            {
                spreadHealingSkill(k);
                if (DS)
                {
                    spreadHealingSkill(k);
                }
            }
        }
    }

    public static void staffSkillSelection(int k, int sp, bool unity, bool DS)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;
        //int targetMethod = 0;

        if (sp < 2)
        {
            Logic.heroNormalAttack(k, DS);
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
                Logic.heroNormalAttack(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 20)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.stClosest1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.stClosest1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }

            }
            else if (skillRoll >= 20 && skillRoll < 80)
            {
                Simulation.hero[k].sp -= 2;
                SkillList.stHeal1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                if (DS)
                {
                    SkillList.stHeal1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                }
            }
            else if (skillRoll >= 80)
            {
                spreadHealingSkill(k);
                if (DS)
                {
                    spreadHealingSkill(k);
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
                Logic.heroNormalAttack(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 20)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.stClosest1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.stClosest1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 20 && skillRoll < 80)
            {
                Simulation.hero[k].sp -= 2;
                SkillList.stHeal1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                if (DS)
                {
                    SkillList.stHeal1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                }
            }
            else if (skillRoll >= 80 && skillRoll < 90)
            {
                Simulation.hero[k].sp -= 6;
                attackValue = SkillList.stTarget3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.stTarget3sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 90)
            {
                spreadHealingSkill(k);
                if (DS)
                {
                    spreadHealingSkill(k);
                }
            }
        }
    }

    public static void axeSkillSelection(int k, int sp, bool unity, bool DS)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int attackValue = 0;
        int skillRoll = 0;
        int range = 0;
        //int targetMethod = 0;

        if (sp < 2)
        {
            Logic.heroNormalAttack(k, DS);
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
                Logic.heroNormalAttack(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll <20)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.aClosest_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.aClosest_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 20)
            {
                spreadHealingSkill(k);
                if (DS)
                {
                    spreadHealingSkill(k);
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
                Logic.heroNormalAttack(k, DS);
            }
            else if (skillRoll >= 10 && skillRoll < 20)
            {
                Simulation.hero[k].sp -= 2;
                attackValue = SkillList.aClosest_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.aClosest_1sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 20 && skillRoll < 80)
            {
                Simulation.hero[k].sp -= 4;
                attackValue = SkillList.aTarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                Logic.heroDamageApplication(k, attackValue);
                if (DS)
                {
                    attackValue = SkillList.aTarget2sp(Simulation.hero[k].power, Simulation.hero[k].critChance, Simulation.hero[k].critDamage);
                    Logic.heroDamageApplication(k, attackValue);
                }
            }
            else if (skillRoll >= 80)
            {
                spreadHealingSkill(k);
                if (DS)
                {
                    spreadHealingSkill(k);
                }
            }
        }
    }

    public static void weaponSelection(int k, bool DS)
    {
        int weapon = (int)Simulation.hero[k].weapon;
        switch (weapon)
        {
            case 0:
                bowSkillSelection(k, Simulation.hero[k].sp, Simulation.hero[k].unity, DS);
                break;
            case 1:
                spearSkillSelection(k, Simulation.hero[k].sp, Simulation.hero[k].unity, DS);
                break;
            case 2:
                swordSkillSelection(k, Simulation.hero[k].sp, Simulation.hero[k].unity, DS);
                break;
            case 3:
                staffSkillSelection(k, Simulation.hero[k].sp, Simulation.hero[k].unity, DS);
                break;
            case 4:
                axeSkillSelection(k, Simulation.hero[k].sp, Simulation.hero[k].unity, DS);
                break;
            default:
                break;
        }
    }
}


/*if ((int)Simulation.hero[k].weapon == 0)
       {
           bowSkillSelection(k, Simulation.hero[k].sp, Simulation.hero[k].unity, DS);
       }
       else if ((int)Simulation.hero[k].weapon == 1)
       {
           spearSkillSelection(k, Simulation.hero[k].sp, Simulation.hero[k].unity, DS);
       }
       else if ((int)Simulation.hero[k].weapon == 2)
       {
           swordSkillSelection(k, Simulation.hero[k].sp, Simulation.hero[k].unity, DS);
       }
       else if ((int)Simulation.hero[k].weapon == 3)
       {
           staffSkillSelection(k, Simulation.hero[k].sp, Simulation.hero[k].unity, DS);
       }
       else if ((int)Simulation.hero[k].weapon == 4)
       {
           axeSkillSelection(k, Simulation.hero[k].sp, Simulation.hero[k].unity, DS);
       }*/
