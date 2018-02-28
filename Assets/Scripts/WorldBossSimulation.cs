using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class WorldBossSimulation
{

    public Character[] heroes;
    public Character[] enemies;
    public int progressionBar = 0;
    private Slider slider;
    public int redirectCount = 0;
    private int DifficultyModifier;
    public float winRate;
    public int Games = 1000;
    Random rand;
    private bool isNotHero = false;
    private bool isHero = true;

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



    public WorldBossSimulation(int difficultyModifier)
    {
        rand = new Random(Guid.NewGuid().GetHashCode());
        DifficultyModifier = difficultyModifier;
    }

    public IEnumerator Simulation(int boss, System.Action<float> callback)
    {
        slider = UnityEngine.GameObject.Find("Progress").GetComponent<Slider>();
        int p;
        int counterMax = 100;
        redirectCount = 0;

        
        int games = 1000;
        int gameDivider = Convert.ToInt32(games / 100);
        float win = 0;
        float lose = 0;

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
                                hero.pet.PetSelection(hero, heroes, enemies, PetProcType.PerTurn);
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
                                enemy.pet.PetSelection(enemy, enemies, heroes, PetProcType.PerTurn);
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
        Character placeholder;
        Character bossPlaceholder;
        if (boss == 0)
        {
            Character dpsPlaceholder = GetOrlagDPS(rand.Next(3));
            Character tankPlaceholder = GetOrlagTank(rand.Next(2));
            placeholder = GetOrlagMix(rand.Next(5));
            bossPlaceholder = GetOrlagBoss(rand.Next(3));
            List<Character> enemyList = new List<Character>() { dpsPlaceholder, tankPlaceholder, placeholder, bossPlaceholder };
            enemies = enemyList.OrderByDescending(mob => mob.priority).ToArray();
        }
        else
        {
            enemies = new Character[2];
            bossPlaceholder = GetNetherBoss(rand.Next(3));
            placeholder = GetNetherMix(rand.Next(5));
            if (bossPlaceholder.priority > placeholder.priority)
            {
                enemies[0] = bossPlaceholder;
                enemies[1] = placeholder;
            }
            else
            {
                enemies[1] = bossPlaceholder;
                enemies[0] = placeholder;
            }
        }
        foreach (Character mob in enemies)
        {
            mob.InitialiseMobs();
        }
    }
    private Character GetOrlagBoss(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(9, 22, 9, 10f, 50f, 0f, 0f, 20f, 2.5f, 0f, 5f, 0f, 0f, 0f, 0f, DifficultyModifier, 1.9, isNotHero, "BlueOrc");
            case 1:
                return new Character(11, 18, 11, 30f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 0.9, isNotHero, "GreenOrc");
            default:
                return new Character(10, 20, 10, 10f, 50f, 10f, 10f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 1.9, isNotHero, "PurpleOrc");
        }
    }
    private Character GetOrlagMix(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(3.4f, 4, 4.6f, 10f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 2, isNotHero, "ArcherOrc");
            case 1:
                return new Character(6.2f, 2f, 1.8f, 10f, 50f, 15f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 1, isNotHero, "MageOrc");
            case 2:
                return new Character(5.8f, 3.8f, 2.4f, 30f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 2, isNotHero, "AssassinOrc");
            case 3:
                return new Character(0.4f, 11f, 0.6f, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 10f, 0f, 0f, 0f, 0f, DifficultyModifier, 4, isNotHero, "MeatOrc");
            default:
                return new Character(2.2f, 7f, 2.8f, 10f, 50f, 0f, 0f, 15f, 2.5f, 4.5f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 3, isNotHero, "BruiserOrc");
        }
    }
    private Character GetOrlagDPS(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(3.4f, 4, 4.6f, 10f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 2, isNotHero, "ArcherOrc");
            case 1:
                return new Character(6.2f, 2f, 1.8f, 10f, 50f, 15f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 1, isNotHero, "MageOrc");
            default:
                return new Character(5.8f, 3.8f, 2.4f, 30f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 2, isNotHero, "AssassinOrc");
        }
    }
    private Character GetOrlagTank(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(0.4f, 11f, 0.6f, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 10f, 0f, 0f, 0f, 0f, DifficultyModifier, 4, isNotHero, "MeatOrc");
            default:
                return new Character(2.2f, 7f, 2.8f, 10f, 50f, 0f, 0f, 15f, 2.5f, 4.5f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 3, isNotHero, "BruiserOrc");
        }
    }

    private Character GetNetherBoss(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(7f, 18f, 7f, 10f, 50f, 0f, 0f, 0f, 12.5f, 6f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 2.9, isNotHero, "BlueNether");
            case 1:
                return new Character(7.5f, 17f, 7.5f, 30f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 1.9, isNotHero, "PurpleNether");
            default:
                return new Character(8f, 16f, 8f, 50f, 50, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 0.9, isNotHero, "YellowNether");
        }
    }
    private Character GetNetherMix(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(4f, 6f, 4f, 10f, 250f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 2, isNotHero, "DemonNether");
            case 1:
                return new Character(4f, 5f, 4f, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 1, isNotHero, "ImpNether");
            case 2:
                return new Character(5.5f, 3f, 5.5f, 10f, 50f, 15f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 1, isNotHero, "MageNether");
            case 3:
                return new Character(5f, 4.5f, 5f, 10f, 50f, 0f, 0f, 0f, 2, 5f, 0f, 15f, 0f, 0f, 0f, DifficultyModifier, 2, isNotHero, "BeastNether");
            default:
                return new Character(3.5f, 7f, 3.5f, 10f, 50f, 0f, 0f, 20f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 3, isNotHero, "TankNether");
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
