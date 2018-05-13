using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;

public class RaidSimulation
{
    public Character[] heroes = new Character[5];
    public Character[] enemies = new Character[1];
    private bool isNotHero = false;
    public int difficultyModifier;
    public float winRate;
    public int progressionBar = 0;
    private Slider slider;
    public int aliveCount = 5;
    public int games = 1000;//number of times fight will run.
    private bool heroesAlive
    {
        get
        {
            return GetPartyCount(heroes) > 0;
        }
    }
    private bool enemiesAlive
    {
        get
        {
            return GetPartyCount(enemies) > 0;
        }
    }
    private bool matchOver
    {
        get
        {
            return !heroesAlive || !enemiesAlive;
        }
    }

    public IEnumerator Simulation(int boss, System.Action<float> callback)
    {
        slider = UnityEngine.GameObject.Find("Progress").GetComponent<Slider>();
        int p;
        //if (boss == 3)
        //{
        //    enemies = new Character[2];
        //}
        float win = 0;
        float lose = 0;

        int games = 1000;//number of times fight will run.
        int gameDivider = Convert.ToInt32(games / 100);
        progressionBar = 0;

        foreach (Character hero in heroes)
        {  //initialisation
            hero.InitialiseHero();
        }
        for (p = 0; p < games; p++)
        {
            float trCounter = 0;
            SetupEnemies(boss);
            Character[] charArray = new Character[heroes.Length + enemies.Length];
            int charIndex = 0;
            foreach (Character hero in heroes)
            {
                hero.Revive();
                trCounter += hero.turnRate;
                charArray[charIndex] = hero;
                charIndex++;
            }
            foreach (Character enemy in enemies)
            {
                trCounter += enemy.turnRate;
                charArray[charIndex] = enemy;
                charIndex++;
            }
            charArray = charArray.OrderByDescending(chr => chr.turnRate).ToArray();

            while (heroesAlive && enemiesAlive)
            {
                foreach (Character character in charArray)
                {
                    if (character.alive)
                    {
                        character.IncrementCounter();
                        if (character.counter > trCounter)
                        {
                            if (character._isHero)
                            {
                                character.IncrementSp(heroes);
                                character.ActivateOnTurnPassives();
                                if (character.pet != null) character.pet.PetSelection(character, heroes, enemies, PetProcType.PerTurn);
                                if (matchOver) break;
                                character.ChooseSkill(heroes, enemies);
                            }
                            else
                            {
                                character.IncrementSp(enemies);
                                if (character.pet != null) character.pet.PetSelection(character, enemies, heroes, PetProcType.PerTurn);
                                if (matchOver) break;
                                character.ChooseSkill(enemies, heroes);
                            }
                            character.SubstractCounter(trCounter);
                        }
                    }
                }
            }
            if (heroesAlive)
            {
                win++;
            }
            else
            {
                lose++;
            }
            if ((float)p % gameDivider == 0 && p > 0)
            {
                progressionBar += 1;
                slider.value = progressionBar;
                winRate = (win / p) * 100;
                yield return null;
            }
        }


        winRate = (win / p) * 100;
        callback(winRate);
    }

    private void SetupEnemies(int boss)
    {
        //int bossType = rand.Next(3);
        enemies[0] = GetRaidBoss(boss);
        enemies[0].InitialiseMobs();
    }




    private Character GetRaidBoss(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(10, 18, 4, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1.9, isNotHero, "Kaleido");
            case 1:
                return new Character(10, 18, 4, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 0.9, isNotHero, "Woodbeard");
            case 2:
                return new Character(10, 18, 4, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1.9, isNotHero, "Robomech");
            default:
                return new Character(8, 23, 5, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 0.9, isNotHero, "Zol"); // prio subject to change
        }
    }

    private Character GetRaidTrash(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(3.4f, 4, 4.6f, 10f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 2, isNotHero, "ArcherOrc");
            case 1:
                return new Character(6.2f, 2f, 1.8f, 10f, 50f, 15f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1, isNotHero, "MageOrc");
            default:
                return new Character(5.8f, 3.8f, 2.4f, 30f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 2, isNotHero, "AssassinOrc");
        }
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
    public void Log(string str)
    {
        UnityEngine.Debug.Log(str);
    }
}