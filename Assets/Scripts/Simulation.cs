using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


public class Simulation
{
    protected int instanceNumber;
    protected Character[] heroes;
    protected Character[] enemies;
    protected Character _boss;
    protected bool isNotHero = false;
    protected int difficultyModifier;
    public float winRate;

    protected bool heroesAlive
    {
        get
        {
            return GetPartyCount(heroes) > 0;
        }
    }
    protected bool enemiesAlive
    {
        get
        {
            return GetPartyCount(enemies) > 0;
        }
    }
    protected bool simOver
    {
        get
        {
            return !heroesAlive || !enemiesAlive;
        }
    }


    public void Run(int boss, Action<float> callback, Func<bool, bool> stopSim, Action<bool> simOutcome)
    {
        if (stopSim(false)) return;
        int safetyNet = 4000;

        foreach (var hero in heroes) hero.InitialiseHero();
        SetupEnemies(boss);
        int turnCount = 0;
        float trCounter = 0;
        Character[] charArray = InitCharacterArray(ref trCounter);
        while (heroesAlive && enemiesAlive)
        {
            foreach (var character in charArray)
            {
                if (character.alive)
                {
                    character.IncCounter();
                    if (character.counter > trCounter)
                    {
                        if (character._isHero)
                        {
                            character.IncSp(heroes);
                            character.ActivateOnTurnPassives(heroes, enemies);
                            if (character.pet != null) character.pet.PetSelection(character, heroes, enemies, PetProcType.PerTurn);
                            if (simOver) break;
                            character.ChooseSkill(heroes, enemies);
                            turnCount++;
                        }
                        else
                        {
                            character.IncSp(enemies);
                            if (character.pet != null) character.pet.PetSelection(character, enemies, heroes, PetProcType.PerTurn);
                            if (simOver) break;
                            character.ChooseSkill(enemies, heroes);
                        }
                        character.SubCount(trCounter);
                    }
                }
            }
            if (stopSim(turnCount > safetyNet))
            {
                UnityEngine.Debug.Log("Instance = " + instanceNumber + " || TurnCount = " + turnCount);
                UnityEngine.Debug.Log("hero hp = " + heroes[0].hp.ToString() + "|| hero shield = " + heroes[0].shield.ToString());
                foreach (var mob in enemies)
                {
                    UnityEngine.Debug.Log("enemy hp = " + mob.hp.ToString() + "|| enemy shield = " + mob.shield.ToString() + "|| enemy name = " + mob.name);
                }
                return;
            }
        }
        simOutcome(heroesAlive);
        callback(0);
    }

    protected virtual void SetupEnemies(int boss)
    { }

    protected Character[] InitCharacterArray(ref float trCounter)
    {
        Character[] charArray = new Character[heroes.Length + enemies.Length];
        int charIndex = 0;
        foreach (var hero in heroes)
        {
            hero.Revive();
            trCounter += hero.turnRate;
            charArray[charIndex] = hero;
            charIndex++;
        }
        foreach (var enemy in enemies)
        {
            trCounter += enemy.turnRate;
            charArray[charIndex] = enemy;
            charIndex++;
        }
        charArray = charArray.OrderByDescending(chr => chr.turnRate).ToArray();
        return charArray;
    }

    public static int GetPartyCount(Character[] opponents)
    {
        return opponents.Count(member => member.alive);
    }
    public static Boolean IsAoeEnabled(Character[] opponents)
    {
        if (opponents.Count(member => member.alive) > 2) return Boolean.True;
        else return Boolean.False;
    }

    private void ShowCurrentBattleStatus()
    {
        var one = "hero hp = "+ heroes[0].hp.ToString() + "|| hero shield = " + heroes[0].shield.ToString();
        var two = "hero hp = " + _boss.hp.ToString() + "|| hero shield = " + _boss.shield.ToString();
        UnityEngine.Debug.Log(one);
        UnityEngine.Debug.Log(two);
    }
}
