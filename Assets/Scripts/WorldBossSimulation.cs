using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class WorldBossSimulation {

    public Hero[] heroes;
    public Enemy[] enemies;
    //public int difficultyModifier;
    public int progressionBar = 0;
    private Slider slider;
    public int redirectCount = 0;
    Random rand;

    private Enemy[] OrlagDpsFams;
    private Enemy[] OrlagTankFams;
    private Enemy[] OrlagBossFams;
    private Enemy[] OrlagAllFams;

    private Enemy[] NetherDpsFams;
    private Enemy[] NetherTankFams;
    private Enemy[] NetherBossFams;


    public WorldBossSimulation(int difficultyModifier)
    {
        rand = new Random(Guid.NewGuid().GetHashCode());

        OrlagBossFams = new Enemy[3]
        {
            //blue
            new Enemy(9, 22, 9, 10f, 50f, 0f, 0f, 20f, 2.5f, 0f, 5f, 0f, 0f, 0f, 0f, difficultyModifier, 1.9),
            //green
            new Enemy(18, 11, 11, 30f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 0.9),
            //purple
            new Enemy(20, 10, 10, 10f, 50f, 10f, 10f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1.9)
        };
        OrlagDpsFams = new Enemy[3]
        {
            //tilge
            new Enemy(3.4f, 4, 4.6f, 10f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 2),
            //velk
            new Enemy(6.2f, 2f, 1.8f, 10f, 50f, 15f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1),
            //drek
            new Enemy(5.8f, 3.8f, 2.4f, 30f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 2)
        };
        OrlagTankFams = new Enemy[2]
        {
            //meatwall
            new Enemy(0.4f, 11f, 0.6f, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 10f, 0f, 0f, 0f, 0f, difficultyModifier, 4),
            //garekk
            new Enemy(2.2f, 7f, 2.8f, 10f, 50f, 0f, 0f, 15f, 2.5f, 4.5f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 3),

        };
        OrlagAllFams = new Enemy[5]
        {
            //tilge
            new Enemy(3.4f, 4, 4.6f, 10f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 2),
            //velk
            new Enemy(6.2f, 2f, 1.8f, 10f, 50f, 15f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1),
            //drek
            new Enemy(5.8f, 3.8f, 2.4f, 30f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 2),
            //meatwall
            new Enemy(0.4f, 11f, 0.6f, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 10f, 0f, 0f, 0f, 0f, difficultyModifier, 4),
            //garekk
            new Enemy(2.2f, 7f, 2.8f, 10f, 50f, 0f, 0f, 15f, 2.5f, 4.5f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 3),
        };

        NetherBossFams = new Enemy[3]
        {
            //blue
            new Enemy (7f, 18f, 7f, 10f, 50f, 0f, 0f, 0f, 12.5f, 6f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 2.9),
            //purple
            new Enemy(7.5f, 17f, 7.5f, 30f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1.9),
            //yellow
            new Enemy (8f, 16f, 8f, 50f, 50, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 0.9)
        };
        NetherDpsFams = new Enemy[5]
        {
            //d3mon
            new Enemy(4f, 6f, 4f, 10f, 250f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 2),
            //Omo
            new Enemy(4f, 5f, 4f, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1),
            //Clavid
            new Enemy(5.5f, 3f, 5.5f, 10f, 50f, 15f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1),
            //Zorgim
            new Enemy(5f, 4.5f, 5f, 10f, 50f, 0f, 0f, 0f, 2,5f, 0f, 15f, 0f, 0f, 0f, difficultyModifier, 2),
            //tankfam
            new Enemy(3.5f, 7f, 3.5f, 10f, 50f, 0f, 0f, 20f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 3)
        };
        NetherTankFams = new Enemy[1]
        {
            new Enemy(3.5f, 7f, 3.5f, 10f, 50f, 0f, 0f, 20f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 3)
        };

    }

    public  IEnumerator Simulation(int boss , System.Action<float> callback)
    {
        bool heroesAlive;
        bool enemiesAlive;
        int counterMax = 100;
        redirectCount = 0;

        foreach (Hero hero in heroes)
        {  //initialisation
            if (hero.metaRune == Hero.MetaRune.Redirect)
            {
                redirectCount++;
            }
            hero.Initialise();
        }
        for (int p = 0; p < 1000; p++)
        {
            heroesAlive = true;
            enemiesAlive = true;
            SetupEnemies(boss);
            foreach (Hero hero in heroes)
            {
                hero.Revive();
            }

            while (heroesAlive && enemiesAlive)
            {
                for (int i = 0; i < counterMax; i++)
                {
                    foreach (Hero hero in heroes)
                    {
                        hero.IncrementCounter();
                        if (hero.counter > hero.interval)
                        {
                            Logic.HpPerc(heroes);
                            hero.IncrementSp();
                            PetLogic.PetSelection(hero, heroes, enemies);
                            HeroLogic.WeaponSelection(hero, heroes, enemies);
                        }
                    }
                }
            }
        }
        yield return null;
    }

    private void SetupEnemies(int boss)
    {
        int bossType = rand.Next(3);
        Enemy placeholder;
        Enemy bossPlaceholder;
        if (boss == 0)
        {
            Enemy dpsPlaceholder = OrlagDpsFams[rand.Next(OrlagDpsFams.Length)];
            Enemy tankPlaceholder = OrlagTankFams[rand.Next(OrlagTankFams.Length)];
            placeholder = OrlagAllFams[rand.Next(OrlagAllFams.Length)];
            bossPlaceholder = OrlagBossFams[rand.Next(OrlagBossFams.Length)];
            List<Enemy> enemyList = new List<Enemy>() { dpsPlaceholder, tankPlaceholder, placeholder, bossPlaceholder };
            enemies = enemyList.OrderByDescending(mob => mob.priority).ToArray();
        }
        else
        {
            enemies = new Enemy[2];
            bossPlaceholder = NetherBossFams[rand.Next(NetherBossFams.Length)];
            placeholder = NetherDpsFams[rand.Next(NetherDpsFams.Length)];
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

    }


    }
