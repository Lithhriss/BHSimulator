using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

class Logic
{
    public static Random random = new Random(Guid.NewGuid().GetHashCode());
    #region Old Code
    //methods for game logic
    public static bool RNGroll(float a)
    {
        bool outcome;
        float chance = a * 10f;
        float roll = random.Next(1000);
        if (roll <= chance)
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
        float tr = 0f;
        tr = ((agility + power) / 2f);
        tr = (float)Math.Pow(tr, 2);
        tr = tr / (100f * power);
        return tr;
    }


    public static void HpPerc()
    {
        int i;
        for (i = 0; i < 5; i++)
        {
            if (RaidSimulation.hero[i].alive)
            {
                RaidSimulation.hero[i].hpPerc = (float)(RaidSimulation.hero[i].hp) / (float)(RaidSimulation.hero[i].maxHp);
            }
            else
            {
                RaidSimulation.hero[i].hpPerc = 10;
            }
        }
    }

    public static int HealLogic()
    {
        int i;
        int lowest = 0;
        HpPerc();
        for (i = 0; i < 4; i++)
        {
            if (RaidSimulation.hero[lowest].hpPerc >= RaidSimulation.hero[i + 1].hpPerc)
            {
                if (RaidSimulation.hero[i + 1].alive)
                {
                    lowest = i + 1;
                }
                else
                {
                    if (!RaidSimulation.hero[lowest].alive)
                    {
                        lowest = i + 1;
                    }
                }
            }
        }
        return lowest;
    }

    public static int FindLowestHealth()
    {
        int lowest = 0;
        for (int i = 0; i < 4; i++)
        {
            if (RaidSimulation.hero[i].hp >= RaidSimulation.hero[i + 1].hp)
            {
                if (RaidSimulation.hero[i + 1].alive)
                {
                    lowest = i + 1;
                }
                else
                {
                    if (!RaidSimulation.hero[lowest].alive)
                    {
                        lowest = i + 1;
                    }
                }
            }
        }
        return lowest;
    }

    public static int TargetSelection(int method)
    {
        int target = 0;
        int i = 0;
        bool targetLocked = false;

        switch (method)
        {
            case 1:
                while (!targetLocked)
                {
                    if (RaidSimulation.hero[i].alive)
                    {
                        target = i;
                        targetLocked = true;
                    }
                    i++;
                }
                break;
            case 2:
                i = 4;
                while (!targetLocked)
                {
                    if (RaidSimulation.hero[i].alive)
                    {
                        target = i;
                        targetLocked = true;
                    }
                    i--;
                }
                break;
            case 3:
                while (!targetLocked)
                {
                    i = random.Next(5);
                    if (RaidSimulation.hero[i].alive)
                    {
                        target = i;
                        targetLocked = true;
                    }
                }
                break;
            default:
                break;
        }
        return target;
    }

    // method for hero to deal damage
    public static void HeroDamageApplication(int k, int attackValue)
    {
        bool bossEvade = RNGroll(2.5f);
        if (!bossEvade)
        {
            PetLogic.PetSelection(k);
            if ((int)RaidSimulation.hero[k].weapon == 3)
            {
                switch ((int)RaidSimulation.hero[k].divinityBonus)
                {
                    case 1:
                        attackValue = Convert.ToInt32(attackValue * 1.05);
                        break;
                    case 2:
                        attackValue = Convert.ToInt32(attackValue * 1.05);
                        if (RaidSimulation.hpDummy < Convert.ToInt32(RaidSimulation.maxHpDummy / 4))
                        {
                            attackValue = Convert.ToInt32(attackValue * 1.30);
                        }
                        break;
                    default:
                        break;
                }
            }
            if (RaidSimulation.hero[k].bushidoBonus)
            {
                attackValue = Convert.ToInt32(attackValue * 1.10);
            }
            RaidSimulation.hpDummy -= attackValue;
            if (RaidSimulation.hero[k].drain)
            {
                RaidSimulation.hero[k].hp += attackValue;
                if (RaidSimulation.hero[k].hp > RaidSimulation.hero[k].maxHp)
                {
                    RaidSimulation.hero[k].hp = RaidSimulation.hero[k].maxHp;
                }
            }
            if (RaidSimulation.hero[k].lifeSteal > 0f)
            {
                RaidSimulation.hero[k].hp = RaidSimulation.hero[k].hp + Convert.ToInt32(attackValue * RaidSimulation.hero[k].lifeSteal);
            }
        }

    }

    // following statements to choose a def proc and to select the redirected target
    public static int DefensiveProcCase(int k)
    {
        int scenario = 10;
        if (RNGroll(RaidSimulation.hero[k].blockChance)) { scenario = 3; }
        if (RNGroll(RaidSimulation.hero[k].evadeChance)) { scenario = 2; }
        if (RNGroll(RaidSimulation.hero[k].deflectChance)) { scenario = 1; }
        if (RNGroll(RaidSimulation.hero[k].absorbChance)) { scenario = 0; }
        return scenario;
    }

    public static int RedirectSelection(int k)
    {
        int redirectCountLive = RaidSimulation.redirectCount;
        while (redirectCountLive > 0)
        {//redirect loop will run only if at least one member has the rune
            for (int i = 0; i < 5; i++)
            {
                if (RaidSimulation.hero[i].metaRune == Character.MetaRune.Redirect && RaidSimulation.hero[i].redirect && RaidSimulation.hero[i].alive)
                { //3 part condition, that they have rune, that their last redirect roll was successful and alive
                    RaidSimulation.hero[i].redirect = RNGroll(25f);
                    if (!RaidSimulation.hero[i].redirect)
                    {
                        redirectCountLive--;
                    }
                    else
                    {
                        k = i;
                        if (redirectCountLive == 1)
                        {//if only one member has the rune. will stop the loop to lock itself as target
                            redirectCountLive = 0;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < 5; i++)
        { //reset redirect rolls to true
            if (RaidSimulation.hero[i].metaRune == Character.MetaRune.Redirect)
            {
                RaidSimulation.hero[i].redirect = true;
            }
        }
        return k;
    }


    // following methods used when defensiveproc is successful in boss' damage application method
    public static void HeroAbsorb(int attackValue, int k)
    {
        RaidSimulation.hero[k].shield += attackValue;
        if (RaidSimulation.hero[k].shield > RaidSimulation.hero[k].maxShield)
        {
            RaidSimulation.hero[k].shield = RaidSimulation.hero[k].maxShield;
        }
    }

    public static void HeroDeflect(int attackValue, int k)
    {
        RaidSimulation.hpDummy -= attackValue;
        if (RaidSimulation.dummyDrain)
        {
            RaidSimulation.hpDummy += attackValue;
        }
        if (RaidSimulation.dummySelfInjure)
        {
            RaidSimulation.hpDummy -= Convert.ToInt32(attackValue * 0.10);
        }
    }

    public static void HeroBlock(int attackValue, int k)
    {
        float reductionModifier;
        reductionModifier = 1f - (RaidSimulation.hero[k].damageReduction / 100f);
        if (RaidSimulation.hero[k].bushidoBonus)
        {
            attackValue = Convert.ToInt32(attackValue * 1.10);
        }
        attackValue = Convert.ToInt32(0.5 * attackValue);
        attackValue = Convert.ToInt32(attackValue * reductionModifier);
        if (RaidSimulation.dummyDrain)
        {
            RaidSimulation.hpDummy += attackValue;
        }
        if (RaidSimulation.dummySelfInjure)
        {
            RaidSimulation.hpDummy -= Convert.ToInt32(attackValue * 0.10);
        }
        if (RaidSimulation.hero[k].shield > 0)
        {
            if (attackValue > RaidSimulation.hero[k].shield)
            {
                attackValue -= RaidSimulation.hero[k].shield;
                RaidSimulation.hero[k].shield = 0;
            }
            else
            {
                RaidSimulation.hero[k].shield -= attackValue;
                attackValue = 0;
            }
        }
        RaidSimulation.hero[k].hp -= attackValue;
        if (RaidSimulation.hero[k].hp <= 0)
        {
            RaidSimulation.aliveCount--;
        }
        else
        {
            PetLogic.PetSelection(k);
        }
    }

    public static void HeroNormal(int attackValue, int k)
    {
        if (RaidSimulation.hero[k].bushidoBonus)
        {
            attackValue = Convert.ToInt32(attackValue * 1.10);
        }
        if (RaidSimulation.dummySelfInjure)
        {
            RaidSimulation.hpDummy -= Convert.ToInt32(attackValue * 0.10);
        }

        float reductionModifier = 1f - (RaidSimulation.hero[k].damageReduction / 100f);
        attackValue = Convert.ToInt32((float)attackValue * reductionModifier);
        if (RaidSimulation.dummyDrain)
        {
            RaidSimulation.hpDummy += attackValue;
        }
        if (RaidSimulation.hero[k].shield > 0)
        {
            if (attackValue > RaidSimulation.hero[k].shield)
            {
                attackValue -= RaidSimulation.hero[k].shield;
                RaidSimulation.hero[k].shield = 0;
            }
            else
            {
                RaidSimulation.hero[k].shield -= attackValue;
                attackValue = 0;
            }
        }
        RaidSimulation.hero[k].hp -= attackValue;
        if (RaidSimulation.hero[k].hp <= 0)
        {
            RaidSimulation.aliveCount--;
            if (RaidSimulation.hero[k].metaRune == Character.MetaRune.Redirect)
            {
                RaidSimulation.redirectCount--;
            }
        }
        else
        {
            PetLogic.PetSelection(k);
        }
    }


    //Hero skills
    public static void HeroAttak0SP(int k, bool DS)
    {
        int attackValue = SkillList.NormalAttack(RaidSimulation.hero[k].power);
        if (Logic.RNGroll(RaidSimulation.hero[k].critChance))
        {
            attackValue = Convert.ToInt32(attackValue * RaidSimulation.hero[k].critDamage);
        }
        Logic.HeroDamageApplication(k, attackValue);
        if (DS)
        {
            attackValue = SkillList.NormalAttack(RaidSimulation.hero[k].power);
            if (Logic.RNGroll(RaidSimulation.hero[k].critChance))
            {
                attackValue = Convert.ToInt32(attackValue * RaidSimulation.hero[k].critDamage);
            }
            if (Logic.RNGroll(RaidSimulation.hero[k].empowerChance))
            {
                attackValue *= 2;
            }
            Logic.HeroDamageApplication(k, attackValue);
        }
    }

    #endregion

    #region New Code

    public static bool IsHealingNeeded(Character[] party)
    {
        HpPerc(party);
        foreach (var member in party)
        {
            if (member.alive && member.hpPerc < 0.95f) return true;
        }
        return false;
    }
    public static void HitAbsorbed(int attackValue, Character target)
    {
        target.shield += attackValue;
        if (target.shield > target.maxShield)
        {
            target.shield = target.maxShield;
        }
    }
    public static void Hit(int attackValue, Character target, Character author, bool isBlocked, Character[] opponents, Character[] party)
    {
        float attackModifier = 1f;
        float reductionModifier = 0f;
        if (target.obliterationBonus == Character.ObliterationBonus.Bonus_4_of_4 && WorldBossSimulation.GetPartyCount(opponents) == opponents.Length) reductionModifier += 0.15f;
        int position = Array.IndexOf(opponents, target);
        for (int i = position; i < 0; i--)
        {
            if (opponents[i].obliterationBonus == Character.ObliterationBonus.Bonus_2_of_4) reductionModifier += 0.05f;
        }
        position = Array.IndexOf(party, author);
        for (int i = position; i < 0; i--)
        {
            if (party[i].obliterationBonus == Character.ObliterationBonus.Bonus_3_of_4) attackModifier += 0.05f;
        }
        if (target.bushidoBonus) attackModifier += 0.1f; 
        if (author.bushidoBonus) attackModifier += 0.1f;
        if (author.nightVisageBonus && author.hpPerc >= 0.99f) attackModifier += 0.05f;
        if (WorldBossSimulation.GetPartyCount(opponents) == 1 && author.conductionBonus == Character.ConductionBonus.Bonus_4_of_4) attackModifier += 0.25f;
        if (author.divinityBonus == Character.DivinityBonus.Bonus_3_of_3 && target.hpPerc <= 0.3f) attackModifier += 0.30f;

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
        if (!target.alive)
        {
            target.hp = -1;
            target.hp += ConsumptionProc(opponents);
        }
        if (!target.alive && target.illustriousBonus == Character.IllustriousBonus.Bonus_3_of_3)
        {
            target.hp = target.power;
            if (target.hp > target.maxHp) target.hp = target.maxHp;
            target.illustriousBonus = Character.IllustriousBonus.None;
        }
    }
    public static int ConsumptionProc(Character[] party)
    {

        if (party.Count(member => member.consumptionBonus) > 0)
        {
            foreach (var member in party)
            {
                if (member.consumptionBonus && RNGroll(5f))
                {
                    return member.power;
                }
            }
            return 0;
        }
        else
        {
            return 0;
        }
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
        if (RNGroll(hero.blockChance)) { scenario = 1; }
        if (RNGroll(hero.evadeChance)) { scenario = 0; }
        return scenario;
    }
    public static Character RedirectSelection(Character target, Character[] party)
    {
        Character targetHero = target;
        int redirectCountLive = CountRedirect(party);
        while (redirectCountLive > 0)
        {//redirect loop will run only if at least one member has the rune
            for (int i = 0; i < 5; i++)
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
    public static void HpPerc(Character[] party)
    {
        int i;
        for (i = 0; i < party.Length; i++)
        {
            if (party[i].alive)
            {
                party[i].hpPerc = (float)(party[i].hp) / (float)(party[i].maxHp);
            }
            else
            {
                party[i].hpPerc = 100;
            }
        }
    }
    public static int HealFindWeakestPerc(Character[] heroes)
    {
        int i;
        int lowest = 0;
        HpPerc(heroes);
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
        return lowest;
    }
    public static Character SelectTarget(Character[] party)
    {
        while (true)
        {
            int target = random.Next(party.Length);
            if (party[target].hp > 0) return party[target];
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
        return party.OrderBy(hero => hero.hp).First();
    }
    public static Character SelectRicochet(Character[] party, Character currentTarget)
    {
        Character newTarget = party[random.Next(party.Length)];
        while (true)
        {
            if (newTarget != currentTarget || newTarget.alive)
            {
                break;
            }
        }
        return newTarget;
    }
    public static void HeroDamageApplication(Character hero, Character[] heroes, Character[] enemies, int attackValue, Character target)
    {
        bool bossEvade = RNGroll(2.5f);
        if (!bossEvade)
        {
            PetLogic.PetSelection(hero, heroes, enemies);
            if ((int)hero.weapon == 3)
            {
                switch ((int)hero.divinityBonus)
                {
                    case 1:
                        attackValue = Convert.ToInt32(attackValue * 1.05);
                        break;
                    case 2:
                        attackValue = Convert.ToInt32(attackValue * 1.05);
                        if (target.hp < Convert.ToInt32(target.maxHp / 4))
                        {
                            attackValue = Convert.ToInt32(attackValue * 1.30);
                        }
                        break;
                    default:
                        break;
                }
            }
            if (hero.bushidoBonus)
            {
                attackValue = Convert.ToInt32(attackValue * 1.10);
            }
            target.hp -= attackValue;
            if (hero.drain)
            {
                hero.hp += attackValue;
                if (hero.hp > hero.maxHp)
                {
                    hero.hp = hero.maxHp;
                }
            }
            if (hero.lifeSteal > 0f)
            {
                hero.hp = hero.hp + Convert.ToInt32(attackValue * hero.lifeSteal);
                if (hero.hp > hero.maxHp)
                {
                    hero.hp = hero.maxHp;
                }
            }
        }

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
                isBlocked = true;
                Hit(attackValue, target, author, isBlocked, opponents, party);
                if (target.alive) PetLogic.PetSelection(target, opponents, party);
                break;
            default: //normal
                Hit(attackValue, target, author, isBlocked, opponents, party);
                if (target.alive) PetLogic.PetSelection(target, opponents, party);
                break;
        }
        PetLogic.PetSelection(author, party, opponents);
    }
    public static void HeroAttak0SP(Character hero, Character[] heroes, Character[] enemies)
    {
        int attackValue = SkillList.NormalAttack(hero.power);
        if (RNGroll(hero.critChance))
        {
            attackValue = Convert.ToInt32(attackValue * hero.critDamage);
        }
        HeroDamageApplication(hero, heroes, enemies, attackValue, SelectFront(enemies));
        if (RNGroll(hero.dsChance))
        {
            attackValue = SkillList.NormalAttack(hero.power);
            if (RNGroll(hero.critChance))
            {
                attackValue = Convert.ToInt32(attackValue * hero.critDamage);
            }
            if (RNGroll(hero.empowerChance))
            {
                attackValue *= 2;
            }
            HeroDamageApplication(hero, heroes, enemies, attackValue, SelectFront(enemies));
        }
    }

    #endregion
}
