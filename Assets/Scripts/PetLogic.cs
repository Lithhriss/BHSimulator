using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

class PetLogic
{
    private static void TeamHealPet(int l)
    {
        int healModifier = Convert.ToInt32(Simulation.hero[l].power * 0.072);
        float healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.324 * Simulation.hero[l].power);

        bool critroll = Logic.RNGroll(Simulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critroll)
        {
            healValue *= Simulation.hero[l].critDamage;
        }
        if (Logic.RNGroll(Simulation.hero[l].empowerChance))
        {
            healValue *= 2;
        }
        if (petRoll)
        {
            for (int i = 0; i < 5; i++)
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

    private static void OffPetProc(int l)
    {
        int attackModifier = Convert.ToInt32(0.54 * Simulation.hero[l].power);
        int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + Simulation.hero[l].power * 0.63);

        bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll((float)20);

        if (critRoll)
        {
            attackValue = Convert.ToInt32(attackValue * Simulation.hero[l].critDamage);
        }
        if (Logic.RNGroll(Simulation.hero[l].empowerChance))
        {
            attackValue *= 2;
        }
        if (petRoll)
        {
            Simulation.hpDummy -= attackValue;
        }

    }

    private static void SuperOffPetProc(int l)
    {
        int attackModifier = Convert.ToInt32(Simulation.hero[l].power * 0.37);
        int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + Simulation.hero[l].power * 1.668);

        bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll(10f);

        if (critRoll)
        {
            attackValue = Convert.ToInt32(attackValue * Simulation.hero[l].critDamage);
        }
        if (Logic.RNGroll(Simulation.hero[l].empowerChance))
        {
            attackValue *= 2;
        }
        if (petRoll)
        {
            Simulation.hpDummy -= attackValue;
        }

    }

    private static void SpreadHealPet(int l)
    {
        int i;
        int target = 0;
        int healModifier = Convert.ToInt32(Simulation.hero[l].power * 0.14);
        int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.66 * Simulation.hero[l].power);

        bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critRoll)
        {
            healValue = Convert.ToInt32(healValue * Simulation.hero[l].critDamage);
        }
        if (Logic.RNGroll(Simulation.hero[l].empowerChance))
        {
            healValue *= 2;
        }
        if (petRoll)
        {
            for (i = 0; i < healValue; i++)
            {
                target = Logic.HealLogic();
                Simulation.hero[target].hp++;
                if (Simulation.hero[target].hp > Simulation.hero[target].maxHp)
                {
                    Simulation.hero[target].hp = Simulation.hero[target].maxHp;
                }
            }
        }
    }

    private static void SuperSpreadHealPet(int l)
    {
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
        if (Logic.RNGroll(Simulation.hero[l].empowerChance))
        {
            healValue *= 2;
        }
        if (petRoll)
        {
            for (i = 0; i < healValue; i++)
            {
                target = Logic.HealLogic();
                Simulation.hero[target].hp++;
                if (Simulation.hero[target].hp > Simulation.hero[target].maxHp)
                {
                    Simulation.hero[target].hp = Simulation.hero[target].maxHp;
                }
            }
        }
    }

    private static void TeamShieldPet(int l)
    {
        int i;
        int shieldModifier = Convert.ToInt32(Simulation.hero[l].power * 0.06);
        float shieldValue = Convert.ToInt32(UnityEngine.Random.Range(0, shieldModifier) + 0.27 * Simulation.hero[l].power);

        bool critroll = Logic.RNGroll(Simulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critroll)
        {
            shieldValue *= Simulation.hero[l].critDamage;
        }
        if (Logic.RNGroll(Simulation.hero[l].empowerChance))
        {
            shieldValue *= 2;
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

    private static void SuperTeamShieldPet(int l)
    {
        int i;
        int shieldModifier = Convert.ToInt32(Simulation.hero[l].power * 0.12);
        float shieldValue = Convert.ToInt32(UnityEngine.Random.Range(0, shieldModifier) + 0.54 * Simulation.hero[l].power);

        bool critroll = Logic.RNGroll(Simulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll(10f);

        if (critroll)
        {
            shieldValue *= Simulation.hero[l].critDamage;
        }
        if (Logic.RNGroll(Simulation.hero[l].empowerChance))
        {
            shieldValue *= 2;
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


    private static void SuperSelfHealPet(int l)
    {
        int healModifier = Convert.ToInt32(Simulation.hero[l].power * 0.454);
        int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.89 * Simulation.hero[l].power);

        bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll((float)10);

        if (critRoll)
        {
            healValue = Convert.ToInt32(healValue * Simulation.hero[l].critDamage);
        }
        if (Logic.RNGroll(Simulation.hero[l].empowerChance))
        {
            healValue *= 2;
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

    private static void TargetWeakestOffPet(int l)
    {
        int attackModifier = Convert.ToInt32(Simulation.hero[l].power * 0.64);
        int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + Simulation.hero[l].power * 0.48);

        bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critRoll)
        {
            attackValue = Convert.ToInt32(attackValue * Simulation.hero[l].critDamage);
        }
        if (Logic.RNGroll(Simulation.hero[l].empowerChance))
        {
            attackValue *= 2;
        }
        if (petRoll)
        {
            Simulation.hpDummy -= attackValue;
        }

    }

    private static void TargetWeakestHealPet(int l)
    {
        int target = 0;
        int healModifier = Convert.ToInt32(Simulation.hero[l].power * 0.288);
        int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.576 * Simulation.hero[l].power);

        bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critRoll)
        {
            healValue = Convert.ToInt32(healValue * Simulation.hero[l].critDamage);
        }
        if (Logic.RNGroll(Simulation.hero[l].empowerChance))
        {
            healValue *= 2;
        }
        if (petRoll)
        {
            target = Logic.HealLogic();
            Simulation.hero[target].hp += healValue;
            if (Simulation.hero[target].hp > Simulation.hero[target].maxHp)
            {
                Simulation.hero[target].hp = Simulation.hero[target].maxHp;
            }
        }
    }

    private static void TeamHealShieldpet(int l)
    {
        int regenModifier = Convert.ToInt32(Simulation.hero[l].power * 0.034);
        float regenValue = Convert.ToInt32(UnityEngine.Random.Range(0, regenModifier) + 0.153 * Simulation.hero[l].power);

        bool critroll = Logic.RNGroll(Simulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critroll)
        {
            regenValue *= Simulation.hero[l].critDamage;
        }
        if (Logic.RNGroll(Simulation.hero[l].empowerChance))
        {
            regenValue *= 2;
        }
        if (petRoll)
        {
            for (int i = 0; i < 5; i++)
            {
                if (Simulation.hero[i].hp > 0)
                {
                    Simulation.hero[i].hp += Convert.ToInt32(regenValue);
                    if (Simulation.hero[i].hp >= Simulation.hero[i].maxHp)
                    {
                        Simulation.hero[i].hp = Simulation.hero[i].maxHp;
                    }
                    Simulation.hero[i].shield += Convert.ToInt32(regenValue);
                    if (Simulation.hero[i].shield >= Simulation.hero[i].maxShield)
                    {
                        Simulation.hero[i].shield = Simulation.hero[i].maxShield;
                    }
                }
            }
        }
    }

    private static void RandomOffPet(int l)
    {
        int attackModifier = Convert.ToInt32(1.76 * Simulation.hero[l].power);
        int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + Simulation.hero[l].power * 0.22);

        bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll((float)20);

        if (critRoll)
        {
            attackValue = Convert.ToInt32(attackValue * Simulation.hero[l].critDamage);
        }
        if (Logic.RNGroll(Simulation.hero[l].empowerChance))
        {
            attackValue *= 2;
        }
        if (petRoll)
        {
            Simulation.hpDummy -= attackValue;
        }
    }

    public static void PetSelection(int k)
    {
        switch (Simulation.hero[k].pet)
        {
            case Hero.Pet.Nelson:
                OffPetProc(k);
                break;
            case Hero.Pet.Gemmi:
                TeamHealPet(k);
                break;
            case Hero.Pet.Boogie:
                SpreadHealPet(k);
                break;
            case Hero.Pet.Nemo:
                SuperOffPetProc(k);
                break;
            case Hero.Pet.Crem:
                SuperSpreadHealPet(k);
                break;
            case Hero.Pet.Boiguh:
                TeamShieldPet(k);
                break;
            case Hero.Pet.Nerder:
                SuperSelfHealPet(k);
                break;
            case Hero.Pet.Quimby:
                TargetWeakestOffPet(k);
                break;
            case Hero.Pet.Snut:
                SuperTeamShieldPet(k);
                break;
            case Hero.Pet.Wuvboi:
                TeamHealShieldpet(k);
                break;
            case Hero.Pet.Buvboi:
                RandomOffPet(k);
                break;
            case Hero.Pet.Skulldemort:
                TargetWeakestHealPet(k);
                break;
        }
    }

    #region New Code
    public static void PetSelection(Hero hero, Hero[] heroes, Enemy[] enemies)
    {
        switch (hero.pet)
        {
            case Hero.Pet.Nelson:
                OffPetProc(hero, enemies);
                break;
            case Hero.Pet.Gemmi:
                TeamHealPet(hero, heroes);
                break;
            case Hero.Pet.Boogie:
                SpreadHealPet(hero, heroes);
                break;
            case Hero.Pet.Nemo:
                SuperOffPetProc(hero, enemies);
                break;
            case Hero.Pet.Crem:
                SuperSpreadHealPet(hero, heroes);
                break;
            case Hero.Pet.Boiguh:
                TeamShieldPet(hero, heroes);
                break;
            case Hero.Pet.Nerder:
                SuperSelfHealPet(hero);
                break;
            case Hero.Pet.Quimby:
                TargetWeakestOffPet(hero, enemies);
                break;
            case Hero.Pet.Snut:
                SuperTeamShieldPet(hero, heroes);
                break;
            case Hero.Pet.Wuvboi:
                TeamHealShieldpet(hero, heroes);
                break;
            case Hero.Pet.Buvboi:
                RandomOffPet(hero, enemies);
                break;
            case Hero.Pet.Skulldemort:
                TargetWeakestHealPet(hero, heroes);
                break;
        }

    }
    private static void TeamHealPet(Hero hero, Hero[] heroes)
    {
        int healModifier = Convert.ToInt32(hero.power * 0.072);
        float healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.324 * hero.power);

        bool critroll = Logic.RNGroll(hero.critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critroll)
        {
            healValue *= hero.critDamage;
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            healValue *= 2;
        }
        if (petRoll)
        {
            for (int i = 0; i < 5; i++)
            {
                if (heroes[i].hp > 0)
                {
                    heroes[i].hp += Convert.ToInt32(healValue);
                    if (heroes[i].hp >= heroes[i].maxHp)
                    {
                        heroes[i].hp = heroes[i].maxHp;
                    }
                }
            }
        }
    }
    private static void OffPetProc(Hero hero, Enemy[] enemies)
    {
        int attackModifier = Convert.ToInt32(0.54 * hero.power);
        int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + hero.power * 0.63);

        bool critRoll = Logic.RNGroll(hero.critChance);
        bool petRoll = Logic.RNGroll((float)20);

        if (critRoll)
        {
            attackValue = Convert.ToInt32(attackValue * hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            attackValue *= 2;
        }
        if (petRoll)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].hp > 0)
                {
                    enemies[i].hp -= attackValue;
                    break;
                }
            }
        }

    }
    private static void SuperOffPetProc(Hero hero, Enemy[] enemies)
    {
        int attackModifier = Convert.ToInt32(hero.power * 0.37);
        int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + hero.power * 1.668);

        bool critRoll = Logic.RNGroll(hero.critChance);
        bool petRoll = Logic.RNGroll(10f);

        if (critRoll)
        {
            attackValue = Convert.ToInt32(attackValue * hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            attackValue *= 2;
        }
        if (petRoll)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].hp > 0)
                {
                    enemies[i].hp -= attackValue;
                    break;
                }
            }
        }

    }
    private static void SpreadHealPet(Hero hero, Hero[] heroes)
    {
        int i;
        int target = 0;
        int healModifier = Convert.ToInt32(hero.power * 0.14);
        int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.66 * hero.power);

        bool critRoll = Logic.RNGroll(hero.critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critRoll)
        {
            healValue = Convert.ToInt32(healValue * hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            healValue *= 2;
        }
        if (petRoll)
        {
            for (i = 0; i < healValue; i++)
            {
                target = Logic.HealLogic(heroes);
                heroes[target].hp++;
                if (heroes[target].hp > heroes[target].maxHp)
                {
                    heroes[target].hp = heroes[target].maxHp;
                }
            }
        }
    }
    private static void SuperSpreadHealPet(Hero hero, Hero[] heroes)
    {
        int i;
        int target = 0;
        int healModifier = Convert.ToInt32(hero.power * 0.288);
        int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 1.296 * hero.power);

        bool critRoll = Logic.RNGroll(hero.critChance);
        bool petRoll = Logic.RNGroll((float)10);

        if (critRoll)
        {
            healValue = Convert.ToInt32(healValue * hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            healValue *= 2;
        }
        if (petRoll)
        {
            for (i = 0; i < healValue; i++)
            {
                target = Logic.HealLogic(heroes);
                heroes[target].hp++;
                if (heroes[target].hp > heroes[target].maxHp)
                {
                    heroes[target].hp = heroes[target].maxHp;
                }
            }
        }
    }
    private static void TeamShieldPet(Hero hero, Hero[] heroes)
    {
        int i;
        int shieldModifier = Convert.ToInt32(hero.power * 0.06);
        float shieldValue = Convert.ToInt32(UnityEngine.Random.Range(0, shieldModifier) + 0.27 * hero.power);

        bool critroll = Logic.RNGroll(hero.critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critroll)
        {
            shieldValue *= hero.critDamage;
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            shieldValue *= 2;
        }
        if (petRoll)
        {
            for (i = 0; i < 5; i++)
            {
                if (heroes[i].hp > 0)
                {
                    heroes[i].shield += Convert.ToInt32(shieldValue);
                    if (heroes[i].shield >= heroes[i].maxShield)
                    {
                        heroes[i].shield = heroes[i].maxShield;
                    }
                }
            }
        }
    }
    private static void SuperTeamShieldPet(Hero hero, Hero[] heroes)
    {
        int i;
        int shieldModifier = Convert.ToInt32(hero.power * 0.12);
        float shieldValue = Convert.ToInt32(UnityEngine.Random.Range(0, shieldModifier) + 0.54 * hero.power);

        bool critroll = Logic.RNGroll(hero.critChance);
        bool petRoll = Logic.RNGroll(10f);

        if (critroll)
        {
            shieldValue *= hero.critDamage;
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            shieldValue *= 2;
        }
        if (petRoll)
        {
            for (i = 0; i < 5; i++)
            {
                if (heroes[i].hp > 0)
                {
                    heroes[i].shield += Convert.ToInt32(shieldValue);
                    if (heroes[i].shield >= heroes[i].maxShield)
                    {
                        heroes[i].shield = heroes[i].maxShield;
                    }
                }
            }
        }
    }
    private static void SuperSelfHealPet(Hero hero)
    {
        int healModifier = Convert.ToInt32(hero.power * 0.454);
        int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.89 * hero.power);

        bool critRoll = Logic.RNGroll(hero.critChance);
        bool petRoll = Logic.RNGroll((float)10);

        if (critRoll)
        {
            healValue = Convert.ToInt32(healValue * hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            healValue *= 2;
        }
        if (petRoll)
        {
            hero.hp += healValue;
            if (hero.hp > hero.maxHp)
            {
                hero.hp = hero.maxHp;
            }
        }
    }
    private static void TargetWeakestOffPet(Hero hero, Enemy[] enemies)
    {
        int attackModifier = Convert.ToInt32(hero.power * 0.64);
        int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + hero.power * 0.48);

        bool critRoll = Logic.RNGroll(hero.critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critRoll)
        {
            attackValue = Convert.ToInt32(attackValue * hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            attackValue *= 2;
        }
        if (petRoll)
        {
            int target = 0;
            for (int i = 1; i < enemies.Length; i++)
            {
                if (enemies[target].hp > enemies[i].hp && enemies[i].alive) target = i;
            }
            enemies[target].hp -= attackModifier;
        }

    }
    private static void TargetWeakestHealPet(Hero hero, Hero[] heroes)
    {
        int target = 0;
        int healModifier = Convert.ToInt32(hero.power * 0.288);
        int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.576 * hero.power);

        bool critRoll = Logic.RNGroll(hero.critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critRoll)
        {
            healValue = Convert.ToInt32(healValue * hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            healValue *= 2;
        }
        if (petRoll)
        {
            target = Logic.HealLogic(heroes);
            heroes[target].hp += healValue;
            if (heroes[target].hp > heroes[target].maxHp)
            {
                heroes[target].hp = heroes[target].maxHp;
            }
        }
    }
    private static void TeamHealShieldpet(Hero hero, Hero[] heroes)
    {
        int regenModifier = Convert.ToInt32(hero.power * 0.034);
        float regenValue = Convert.ToInt32(UnityEngine.Random.Range(0, regenModifier) + 0.153 * hero.power);

        bool critroll = Logic.RNGroll(hero.critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critroll)
        {
            regenValue *= hero.critDamage;
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            regenValue *= 2;
        }
        if (petRoll)
        {
            for (int i = 0; i < 5; i++)
            {
                if (heroes[i].hp > 0)
                {
                    heroes[i].hp += Convert.ToInt32(regenValue);
                    if (heroes[i].hp >= heroes[i].maxHp)
                    {
                        heroes[i].hp = heroes[i].maxHp;
                    }
                    heroes[i].shield += Convert.ToInt32(regenValue);
                    if (heroes[i].shield >= heroes[i].maxShield)
                    {
                        heroes[i].shield = heroes[i].maxShield;
                    }
                }
            }
        }
    }
    private static void RandomOffPet(Hero hero, Enemy[] enemies)
    {
        int attackModifier = Convert.ToInt32(1.76 * hero.power);
        int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + hero.power * 0.22);

        bool critRoll = Logic.RNGroll(hero.critChance);
        bool petRoll = Logic.RNGroll((float)20);

        if (critRoll)
        {
            attackValue = Convert.ToInt32(attackValue * hero.critDamage);
        }
        if (Logic.RNGroll(hero.empowerChance))
        {
            attackValue *= 2;
        }
        if (petRoll)
        {
            while (true)
            {
                int target = UnityEngine.Random.Range(0, 4);
                if (enemies[target].hp > 0)
                {
                    enemies[target].hp -= attackValue;
                }
            }
        }
    }


    #endregion
}