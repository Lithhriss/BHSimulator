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
        if (RaidSimulation.spDummy < 2)
        {
            target = Logic.TargetSelection(1);
            attackValue = SkillList.NormalAttack(RaidSimulation.dummyPower);
            if (critRoll)
            {
                attackValue = Convert.ToInt32(attackValue * 1.5);
            }
            BossDamageApplication(target, attackValue);

        }
        else if (RaidSimulation.spDummy < 4)
        {
            
            switch (RaidSimulation.aliveCount)
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
                attackValue = SkillList.NormalAttack(RaidSimulation.dummyPower);
                if (critRoll) {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                BossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                RaidSimulation.spDummy -= 2;
                target = Logic.TargetSelection(1);
                attackValue = SkillList.WbClosest1sp(RaidSimulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                BossDamageApplication(target, attackValue);
            }
            else if (skillRoll >=25 && skillRoll < 60)
            {
                //UnityEngine.Debug.Log("pierce entered");
                RaidSimulation.spDummy -= 2;
                target = Logic.TargetSelection(1);
                for (int i = 0; i < 2; i++)
                {
                    attackValue = SkillList.WbPierce2_1sp(RaidSimulation.dummyPower);
                    
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                    if (RaidSimulation.hero[target + i].alive)
                    {
                        BossDamageApplication(target + i, attackValue);
                    }
                }   
            }
            else if (skillRoll >= 60 )
            {
                RaidSimulation.spDummy -= 2;
                RaidSimulation.dummyDrain = true;
                for (int i = 0; i < 5; i++) {
                    attackValue = SkillList.WbAOEDrain1sp(RaidSimulation.dummyPower);
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                    if (RaidSimulation.hero[i].alive)
                    {
                        BossDamageApplication(i, attackValue);
                    }
                }
                RaidSimulation.dummyDrain = false;
            }
        }
        else if (RaidSimulation.spDummy >= 4)
        {
            switch (RaidSimulation.aliveCount)
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
                attackValue = SkillList.NormalAttack(RaidSimulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                BossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 10 && skillRoll < 25)
            {
                RaidSimulation.spDummy -= 2;
                target = Logic.TargetSelection(1);
                attackValue = SkillList.WbClosest1sp(RaidSimulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                BossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 25 && skillRoll < 35)
            {
                target = Logic.TargetSelection(3);
                RaidSimulation.dummySelfInjure = true;
                attackValue = SkillList.WbTarget2sp(RaidSimulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                BossDamageApplication(target, attackValue);
                RaidSimulation.dummySelfInjure = false;
            }
            else if (skillRoll >= 35 && skillRoll < 60)
            {
                RaidSimulation.spDummy -= 2;
                target = Logic.TargetSelection(1);
                for (int i = 0; i < 2; i++)
                {
                    attackValue = SkillList.WbPierce2_1sp(RaidSimulation.dummyPower);
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                    if (RaidSimulation.hero[target + i].alive)
                    {
                        BossDamageApplication(target + i, attackValue);
                    }
                }
            }
            else if (skillRoll >= 60 && skillRoll < 90)
            {
                RaidSimulation.spDummy -= 2;
                RaidSimulation.dummyDrain = true;
                for (int i = 0; i < 5; i++)
                {
                    attackValue = SkillList.WbAOEDrain1sp(RaidSimulation.dummyPower);
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                    if (RaidSimulation.hero[i].alive)
                    {
                        BossDamageApplication(i, attackValue);
                    }
                }
                RaidSimulation.dummyDrain = false;
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
        if (RaidSimulation.spDummy < 2)
        {
            target = Logic.TargetSelection(1);
            attackValue = SkillList.NormalAttack(RaidSimulation.dummyPower);
            if (critRoll)
            {
                attackValue = Convert.ToInt32(attackValue * 1.5);
            }
        }
        else if (RaidSimulation.spDummy < 4)
        {
            skillRoll = rnd.Next(0, 50);
            if (skillRoll < 10)
            {
                target = Logic.TargetSelection(1);
                attackValue = SkillList.NormalAttack(RaidSimulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
            }
            else if (skillRoll >= 10 && skillRoll < 30)
            {
                RaidSimulation.spDummy -= 2;
                target = Logic.TargetSelection(1);
                attackValue = SkillList.KlCLosest1sp(RaidSimulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
            }
            else if (skillRoll >= 30 && skillRoll < 50)
            {
                RaidSimulation.spDummy -= 2;
                target = Logic.TargetSelection(2);
                attackValue = SkillList.KlBack1sp(RaidSimulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
            }
        }
        else if (RaidSimulation.spDummy >= 4)
        {
            skillRoll = rnd.Next(0, 100);
            if (skillRoll < 10)
            {
                target = Logic.TargetSelection(1);
                attackValue = SkillList.NormalAttack(RaidSimulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
            }
            else if (skillRoll >= 10 && skillRoll < 30)
            {
                RaidSimulation.spDummy -= 2;
                target = Logic.TargetSelection(1);
                attackValue = SkillList.KlCLosest1sp(RaidSimulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
            }
            else if (skillRoll >= 30 && skillRoll < 50)
            {
                RaidSimulation.spDummy -= 2;
                target = Logic.TargetSelection(2);
                attackValue = SkillList.KlBack1sp(RaidSimulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
            }
            else if (skillRoll >= 50 && skillRoll < 75)
            {
                RaidSimulation.spDummy -= 2;
                target = Logic.TargetSelection(3);
                attackValue = SkillList.KlTarget2sp(RaidSimulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
            }
            else if (skillRoll >= 75)
            {
                SkillList.KlHeal2sp(RaidSimulation.dummyPower);
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
        if (RaidSimulation.spDummy < 2)
        {
            target = Logic.TargetSelection(1);
            attackValue = SkillList.NormalAttack(RaidSimulation.dummyPower);
            if (critRoll)
            {
                attackValue = Convert.ToInt32(attackValue * 1.5);
            }
            BossDamageApplication(target, attackValue);
        }
        else if (RaidSimulation.spDummy < 4)
        {

            switch (RaidSimulation.aliveCount)
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
                attackValue = SkillList.NormalAttack(RaidSimulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                BossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 10 && skillRoll < 15)
            {
                RaidSimulation.spDummy -= 2;
                target = Logic.TargetSelection(1);
                int target2 = Logic.TargetSelection(3);
                int target3 = Logic.TargetSelection(3);

                attackValue = SkillList.RMBackBounce1sp(RaidSimulation.dummyPower);
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
                RaidSimulation.spDummy -= 2;

                for (int i = 0; i < 5; i++)
                {
                    attackValue = SkillList.RMAOE1sp(RaidSimulation.dummyPower);
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                    if (RaidSimulation.hero[i].alive)
                    {
                        BossDamageApplication(i, attackValue);
                    }
                }
            }
        }
        else if (RaidSimulation.spDummy >= 4)
        {
            switch (RaidSimulation.aliveCount)
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
                attackValue = SkillList.NormalAttack(RaidSimulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                BossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 10 && skillRoll < 60)
            {
                RaidSimulation.spDummy -= 4;
                target = Logic.FindLowestHealth();
                    attackValue = SkillList.RMWeakest2sp(RaidSimulation.dummyPower);
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                        BossDamageApplication(target, attackValue);
            }
            else if (skillRoll >= 60 && skillRoll < 90)
            {
                RaidSimulation.spDummy -= 4;
                
                attackValue = SkillList.RMHealTeam2sp(RaidSimulation.dummyPower);
                if (critRoll)
                {
                    attackValue = Convert.ToInt32(attackValue * 1.5);
                }
                RaidSimulation.hpDummy += attackValue;
            }
            else if (skillRoll >= 90 && skillRoll < 95)
            {
                RaidSimulation.spDummy -= 2;
                target = Logic.TargetSelection(1);
                int target2 = Logic.TargetSelection(3);
                int target3 = Logic.TargetSelection(3);

                attackValue = SkillList.RMBackBounce1sp(RaidSimulation.dummyPower);
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
                RaidSimulation.spDummy -= 2;

                for (int i = 0; i < 5; i++)
                {
                    attackValue = SkillList.RMAOE1sp(RaidSimulation.dummyPower);
                    if (critRoll)
                    {
                        attackValue = Convert.ToInt32(attackValue * 1.5);
                    }
                    if (RaidSimulation.hero[i].alive)
                    {
                        BossDamageApplication(i, attackValue);
                    }
                }
            }
        }
    }


    //public static void BossDamageApplication(Enemy enemy, Enemy[] enemies,Character[] heroes, int attackValue, Character hero)
    //{
    //    hero = Logic.RedirectSelection(hero, heroes);
    //    int scenario = Logic.DefensiveProcCase(hero);
    //    switch (scenario)
    //    {
    //        case 0:
    //            Logic.HeroAbsorb(attackValue, hero);
    //            if (hero.alive)
    //                PetLogic.PetSelection(hero, heroes, enemies);
    //            break;
    //        case 1:
    //            Logic.HeroDeflect(attackValue, hero, enemy);
    //            break;
    //        case 2:
    //            //evade do nothing
    //            PetLogic.PetSelection(hero, heroes, enemies);
    //            break;
    //        case 3:
    //            Logic.HeroBlock(attackValue, hero, enemy);
    //            if (hero.alive)
    //            PetLogic.PetSelection(hero, heroes, enemies);
    //            break;
    //        default:
    //            Logic.HeroNoProc(attackValue, hero, enemy);
    //            if (hero.alive)
    //                PetLogic.PetSelection(hero, heroes, enemies);
    //            break;
    //    }
    //}

    public static void PickMobAI(Enemy enemy, Enemy[] enemies, Character[] heroes)
    {
        switch (enemy.name)
        {
            case "BlueOrc":
                break;
            case "GreenOrc":
                break;
            case "PurpleOrc":
                break;
            case "ArcherOrc":
                break;
            case "AssassinOrc":
                break;
            case "BruiserOrc":
                break;
            case "MeatOrc":
                break;
            case "MageOrc":
                break;
            case "BlueNether":
                break;
            case "PurpleNether":
                break;
            case "YellowNether":
                break;
            case "ImpNether":
                break;
            case "MageNether":
                break;
            case "BeastNether":
                break;
            case "TankNether":
                break;
            case "DemonNether":
                break;
        }
    }

}