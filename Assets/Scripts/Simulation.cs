using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Simulation : MonoBehaviour {

    public HeroPanel hero_1;
    public HeroPanel hero_2;
    public HeroPanel hero_3;
    public HeroPanel hero_4;
    public HeroPanel hero_5;
    public static Hero[] hero = new Hero[5];
    public static int dummyPower = 1600, dummyStamina = 2880, dummyAgility = 640, hpDummy, spDummy = 0;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyUp(KeyCode.Space)) {
            //StartCoroutine(simulation());
            StartCoroutine(test());
        }
    }

    public void onClickInit() {
        StartCoroutine(simulation());
        //StartCoroutine(test());
    }

    IEnumerator test() {
        int i = 0, a = 0, b = 0;
        bool te;
        int s;
        for (s = 0; s < 1; s++) {
            a = 0;
            b = 0;
            for (i = 0; i < 100000; i++)
            {
                te = Logic.RNGroll(10f);
                if (te)
                {
                    a++;
                }
                else { b++; }
            }
            Debug.Log(a);
            Debug.Log(b); }
        yield return null;
    }

    IEnumerator simulation() {
        int progressionBar = 0;
        int p;
        int i;

        float win = 0;
        float lose = 0;
        float winRate;

        int games = 1000; //number of times fight will run.
        int playerNo;
        int counterMax = 100;
        int cycle;

        float dummyTR;
        float dummyInterval;
        float dummyCounter = 0;

        bool DS;
        bool teamAlive = true;


        for (p = 0; p < games; p++)
        {  // for loop to simulate as many fights as you want.

            
            if (p % 100 == 0 && p > 0)
            {
                progressionBar += 10;
                Debug.Log(progressionBar);
            }

            teamAlive = true;
            playerNo = 5;

            hero[0].power = 452;
            hero[1].power = 600;
            hero[2].power = 1020;
            hero[3].power = 600;
            hero[4].power = 100;
            hero[0].stamina = 704;
            hero[1].stamina = 200;
            hero[2].stamina = 138;
            hero[3].stamina = 200;
            hero[4].stamina = 1010;
            hero[0].agility = 101;
            hero[1].agility = 600;
            hero[2].agility = 77;
            hero[3].agility = 555;
            hero[4].agility = 100;
            hero[0].sp = 4;
            hero[1].sp = 4;
            hero[2].sp = 4;
            hero[3].sp = 4;
            hero[4].sp = 4;
            hero[0].critChance = 10f;
            hero[1].critChance = 29f;
            hero[2].critChance = 25f;
            hero[3].critChance = 10f;
            hero[4].critChance = 10f;
            hero[0].critDamage = 1.5f;
            hero[1].critDamage = 1.5f;
            hero[2].critDamage = 1.5f;
            hero[3].critDamage = 1.5f;
            hero[4].critDamage = 1.5f;
            hero[0].dsChance = 0f;
            hero[1].dsChance = 7.5f;
            hero[2].dsChance = 18f;
            hero[3].dsChance = 10f;
            hero[4].dsChance = 0f;
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
            hero[0].powerRunes = 1f;
            hero[1].powerRunes = 1.22f;
            hero[2].powerRunes = 1.155f;
            hero[3].powerRunes = 1.16f;
            hero[4].powerRunes = 1f;
            hero[0].agilityRunes = 1f;
            hero[1].agilityRunes = 1f;
            hero[2].agilityRunes = 1f;
            hero[3].agilityRunes = 1.025f;
            hero[4].agilityRunes = 1f;
            hero[0].pets = "gemmi";
            hero[1].pets = "nelson";
            hero[2].pets = "gemmi";
            hero[3].pets = "nelson";
            hero[4].pets = "gemmi";
            hero[0].alive = true;
            hero[1].alive = true;
            hero[2].alive = true;
            hero[3].alive = true;
            hero[4].alive = true;

            /*hero[0] = hero_1.GetHeroStruct();
            hero[1] = hero_2.GetHeroStruct();
            hero[2] = hero_3.GetHeroStruct();
            hero[3] = hero_4.GetHeroStruct();
            hero[4] = hero_5.GetHeroStruct();*/


            hpDummy = dummyStamina * 10;
            dummyTR = Logic.turnRate(dummyPower, dummyAgility);
            dummyInterval = (float)counterMax / dummyTR;


            for (i = 0; i < playerNo; i++)
            {  //initialisation
                hero[i].turnRate = Logic.turnRate(hero[i].power, hero[i].agility);
                hero[i].power = System.Convert.ToInt32(hero[i].power * hero[i].powerRunes);
                hero[i].turnRate *= hero[i].agilityRunes;
                hero[i].hp = hero[i].stamina * 10;
                hero[i].maxHp = hero[i].hp;
                hero[i].interval = counterMax / hero[i].turnRate;
                hero[i].counter = 0;
                hero[i].sp = 4;
                hero[i].alive = true;
            }


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
                            if (DS)
                            {
                                HeroLogic.heroAttack(i, DS);
                                DS = false;
                                HeroLogic.heroAttack(i, DS);
                            }
                            else { HeroLogic.heroAttack(i, DS); }
                            hero[i].counter -= hero[i].interval;
                            if (hpDummy <= 0)
                            {
                                win++;
                                i = playerNo;
                                cycle = counterMax;
                                dummyCounter = 0;
                            }
                            //Console.WriteLine("boss hP ={0}", hpDummy);

                            //Console.WriteLine("hp0 = {0}  hp1 = {1} hp2 = {2} hp3 = {3} hp4 = {4}\n", hero[0].hp, hero[1].hp, hero[2].hp, hero[3].hp, hero[4].hp);
                        }
                    }
                    if (hpDummy > 0 && dummyCounter >= dummyInterval)
                    {         //checks if it's boss' turn to attack
                        spDummy++;
                        BossLogic.bossAttack();
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
                        //Debug.Log("boss attacked ; hp0 = {0}  hp1 = {1} hp2 = {2} hp3 = {3} hp4 = {4}\n", hero[0].hp, hero[1].hp, hero[2].hp, hero[3].hp,hero[4].hp);
                        //Console.WriteLine("{0}. {1}. {2}. [3}. {4}",hero[0].hp, hero[1].hp, hero[2].hp, hero[3].hp, hero[4].hp);
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

        Debug.Log("winrate = " + winRate);
        //printf("won = %f lost = %f\n", win, lose);
        //printf("winRate = %f %%\n", winRate);
        yield return null;
    }
}
