using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System;
public enum SkillType
{
    Normal,
    Closest,
    Furthest,
    Target,
    Weakest,
    Random,
    SelfHeal,
    SelfSHield,
    SpreadHeal,
    TargetHeal,
    AOEHeal,
    Drain,
    AOEDrain,
    AOE,
    Pierce3,
    Pierce2,
    Execute,
    Ricochet4,
    Ricochet2,
    Unity,
    Revive,
    OnTurnShield,
    WeakestTayto

}
public enum Boolean
{
    True,
    False,
    Null
}
public class Skill
{
    public float Value;
    public float Range;
    public int Weight;
    public int Cost;
    public Boolean IsHealing
    {
        get
        {
            switch (skillType)
            {
                case SkillType.AOEHeal:
                case SkillType.SelfHeal:
                case SkillType.SpreadHeal:
                case SkillType.TargetHeal:
                    return Boolean.True;
                default:
                    return Boolean.Null;
            }
        }
    }

    public Boolean IsShielding
    {
        get
        {
            switch (skillType)
            {
                case SkillType.SelfSHield:
                    return Boolean.True;
                default:
                    return Boolean.Null;
            }
        }
    }

    public Boolean IsAOE
    {
        get
        {
            switch (skillType)
            {
                case SkillType.AOE:
                case SkillType.AOEDrain:
                case SkillType.Pierce3:
                case SkillType.Pierce2:
                    return Boolean.True;
                default:
                    return Boolean.Null;
            }
        }
    }
    public Boolean IsTarget
    {
        get
        {
            switch (skillType)
            {
                case SkillType.AOE:
                case SkillType.AOEDrain:
                case SkillType.Pierce3:
                case SkillType.Pierce2:
                case SkillType.AOEHeal:
                case SkillType.SelfHeal:
                case SkillType.SpreadHeal:
                case SkillType.TargetHeal:
                    return Boolean.Null;
                default:
                    return Boolean.True;
            }
        }
    }
    private bool IsCrit;
    private bool IsEmp;
    //public static Random random = new Random(Guid.NewGuid().GetHashCode());
    private SkillType skillType;


    public Skill(float value, float range, int weight, int cost, SkillType skilltype)
    {
        Value = value;
        Range = range;
        Weight = weight;
        Cost = cost;
        skillType = skilltype;
    }

    private void StoreRandomFactors(Character character)
    {
        IsCrit = Logic.RNGroll(character.critChance);
        IsEmp = Logic.RNGroll(character.empowerChance);
    }

    public int GetValue(Character author)
    {
        int attackModifier = Convert.ToInt32(Value * Range * author.power);
        int returnValue = 0;
        int mod = Convert.ToInt32(Math.Pow(-1, ThreadSafeRandom.Next(2)));
        returnValue = Convert.ToInt32((author.power + author.enrageBar) * Value + ThreadSafeRandom.Next(attackModifier) * mod);
        author.enrageBar = 0;

        if (IsCrit)
        {
            returnValue = Convert.ToInt32(returnValue * author.critDamage);
        }
        if (IsEmp)
        {
            returnValue *= 2;
        }
        return returnValue;
    }

    public void ApplySkill(Character author, Character[] party, Character[] opponents)
    {
        int amountToCast = 1;
        if (author.gateKeeperBonus)
        {
            if (Logic.RNGroll(0.5f)) amountToCast += 3;
        }
        if (Logic.RNGroll(author.dsChance)) amountToCast++;
        if (Logic.RNGroll(author.quadChance)) amountToCast += 4;
        while (amountToCast != 0)
        {
            if (WorldBossSimulation.GetPartyCount(opponents) == 0) return;
            StoreRandomFactors(author);
            switch (skillType)
            {
                case SkillType.Normal:
                case SkillType.Closest:
                    ClosestSkill(author, party, opponents);
                    break;
                case SkillType.Target:
                case SkillType.Random:
                    TargetSkill(author, party, opponents);
                    break;
                case SkillType.Weakest:
                    WeakestSkill(author, party, opponents);
                    break;
                case SkillType.Furthest:
                    FurthestSkill(author, party, opponents);
                    break;
                case SkillType.AOE:
                    AoeSkill(author, party, opponents);
                    break;
                case SkillType.AOEHeal:
                    AoeHealSkill(author, party);
                    break;
                case SkillType.SelfHeal:
                    SelfHealSkill(author);
                    break;
                case SkillType.SelfSHield:
                    SelfShieldSkill(author);
                    break;
                case SkillType.SpreadHeal:
                    SpreadHealSkill(author, party);
                    break;
                case SkillType.TargetHeal:
                    TargetHealSkill(author, party);
                    break;
                case SkillType.AOEDrain:
                    author.drain = true;
                    AoeSkill(author, party, opponents);
                    author.drain = false;
                    break;
                case SkillType.Drain:
                    author.drain = true;
                    ClosestSkill(author, party, opponents);
                    author.drain = false;
                    break;
                case SkillType.Pierce2:
                    Pierce2Skill(author, party, opponents);
                    break;
                case SkillType.Pierce3:
                    Pierce3Skill(author, party, opponents);
                    break;
                case SkillType.Execute:
                    author.selfInjure = true;
                    TargetSkill(author, party, opponents);
                    author.selfInjure = false;
                    break;
                case SkillType.Ricochet4:
                    RicochetSkill(author, party, opponents, 4);
                    break;
                case SkillType.Ricochet2:
                    RicochetSkill(author, party, opponents, 2);
                    break;
                case SkillType.Unity:
                    SpreadHealSkill(author, party);
                    break;
                case SkillType.Revive:
                    break;
                case SkillType.OnTurnShield:
                    OnTurnShieldTeam(author, party);
                    amountToCast = 1;
                    break;
                case SkillType.WeakestTayto:
                    WeakestTaytoSkill(author, party, opponents);
                    amountToCast = 1;
                    break;

            }
            amountToCast--;
        }
    }

    private void DamageLogic(Character author, Character[] party, Character[] opponents, Character target, bool absorbProc)
    {
        Character[] receivingParty;
        Character[] sendingParty;
        if (party.Contains(target))
        {
            receivingParty = party;
            sendingParty = opponents;
        }
        else
        {
            receivingParty = opponents;
            sendingParty = party;
        }
        //apply damage
        int attackValue = GetValue(author);
        if (absorbProc)
        {
            author.pet.PetSelection(author, party, opponents, PetProcType.PerHit);
            Logic.HitAbsorbed(attackValue, target);
            target.pet.PetSelection(target, receivingParty, sendingParty, PetProcType.GetHit);
        }
        else
        {
            Logic.DamageApplication(attackValue, target, author, party, receivingParty);
            if (Logic.RNGroll(author.ricochetChance) && WorldBossSimulation.GetPartyCount(opponents) > 0) DamageLogic(author, party, opponents, Logic.SelectRicochet(opponents, target), absorbProc); //this implmentation won't work as well if enemies have redirect/deflect
        }
    }

    private void FurthestSkill(Character author, Character[] party, Character[] opponents)
    {
        //find target 
        bool absorbProc = false;
        Character target = Logic.RedirectDeflectLoop(Logic.SelectBack(opponents), author, opponents, party, ref absorbProc);
        DamageLogic(author, party, opponents, target, absorbProc);

    }
    private void ClosestSkill(Character author, Character[] party, Character[] opponents)
    {
        //find target 
        bool absorbProc = false;
        Character target = Logic.RedirectDeflectLoop(Logic.SelectFront(opponents), author, opponents, party, ref absorbProc);
        DamageLogic(author, party, opponents, target, absorbProc);

    }
    //until moki input, random and target will be treated as similar skills
    private void TargetSkill(Character author, Character[] party, Character[] opponents)
    {
        //find target 
        bool absorbProc = false;
        Character target = Logic.RedirectDeflectLoop(Logic.SelectTarget(opponents), author, opponents, party, ref absorbProc);
        DamageLogic(author, party, opponents, target, absorbProc);

    }
    private void WeakestSkill(Character author, Character[] party, Character[] opponents)
    {
        //find target 
        bool absorbProc = false;
        Character target = Logic.RedirectDeflectLoop(Logic.SelectWeakest(opponents), author, opponents, party, ref absorbProc);
        DamageLogic(author, party, opponents, target, absorbProc);
    }
    private void WeakestTaytoSkill(Character author, Character[] party, Character[] opponents)
    {
        //find target 
        bool absorbProc = false;
        Character target = Logic.SelectWeakest(opponents);
        DamageLogic(author, party, opponents, target, absorbProc);
    }
    private void TargetHealSkill(Character author, Character[] party)
    {
        Character target = Logic.HealFindWeakestPerc(party);
        int attackValue = GetValue(author);
        attackValue = Convert.ToInt32(attackValue * author.bonusHealing);

        if (target.FindMythBonus(MythicBonus.Decay))
        {
            if (Logic.RNGroll(5f)) attackValue *= 2;
        }

        if (author.percToShield > 0f)
        {
            target.shield += Convert.ToInt32(attackValue * author.percToShield);
            if (target.shield > target.maxShield) target.shield = target.maxShield;
        }
        if (author.overHeal)
        {
            if (target.maxHp - target.hp < attackValue)
            {
                attackValue -= (target.maxHp - target.hp);
                target.hp = target.maxHp;
                target.shield += attackValue;
                if (target.shield > target.maxShield) target.shield = target.maxShield;
                attackValue = 0;
            }
        }
        target.hp += attackValue;
        if (target.hp > target.maxHp) target.hp = target.maxHp;
    }
    private void AoeHealSkill(Character author, Character[] party)
    {
        for (int i = 0; i < party.Length; i++)
        {
            int attackValue = GetValue(author);
            attackValue = Convert.ToInt32(attackValue * author.bonusHealing);
            if (party[i].alive)
            {
                if (party[i].FindMythBonus(MythicBonus.Decay))
                {
                    if (Logic.RNGroll(5f)) attackValue *= 2;
                }
                if (author.percToShield > 0f)
                {
                    party[i].shield += Convert.ToInt32(attackValue * author.percToShield);
                    if (party[i].shield > party[i].maxShield) party[i].shield = party[i].maxShield;
                }
                if (author.overHeal)
                {
                    if (party[i].maxHp - party[i].hp < attackValue)
                    {
                        attackValue -= (party[i].maxHp - party[i].hp);
                        party[i].hp = party[i].maxHp;
                        party[i].shield += attackValue;
                        if (party[i].shield > party[i].maxShield) party[i].shield = party[i].maxShield;
                        attackValue = 0;
                    }
                }
                party[i].hp += attackValue;
                if (party[i].hp > party[i].maxHp) party[i].hp = party[i].maxHp;
            }
        }
    }
    private void SpreadHealSkill(Character author, Character[] party)
    {
        Character target = Logic.HealFindWeakestPerc(party);
        int healingValue = GetValue(author);
        healingValue = Convert.ToInt32(healingValue * author.bonusHealing);

        for (int i = 0; i < 10; i++)
        {
            target = Logic.HealFindWeakestPerc(party);
            target.hp += healingValue / 10;
            if (target.FindMythBonus(MythicBonus.Decay))
            {
                if (Logic.RNGroll(5f)) target.hp += healingValue / 10;
            }
            if (author.percToShield > 0f)
            {
                target.shield += Convert.ToInt32(healingValue * author.percToShield / 10);
                if (target.shield > target.maxShield) target.shield = target.maxShield;
            }
            if (author.overHeal)
            {
                int attackValue = healingValue / 10;
                if (target.maxHp - target.hp < attackValue)
                {
                    attackValue -= (target.maxHp - target.hp);
                    target.hp = target.maxHp;
                    target.shield += attackValue;
                    if (target.shield > target.maxShield) target.shield = target.maxShield;
                    attackValue = 0;
                }
            }
            if (target.hp > target.maxHp)
            {
                target.hp = target.maxHp;
            }
        }
    }
    private void SelfHealSkill(Character author)
    {
        int attackValue = GetValue(author);
        if (author.FindMythBonus(MythicBonus.Decay))
        {
            if (Logic.RNGroll(5f)) attackValue *= 2;
        }
        attackValue = Convert.ToInt32(attackValue * author.bonusHealing);
        if (author.percToShield > 0f)
        {
            author.shield = Convert.ToInt32(attackValue * 0.1);
            if (author.shield > author.maxShield) author.shield = author.maxShield;
        }
        if (author.overHeal)
        {
            if (author.maxHp - author.hp < attackValue)
            {
                attackValue -= (author.maxHp - author.hp);
                author.hp = author.maxHp;
                author.shield += attackValue;
                if (author.shield > author.maxShield) author.shield = author.maxShield;
                attackValue = 0;
            }
        }
        author.hp += attackValue;
        if (author.hp > author.maxHp) author.hp = author.maxHp;
    }
    private void SelfShieldSkill(Character author)
    {
        int attackValue = GetValue(author);
        author.shield += attackValue;
        if (author.shield > author.maxShield) author.shield = author.maxShield;
    }
    private void AoeSkill(Character author, Character[] party, Character[] opponents)
    {
        bool absorbProc = false;
        for (int i = 0; i < opponents.Length; i++)
        {
            if (opponents[i].alive)
            {
                Character target = Logic.RedirectDeflectLoop(Logic.SelectFront(opponents), author, opponents, party, ref absorbProc);
                DamageLogic(author, party, opponents, target, absorbProc);
                absorbProc = false;
            }
        }
    }
    private void Pierce3Skill(Character author, Character[] party, Character[] opponents)
    {
        bool absorbProc = false;
        int firstTarget = Logic.SelectPierce(opponents);
        for (int i = firstTarget; i < firstTarget + 3; i++)
        {
            if (i < opponents.Length && opponents[i].alive)
            {
                Character target = Logic.RedirectDeflectLoop(Logic.SelectFront(opponents), author, opponents, party, ref absorbProc);
                DamageLogic(author, party, opponents, target, absorbProc);
                absorbProc = false;
            }
        }
    }
    private void Pierce2Skill(Character author, Character[] party, Character[] opponents)
    {
        bool absorbProc = false;
        int firstTarget = Logic.SelectPierce(opponents);
        for (int i = firstTarget; i < firstTarget + 2; i++)
        {
            if (i < opponents.Length && opponents[i].alive)
            {
                Character target = Logic.RedirectDeflectLoop(Logic.SelectFront(opponents), author, opponents, party, ref absorbProc);
                DamageLogic(author, party, opponents, target, absorbProc);
                absorbProc = false;
            }
        }
    }
    private void RicochetSkill(Character author, Character[] party, Character[] opponents, int bounce)
    {
        bool absorbProc = false;
        Character target = Logic.RedirectDeflectLoop(Logic.SelectTarget(opponents), author, opponents, party, ref absorbProc);
        DamageLogic(author, party, opponents, target, absorbProc);
        absorbProc = false;
        for (int i = 0; i < bounce; i++)
        {
            if (Logic.CountAlive(opponents) > 1)
            {
                target = Logic.RedirectDeflectLoop(Logic.SelectRicochet(opponents, target), author, opponents, party, ref absorbProc);
                DamageLogic(author, party, opponents, target, absorbProc);
                absorbProc = false;
            }
            else { break; }
        }
    }
    private void OnTurnShieldTeam(Character author, Character[] party)
    {
        int attackValue = GetValue(author);
        foreach (var member in party)
        {
            if (member.alive)
            {
                member.shield += attackValue;
                if (member.shield > member.maxHp) member.shield = member.maxShield;
            }
        }
    }

}
