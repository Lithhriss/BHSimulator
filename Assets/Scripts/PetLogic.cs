using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

class PetLogic
{
    private static void TeamHealPet(int l)
    {
        int healModifier = Convert.ToInt32(RaidSimulation.hero[l].power * 0.072);
        float healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.324 * RaidSimulation.hero[l].power);

        bool critroll = Logic.RNGroll(RaidSimulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critroll)
        {
            healValue *= RaidSimulation.hero[l].critDamage;
        }
        if (Logic.RNGroll(RaidSimulation.hero[l].empowerChance))
        {
            healValue *= 2;
        }
        if (petRoll)
        {
            for (int i = 0; i < 5; i++)
            {
                if (RaidSimulation.hero[i].hp > 0)
                {
                    RaidSimulation.hero[i].hp += Convert.ToInt32(healValue);
                    if (RaidSimulation.hero[i].hp >= RaidSimulation.hero[i].maxHp)
                    {
                        RaidSimulation.hero[i].hp = RaidSimulation.hero[i].maxHp;
                    }
                }
            }
        }
    }

    private static void OffPetProc(int l)
    {
        int attackModifier = Convert.ToInt32(0.54 * RaidSimulation.hero[l].power);
        int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + RaidSimulation.hero[l].power * 0.63);

        bool critRoll = Logic.RNGroll(RaidSimulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll((float)20);

        if (critRoll)
        {
            attackValue = Convert.ToInt32(attackValue * RaidSimulation.hero[l].critDamage);
        }
        if (Logic.RNGroll(RaidSimulation.hero[l].empowerChance))
        {
            attackValue *= 2;
        }
        if (petRoll)
        {
            RaidSimulation.hpDummy -= attackValue;
        }

    }

    private static void SuperOffPetProc(int l)
    {
        int attackModifier = Convert.ToInt32(RaidSimulation.hero[l].power * 0.37);
        int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + RaidSimulation.hero[l].power * 1.668);

        bool critRoll = Logic.RNGroll(RaidSimulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll(10f);

        if (critRoll)
        {
            attackValue = Convert.ToInt32(attackValue * RaidSimulation.hero[l].critDamage);
        }
        if (Logic.RNGroll(RaidSimulation.hero[l].empowerChance))
        {
            attackValue *= 2;
        }
        if (petRoll)
        {
            RaidSimulation.hpDummy -= attackValue;
        }

    }

    private static void SpreadHealPet(int l)
    {
        int i;
        int target = 0;
        int healModifier = Convert.ToInt32(RaidSimulation.hero[l].power * 0.14);
        int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.66 * RaidSimulation.hero[l].power);

        bool critRoll = Logic.RNGroll(RaidSimulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critRoll)
        {
            healValue = Convert.ToInt32(healValue * RaidSimulation.hero[l].critDamage);
        }
        if (Logic.RNGroll(RaidSimulation.hero[l].empowerChance))
        {
            healValue *= 2;
        }
        if (petRoll)
        {
            for (i = 0; i < healValue; i++)
            {
                target = Logic.HealLogic();
                RaidSimulation.hero[target].hp++;
                if (RaidSimulation.hero[target].hp > RaidSimulation.hero[target].maxHp)
                {
                    RaidSimulation.hero[target].hp = RaidSimulation.hero[target].maxHp;
                }
            }
        }
    }

    private static void SuperSpreadHealPet(int l)
    {
        int i;
        int target = 0;
        int healModifier = Convert.ToInt32(RaidSimulation.hero[l].power * 0.288);
        int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 1.296 * RaidSimulation.hero[l].power);

        bool critRoll = Logic.RNGroll(RaidSimulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll((float)10);

        if (critRoll)
        {
            healValue = Convert.ToInt32(healValue * RaidSimulation.hero[l].critDamage);
        }
        if (Logic.RNGroll(RaidSimulation.hero[l].empowerChance))
        {
            healValue *= 2;
        }
        if (petRoll)
        {
            for (i = 0; i < healValue; i++)
            {
                target = Logic.HealLogic();
                RaidSimulation.hero[target].hp++;
                if (RaidSimulation.hero[target].hp > RaidSimulation.hero[target].maxHp)
                {
                    RaidSimulation.hero[target].hp = RaidSimulation.hero[target].maxHp;
                }
            }
        }
    }

    private static void TeamShieldPet(int l)
    {
        int i;
        int shieldModifier = Convert.ToInt32(RaidSimulation.hero[l].power * 0.06);
        float shieldValue = Convert.ToInt32(UnityEngine.Random.Range(0, shieldModifier) + 0.27 * RaidSimulation.hero[l].power);

        bool critroll = Logic.RNGroll(RaidSimulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critroll)
        {
            shieldValue *= RaidSimulation.hero[l].critDamage;
        }
        if (Logic.RNGroll(RaidSimulation.hero[l].empowerChance))
        {
            shieldValue *= 2;
        }
        if (petRoll)
        {
            for (i = 0; i < 5; i++)
            {
                if (RaidSimulation.hero[i].hp > 0)
                {
                    RaidSimulation.hero[i].shield += Convert.ToInt32(shieldValue);
                    if (RaidSimulation.hero[i].shield >= RaidSimulation.hero[i].maxShield)
                    {
                        RaidSimulation.hero[i].shield = RaidSimulation.hero[i].maxShield;
                    }
                }
            }
        }
    }

    private static void SuperTeamShieldPet(int l)
    {
        int i;
        int shieldModifier = Convert.ToInt32(RaidSimulation.hero[l].power * 0.12);
        float shieldValue = Convert.ToInt32(UnityEngine.Random.Range(0, shieldModifier) + 0.54 * RaidSimulation.hero[l].power);

        bool critroll = Logic.RNGroll(RaidSimulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll(10f);

        if (critroll)
        {
            shieldValue *= RaidSimulation.hero[l].critDamage;
        }
        if (Logic.RNGroll(RaidSimulation.hero[l].empowerChance))
        {
            shieldValue *= 2;
        }
        if (petRoll)
        {
            for (i = 0; i < 5; i++)
            {
                if (RaidSimulation.hero[i].hp > 0)
                {
                    RaidSimulation.hero[i].shield += Convert.ToInt32(shieldValue);
                    if (RaidSimulation.hero[i].shield >= RaidSimulation.hero[i].maxShield)
                    {
                        RaidSimulation.hero[i].shield = RaidSimulation.hero[i].maxShield;
                    }
                }
            }
        }
    }


    private static void SuperSelfHealPet(int l)
    {
        int healModifier = Convert.ToInt32(RaidSimulation.hero[l].power * 0.454);
        int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.89 * RaidSimulation.hero[l].power);

        bool critRoll = Logic.RNGroll(RaidSimulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll((float)10);

        if (critRoll)
        {
            healValue = Convert.ToInt32(healValue * RaidSimulation.hero[l].critDamage);
        }
        if (Logic.RNGroll(RaidSimulation.hero[l].empowerChance))
        {
            healValue *= 2;
        }
        if (petRoll)
        {
            RaidSimulation.hero[l].hp += healValue;
            if (RaidSimulation.hero[l].hp > RaidSimulation.hero[l].maxHp)
            {
                RaidSimulation.hero[l].hp = RaidSimulation.hero[l].maxHp;
            }
        }
    }

    private static void TargetWeakestOffPet(int l)
    {
        int attackModifier = Convert.ToInt32(RaidSimulation.hero[l].power * 0.64);
        int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + RaidSimulation.hero[l].power * 0.48);

        bool critRoll = Logic.RNGroll(RaidSimulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critRoll)
        {
            attackValue = Convert.ToInt32(attackValue * RaidSimulation.hero[l].critDamage);
        }
        if (Logic.RNGroll(RaidSimulation.hero[l].empowerChance))
        {
            attackValue *= 2;
        }
        if (petRoll)
        {
            RaidSimulation.hpDummy -= attackValue;
        }

    }

    private static void TargetWeakestHealPet(int l)
    {
        int target = 0;
        int healModifier = Convert.ToInt32(RaidSimulation.hero[l].power * 0.288);
        int healValue = Convert.ToInt32(UnityEngine.Random.Range(0, healModifier) + 0.576 * RaidSimulation.hero[l].power);

        bool critRoll = Logic.RNGroll(RaidSimulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critRoll)
        {
            healValue = Convert.ToInt32(healValue * RaidSimulation.hero[l].critDamage);
        }
        if (Logic.RNGroll(RaidSimulation.hero[l].empowerChance))
        {
            healValue *= 2;
        }
        if (petRoll)
        {
            target = Logic.HealLogic();
            RaidSimulation.hero[target].hp += healValue;
            if (RaidSimulation.hero[target].hp > RaidSimulation.hero[target].maxHp)
            {
                RaidSimulation.hero[target].hp = RaidSimulation.hero[target].maxHp;
            }
        }
    }

    private static void TeamHealShieldpet(int l)
    {
        int regenModifier = Convert.ToInt32(RaidSimulation.hero[l].power * 0.034);
        float regenValue = Convert.ToInt32(UnityEngine.Random.Range(0, regenModifier) + 0.153 * RaidSimulation.hero[l].power);

        bool critroll = Logic.RNGroll(RaidSimulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critroll)
        {
            regenValue *= RaidSimulation.hero[l].critDamage;
        }
        if (Logic.RNGroll(RaidSimulation.hero[l].empowerChance))
        {
            regenValue *= 2;
        }
        if (petRoll)
        {
            for (int i = 0; i < 5; i++)
            {
                if (RaidSimulation.hero[i].hp > 0)
                {
                    RaidSimulation.hero[i].hp += Convert.ToInt32(regenValue);
                    if (RaidSimulation.hero[i].hp >= RaidSimulation.hero[i].maxHp)
                    {
                        RaidSimulation.hero[i].hp = RaidSimulation.hero[i].maxHp;
                    }
                    RaidSimulation.hero[i].shield += Convert.ToInt32(regenValue);
                    if (RaidSimulation.hero[i].shield >= RaidSimulation.hero[i].maxShield)
                    {
                        RaidSimulation.hero[i].shield = RaidSimulation.hero[i].maxShield;
                    }
                }
            }
        }
    }

    private static void RandomOffPet(int l)
    {
        int attackModifier = Convert.ToInt32(1.76 * RaidSimulation.hero[l].power);
        int attackValue = Convert.ToInt32(UnityEngine.Random.Range(0, attackModifier) + RaidSimulation.hero[l].power * 0.22);

        bool critRoll = Logic.RNGroll(RaidSimulation.hero[l].critChance);
        bool petRoll = Logic.RNGroll((float)20);

        if (critRoll)
        {
            attackValue = Convert.ToInt32(attackValue * RaidSimulation.hero[l].critDamage);
        }
        if (Logic.RNGroll(RaidSimulation.hero[l].empowerChance))
        {
            attackValue *= 2;
        }
        if (petRoll)
        {
            RaidSimulation.hpDummy -= attackValue;
        }
    }

    public static void PetSelection(int k)
    {
        switch (RaidSimulation.hero[k].pet)
        {
            case PetType.Nelson:
                OffPetProc(k);
                break;
            case PetType.Gemmi:
                TeamHealPet(k);
                break;
            case PetType.Boogie:
                SpreadHealPet(k);
                break;
            case PetType.Nemo:
                SuperOffPetProc(k);
                break;
            case PetType.Crem:
                SuperSpreadHealPet(k);
                break;
            case PetType.Boiguh:
                TeamShieldPet(k);
                break;
            case PetType.Nerder:
                SuperSelfHealPet(k);
                break;
            case PetType.Quimby:
                TargetWeakestOffPet(k);
                break;
            case PetType.Snut:
                SuperTeamShieldPet(k);
                break;
            case PetType.Wuvboi:
                TeamHealShieldpet(k);
                break;
            case PetType.Buvboi:
                RandomOffPet(k);
                break;
            case PetType.Skulldemort:
                TargetWeakestHealPet(k);
                break;
        }
    }

    #region New Code
    public static void PetSelection(Character author, Character[] party, Character[] opponents)
    {
        switch (author.pet)
        {
            case PetType.Nelson:
                OffPetProc(author, opponents);
                break;
            case PetType.Gemmi:
                TeamHealPet(author, party);
                break;
            case PetType.Boogie:
                SpreadHealPet(author, party);
                break;
            case PetType.Nemo:
                SuperOffPetProc(author, opponents);
                break;
            case PetType.Crem:
                SuperSpreadHealPet(author, party);
                break;
            case PetType.Boiguh:
                TeamShieldPet(author, party);
                break;
            case PetType.Nerder:
                SuperSelfHealPet(author);
                break;
            case PetType.Quimby:
                TargetWeakestOffPet(author, opponents);
                break;
            case PetType.Snut:
                SuperTeamShieldPet(author, party);
                break;
            case PetType.Wuvboi:
                TeamHealShieldpet(author, party);
                break;
            case PetType.Buvboi:
                RandomOffPet(author, opponents);
                break;
            case PetType.Skulldemort:
                TargetWeakestHealPet(author, party);
                break;
        }

    }
    private static void TeamHealPet(Character hero, Character[] heroes)
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
    private static void OffPetProc(Character hero, Character[] enemies)
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
    private static void SuperOffPetProc(Character hero, Character[] enemies)
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
    private static void SpreadHealPet(Character hero, Character[] heroes)
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
                target = Logic.HealFindWeakestPerc(heroes);
                heroes[target].hp++;
                if (heroes[target].hp > heroes[target].maxHp)
                {
                    heroes[target].hp = heroes[target].maxHp;
                }
            }
        }
    }
    private static void SuperSpreadHealPet(Character hero, Character[] heroes)
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
                target = Logic.HealFindWeakestPerc(heroes);
                heroes[target].hp++;
                if (heroes[target].hp > heroes[target].maxHp)
                {
                    heroes[target].hp = heroes[target].maxHp;
                }
            }
        }
    }
    private static void TeamShieldPet(Character author, Character[] party)
    {
        int i;
        int shieldModifier = Convert.ToInt32(author.power * 0.06);
        float shieldValue = Convert.ToInt32(UnityEngine.Random.Range(0, shieldModifier) + 0.27 * author.power);

        bool critroll = Logic.RNGroll(author.critChance);
        bool petRoll = Logic.RNGroll(20f);

        if (critroll)
        {
            shieldValue *= author.critDamage;
        }
        if (Logic.RNGroll(author.empowerChance))
        {
            shieldValue *= 2;
        }
        if (petRoll)
        {
            for (i = 0; i < party.Length; i++)
            {
                if (party[i].hp > 0)
                {
                    party[i].shield += Convert.ToInt32(shieldValue);
                    if (party[i].shield >= party[i].maxShield)
                    {
                        party[i].shield = party[i].maxShield;
                    }
                }
            }
        }
    }
    private static void SuperTeamShieldPet(Character hero, Character[] heroes)
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
            for (i = 0; i < heroes.Length; i++)
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
    private static void SuperSelfHealPet(Character hero)
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
    private static void TargetWeakestOffPet(Character hero, Character[] enemies)
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
    private static void TargetWeakestHealPet(Character hero, Character[] heroes)
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
            target = Logic.HealFindWeakestPerc(heroes);
            heroes[target].hp += healValue;
            if (heroes[target].hp > heroes[target].maxHp)
            {
                heroes[target].hp = heroes[target].maxHp;
            }
        }
    }
    private static void TeamHealShieldpet(Character hero, Character[] heroes)
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
            for (int i = 0; i < heroes.Length; i++)
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
    private static void RandomOffPet(Character hero, Character[] enemies)
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