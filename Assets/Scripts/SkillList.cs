using System;

public class SkillList
{
    public static int attackModifier;
    public static int attackValue;


    //0 sp attack

    public static int normalAttack(int power)
    {

        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.2 * power);
        attackValue = Convert.ToInt32(rnd.Next(1, attackModifier) + 0.9 * power);
        return attackValue;
    }


    //Spear skill set


    public static int spBack1sp(int power, float critChance, float critDamage)
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
    public static int spPierce3_1sp(int power, float critChance, float critDamage)
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
    public static int spTarget2sp(int power, float critChance, float critDamage)
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
    public static int spClosest3sp(int power, float critChance, float critDamage)
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


    public static int bTarget1sp(int power, float critChance, float critDamage)
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
    public static int bAoe1sp(int power, float critChance, float critDamage)
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
    public static int bBack2sp(int power, float critChance, float critDamage)
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
    public static int bAoeDraint3sp(int power, float critChance, float critDamage)
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

    public static int swTarget_1sp(int power, float critChance, float critDamage)
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
    public static int swPierce3_1sp(int power, float critChance, float critDamage)
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
    public static int swAoe2sp(int power, float critChance, float critDamage)
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
    public static int swClosest3sp(int power, float critChance, float critDamage)
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

    public static int aAoeDrain1sp(int power, float critChance, float critDamage)
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
    public static int aClosest_1sp(int power, float critChance, float critDamage)
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
    public static int aTarget2sp(int power, float critChance, float critDamage)
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
    public static int aSpreadHealt3sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        //add spread healing
        return 0;
    }

    //Staff skill set

    public static int stClosest1sp(int power, float critChance, float critDamage)
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
    public static void stHeal1sp(int power, float critChance, float critDamage)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int k = Logic.healLogic(); ;
        int healModifier = Convert.ToInt32(0.15 * power);
        int healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 0.675 * power);
        bool critroll = Logic.RNGroll(critChance);
        if (critroll)
        {
            healValue = Convert.ToInt32(healValue * critDamage);
        }
        Simulation.hero[k].hp += healValue;
    }
    public static int stAOE2sp(int power, float critChance, float critDamage)
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
    public static int stTarget3sp(int power, float critChance, float critDamage)
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
    public static int wbClosest1sp(int power)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.95 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.10 * power);
        bool critroll = Logic.RNGroll(10f);
        return attackValue;
    }
    public static int wbPierce2_1sp(int power)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.58 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.68 * power);
        return attackValue;
    }
    public static int wbAOEDrain1sp(int power)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.18 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.21 * power);
        return attackValue;
    }
    public static int wbTarget2sp(int power)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(0.95 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.10 * power);
        return attackValue;
    }

    //Kaleido skill set
    public static int klCLosest1sp(int power)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(1.26 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.94 * power);
        return attackValue;
    }
    public static int klBack1sp(int power)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(1.32 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.99 * power);
        return attackValue;

    }
    public static int klTarget2sp(int power)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        attackModifier = Convert.ToInt32(1.36 * power);
        attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.02 * power);
        return attackValue;

    }
    public static void klHeal2sp(int power)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int healModifier = Convert.ToInt32(0.80 * power);
        int healValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.60 * power);
        bool critroll = Logic.RNGroll(10f);
        if (critroll)
        {
            healValue = Convert.ToInt32(healValue * 1.5f);
        }
        Simulation.hpDummy += healValue;
    }
}


