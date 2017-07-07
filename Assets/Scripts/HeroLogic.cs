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

}