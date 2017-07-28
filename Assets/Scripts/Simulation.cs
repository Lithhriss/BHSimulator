using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class Simulation
{
    public static Hero[] hero = new Hero[5];
    public static int dummyPower = 1600, dummyStamina = 2880, dummyAgility = 640, hpDummy, spDummy = 0;
    public static bool dummyDrain = false;
    public static bool dummySelfInjure = false;
    public static float winRate;
    public static int progressionBar = 0;
    private static Slider slider;
    public static int redirectCount = 0;
    public static int aliveCount = 5;

    public static void simulation()
    {
        slider = UnityEngine.GameObject.Find("Progress").GetComponent<Slider>();
        int p;
        int i;

        float win = 0;
        float lose = 0;
        //float winRate;

        int games = 1000; //number of times fight will run.
        int playerNo = 5;
        int counterMax = 100;
        int cycle;

        float dummyTR;
        float dummyInterval;
        float dummyCounter = 0;

        bool DS;
        bool teamAlive = true;
        progressionBar = 0;

        /*hero[0].power = 452;
            hero[1].power = 600;
            hero[2].power = 1085;
            hero[3].power = 600;
            hero[4].power = 100;
            hero[0].stamina = 704;
            hero[1].stamina = 205;
            hero[2].stamina = 135;
            hero[3].stamina = 205;
            hero[4].stamina = 1010;
            hero[0].agility = 101;
            hero[1].agility = 600;
            hero[2].agility = 69;
            hero[3].agility = 555;
            hero[4].agility = 100;
            hero[0].critChance = 10f;
            hero[1].critChance = 29f;
            hero[2].critChance = 15f;
            hero[3].critChance = 10f;
            hero[4].critChance = 10f;
            hero[0].critDamage = 50f;
            hero[1].critDamage = 50f;
            hero[2].critDamage = 50f;
            hero[3].critDamage = 50f;
            hero[4].critDamage = 50f;
            hero[0].dsChance = 2.5f;
            hero[1].dsChance = 7.5f;
            hero[2].dsChance = 18f;
            hero[3].dsChance = 10f;
            hero[4].dsChance = 2.5f;
            hero[0].blockChance = 31f;
            hero[1].blockChance = 0f;
            hero[2].blockChance = 0f;
            hero[3].blockChance = 0f;
            hero[4].blockChance = 40f;
            hero[0].evadeChance = 14f;
            hero[1].evadeChance = 2.5f;
            hero[2].evadeChance = 2.5f;
            hero[3].evadeChance = 2.5f;
            hero[4].evadeChance = 12.5f;
            hero[0].deflectChance = 5f;
            hero[1].deflectChance = 0f;
            hero[2].deflectChance = 0f;
            hero[3].deflectChance = 0f;
            hero[4].deflectChance = 5f;
            hero[0].powerRunes = 0f;
            hero[1].powerRunes = 22f;
            hero[2].powerRunes = 15.5f;
            hero[3].powerRunes = 16f;
            hero[4].powerRunes = 0f;
            hero[0].agilityRunes = 0f;
            hero[1].agilityRunes = 0f;
            hero[2].agilityRunes = 0f;
            hero[3].agilityRunes = 2.5f;
            hero[4].agilityRunes = 0f;
            hero[0].pets = "gemmi";
            hero[1].pets = "nelson";
            hero[2].pets = "gemmi";
            hero[3].pets = "nelson";
            hero[4].pets = "gemmi";
            hero[0].pet = Hero.Pet.Gemmi;
            hero[1].pet = Hero.Pet.Nelson;
            hero[2].pet = Hero.Pet.Gemmi;
            hero[3].pet = Hero.Pet.Nelson;
            hero[4].pet = Hero.Pet.Gemmi;*/


        for (i = 0; i < 5; i++)
        {  //initialisation
            if (hero[i].redirectRune) {
                redirectCount++;
            }
            hero[i].powerRunes = (100f + hero[i].powerRunes) / 100f;
            hero[i].agilityRunes = (100f + hero[i].agilityRunes) / 100f;
            hero[i].critDamage = (100f + hero[i].critDamage) / 100f;
            hero[i].staminaRunes = (100f + hero[i].staminaRunes) / 100f;
            hero[i].turnRate = Logic.turnRate(hero[i].power, hero[i].agility);
            hero[i].power = Convert.ToInt32(hero[i].power * hero[i].powerRunes);
            hero[i].turnRate *= hero[i].agilityRunes;
            hero[i].hp = Convert.ToInt32(hero[i].stamina * 10 * hero[i].staminaRunes);
            hero[i].maxHp = hero[i].hp;
            hero[i].maxShield = Convert.ToInt32(hero[i].maxHp / 2);
            hero[i].interval = counterMax / hero[i].turnRate;
            hero[i].counter = 0;
            hero[i].sp = 4;
            hero[i].alive = true;
            hero[i].drain = false;
        }

        dummyTR = Logic.turnRate(dummyPower, dummyAgility);//boss init
        dummyInterval = (float)counterMax / dummyTR;


        for (p = 0; p < games; p++)
        {  // for loop to simulate as many fights as you want.


            if ((float)p % 100 == 0 && p > 0)
            {
                progressionBar += 10;
                //UnityEngine.Debug.Log(progressionBar);
                slider.value = progressionBar;
            }

            teamAlive = true;

            for (i = 0; i < 5; i++)
            {  //hero  values that need to be reset every game
                hero[i].hp = Convert.ToInt32(hero[i].stamina * 10 * hero[i].staminaRunes);
                hero[i].shield = Convert.ToInt32(hero[i].hp / 10);
                hero[i].counter = 0;
                hero[i].sp = 4;
                hero[i].alive = true;
                hero[i].redirect = true;
            }

            hpDummy = dummyStamina * 10;
            
            while (hpDummy > 0 && teamAlive == true)
            {           //fight will stop if either party is dead
                for (cycle = 1; cycle <= counterMax; cycle++)
                {
                    dummyCounter++;
                    for (i = 0; i < playerNo; i++)
                    {
                        hero[i].counter++;
                        if (hero[i].counter >= hero[i].interval && hero[i].alive)
                        {      //checks if it's player's turn to attack
                            Logic.hpPerc();
                            hero[i].sp++;
                            PetLogic.petSelection(i);
                            DS = Logic.RNGroll(hero[i].dsChance);
                            HeroLogic.weaponSelection(i, DS);
                            hero[i].counter -= hero[i].interval;
                            if (hpDummy <= 0)
                            {
                                win++;
                                i = playerNo;
                                cycle = counterMax;
                                dummyCounter = 0;
                            }
                            //UnityEngine.Debug.Log("boss hP = "+ hpDummy);

                            //Console.WriteLine("hp0 = {0}  hp1 = {1} hp2 = {2} hp3 = {3} hp4 = {4}\n", hero[0].hp, hero[1].hp, hero[2].hp, hero[3].hp, hero[4].hp);
                        }
                    }
                    if (hpDummy > 0 && dummyCounter >= dummyInterval)
                    {         //checks if it's boss' turn to attack
                        spDummy++;
                        BossLogic.kaleidoAI();
                        dummyCounter -= dummyInterval;
                        if (hpDummy <= 0)
                        {
                            win++;
                            i = playerNo;
                            cycle = counterMax;
                            dummyCounter = 0;
                        }
                        if (!hero[0].alive && !hero[1].alive && !hero[2].alive && !hero[3].alive && !hero[4].alive)
                        {
                            teamAlive = false;
                            cycle = counterMax;
                        }
                    }
                }
            }
            if (!teamAlive)
            {
                lose++;
                dummyCounter = 0;
            }

        }
        winRate = (win / games) * 100;


        //Console.WriteLine("won = %f lost = %f\n", win, lose);
        //Console.WriteLine(win);
        //Console.WriteLine(lose);
        //Console.WriteLine(games);
        //Console.WriteLine(winRate + "%");
        UnityEngine.Debug.Log(winRate);
        

    }
}