using System;

public class SkillList
{
    public static int attackModifier;
    public static int attackValue;
    private static Random rnd = new Random(Guid.NewGuid().GetHashCode());

    //0 sp attack
    public static int NormalAttack(int power)
    {

        
        attackModifier = Convert.ToInt32(0.2 * power);
        attackValue = Convert.ToInt32(rnd.Next(1, attackModifier) + 0.9 * power);
        return attackValue;
    }


	#region Spear skill set


    public static int SpBack1sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(0.6 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.3 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
		if (Logic.RNGroll (empowerChance)) {
			attackValue *= 2;
		}
        return attackValue;
    }
    public static int SpPierce3_1sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(0.3 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.7 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int SpTarget2sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(0.73 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.46 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int SpClosest3sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(1.07 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 2.12 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
	#endregion
    
	#region Bow skill set


    public static int BTarget1sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(0.545 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.095 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int BAoe1sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(0.25 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.48 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int BBack2sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(0.85 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.70 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int BAoeDraint3sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(0.2 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.4 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
	#endregion
    
	#region Sword skill set

    public static int SwTarget_1sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(0.27 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.23 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int SwPierce3_1sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(0.16 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.74 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int SwAoe2sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(0.16 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.73 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int SwClosest3sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(0.25 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.14 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
	#endregion
    
	#region Axe skill set

    public static int AAoeDrain1sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(0.18 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.21 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int AClosest_1sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(0.96 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.11 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int ATarget2sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(1.095 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.28 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
	#endregion

	#region Staff skill set

    public static int StClosest1sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(0.31 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.41 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static void StHeal1sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        int k = Logic.HealLogic(); ;
        int healModifier = Convert.ToInt32(0.15 * power);
        int healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 0.675 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            healValue = Convert.ToInt32(healValue * critDamage);
        }
        RaidSimulation.hero[k].hp += healValue;
    }
    public static int StAOE2sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(1.095 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.28 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int StTarget3sp(int power, float critChance, float critDamage, float empowerChance)
    {
        
        attackModifier = Convert.ToInt32(0.45 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 2.02 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
	#endregion

	#region Woodbeard skill set
    public static int WbClosest1sp(int power)
    {
        
        attackModifier = Convert.ToInt32(0.95 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.10 * power);
        return attackValue;
    }
    public static int WbPierce2_1sp(int power)
    {
        
        attackModifier = Convert.ToInt32(0.58 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.68 * power);
        return attackValue;
    }
    public static int WbAOEDrain1sp(int power)
    {
        
        attackModifier = Convert.ToInt32(0.18 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.21 * power);
        return attackValue;
    }
    public static int WbTarget2sp(int power)
    {
        
        attackModifier = Convert.ToInt32(0.95 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.10 * power);
        return attackValue;
    }
	#endregion
    
	#region Kaleido skill set
    public static int KlCLosest1sp(int power)
    {
        
        attackModifier = Convert.ToInt32(1.26 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.94 * power);
        return attackValue;
    }
    public static int KlBack1sp(int power)
    {
        
        attackModifier = Convert.ToInt32(1.32 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.99 * power);
        return attackValue;

    }
    public static int KlTarget2sp(int power)
    {
        
        attackModifier = Convert.ToInt32(1.36 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.02 * power);
        return attackValue;

    }
    public static void KlHeal2sp(int power)
    {
        
        int healModifier = Convert.ToInt32(0.80 * power);
        int healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 0.60 * power);
        bool critroll = Logic.RNGroll(10f);
        if (critroll)
        {
            healValue = Convert.ToInt32(healValue * 1.5f);
        }
        RaidSimulation.hpDummy += healValue;
    }
    #endregion

    #region Roboss set
    public static int RMBackBounce1sp(int power)
    {
        
        attackModifier = Convert.ToInt32(0.4 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.32 * power);
        return attackValue;
    }

    public static int RMAOE1sp(int power)
    {
        
        attackModifier = Convert.ToInt32(0.4 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.4 * power);
        return attackValue;
    }

    public static int RMWeakest2sp(int power)
    {
        
        attackModifier = Convert.ToInt32(0.4 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1 * power);
        return attackValue;
    }

    public static int RMHealTeam2sp(int power)
    {
        
        attackModifier = Convert.ToInt32(0.4 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.4 * power);
        return attackValue;
    }
    #endregion
    #region New Code
    #region Spear skill set


    public static int SpBack1sp(Character hero)
    {

        attackModifier = Convert.ToInt32(0.6 *  hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.3 *  hero.power);
        bool critroll = Logic.RNGroll( hero.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int SpPierce3_1sp(Character hero)
    {

        attackModifier = Convert.ToInt32(0.3 * hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.7 * hero.power);
        //bool critroll = Logic.RNGroll( hero.critChance);
        //if (critroll)
        //{
        //    attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        //}
        //if (Logic.RNGroll(hero.empowerChance))
        //{
        //    attackValue *= 2;
        //}
        return attackValue;
    }
    public static int SpTarget2sp(Character hero)
    {

        attackModifier = Convert.ToInt32(0.73 * hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.46 * hero.power);
        bool critroll = Logic.RNGroll( hero.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int SpClosest3sp(Character hero)
    {

        attackModifier = Convert.ToInt32(1.07 * hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 2.12 * hero.power);
        bool critroll = Logic.RNGroll( hero.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    #endregion

    #region Bow skill set


    public static int BTarget1sp(Character hero)
    {

        attackModifier = Convert.ToInt32(0.545 * hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.095 * hero.power);
        bool critroll = Logic.RNGroll( hero.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int BAoe1sp(Character hero)
    {

        attackModifier = Convert.ToInt32(0.25 * hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.48 * hero.power);
        //bool critroll = Logic.RNGroll( hero.critChance);
        //if (critroll)
        //{
        //    attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        //}
        //if (Logic.RNGroll(hero.empowerChance))
        //{
        //    attackValue *= 2;
        //}
        return attackValue;
    }
    public static int BBack2sp(Character hero)
    {

        attackModifier = Convert.ToInt32(0.85 * hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.70 * hero.power);
        bool critroll = Logic.RNGroll( hero.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int BAoeDraint3sp(Character hero)
    {

        attackModifier = Convert.ToInt32(0.2 * hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.4 * hero.power);
        //bool critroll = Logic.RNGroll( hero.critChance);
        //if (critroll)
        //{
        //    attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        //}
        //if (Logic.RNGroll(hero.empowerChance))
        //{
        //    attackValue *= 2;
        //}
        return attackValue;
    }
    #endregion

    #region Sword skill set

    public static int SwTarget_1sp(Character hero)
    {

        attackModifier = Convert.ToInt32(0.27 * hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.23 * hero.power);
        bool critroll = Logic.RNGroll( hero.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int SwPierce3_1sp(Character hero)
    {

        attackModifier = Convert.ToInt32(0.16 * hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.74 * hero.power);
        //bool critroll = Logic.RNGroll( hero.critChance);
        //if (critroll)
        //{
        //    attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        //}
        //if (Logic.RNGroll(hero.empowerChance))
        //{
        //    attackValue *= 2;
        //}
        return attackValue;
    }
    public static int SwAoe2sp(Character hero)
    {

        attackModifier = Convert.ToInt32(0.16 * hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.73 * hero.power);
        //bool critroll = Logic.RNGroll( hero.critChance);
        //if (critroll)
        //{
        //    attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        //}
        //if (Logic.RNGroll(hero.empowerChance))
        //{
        //    attackValue *= 2;
        //}
        return attackValue;
    }
    public static int SwClosest3sp(Character hero)
    {

        attackModifier = Convert.ToInt32(0.25 * hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.14 * hero.power);
        bool critroll = Logic.RNGroll( hero.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    #endregion
    #region Axe skill set

    public static int AAoeDrain1sp(Character hero)
    {

        attackModifier = Convert.ToInt32(0.18 * hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.21 * hero.power);
        //bool critroll = Logic.RNGroll( hero.critChance);
        //if (critroll)
        //{
        //    attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        //}
        //if (Logic.RNGroll(hero.empowerChance))
        //{
        //    attackValue *= 2;
        //}
        return attackValue;
    }
    public static int AClosest_1sp(Character hero)
    {

        attackModifier = Convert.ToInt32(0.96 * hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.11 * hero.power);
        bool critroll = Logic.RNGroll( hero.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int ATarget2sp(Character hero)
    {

        attackModifier = Convert.ToInt32(1.095 * hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.28 * hero.power);
        bool critroll = Logic.RNGroll( hero.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int ASpreadHeal3sp(Character hero)
    {
        int healModifier = Convert.ToInt32(0.2 * hero.power);
        int healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 1.4 * hero.power);
        bool critroll = Logic.RNGroll(hero.critChance);
        if (critroll)
        {
            healValue = Convert.ToInt32(healValue * hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            healValue *= 2;
        }
        return healValue;
    }
    #endregion
    #region Staff skill set

    public static int StClosest1sp(Character hero)
    {

        attackModifier = Convert.ToInt32(0.31 * hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.41 * hero.power);
        bool critroll = Logic.RNGroll( hero.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int StHeal1sp(Character hero)
    {
        int healModifier = Convert.ToInt32(0.15 * hero.power);
        int healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 0.675 * hero.power);
        bool critroll = Logic.RNGroll( hero.critChance);
        if (critroll)
        {
            healValue = Convert.ToInt32(healValue *  hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            healValue *= 2;
        }
        return healValue;

    }
    public static int StAOE2sp(Character hero)
    {

        attackModifier = Convert.ToInt32(1.095 * hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.28 * hero.power);
        //bool critroll = Logic.RNGroll( hero.critChance);
        //if (critroll)
        //{
        //    attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        //}
        //if (Logic.RNGroll(hero.empowerChance))
        //{
        //    attackValue *= 2;
        //}
        return attackValue;
    }
    public static int StTarget3sp(Character hero)
    {

        attackModifier = Convert.ToInt32(0.45 * hero.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 2.02 * hero.power);
        bool critroll = Logic.RNGroll( hero.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue *  hero.critDamage);
        }
                if (Logic.RNGroll(hero.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    #endregion
    #region Orlag Fams
    #region Blue Orc
    public static int BlueOrcAOEDrain_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.2 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.2 * enemy.power);
        return attackValue;
    }
    public static int BlueOrcSelfHeal_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.78 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.73 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int BlueOrcAOE_2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.56 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.52 * enemy.power);
        return attackValue;
    }
    public static int BlueOrcWeakest_2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.23 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.23 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    #endregion
    #region Green Orc
    public static int GreenOrcHeal1sp(Enemy enemy)
    {
        int healModifier = Convert.ToInt32(0.5 * enemy.power);
        int healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 0.574 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            healValue = Convert.ToInt32(healValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            healValue *= 2;
        }
        return healValue;

    }
    public static int GreenOrcWeakest1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.846 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int GreenOrcAOE_2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.48 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.56 * enemy.power);
        return attackValue;
    }
    public static int GreenOrcTarget2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.08 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.26 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    #endregion
    #region Purple Orc
    public static int PurpleOrcTarget1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.08 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.81 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int PurpleOrcFurthest1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.26 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.94 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int PurpleOrcAOE_2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.64 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.48 * enemy.power);
        return attackValue;
    }
    public static int PurpleOrcHeal2sp(Enemy enemy)
    {
        int healModifier = Convert.ToInt32(1.08 * enemy.power);
        int healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 1.26 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            healValue = Convert.ToInt32(healValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            healValue *= 2;
        }
        return healValue;
    }
    #endregion
    #region Meat Orc
    public static int MeatOrcDrain_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.6 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.45 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int MeatOrcSelfHeal_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.9 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.67 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int MeatOrcSpread_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.72 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.54 * enemy.power);
        return attackValue;
    }
    public static int MeatOrcHealAll_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.24 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.18 * enemy.power);

        return attackValue;
    }
    #endregion
    #region Bruiser Orc
    public static int BruiserOrcDrain_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.45 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.52 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int BruiserOrcTarget_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.81 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.94 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int BruiserOrcPierce2_2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.78 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.91 * enemy.power);
        return attackValue;
    }
    #endregion
    #region Mage Orc
    public static int MageOrcSpread_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.36 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.72 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int MageOrcTarget_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.54 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.08 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int MageOrcWeakest_2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.76 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.52 * enemy.power);
        return attackValue;
    }
    #endregion
    #region Archer Orc
    public static int ArcherOrcTarget_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.54 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.08 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int ArcherOrcWeakest_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.57 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.13 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    #endregion
    #region Assassin Orc
    public static int AssassinOrcDrain_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.54 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.45 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int AssassinOrcWeakest_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.13 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.85 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    #endregion
    #endregion
    #region Nether Fams
    #region Blue Nether
    public static int BlueNetherDrain_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.45 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.52 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int BlueNetherFurthest_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.95 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.09 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int BlueNetherSelfHeal_2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.9 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.05 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int BlueNetherAOEDrain_2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.24 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.28 * enemy.power);
        return attackValue;
    }
    #endregion
    #region Purple Nether
    public static int PurpleNetherTargetAndSelf_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.44 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.08 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int PurpleNetherHeal_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.65 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.49 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int PurpleNetherWeakest_2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.52 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.14 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int PurpleNetherRandom_2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(2.08 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.56 * enemy.power);
        return attackValue;
    }
    #endregion
    #region Yellow Nether
    public static int YellowNetherRandom_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.95 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.975 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int YellowNetherFurthest_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.57 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.78 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int YellowNetherSpread_2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.2 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.6 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int YellowNetherAOE_2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.8 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.4 * enemy.power);
        return attackValue;
    }
    #endregion
    #region Demon Nether
    public static int DemonNetherTarget_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.08 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.81 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int DemonNetherRandom_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.56 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.17 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int DemonNetherDrain_2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.8 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.6 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int DemonNetherAOEHeal_2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.32 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.24 * enemy.power);
        return attackValue;
    }
    #endregion
    #region Imp Nether
    public static int ImpNetherSpreadHeal_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.72 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.54 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int ImpNetherRandom_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.56 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.17 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int ImpNetherPierce2_2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.04 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.78 * enemy.power);
        return attackValue;
    }

    #endregion
    #region Mage Nether
    public static int MageNetherHeal_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.65 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.49 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int MageNetherWeakest_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.13 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.85 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int MageNetherTarget_2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.44 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.08 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }

    #endregion
    #region Beast Nether
    public static int BeastNetherRandom_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.17 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.36 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int BeastNetherClosest_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(1.03 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.20 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int BeastNetherDrain_2sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.6 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.70 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }

    #endregion
    #region Tank Nether
    public static int TankNetherFurthest_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.63 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.25 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    public static int TankNetherClosest_1sp(Enemy enemy)
    {
        attackModifier = Convert.ToInt32(0.91 * enemy.power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.83 * enemy.power);
        bool critroll = Logic.RNGroll(enemy.critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * enemy.critDamage);
        }
        if (Logic.RNGroll(enemy.empowerChance))
        {
            attackValue *= 2;
        }
        return attackValue;
    }
    #endregion
    #endregion
    #endregion
}