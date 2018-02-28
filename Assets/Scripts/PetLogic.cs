using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

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
    public Pet()
    {
        ProcChance = 0;
        petProcType = PetProcType.AllType;
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

    public void PetSelection(Character author, Character[] party, Character[] enemies, PetProcType currentPetProcType)
    {
        if (petProcType == currentPetProcType || petProcType == PetProcType.AllType)
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