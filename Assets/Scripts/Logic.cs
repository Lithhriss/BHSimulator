using System.Collections;
using System.Collections.Generic;
using System;

class Logic
{

    public static bool RNGroll(float a)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        //UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
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

    public static float turnRate(int b, int a)
    {
        float tr = 0f;
        tr = ((a + b) / 2f);
        tr = (float)Math.Pow(tr, 2);
        tr = tr / (100f * b);
        return tr;
    }

    public static void teamHeal(int l)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        //UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        int i;
        int healModifier = Convert.ToInt32(Simulation.hero[l].power * 0.072);
        float healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.324 * Simulation.hero[l].power);

        bool critroll = RNGroll(Simulation.hero[l].critChance);
        bool petRoll = RNGroll(20f);

        if (critroll)
        {
            healValue *= Simulation.hero[l].critDamage;
        }
        if (petRoll)
        {
            for (i = 0; i < 5; i++)
            {
                if (Simulation.hero[i].hp > 0)
                {
                    Simulation.hero[i].hp += Convert.ToInt32(healValue);
                    if (Simulation.hero[i].hp >= Simulation.hero[i].maxHp)
                    {
                        Simulation.hero[i].hp = Simulation.hero[i].maxHp;
                    }
                }
            }
        }
    }

    public static void hpPerc()
    {
        int i;
        for (i = 0; i < 5; i++)
        {

            Simulation.hero[i].hpPerc = (float)(Simulation.hero[i].hp) / (float)(Simulation.hero[i].maxHp);

        }
    }

    public static int healLogic()
    {
        int i;
        int lowest = 0;
        hpPerc();
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

    public static int targetSelection(int method)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        //UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        int target = 0;
        int i = 0;
        bool targetLocked = false;
        if (method == 1)
        {
            while (!targetLocked)
            {
                if (Simulation.hero[i].alive)
                {
                    target = i;
                    targetLocked = true;
                }
                i++;
            }
        }
        if (method == 2)
        {
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
        }
        if (method == 3)
        {
            while (!targetLocked)
            {
                i = UnityEngine.Random.Range(0, 5);
                if (Simulation.hero[i].alive)
                {
                    target = i;
                    targetLocked = true;
                }
            }
        }
        return target;
    }


    public static int bossSkillSelection(int sp, out int finalAttack)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        //UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        int attackValue = 0;
        int skillRoll = 0;
        int attackModifier = 0;
        int targetMethod = 0;

        if (sp < 2)
        {
            //normal attack
            attackModifier = Convert.ToInt32(0.2 * Simulation.dummyPower);
            attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + 0.9 * Simulation.dummyPower);
            targetMethod = 1;
        }
        else if (sp < 4)
        {
            // 1 sp skill AI
            skillRoll = UnityEngine.Random.Range(0, 100);
            if (skillRoll < 20)
            {
                attackModifier = Convert.ToInt32(0.2 * Simulation.dummyPower);
                attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + 0.9 * Simulation.dummyPower);
                targetMethod = 1;
            }
            else if (skillRoll >= 20 && skillRoll < 60)
            {
                float skillModifier = (UnityEngine.Random.Range(0, 126) + 94);
                skillModifier /= 100;
                attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                Simulation.spDummy -= 2;
                targetMethod = 1;
            }
            else if (skillRoll >= 60)
            {
                float skillModifier = (UnityEngine.Random.Range(0, 132) + 99);
                skillModifier /= 100;
                attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                Simulation.spDummy -= 2;
                targetMethod = 2;
            }
        }
        else if (sp < 6)
        {
            // 1 - 2 sp skill AI
            skillRoll = UnityEngine.Random.Range(0, 100);
            if (skillRoll < 15)
            {
                attackModifier = Convert.ToInt32(0.2 * Simulation.dummyPower);
                attackValue = Convert.ToInt32(UnityEngine.Random.Range(1, attackModifier) + 0.9 * Simulation.dummyPower);
                targetMethod = 1;
            }
            else if (skillRoll >= 15 && skillRoll < 55)
            {
                float skillModifier = (UnityEngine.Random.Range(0, 126) + 94);
                skillModifier /= 100;
                attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                Simulation.spDummy -= 2;
                targetMethod = 1;
            }
            else if (skillRoll >= 55 && skillRoll < 95)
            {
                float skillModifier = (UnityEngine.Random.Range(0, 132) + 99);
                skillModifier /= 100;
                attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                Simulation.spDummy -= 2;
                targetMethod = 2;
            }
            else if (skillRoll >= 95)
            {
                float skillModifier = (UnityEngine.Random.Range(0, 136) + 102);
                skillModifier /= 100;
                attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                Simulation.spDummy -= 4;
                targetMethod = 3;
            }
        }
        else if (sp < 8)
        {
            // 1 - 2 sp skill AI
            skillRoll = UnityEngine.Random.Range(0, 100);
            if (skillRoll < 5)
            {
                attackModifier = Convert.ToInt32(0.2 * Simulation.dummyPower);
                attackValue = Convert.ToInt32(UnityEngine.Random.Range(1, attackModifier) + 0.9 * Simulation.dummyPower);
                targetMethod = 1;
            }
            else if (skillRoll >= 5 && skillRoll < 50)
            {
                float skillModifier = (UnityEngine.Random.Range(0, 126) + 94);
                skillModifier /= 100;
                attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                Simulation.spDummy -= 2;
                targetMethod = 1;
            }
            else if (skillRoll >= 50 && skillRoll < 95)
            {
                float skillModifier = (UnityEngine.Random.Range(0, 132) + 99);
                skillModifier /= 100;
                attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                Simulation.spDummy -= 2;
                targetMethod = 2;
            }
            else if (skillRoll >= 95)
            {
                float skillModifier = (UnityEngine.Random.Range(0, 136) + 102);
                skillModifier /= 100;
                attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                Simulation.spDummy -= 4;
                targetMethod = 3;
            }
        }
        else if (sp == 8)
        {
            // 1 - 2 sp skill AI
            skillRoll = UnityEngine.Random.Range(0, 100);
            if (skillRoll < 0)
            {
                attackModifier = Convert.ToInt32(0.2 * Simulation.dummyPower);
                attackValue = Convert.ToInt32(UnityEngine.Random.Range(1, attackModifier) + 0.9 * Simulation.dummyPower);
                targetMethod = 1;
            }
            else if (skillRoll >= 0 && skillRoll < 45)
            {
                float skillModifier = (UnityEngine.Random.Range(0, 126) + 94);
                skillModifier /= 100;
                attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                Simulation.spDummy -= 2;
                targetMethod = 1;
            }
            else if (skillRoll >= 45 && skillRoll < 95)
            {
                float skillModifier = (UnityEngine.Random.Range(0, 132) + 99);
                skillModifier /= 100;
                attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                Simulation.spDummy -= 2;
                targetMethod = 2;
            }
            else if (skillRoll >= 95)
            {
                float skillModifier = (UnityEngine.Random.Range(0, 136) + 102);
                skillModifier /= 100;
                attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                Simulation.spDummy -= 4;
                targetMethod = 3;
            }
        }
        finalAttack = attackValue;
        return targetMethod;
    }



}
