using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

class Logic
{
    //public static Random random = new Random(Guid.NewGuid().GetHashCode());
    //methods for game logic
    //private static RngLogger<float> logger = new RngLogger<float>();
    public static bool RNGroll(float a)
    {
        bool outcome;
        float chance = a * 10f;
        float roll = ThreadSafeRandom.Next(1000);
        //logger.WriteLine((int)roll);
        if (roll < chance)
        {
            outcome = true;
        }
        else
        {
            outcome = false;
        }
        return outcome;
    }

    public static float TurnRate(int power, int agility)
    {
        if (power == 0) power = 1;
        float tr = 0f;
        tr = ((agility + power) / 2f);
        tr = (float)Math.Pow(tr, 2);
        tr = tr / (100f * power);
        return tr;
    }

    public static Boolean IsHealingNeeded(Character[] party)
    {
        //HpPerc(party);
        foreach (var member in party)
        {
            if (member.alive && member.hp < member.maxHp) return Boolean.True;
        }
        return Boolean.False;
    }
    public static void HitAbsorbed(int attackValue, Character target)
    {
        target.shield += attackValue;
        if (target.shield > target.maxShield)
        {
            target.shield = target.maxShield;
        }
    }

    public static Boolean IsShieldingNeeded(Character author)
    {

        if (author.alive && author.shield < author.maxShield * 0.75f) return Boolean.True;

        return Boolean.False;
    }

    public static void Hit(int attackValue, Character target, Character author, bool isBlocked, Character[] opponents, Character[] party)
    {
        float attackModifier = 1f + author.ReturnPersonalAttackMods(target, opponents, party);
        float reductionModifier = 0f + target.ReturnPersonalDefenceMods(opponents);
        int attackValuePrint = attackValue;
        attackValue = Convert.ToInt32(attackValue * attackModifier);

        if (isBlocked) attackValue = Convert.ToInt32(0.5 * attackValue);

        attackValue = Convert.ToInt32(attackValue * (target.damageReduction - reductionModifier));
        if (author.drain)
        {
            author.hp += attackValue;
            if (author.hp > author.maxHp) author.hp = author.maxHp;
        }
        if (author.selfInjure)
        {
            author.hp -= Convert.ToInt32(attackValue * 0.10);
        }
        //enrage and team enrage
        target.enrageBar += attackValue * target.enrage / 100f;
        if (target.enrageBar > target.maxEnrage) target.enrageBar = target.maxEnrage;
        foreach (Character member in opponents)
        {
            if (member.alive && member.teamEnrage > 0)
            {
                member.enrageBar += attackValue * target.teamEnrage / 100f;
                if (target.enrageBar > target.maxEnrage) target.enrageBar = target.maxEnrage;
            }
        }
        if (target.shield > 0)
        {
            if (attackValue > target.shield)
            {
                attackValue -= target.shield;
                target.shield = 0;
            }
            else
            {
                target.shield -= attackValue;
                attackValue = 0;
            }
        }
        target.hp -= attackValue;
        // when init if user doesn't have NW, set bool to true
        if (target.hp < target.maxHp / 2
            && !target.nightWalkerUsed
            && target.FindSetBonus(SetBonus.NWBonus, 3))
        {
            target.nightWalkerUsed = true;
            target.shield = target.maxShield;
        }

        if (target.hp < target.maxHp / 2
    && !target.polarisUsed
    && target.FindSetBonus(SetBonus.PolarisBonus, 3))
        {
            target.polarisUsed = true;
            target.hp = target.maxHp;
        }

        if (!target.alive)
        {
            target.hp = -1;
            target.hp += ConsumptionProc(opponents);
            if (!target.alive)
            {
                if (target.luminaryLife && target.FindSetBonus(SetBonus.LuminaryBonus, 4))
                {
                    target.hp += attackValuePrint;
                    if (target.hp > target.maxHp) target.hp = target.maxHp;
                    target.luminaryLife = false;
                }
            }
        }
        if (!target.alive
            && target.FindSetBonus(SetBonus.IllustriousBonus, 3)
            && target.illustriousRevive)
        {
            target.hp = target.power;
            if (target.hp > target.maxHp) target.hp = target.maxHp;
            target.illustriousRevive = false;
        }
    }
    public static int ConsumptionProc(Character[] party)
    {
        foreach (Character member in party)
        {
            if (member.alive && member.FindMythBonus(MythicBonus.Consumption))
            {
                if (RNGroll(5f)) return member.power;
            }
        }
        return 0;

    }
    public static int CountAlive(Character[] party)
    {
        return party.Count(hero => hero.alive == true);
    }
    public static int CountRedirect(Character[] party)
    {
        return party.Count(hero => (hero.metaRune == Character.MetaRune.Redirect && hero.redirect == true && hero.alive == true));
    }
    public static int DefensiveProcCase(Character hero)
    {
        int scenario = 10;
        float evadeMod = 0f;
        if (RNGroll(hero.blockChance)) { scenario = 1; }
        if (hero.FindMythBonus(MythicBonus.HoodOfMenace) && hero.hp > 0.75f * hero.maxHp) evadeMod += 5f;

        if (RNGroll(hero.evadeChance + evadeMod)) { scenario = 0; }
        return scenario;
    }
    public static Character RedirectSelection(Character target, Character[] party)
    {
        Character targetHero = target;
        int redirectCountLive = CountRedirect(party);
        while (redirectCountLive > 0)
        {//redirect loop will run only if at least one member has the rune
            for (int i = 0; i < party.Length; i++)
            {
                if (redirectCountLive == 0) break;
                if (party[i].metaRune == Character.MetaRune.Redirect && party[i].redirect && party[i].alive)
                { //3 part condition, that they have rune, that their last redirect roll was successful and alive
                    party[i].redirect = RNGroll(25f);
                    if (!party[i].redirect)
                    {
                        redirectCountLive--;
                    }
                    else
                    {
                        targetHero = party[i];
                        if (redirectCountLive == 1)
                        {//if only one member has the rune. will stop the loop to lock itself as target
                            redirectCountLive = 0;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < party.Length; i++)
        { //reset redirect rolls to true
            if (party[i].metaRune == Character.MetaRune.Redirect)
            {
                party[i].redirect = true;
            }
        }
        return targetHero;
    }
    public static Character RedirectDeflectLoop(Character target, Character author, Character[] opponents, Character[] party, ref bool aborbProc)
    {
        Character returnChar = target;
        returnChar = RedirectSelection(returnChar, opponents);
        if (RNGroll(returnChar.absorbChance))
        {
            aborbProc = true;
            return returnChar;
        }
        if (RNGroll(returnChar.deflectChance))
        {
            returnChar = RedirectDeflectLoop(author, returnChar, party, opponents, ref aborbProc);
        }
        return returnChar;
    }

    public static Character HealFindWeakestPerc(Character[] heroes)
    {
        int i;
        int lowest = 0;
        //HpPerc(heroes);
        for (i = 0; i < heroes.Length - 1; i++)
        {
            if (heroes[lowest].hpPerc >= heroes[i + 1].hpPerc)
            {
                if (heroes[i + 1].alive)
                {
                    lowest = i + 1;
                }
                else
                {
                    if (!heroes[lowest].alive)
                    {
                        lowest = i + 1;
                    }
                }
            }
        }
        return heroes[lowest];

        //return heroes.Where(hero => hero.alive).OrderBy(hero => hero.hpPerc).First();
    }

    public static Character ShieldFindWeakestPerc(Character[] heroes)
    {
        int i;
        int lowest = 0;
        //HpPerc(heroes);
        for (i = 0; i < heroes.Length - 1; i++)
        {
            if (heroes[lowest].shieldPerc >= heroes[i + 1].shieldPerc)
            {
                if (heroes[i + 1].alive)
                {
                    lowest = i + 1;
                }
                else
                {
                    if (!heroes[lowest].alive)
                    {
                        lowest = i + 1;
                    }
                }
            }
        }
        return heroes[lowest];

        //return heroes.Where(hero => hero.alive).OrderBy(hero => hero.hpPerc).First();
    }

    public static Character SelectTarget(Character[] party)
    {
        while (true)
        {
            int target = ThreadSafeRandom.Next(party.Length);
            if (party[target].alive) return party[target];
        }
    }
    public static Character SelectBack(Character[] party)
    {
        int target = party.Length - 1;
        while (true)
        {
            if (party[target].hp > 0) return party[target];
            target--;
        }
    }
    public static Character SelectFront(Character[] party)
    {
        int target = 0;
        while (true)
        {
            if (party[target].alive) return party[target];
            target++;
        }
    }
    public static int SelectPierce(Character[] party)
    {
        int target = 0;
        while (true)
        {
            if (party[target].alive) return target;
            target++;
        }
    }
    public static Character SelectWeakest(Character[] party)
    {
        Character returnChar = party[0];
        foreach (var member in party)
        {
            if (member.alive)
            {
                if (returnChar.alive)
                {
                    if (member.hp < returnChar.hp)
                    {
                        returnChar = member;
                    }
                }
                else
                {
                    returnChar = member;
                }
            }
        }
        return returnChar;
    }

    public static Character SelectStrongest(Character[] party)
    {
        Character returnChar = party[0];
        foreach (var member in party)
        {
            if (member.alive)
            {
                if (returnChar.alive)
                {
                    if (member.hp > returnChar.hp)
                    {
                        returnChar = member;
                    }
                }
                else
                {
                    returnChar = member;
                }
            }
        }
        return returnChar;
    }


    public static Character SelectRicochet(Character[] party, Character currentTarget)
    {
        Character newTarget = party[ThreadSafeRandom.Next(party.Length)];
        while (true)
        {
            if (newTarget != currentTarget || newTarget.alive)
            {
                break;
            }
            newTarget = party[ThreadSafeRandom.Next(party.Length)];
        }
        return newTarget;
    }
    public static void DamageApplication(int attackValue, Character target, Character author, Character[] party, Character[] opponents)
    {
        int scenario = DefensiveProcCase(target);
        bool isBlocked = false;
        switch (scenario)
        {
            case 0: // evade
                break;
            case 1: //block
                if (!target.eruptionUsed)
                {
                    HitAbsorbed(attackValue, target);
                    target.eruptionUsed = true;
                    break;
                }
                isBlocked = true;
                Hit(attackValue, target, author, isBlocked, opponents, party);
                if (target.alive) target.pet.PetSelection(target, opponents, party, PetProcType.GetHit);
                break;
            default: //normal
                Hit(attackValue, target, author, isBlocked, opponents, party);
                if (target.alive) target.pet.PetSelection(target, opponents, party, PetProcType.GetHit);
                break;
        }
        author.pet.PetSelection(author, party, opponents, PetProcType.PerHit);
    }

}
