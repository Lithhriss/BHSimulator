using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;

public class RaidSimulation
{
    public  Character[] heroes = new Character[5];
    public Character[] enemies = new Character[1];
    private bool isNotHero = false;
    public  int difficultyModifier;
    public  float winRate;
    public  int progressionBar = 0;
    private  Slider slider;
    public  int redirectCount = 0;
    public  int aliveCount = 5;
    public  int games = 1000;//number of times fight will run.
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

    public  IEnumerator Simulation(int boss, System.Action<float> callback)
    {
        slider = UnityEngine.GameObject.Find("Progress").GetComponent<Slider>();
        UnityEngine.Debug.Log("simulatoin accessed");
        int p;
        redirectCount = 0;

        float win = 0;
        float lose = 0;

        int games = 1000;//number of times fight will run.
        int gameDivider = Convert.ToInt32(games / 100);
        int counterMax = 100;
        progressionBar = 0;

        foreach (Character hero in heroes)
        {  //initialisation
            if (hero.metaRune == Character.MetaRune.Redirect)
            {
                redirectCount++;
            }
            hero.InitialiseHero();
        }
        for (p = 0; p < games; p++)
        {
            SetupEnemies(boss);
            foreach (Character hero in heroes)
            {
                hero.Revive();
            }

            while (heroesAlive && enemiesAlive)
            {
                for (int i = 0; i < counterMax; i++)
                {
                    foreach (Character hero in heroes)
                    {
                        if (hero.alive)
                        {
                            hero.IncrementCounter();
                            if (hero.counter > hero.interval)
                            {
                                Logic.HpPerc(heroes);
                                Logic.HpPerc(enemies);
                                hero.IncrementSp();
                                PetLogic.PetSelection(hero, heroes, enemies);
                                if (matchOver) break;
                                hero.ChooseSkill(heroes, enemies);
                                hero.SubstractCounter();
                            }
                        }
                    }
                    if (matchOver) break;
                    foreach (Character enemy in enemies)
                    {
                        if (enemy.alive)
                        {
                            enemy.IncrementCounter();
                            if (enemy.counter > enemy.interval)
                            {
                                Logic.HpPerc(enemies);
                                Logic.HpPerc(heroes);
                                enemy.IncrementSp();
                                PetLogic.PetSelection(enemy, enemies, heroes);
                                if (matchOver) break;
                                enemy.ChooseSkill(enemies, heroes);
                                enemy.SubstractCounter();
                            }
                        }
                    }
                    if (matchOver) break;
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
            default:
                return new Character(10, 18, 4, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1.9, isNotHero, "Robomech");
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
}