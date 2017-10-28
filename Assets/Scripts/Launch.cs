using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Launch : MonoBehaviour
{
	public delegate void RunEvent();
	public static event RunEvent onRun;
	public static Launch instance;

	private bool _isRunning = false;
	public bool IsRunning
	{
		get { return _isRunning; }
		set
		{
			_isRunning = value;

			if (onRun != null)
			{
				onRun();
			}
		}
	}

    public HeroPanel hero_1;
    public HeroPanel hero_2;
    public HeroPanel hero_3;
    public HeroPanel hero_4;
    public HeroPanel hero_5;
    public Text myText;
    public Dropdown bossName;
    public Dropdown bossDifficulty;
    public static int bossDiff;

	void Awake()
	{
		instance = this;
	}

    void Start()
    {        
    }

    void Update()
    {
        myText.text = "Winrate over " + Simulation.games + " fights = " + Simulation.winRate + "%";
    }

    public void OnClickInit()
    {
		IsRunning = true;

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
            case 20:
                Simulation.difficultyModifier = 150;
                break;
            case 21:
                Simulation.difficultyModifier = 207;
                break;
            case 22:
                Simulation.difficultyModifier = 265;
                break;
            default:
                break;
        }

		StartCoroutine(Simulation.simulation(callback => {
			IsRunning = false;
		}));
    }
}
