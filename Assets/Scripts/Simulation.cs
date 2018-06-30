using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;
using System.Threading;

public class Simulation {

    protected Character[] heroes;
    protected Character[] enemies;
    protected bool isNotHero = false;
    protected int difficultyModifier;
    public float winRate;
    protected int progressionBar = 0;
    protected Slider slider;
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


    public void Run(int boss, Action<float> callback, Func<bool, bool> stopSim, Action<bool> simOutcome, CancellationToken ct)
    {
        if (stopSim(false)) return;
        int safetyNet = 2000;

        foreach (var hero in heroes) hero.InitialiseHero();
        SetupEnemies(boss);
        int turnCount = 0;
        float trCounter = 0;

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
                //UnityEngine.Debug.Log("turncount is " + turnCount.ToString() + " and safetyNet is " + safetyNet.ToString());
                return;
            }
        }
        simOutcome(heroesAlive);
        callback(0);
    }

    protected virtual void SetupEnemies(int boss)
    { } 

    public static int GetPartyCount(Character[] opponents)
    {
        return opponents.Count(member => member.alive);
    }
    public static Boolean IsAoeEnabled(Character[] opponents)
    {
        if (opponents.Count(member => member.alive) > 2) return Boolean.True;
        else return Boolean.False;
    }

}
