using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

class PetLogic
{
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
            for (int i = 0; i < heroes.Length; i++)
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
                int target = UnityEngine.Random.Range(0, enemies.Length);
                if (WorldBossSimulation.GetPartyCount(enemies) > 0 && enemies[target].hp > 0)
                {
                    enemies[target].hp -= attackValue;
                    return;
                }
                else return;

            }
        }
    }
}

public enum PetAbilty
{
    TeamHeal,
    SpreahHeal,
    SelfHeal,
    WeakestHeal,
    TeamShield,
    TeamHealShield,
    AOEAttack,
    ClosestAttack,
    RandomAttack,
    WeakestAttack
}
public enum PetProcType
{
    GetHit,
    PerTurn,
    PerHit,
    AllType
}

public class Pet
{
    private float ProcChance;
    private float Scaling;
    private float Range;
    private PetAbilty petAbility;
    public PetProcType petProcType;
    private int Value;

    private bool IsCrit;
    private bool IsEmp;

    public Pet(float procChance, float scaling, float range, PetAbilty _petAbility, PetProcType _petProcType)
    {
        ProcChance = procChance;
        Scaling = scaling / 100f;
        Range = range / 100f;
        petAbility = _petAbility;
        petProcType = _petProcType;
    }

    private void StoreRandomFactors(Character author)
    {
        IsCrit = Logic.RNGroll(author.critChance);
        IsEmp = Logic.RNGroll(author.empowerChance);
    }

    private void GetValue(Character author)
    {
        int attackModifier = Convert.ToInt32(Scaling * Range * author.power);
        int returnValue = 0;
        int mod = Convert.ToInt32(Math.Pow(-1, UnityEngine.Random.Range(0, 2)));
        returnValue = Convert.ToInt32(author.power * Scaling + UnityEngine.Random.Range(0, attackModifier) * mod);

        if (IsCrit)
        {
            returnValue = Convert.ToInt32(returnValue * author.critDamage);
        }
        if (IsEmp)
        {
            returnValue *= 2;
        }
        Value = returnValue;
    }

    public void PetSelection(Character author, Character[] party, Character[] enemies)
    {
        if (Logic.RNGroll(ProcChance))
        {
            StoreRandomFactors(author);
            GetValue(author);
            switch (petAbility)
            {
                case PetAbilty.TeamHeal:
                    TeamHeal(party);
                    break;
                case PetAbilty.SpreahHeal:
                    SpreadHeal(party);
                    break;
                case PetAbilty.SelfHeal:
                    SelfHeal(author);
                    break;
                case PetAbilty.WeakestHeal:
                    WeakestHeal(party);
                    break;
                case PetAbilty.TeamHealShield:
                    TeamHealShield(party);
                    break;
                case PetAbilty.TeamShield:
                    TeamShield(party);
                    break;
                case PetAbilty.ClosestAttack:
                    ClosestAttack(enemies);
                    break;
                case PetAbilty.WeakestAttack:
                    WeakestAttack(enemies);
                    break;
                case PetAbilty.AOEAttack:
                    AoeAttack(enemies);
                    break;
                case PetAbilty.RandomAttack:
                    RandomAttack(enemies);
                    break;
            }
        }
    }

    private void TeamHeal(Character[] party)
    {
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i].hp > 0)
            {
                party[i].hp += Convert.ToInt32(Value);
                if (party[i].hp >= party[i].maxHp)
                {
                    party[i].hp = party[i].maxHp;
                }
            }
        }
    }
    private void SpreadHeal(Character[] party)
    {
        int target;
        for (int i = 0; i < Value; i++)
        {
            target = Logic.HealFindWeakestPerc(party);
            party[target].hp++;
            if (party[target].hp > party[target].maxHp)
            {
                party[target].hp = party[target].maxHp;
            }
        }
    }
    private void TeamShield(Character[] party)
    {
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i].hp > 0)
            {
                party[i].shield += Convert.ToInt32(Value);
                if (party[i].shield >= party[i].maxShield)
                {
                    party[i].shield = party[i].maxShield;
                }
            }
        }
    }
    private void SelfHeal(Character author)
    {
        author.hp += Value;
        if (author.hp > author.maxHp)
        {
            author.hp = author.maxHp;
        }
    }
    private void TeamHealShield(Character[] party)
    {
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i].hp > 0)
            {
                party[i].hp += Convert.ToInt32(Value);
                if (party[i].hp >= party[i].maxHp)
                {
                    party[i].hp = party[i].maxHp;
                }
                party[i].shield += Convert.ToInt32(Value);
                if (party[i].shield >= party[i].maxShield)
                {
                    party[i].shield = party[i].maxShield;
                }
            }
        }
    }
    private void WeakestHeal(Character[] party)
    {
        int target = Logic.HealFindWeakestPerc(party);
        party[target].hp += Value;
        if (party[target].hp > party[target].maxHp)
        {
            party[target].hp = party[target].maxHp;
        }
    }
    private void AoeAttack(Character[] enemies)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].hp > 0)
            {
                enemies[i].hp -= Value;
            }
        }
    }
    private void ClosestAttack(Character[] enemies)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].hp > 0)
            {
                enemies[i].hp -= Value;
                break;
            }
        }
    }
    private void WeakestAttack(Character[] enemies)
    {
        int target = 0;
        for (int i = 1; i < enemies.Length; i++)
        {
            if (enemies[target].hp > enemies[i].hp && enemies[i].alive) target = i;
        }
        enemies[target].hp -= Value;
    }
    private void RandomAttack(Character[] enemies)
    {
        while (true)
        {
            int target = UnityEngine.Random.Range(0, enemies.Length);
            if (WorldBossSimulation.GetPartyCount(enemies) > 0 && enemies[target].hp > 0)
            {
                enemies[target].hp -= Value;
                return;
            }
            else return;
        }
    }

}