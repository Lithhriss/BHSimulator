using System.Collections;
using System.Collections.Generic;
using System;

class PetLogic
{
    
    public static void offPetProc(int l)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        //UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        int attackModifier = Convert.ToInt32(0.54 * Simulation.hero[l].power);
        int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + Simulation.hero[l].power * 0.63);


        bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll((float)20);

        if (critRoll)
        {
            attackValue = Convert.ToInt32(attackValue * Simulation.hero[l].critDamage);
        }
        if (petRoll)
        {
            Simulation.hpDummy -= attackValue;
            //Console.WriteLine("\npet proc successful\n");
        }

    }

    public static void superOffPetProc(int l)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        //UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        int attackModifier = Convert.ToInt32(Simulation.hero[l].power * 0.37);
        int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + Simulation.hero[l].power * 1.668);


        bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll((float)10);

        if (critRoll)
        {
            attackValue = Convert.ToInt32(attackValue * Simulation.hero[l].critDamage);
        }
        if (petRoll)
        {
            Simulation.hpDummy -= attackValue;
        }

    }

    public static void spreadHealPet(int l)
    {
        //UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int i;
        int target = 0;
        int healModifier = Convert.ToInt32(Simulation.hero[l].power * 0.14);
        int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.66 * Simulation.hero[l].power);

        bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll((float)20);

        if (critRoll)
        {
            healValue = Convert.ToInt32(healValue * Simulation.hero[l].critDamage);
        }
        if (petRoll)
        {
            for (i = 0; i < healValue; i++)
            {
                target = Logic.healLogic();
                Simulation.hero[target].hp++;
                if (Simulation.hero[target].hp > Simulation.hero[target].maxHp)
                {
                    Simulation.hero[target].hp = Simulation.hero[target].maxHp;
                }
            }
        }
    }

    public static void superSpreadHealPet(int l)
    {
        //UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int i;
        int target = 0;
        int healModifier = Convert.ToInt32(Simulation.hero[l].power * 0.288);
        int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 1.296 * Simulation.hero[l].power);

        bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll((float)10);

        if (critRoll)
        {
            healValue = Convert.ToInt32(healValue * Simulation.hero[l].critDamage);
        }
        if (petRoll)
        {
            for (i = 0; i < healValue; i++)
            {
                target = Logic.healLogic();
                Simulation.hero[target].hp++;
                if (Simulation.hero[target].hp > Simulation.hero[target].maxHp)
                {
                    Simulation.hero[target].hp = Simulation.hero[target].maxHp;
                }
            }
        }
    }

    public static void teamShieldPet(int l)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        //UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        int i;
        int shieldModifier = Convert.ToInt32(Simulation.hero[l].power * 0.06);
        float shieldValue = Convert.ToInt32(UnityEngine.Random.Range(0, shieldModifier) + 0.27 * Simulation.hero[l].power);

        bool critroll = Logic.RNGroll(Simulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critroll)
        {
            shieldValue *= Simulation.hero[l].critDamage;
        }
        if (petRoll)
        {
            for (i = 0; i < 5; i++)
            {
                if (Simulation.hero[i].hp > 0)
                {
                    Simulation.hero[i].shield += Convert.ToInt32(shieldValue);
                    if (Simulation.hero[i].shield >= Simulation.hero[i].maxShield)
                    {
                        Simulation.hero[i].shield = Simulation.hero[i].maxShield;
                    }
                }
            }
        }
    }

    public static void superSelfHealPet(int l)
    {
        //UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int healModifier = Convert.ToInt32(Simulation.hero[l].power * 0.454);
        int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.89 * Simulation.hero[l].power);

        bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll((float)10);

        if (critRoll)
        {
            healValue = Convert.ToInt32(healValue * Simulation.hero[l].critDamage);
        }
        if (petRoll)
        {
            Simulation.hero[l].hp += healValue;
            if (Simulation.hero[l].hp > Simulation.hero[l].maxHp)
                {
                    Simulation.hero[l].hp = Simulation.hero[l].maxHp;
               }
        }
    }

    // strcmp to find what pet Simulation.hero is using
    public static void petSelection(int k)
    {
        int pet = (int)Simulation.hero[k].pet;
        switch (pet)
        {
            case 1:
                offPetProc(k);
                break;
            case 2:
                Logic.teamHeal(k);
                break;
            case 3:
                spreadHealPet(k);
                break;
            case 4:
                superOffPetProc(k);
                break;
            case 5:
                superSpreadHealPet(k);
                break;
            case 6:
                teamShieldPet(k);
                break;
            case 7:
                superSelfHealPet(k);
                break;
            default:
                break;
        }
    }
}