using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class RaidSimulation
{
    public static Character[] hero = new Character[5];
    public static int dummyPower = 10, dummyStamina = 18, dummyAgility = 4, hpDummy, maxHpDummy, spDummy = 0;
    public static int difficultyModifier;
    public static bool dummyDrain = false;
    public static bool dummySelfInjure = false;
    public static float winRate;
    public static int progressionBar = 0;
    private static Slider slider;
    public static int redirectCount = 0;
    public static int aliveCount = 5;
    public static int games = 1000;//number of times fight will run.


	public static IEnumerator Simulation(System.Action<float> callback)
    {
        slider = UnityEngine.GameObject.Find("Progress").GetComponent<Slider>();
        UnityEngine.Debug.Log("simulatoin accessed");
        int p;
        int i;
        redirectCount = 0;

        float win = 0;
        float lose = 0;

        int games = 1000;//number of times fight will run.
        int gameDivider = Convert.ToInt32(games / 100);
        int playerNo = 5;
        int counterMax = 100;
        int cycle;

        float dummyTR;
        float dummyInterval;
        float dummyCounter = 0;
        dummyPower = 10 * difficultyModifier;
        dummyAgility = 4 * difficultyModifier;
        dummyStamina = 18 * difficultyModifier;


        bool DS;
        bool teamAlive = true;
        progressionBar = 0;

        for (i = 0; i < 5; i++)
        {  //initialisation
            if (hero[i].metaRune == Character.MetaRune.Redirect)
            {
                redirectCount++;
            }
  
            hero[i].powerRunes = (100f + hero[i].powerRunes) / 100f;
            hero[i].agilityRunes = (100f + hero[i].agilityRunes) / 100f;
            hero[i].critDamage = (100f + hero[i].critDamage) / 100f;
            hero[i].staminaRunes = (100f + hero[i].staminaRunes) / 100f;
            hero[i].turnRate = Logic.TurnRate(hero[i].power, hero[i].agility);
            hero[i].power = Convert.ToInt32(hero[i].power * hero[i].powerRunes);
            hero[i].turnRate *= hero[i].agilityRunes;
            hero[i].hp = Convert.ToInt32(hero[i].stamina * 10 * hero[i].staminaRunes);
            hero[i].maxHp = hero[i].hp;
            hero[i].maxShield = Convert.ToInt32(hero[i].maxHp / 2);
            hero[i].interval = counterMax / hero[i].turnRate;
            hero[i].counter = 0;
            hero[i].sp = 2;
            hero[i].drain = false;
        }

        dummyTR = Logic.TurnRate(dummyPower, dummyAgility);//boss init
        dummyInterval = (float)counterMax / dummyTR;
        for (p = 0; p < games; p++)
        {  // for loop to simulate as many fights as you want.
            teamAlive = true;
            aliveCount = 5;

            for (i = 0; i < 5; i++)
            {  //hero  values that need to be reset every game
                hero[i].hp = Convert.ToInt32(hero[i].stamina * 10 * hero[i].staminaRunes);
                hero[i].shield = 0;
                hero[i].counter = 0;
                hero[i].sp = 0;
                hero[i].redirect = true;
            }

            // reset boss values after each game
            hpDummy = dummyStamina * 10;
            if (Launch.bossDiff == 2)
            {
                hpDummy += 20000; 
            }
            maxHpDummy = hpDummy;
            spDummy = 0;
            
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
                            Logic.HpPerc();
                            hero[i].sp++;
                            PetLogic.PetSelection(i);
                            DS = Logic.RNGroll(hero[i].dsChance);
                            HeroLogic.WeaponSelection(i, DS);
                            hero[i].counter -= hero[i].interval;
                            if (hpDummy <= 0)
                            {
                                win++;
                                i = playerNo;
                                cycle = counterMax;
                                dummyCounter = 0;
                            }
                        }
                    }
                    if (hpDummy > 0 && dummyCounter >= dummyInterval)
                    {         //checks if it's boss' turn to attack
                        spDummy++;
                        switch (Launch.bossDiff)
                        {
                            case 0:
                                BossLogic.KaleidoAI();
                                break;
                            case 1:
                                BossLogic.WoodbeardAI();
                                break;
                            case 2:
                                BossLogic.RoboMechAI();
                                break;
                        }
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
}