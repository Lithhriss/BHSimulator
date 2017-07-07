using System.Collections;
using System.Collections.Generic;
using System;

class BossLogic
{
    public static void bossAttack()
    {
        int k;
        int attackValue = 0;
        int target;
        target = Logic.bossSkillSelection(Simulation.spDummy, out attackValue);

        k = Logic.targetSelection(target);

        bool blockRoll, evadeRoll, deflectRoll, redirectRoll;

        bool critRoll = Logic.RNGroll(10f);
        redirectRoll = Logic.RNGroll(25f);
        if (redirectRoll)
        {
            //Console.WriteLine("\nredirect successful!\n");
            k = 4;
        }
        if (critRoll)
        {
            attackValue = Convert.ToInt32(attackValue * 1.5f);
        }
        deflectRoll = Logic.RNGroll(Simulation.hero[k].deflectChance); //following IFs statements to take account of defensive stats of Simulation.hero
        if (!deflectRoll)
        {
            evadeRoll = Logic.RNGroll(Simulation.hero[k].evadeChance);
            if (!evadeRoll)
            {
                blockRoll = Logic.RNGroll(Simulation.hero[k].blockChance);
                if (blockRoll)
                {
                    //Console.WriteLine("\n block successful!\n");
                    Simulation.hero[k].hp -= Convert.ToInt32(0.5 * attackValue);
                    if (Simulation.hero[k].hp <= 0)
                    {
                        Simulation.hero[k].alive = false;
                    }
                    else
                    {
                        PetLogic.petSelection(k);
                    }
                }
                else
                {
                    Simulation.hero[k].hp -= attackValue;
                    if (Simulation.hero[k].hp <= 0)
                    {
                        Simulation.hero[k].alive = false;
                    }
                    else
                    {
                        PetLogic.petSelection(k);
                    }
                }
            }
            else
            { //Console.WriteLine("\n evade successful!\n"); 
            }
        }
        else
        {
            //Console.WriteLine("\n deflect successful!\n");
            Simulation.hpDummy -= attackValue;

        }
    }


}
