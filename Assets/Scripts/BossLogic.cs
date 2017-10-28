using System.Collections;
using System.Collections.Generic;
using System;

class BossLogic
{
    

    public static void BossDamageApplication(int k, int attackValue)
    {
        k = Logic.RedirectSelection(k);
        int scenario = Logic.DefensiveProcCase(k);
        switch (scenario)
        {
            case 0:
                Logic.HeroAbsorb(attackValue, k);
                break;
            case 1:
                Logic.HeroDeflect(attackValue, k);
                break;
            case 2:
                //evade do nothing
                break;
            case 3:
                Logic.HeroBlock(attackValue, k);
                break;
            default:
                Logic.HeroNormal(attackValue, k);
                break;
        }
    }

    public static void WoodbeardAI() {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int attackValue = 0;
        int skillRoll = 0;
        int target = 0;
        int range = 0;
        bool critRoll = Logic.RNGroll(10f);
        if (Simulation.spDummy < 2)
        {
            target = Logic.TargetSelection(1);
            attackValue = SkillList.NormalAttack(Simulation.dummyPower);
            if (critRoll)
            {
                attackValue = Convert.ToInt32(attackValue * 1.5);
            }
            BossDamageApplication(target, attackValue);

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
                target = Logic.TargetSelection(1);
                attackValue = SkillList.NormalAttack(Simulation.dummyPower);
                if (critRoll) {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                BossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                Simulation.spDummy -= 2;
                target = Logic.TargetSelection(1);
                attackValue = SkillList.WbClosest1sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                BossDamageApplication(target, attackValue);
            }
            else if (skillRoll >=25 && skillRoll < 60)
            {
                //UnityEngine.Debug.Log("pierce entered");
                Simulation.spDummy -= 2;
                target = Logic.TargetSelection(1);
                for (int i = 0; i < 2; i++)
                {
                    attackValue = SkillList.WbPierce2_1sp(Simulation.dummyPower);
                    
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                    if (Simulation.hero[target + i].alive)
                    {
                        BossDamageApplication(target + i, attackValue);
                    }
                }   
            }
            else if (skillRoll >= 60 )
            {
                Simulation.spDummy -= 2;
                Simulation.dummyDrain = true;
                for (int i = 0; i < 5; i++) {
                    attackValue = SkillList.WbAOEDrain1sp(Simulation.dummyPower);
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                    if (Simulation.hero[i].alive)
                    {
                        BossDamageApplication(i, attackValue);
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
                target = Logic.TargetSelection(1);
                attackValue = SkillList.NormalAttack(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                BossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                Simulation.spDummy -= 2;
                target = Logic.TargetSelection(1);
                attackValue = SkillList.WbClosest1sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                BossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 25 && skillRoll < 35)
            {
                target = Logic.TargetSelection(3);
                Simulation.dummySelfInjure = true;
                attackValue = SkillList.WbTarget2sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                BossDamageApplication(target, attackValue);
                Simulation.dummySelfInjure = false;
            }
            else if (skillRoll >= 35 && skillRoll < 60)
            {
                Simulation.spDummy -= 2;
                target = Logic.TargetSelection(1);
                for (int i = 0; i < 2; i++)
                {
                    attackValue = SkillList.WbPierce2_1sp(Simulation.dummyPower);
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                    if (Simulation.hero[target + i].alive)
                    {
                        BossDamageApplication(target + i, attackValue);
                    }
                }
            }
            else if (skillRoll >= 60 && skillRoll < 90)
            {
                Simulation.spDummy -= 2;
                Simulation.dummyDrain = true;
                for (int i = 0; i < 5; i++)
                {
                    attackValue = SkillList.WbAOEDrain1sp(Simulation.dummyPower);
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                    if (Simulation.hero[i].alive)
                    {
                        BossDamageApplication(i, attackValue);
                    }
                }
                Simulation.dummyDrain = false;
            }
            
        }
    }

    public static void KaleidoAI()
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int attackValue = 0;
        int skillRoll = 0;
        int target = 0;
        bool critRoll = Logic.RNGroll(10f);
        if (Simulation.spDummy < 2)
        {
            target = Logic.TargetSelection(1);
            attackValue = SkillList.NormalAttack(Simulation.dummyPower);
            if (critRoll)
            {
                attackValue = Convert.ToInt32(attackValue * 1.5);
            }
        }
        else if (Simulation.spDummy < 4)
        {
            skillRoll = rnd.Next(0, 50);
            if (skillRoll < 10)
            {
                target = Logic.TargetSelection(1);
                attackValue = SkillList.NormalAttack(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
            }
            else if (skillRoll >= 10 && skillRoll < 30)
            {
                Simulation.spDummy -= 2;
                target = Logic.TargetSelection(1);
                attackValue = SkillList.KlCLosest1sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
            }
            else if (skillRoll >= 30 && skillRoll < 50)
            {
                Simulation.spDummy -= 2;
                target = Logic.TargetSelection(2);
                attackValue = SkillList.KlBack1sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
            }
        }
        else if (Simulation.spDummy >= 4)
        {
            skillRoll = rnd.Next(0, 100);
            if (skillRoll < 10)
            {
                target = Logic.TargetSelection(1);
                attackValue = SkillList.NormalAttack(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
            }
            else if (skillRoll >= 10 && skillRoll < 30)
            {
                Simulation.spDummy -= 2;
                target = Logic.TargetSelection(1);
                attackValue = SkillList.KlCLosest1sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
            }
            else if (skillRoll >= 30 && skillRoll < 50)
            {
                Simulation.spDummy -= 2;
                target = Logic.TargetSelection(2);
                attackValue = SkillList.KlBack1sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
            }
            else if (skillRoll >= 50 && skillRoll < 75)
            {
                Simulation.spDummy -= 2;
                target = Logic.TargetSelection(3);
                attackValue = SkillList.KlTarget2sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
            }
            else if (skillRoll >= 75)
            {
                SkillList.KlHeal2sp(Simulation.dummyPower);
            }
        }
        BossDamageApplication(target, attackValue);
    }

    public static void RoboMechAI()
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int attackValue = 0;
        int skillRoll = 0;
        int target = 0;
        int range = 0;
        bool critRoll = Logic.RNGroll(10f);
        if (Simulation.spDummy < 2)
        {
            target = Logic.TargetSelection(1);
            attackValue = SkillList.NormalAttack(Simulation.dummyPower);
            if (critRoll)
            {
                attackValue = Convert.ToInt32(attackValue * 1.5);
            }
            BossDamageApplication(target, attackValue);
        }
        else if (Simulation.spDummy < 4)
        {

            switch (Simulation.aliveCount)
            {
                case 1:
                case 2:
                    range = 10;
                    break;
                default:
                    range = 20;
                    break;
            }
            // 1 sp skill AI
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                target = Logic.TargetSelection(1);
                attackValue = SkillList.NormalAttack(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                BossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 10 && skillRoll < 15)
            {
                Simulation.spDummy -= 2;
                target = Logic.TargetSelection(1);
                int target2 = Logic.TargetSelection(3);
                int target3 = Logic.TargetSelection(3);

                attackValue = SkillList.RMBackBounce1sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                BossDamageApplication(target, attackValue);
                BossDamageApplication(target2, attackValue);
                BossDamageApplication(target3, attackValue);
            }
            else if (skillRoll >= 15 && skillRoll < 20)
            {
                Simulation.spDummy -= 2;

                for (int i = 0; i < 5; i++)
                {
                    attackValue = SkillList.RMAOE1sp(Simulation.dummyPower);
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                    if (Simulation.hero[i].alive)
                    {
                        BossDamageApplication(i, attackValue);
                    }
                }
            }
        }
        else if (Simulation.spDummy >= 4)
        {
            switch (Simulation.aliveCount)
            {
                case 1:
                case 2:
                    range = 90;
                    break;
                default:
                    range = 100;
                    break;
            }
            skillRoll = rnd.Next(0, range);
            if (skillRoll < 10)
            {
                target = Logic.TargetSelection(1);
                attackValue = SkillList.NormalAttack(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                BossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 10 && skillRoll < 60)
            {
                Simulation.spDummy -= 4;
                target = Logic.FindLowestHealth();
                    attackValue = SkillList.RMWeakest2sp(Simulation.dummyPower);
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                        BossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 60 && skillRoll < 90)
            {
                Simulation.spDummy -= 4;
                
                attackValue = SkillList.RMHealTeam2sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                Simulation.hpDummy += attackValue;
            }
            else if (skillRoll >= 90 && skillRoll < 95)
            {
                Simulation.spDummy -= 2;
                target = Logic.TargetSelection(1);
                int target2 = Logic.TargetSelection(3);
                int target3 = Logic.TargetSelection(3);

                attackValue = SkillList.RMBackBounce1sp(Simulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                BossDamageApplication(target, attackValue);
                BossDamageApplication(target2, attackValue);
                BossDamageApplication(target3, attackValue);
            }
            else if (skillRoll >= 95 && skillRoll < 100)
            {
                Simulation.spDummy -= 2;

                for (int i = 0; i < 5; i++)
                {
                    attackValue = SkillList.RMAOE1sp(Simulation.dummyPower);
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                    if (Simulation.hero[i].alive)
                    {
                        BossDamageApplication(i, attackValue);
                    }
                }
            }
        }
    }
}