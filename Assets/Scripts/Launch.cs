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
    RaidSimulation rdSim;
    private bool isRaid;
    private bool isWb;
    private float winrateToShow;
    private int totalGameToShow;
    private GameMode gameMode;
    public enum GameMode
    {
        None,
        Raid,
        Wb
    }

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

        {40, 61 },
        {41, 122 },
        {42, 183 },

        {50, 80 }, //to change
        {51, 180 }, //to change
        {52, 340 }, //to change


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

        {140, 61 },
        {141, 122 },
        {142, 183 },

        {150, 80 }, //to change
        {151, 170 }, //to change
        {152, 320 }, //to change



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
    public InputField fightCountField;

	void Awake()
	{
		instance = this;
        isRaid = false;
        isWb = false;
	}

    void Start()
    {        
    }

    void Update()
    {
        switch (gameMode)
        {
            case GameMode.Raid:
                winrateToShow = rdSim.winRate;
                break;
            case GameMode.Wb:
                winrateToShow = wbSim.winRate;
                break;
        }
        myText.text = " fights = " + winrateToShow + "%";
    }

    public void OnClickInitRaid()
    {
		IsRunning = true;
        gameMode = GameMode.Raid;
        rdSim = new RaidSimulation();
        totalGameToShow = rdSim.games;

        rdSim.heroes[0] = hero_1.GetHero();
        rdSim.heroes[1] = hero_2.GetHero();
        rdSim.heroes[2] = hero_3.GetHero();
        rdSim.heroes[3] = hero_4.GetHero();
        rdSim.heroes[4] = hero_5.GetHero();

        int difficultyChecker = bossName.value * 10 + bossDifficulty.value;
        Debug.Log(difficultyChecker);
        bossDiff = bossName.value;
        switch (difficultyChecker)
        {
            case 0:
                rdSim.difficultyModifier = 70;
                break;
            case 1:
                rdSim.difficultyModifier = 115;
                break;
            case 2:
                rdSim.difficultyModifier = 160;
                break;
            case 10:
                rdSim.difficultyModifier = 105;
                break;
            case 11:
                rdSim.difficultyModifier = 156;
                break;
            case 12:
                rdSim.difficultyModifier = 207;
                break;
            case 20:
                rdSim.difficultyModifier = 150;
                break;
            case 21:
                rdSim.difficultyModifier = 207;
                break;
            case 22:
                rdSim.difficultyModifier = 265;
                break;
            case 30:
                rdSim.difficultyModifier = 205; //to change
                break;
            case 31:
                rdSim.difficultyModifier = 310; //to change
                break;
            case 32:
                rdSim.difficultyModifier = 485; //to change
                break;
            default:
                break;
        }
        if (Convert.ToInt32(fightCountField.text) < 100) fightCountField.text = "100";
		StartCoroutine(rdSim.Simulation(Convert.ToInt32(fightCountField.text), bossDiff, callback => {
			IsRunning = false;
		}));
        isRaid = false;
    }

    public void OnClickInitWB()
    {
        int difficultyChecker = wbName.value * 100 + tier.value * 10 + wbDifficulty.value;
        wbSim = new WorldBossSimulation(WBDictionary[difficultyChecker]);
        totalGameToShow = wbSim.Games;
        gameMode = GameMode.Wb;
        if (wbName.value == 1)
        {
            wbSim.heroes = new Character[3];
            wbSim.heroes[0] = hero_1.GetHero();
            wbSim.heroes[1] = hero_2.GetHero();
            wbSim.heroes[2] = hero_3.GetHero();
        }
        else
        {
            wbSim.heroes = new Character[5];
            wbSim.heroes[0] = hero_1.GetHero();
            wbSim.heroes[1] = hero_2.GetHero();
            wbSim.heroes[2] = hero_3.GetHero();
            wbSim.heroes[3] = hero_4.GetHero();
            wbSim.heroes[4] = hero_5.GetHero();
        }
        if (Convert.ToInt32(fightCountField.text) < 100) fightCountField.text = "100";
        StartCoroutine(wbSim.Simulation(Convert.ToInt32(fightCountField.text), wbName.value, callback => { IsRunning = false; }));
    }
}
