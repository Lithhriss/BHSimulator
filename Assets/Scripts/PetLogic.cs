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

    // strcmp to find what pet Simulation.hero is using
    public static void petSelection(int k)
    {
        /*bool petCheck;
        petCheck = string.Equals(Simulation.hero[k].pets, "gemmi");
        if (petCheck)
        {
            Logic.teamHeal(k);
        }
        petCheck = string.Equals(Simulation.hero[k].pets, "nelson");
        if (petCheck)
        {
            offPetProc(k);
        }
        petCheck = string.Equals(Simulation.hero[k].pets, "boogie");
        if (petCheck)
        {
            spreadHealPet(k);
        }
        petCheck = string.Equals(Simulation.hero[k].pets, "nemo");
        if (petCheck)
        {
            superOffPetProc(k);
        }*/


        if ((int)Simulation.hero[k].pet == 1)
        {
            offPetProc(k);
        }
        if ((int)Simulation.hero[k].pet == 2)
        {
            Logic.teamHeal(k);
        }
        if ((int)Simulation.hero[k].pet == 3)
        {
            spreadHealPet(k);
        }
        if ((int)Simulation.hero[k].pet == 4)
        {
            superOffPetProc(k);
        }

    }


}