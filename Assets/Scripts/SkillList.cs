using System;

public class SkillList
{
    public static int attackModifier;
    public static int attackValue;


    //0 sp attack

    public static int NormalAttack(int power)
    {

        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.2 * power);
        attackValue = Convert.ToInt32(rnd.Next(1, attackModifier) + 0.9 * power);
        return attackValue;
    }


    //Spear skill set


    public static int SpBack1sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.6 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.3 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int SpPierce3_1sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.3 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.7 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int SpTarget2sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.73 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.46 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int SpClosest3sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(1.07 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 2.12 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }

    //Bow skill set


    public static int BTarget1sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.545 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.095 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int BAoe1sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.25 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.48 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int BBack2sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.85 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.70 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int BAoeDraint3sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.2 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.4 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }

    //Sword skill set

    public static int SwTarget_1sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.27 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.23 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int SwPierce3_1sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.16 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.74 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int SwAoe2sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.16 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.73 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int SwClosest3sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.25 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.14 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }

    //Axe skill set

    public static int AAoeDrain1sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.18 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.21 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int AClosest_1sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.96 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.11 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int ATarget2sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(1.095 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.28 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }


    //Staff skill set

    public static int StClosest1sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.31 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.41 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static void StHeal1sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int k = Logic.HealLogic(); ;
        int healModifier = Convert.ToInt32(0.15 * power);
        int healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 0.675 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            healValue = Convert.ToInt32(healValue * critDamage);
        }
        Simulation.hero[k].hp += healValue;
    }
    public static int StAOE2sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(1.095 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.28 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }
    public static int StTarget3sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.45 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 2.02 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            attackValue = Convert.ToInt32(attackValue * critDamage);
        }
        return attackValue;
    }

    //Woodbeard skill set
    public static int WbClosest1sp(int power)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.95 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.10 * power);
        return attackValue;
    }
    public static int WbPierce2_1sp(int power)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.58 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.68 * power);
        return attackValue;
    }
    public static int WbAOEDrain1sp(int power)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.18 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.21 * power);
        return attackValue;
    }
    public static int WbTarget2sp(int power)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.95 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.10 * power);
        return attackValue;
    }

    //Kaleido skill set
    public static int KlCLosest1sp(int power)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(1.26 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.94 * power);
        return attackValue;
    }
    public static int KlBack1sp(int power)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(1.32 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.99 * power);
        return attackValue;

    }
    public static int KlTarget2sp(int power)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(1.36 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.02 * power);
        return attackValue;

    }
    public static void KlHeal2sp(int power)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int healModifier = Convert.ToInt32(0.80 * power);
        int healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 0.60 * power);
        bool critroll = Logic.RNGroll(10f);
        if (critroll)
        {
            healValue = Convert.ToInt32(healValue * 1.5f);
        }
        Simulation.hpDummy += healValue;
    }
}


