using System.Collections;
using System.Collections.Generic;
using System;

class BossLogic
{
    //obsolete method. Keeping it in case I reuse the code
    /*public static void bossAttack()
    {
        int k;
        int attackValue = 0;
        int target;
        
        target = Logic.bossSkillSelection(Simulation.spDummy, out attackValue);

        k = Logic.targetSelection(target);

        bool blockRoll, evadeRoll, deflectRoll;

        bool critRoll = Logic.RNGroll(10f);

        int redirectCountLive = Simulation.redirectCount;
        while (redirectCountLive > 0) {//redirect loop will run only if at least one member has the rune
            for (int i = 0; i < 5; i++) {
                if (Simulation.hero[i].redirectRune && Simulation.hero[i].redirect) { //2 part condition, that they have rune and that their llast redirect roll was successful
                    Simulation.hero[i].redirect = Logic.RNGroll(25f);
                    if (!Simulation.hero[i].redirect)
                    {
                        redirectCountLive--;
                    }
                    else {
                        k = i;
                        if (redirectCountLive == 1) {//if only one member has the rune. will stop the loop to lock itself as target
                            redirectCountLive = 0;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < 5; i++) { //reset redirect rolls to true
            if (Simulation.hero[i].redirectRune) {
                Simulation.hero[i].redirect = true;
            }
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
                    attackValue = Convert.ToInt32(0.5 * attackValue);
                    //Console.WriteLine("\n block successful!\n");
                    if (Simulation.hero[k].shield > 0) {
                        if (attackValue > Simulation.hero[k].shield)
                        {
                            attackValue -= Simulation.hero[k].shield;
                            Simulation.hero[k].shield = 0;
                        }
                        else {
                            Simulation.hero[k].shield -= attackValue;
                            attackValue = 0;
                        }
                    }
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
                else
                {
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
    */

    public static void bossDamageApplication(int k, int attackValue)
    {
        k = Logic.redirectSelection(k);
        int scenario = Logic.defensiveProcCase(k);
        switch (scenario)
        {
            case 0:
                Logic.heroAbsorb(attackValue, k);
                break;
            case 1:
                Logic.heroDeflect(attackValue, k);
                break;
            case 2:
                //evade do nothing
                break;
            case 3:
                Logic.heroBlock(attackValue, k);
                break;
            default:
                Logic.heroNormal(attackValue, k);
                break;
        }
    }

    public static void woodbeardAI() {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int attackValue = 0;
        int skillRoll = 0;
        int target = 0;
        int range = 0;
        bool critRoll = Logic.RNGroll(10f);
        //int targetMethod = 0;
        if (Simulation.spDummy < 2)
        {
            target = Logic.targetSelection(1);
            attackValue = SkillList.normalAttack(Simulation.dummyPower);
            if (critRoll)
            {
                attackValue = Convert.ToInt32(attackValue * 1.5);
            }
            bossDamageApplication(target, attackValue);

        }
        else if (Simulation.spDummy < 4)
        {
            switch (Simulation.aliveCount)
            {
                case 1:
                    range = 25;
                    break;
                case 2:
                    range = 60;
                    break;
                default:
                    range = 90;
                    break;
            }
            // 1 sp skill AI
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                target = Logic.targetSelection(1);
                attackValue = SkillList.normalAttack(Simulation.dummyPower);
                if (critRoll) {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                bossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                Simulation.spDummy -= 2;
                target = Logic.targetSelection(1);
                attackValue = SkillList.wbClosest1sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                bossDamageApplication(target, attackValue);
            }
            else if (skillRoll >=25 && skillRoll < 60)
            {
                Simulation.spDummy -= 2;
                target = Logic.targetSelection(1);
                //UnityEngine.Debug.Log("aliveCount :" + Simulation.aliveCount);
                //UnityEngine.Debug.Log("target is :" + target);
                for (int i = 0; i < 2; i++)
                {
                    attackValue = SkillList.wbPierce2_1sp(Simulation.dummyPower);
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                    if (Simulation.hero[target + i].alive)
                    {
                        bossDamageApplication(target + i, attackValue);
                    }
                }   
            }
            else if (skillRoll >= 60 )
            {
                Simulation.spDummy -= 2;
                Simulation.dummyDrain = true;
                for (int i = 0; i < 5; i++) {
                    attackValue = SkillList.wbAOEDrain1sp(Simulation.dummyPower);
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                    if (Simulation.hero[i].alive)
                    {
                        bossDamageApplication(i, attackValue);
                    }
                }
                Simulation.dummyDrain = false;
            }
        }
        else if (Simulation.spDummy >= 4)
        {
            switch (Simulation.aliveCount)
            {
                case 1:
                    range = 35;
                    break;
                case 2:
                    range = 70;
                    break;
                default:
                    range = 100;
                    break;
            }
            // 1 - 2 sp skill AI
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                target = Logic.targetSelection(1);
                attackValue = SkillList.normalAttack(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                bossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                Simulation.spDummy -= 2;
                target = Logic.targetSelection(1);
                attackValue = SkillList.wbClosest1sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                bossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 25 && skillRoll < 35)
            {
                target = Logic.targetSelection(3);
                Simulation.dummySelfInjure = true;
                attackValue = SkillList.wbTarget2sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                bossDamageApplication(target, attackValue);
                Simulation.dummySelfInjure = false;
            }
            else if (skillRoll >= 35 && skillRoll < 60)
            {
                Simulation.spDummy -= 2;
                target = Logic.targetSelection(1);
                for (int i = 0; i < 2; i++)
                {
                    attackValue = SkillList.wbPierce2_1sp(Simulation.dummyPower);
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                    if (Simulation.hero[target + i].alive)
                    {
                        bossDamageApplication(target + i, attackValue);
                    }
                }
            }
            else if (skillRoll >= 60 && skillRoll < 90)
            {
                Simulation.spDummy -= 2;
                Simulation.dummyDrain = true;
                for (int i = 0; i < 5; i++)
                {
                    attackValue = SkillList.wbAOEDrain1sp(Simulation.dummyPower);
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                    if (Simulation.hero[i].alive)
                    {
                        bossDamageApplication(i, attackValue);
                    }
                }
                Simulation.dummyDrain = false;
            }
            
        }
    }
    public static void kaleidoAI()
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int attackValue = 0;
        int skillRoll = 0;
        int target = 0;
        bool critRoll = Logic.RNGroll(10f);
        if (Simulation.spDummy < 2)
        {
            target = Logic.targetSelection(1);
            attackValue = SkillList.normalAttack(Simulation.dummyPower);
            if (critRoll)
            {
                attackValue = Convert.ToInt32(attackValue * 1.5);
            }
            bossDamageApplication(target, attackValue);

        }
        else if (Simulation.spDummy < 4)
        {
            skillRoll = rnd.Next(0, 50);
            if (skillRoll < 10)
            {
                target = Logic.targetSelection(1);
                attackValue = SkillList.normalAttack(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                bossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 10 && skillRoll < 30)
            {
                Simulation.spDummy -= 2;
                target = Logic.targetSelection(1);
                attackValue = SkillList.klCLosest1sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                bossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 30 && skillRoll < 50)
            {
                Simulation.spDummy -= 2;
                target = Logic.targetSelection(2);
                attackValue = SkillList.klBack1sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                bossDamageApplication(target, attackValue);
            }
        }
        else if (Simulation.spDummy >= 4)
        {
            skillRoll = rnd.Next(0, 100);
            if (skillRoll < 10)
            {
                target = Logic.targetSelection(1);
                attackValue = SkillList.normalAttack(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                bossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 10 && skillRoll < 30)
            {
                Simulation.spDummy -= 2;
                target = Logic.targetSelection(1);
                attackValue = SkillList.klCLosest1sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                bossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 30 && skillRoll < 50)
            {
                Simulation.spDummy -= 2;
                target = Logic.targetSelection(2);
                attackValue = SkillList.klBack1sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                bossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 50 && skillRoll < 75)
            {
                Simulation.spDummy -= 2;
                target = Logic.targetSelection(3);
                attackValue = SkillList.klTarget2sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                bossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 75)
            {
                SkillList.klHeal2sp(Simulation.dummyPower);
            }
        }

    }
}




/* public static void bossDamageApplication(int k, int attackValue)
    {
        bool blockRoll, evadeRoll, deflectRoll, absorbRoll;

        int redirectCountLive = Simulation.redirectCount;
        while (redirectCountLive > 0)
        {//redirect loop will run only if at least one member has the rune
            for (int i = 0; i < 5; i++)
            {
                if (Simulation.hero[i].redirectRune && Simulation.hero[i].redirect)
                { //2 part condition, that they have rune and that their llast redirect roll was successful
                    Simulation.hero[i].redirect = Logic.RNGroll(25f);
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
            if (Simulation.hero[i].redirectRune)
            {
                Simulation.hero[i].redirect = true;
            }
        }
        int scenario = Logic.defensiveProcCase(k);
   
        switch (scenario) {
            case 0:
                Logic.heroAbsorb(attackValue, k);
                break;
            case 1:
                Logic.heroDeflect(attackValue, k);
                break;
            case 3:
                Logic.heroNormal(attackValue, k);
                break;
            default:
                Logic.heroNormal(attackValue, k);
                break;
        }

        absorbRoll = Logic.RNGroll(Simulation.hero[k].absorbChance);  //following IFs statements to take account of defensive stats of Simulation.hero
        if (!absorbRoll)
        {
            deflectRoll = Logic.RNGroll(Simulation.hero[k].deflectChance); 
            if (!deflectRoll)
            {
                evadeRoll = Logic.RNGroll(Simulation.hero[k].evadeChance);
                if (!evadeRoll)
                {
                    blockRoll = Logic.RNGroll(Simulation.hero[k].blockChance);
                    if (blockRoll)
                    {
                        attackValue = Convert.ToInt32(0.5 * attackValue);
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
                        }
                        else
                        {
                            PetLogic.petSelection(k);
                        }
                    }
                    else
                    {
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
                        }
                        else
                        {
                            PetLogic.petSelection(k);
                        }
                    }
                }
            }
            else
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
        }
        else {
            Simulation.hero[k].shield += attackValue;
            if (Simulation.hero[k].shield > Simulation.hero[k].maxShield) {
                Simulation.hero[k].shield = Simulation.hero[k].maxShield;
            }
        }
    }
*/
