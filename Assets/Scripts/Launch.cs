using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Launch : MonoBehaviour {
    public HeroPanel hero_1;
    public HeroPanel hero_2;
    public HeroPanel hero_3;
    public HeroPanel hero_4;
    public HeroPanel hero_5;
    public Text myText;
    private Slider slider;
    public Dropdown bossName;
    public Dropdown bossDifficulty;
    public static int bossDiff;

    void Start()
    {
        slider = GameObject.Find("Progress").GetComponent<Slider>();
        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            slider.value = 0;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            slider.value += 1;    
        }
        myText.text = "Winrate over 10 000 fights = " + Simulation.winRate + "%";

        //slider.value = Simulation.progressionBar;

    }

    public void onClickInit()
    {

        Simulation.hero[0] = hero_1.GetHeroStruct();
        Simulation.hero[1] = hero_2.GetHeroStruct();
        Simulation.hero[2] = hero_3.GetHeroStruct();
        Simulation.hero[3] = hero_4.GetHeroStruct();
        Simulation.hero[4] = hero_5.GetHeroStruct();

        int difficultyChecker = bossName.value * 10 + bossDifficulty.value;
        bossDiff = bossName.value;
        switch (difficultyChecker)
        {
            case 0:
                Simulation.difficultyModifier = 70;
                break;
            case 1:
                Simulation.difficultyModifier = 115;
                break;
            case 2:
                Simulation.difficultyModifier = 160;
                break;
            case 10:
                Simulation.difficultyModifier = 105;
                break;
            case 11:
                Simulation.difficultyModifier = 156;
                break;
            case 12:
                Simulation.difficultyModifier = 207;
                break;
            default:
                break;
        }
        Debug.Log(Simulation.difficultyModifier);

        StartCoroutine(Simulation.simulation());
        //StartCoroutine(launchSimulation());
        //StartCoroutine(test());
    }

 

    IEnumerator launchSimulation() {
        Simulation.simulation();
        yield return null;
    }

   IEnumerator test()
    {
        int i = 0, a = 0, b = 0;
        bool te;
        int s;
        for (s = 0; s < 1; s++)
        {
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
            Debug.Log(b);
        }
        yield return null;
    }

}
