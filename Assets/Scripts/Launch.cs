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
    WorldBossSimulation wbSim;

    private static Dictionary<int, int> WBDictionary = new Dictionary<int, int>()
    {
        {0, 10 },
        {1, 15 },
        {2, 20 },

        {10, 15 },
        {11, 23 },
        {12, 30 },

        {20, 30 },
        {21, 45 },
        {22, 60 },

        {30, 45 },
        {31, 90 },
        {32, 135 },

        {40, 70 },
        {41, 134 },
        {42, 198 },


        {100, 10 },
        {101, 15 },
        {102, 20 },

        {110, 15 },
        {111, 23 },
        {112, 30 },

        {120, 30 },
        {121, 45 },
        {122, 60 },

        {130, 45 },
        {131, 90 },
        {132, 135 },

        {140, 70 },
        {141, 132 },
        {142, 194 }


    };

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
    public Dropdown wbName;
    public Dropdown tier;
    public Dropdown wbDifficulty;

	void Awake()
	{
		instance = this;
	}

    void Start()
    {        
    }

    void Update()
    {
        myText.text = "Winrate over " + RaidSimulation.games + " fights = " + RaidSimulation.winRate + "%";
    }

    public void OnClickInitRaid()
    {
		IsRunning = true;

        RaidSimulation.hero[0] = hero_1.GetHeroStruct();
        RaidSimulation.hero[1] = hero_2.GetHeroStruct();
        RaidSimulation.hero[2] = hero_3.GetHeroStruct();
        RaidSimulation.hero[3] = hero_4.GetHeroStruct();
        RaidSimulation.hero[4] = hero_5.GetHeroStruct();

        int difficultyChecker = bossName.value * 10 + bossDifficulty.value;
        bossDiff = bossName.value;
        switch (difficultyChecker)
        {
            case 0:
                RaidSimulation.difficultyModifier = 70;
                break;
            case 1:
                RaidSimulation.difficultyModifier = 115;
                break;
            case 2:
                RaidSimulation.difficultyModifier = 160;
                break;
            case 10:
                RaidSimulation.difficultyModifier = 105;
                break;
            case 11:
                RaidSimulation.difficultyModifier = 156;
                break;
            case 12:
                RaidSimulation.difficultyModifier = 207;
                break;
            case 20:
                RaidSimulation.difficultyModifier = 150;
                break;
            case 21:
                RaidSimulation.difficultyModifier = 207;
                break;
            case 22:
                RaidSimulation.difficultyModifier = 265;
                break;
            default:
                break;
        }

		StartCoroutine(RaidSimulation.Simulation(callback => {
			IsRunning = false;
		}));
    }

    public void OnClickInitWB()
    {
        int difficultyChecker = wbName.value * 100 + tier.value * 10 + wbDifficulty.value;
        wbSim = new WorldBossSimulation(WBDictionary[difficultyChecker]);
        if (wbName.value == 1)
        {
            wbSim.heroes = new Character[3];
            wbSim.heroes[0] = hero_1.GetHeroStruct();
            wbSim.heroes[1] = hero_2.GetHeroStruct();
            wbSim.heroes[2] = hero_3.GetHeroStruct();
        }
        else
        {
            wbSim.heroes = new Character[5];
            wbSim.heroes[0] = hero_1.GetHeroStruct();
            wbSim.heroes[1] = hero_2.GetHeroStruct();
            wbSim.heroes[2] = hero_3.GetHeroStruct();
            wbSim.heroes[3] = hero_4.GetHeroStruct();
            wbSim.heroes[4] = hero_5.GetHeroStruct();
        }
        StartCoroutine(wbSim.Simulation(wbName.value, callback => { IsRunning = false; }));
    }
}
