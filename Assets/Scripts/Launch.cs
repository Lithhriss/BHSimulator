using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;


public class Launch : MonoBehaviour
{
    public delegate void RunEvent();
    public static event RunEvent onRun;
    public static Launch instance;
    WorldBossSimulation wbSim; //need to get rid off
    RaidSimulation rdSim;      //need to get rid off

    private bool forceStopSimulation;
    private bool showError;
    private int gamesToSimulate;
    private bool simulationsCompleted;
    private int sliderValue;

    private float winrateToShow;

    private GameMode gameMode;

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
    private static Dictionary<int, int> RaidDictionary = new Dictionary<int, int>()
    {
        {0, 70 },
        {1, 115 },
        {2, 160 },

        {10, 105 },
        {11, 156 },
        {12, 207 },

        {20, 150 },
        {21, 207 },
        {22, 265 },

        {30, 205 },
        {31, 310 },
        {32, 485 },

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
    private bool MultiThreadSim;



    public GameObject[] heroContainers;
    private HeroPanel[] heroes;
    public PlayerPanelToggle ppt;

    #region UI Objects
    public Text myText;
    public Text errorText;
    public Slider slider;
    public Slider processorSlider;
    public Dropdown bossName;
    public Dropdown bossDifficulty;
    public Dropdown wbName;
    public Dropdown tier;
    public Dropdown wbDifficulty;
    public InputField fightCountField;
    public Button RaidButton;
    public Button WbButton;
    #endregion


    void Awake()
    {
        instance = this;
        forceStopSimulation = false;

        simulationsCompleted = false;
        showError = false;
        MultiThreadSim = false;

    }

    void Start()
    {
        errorText.text = "";
    }

    void Update()
    {
        if (MultiThreadSim)
        {
            UpdateSlider();
        }
        else
        {

            switch (gameMode)
            {
                case GameMode.Raid:
                    winrateToShow = rdSim.winRate;
                    break;
                case GameMode.WB:
                    winrateToShow = wbSim.winRate;
                    break;
            }
        }
        if (showError)
        {
            errorText.text = "Simulation has been forcibly stopped!";
            RaidButton.interactable = true;
            WbButton.interactable = true;
            showError = false;
        }
        if (simulationsCompleted)
        {
            errorText.text = "Simulation has ended.";
            RaidButton.interactable = true;
            WbButton.interactable = true;
            simulationsCompleted = false;
        }
        myText.text = " fights = " + winrateToShow + "%";
    }

    public void OnClickInitSimulation(int _gameMode)
    {
        RaidButton.interactable = false;
        WbButton.interactable = false;
        ResetUI();
        IsRunning = true;
        MultiThreadSim = true;
        GameMode gameMode = (GameMode)_gameMode;
        int difficulty = GetDifficulty(gameMode);
        int bossValue = 0;
        if (gameMode == GameMode.Raid) bossValue = bossName.value;
        else bossValue = wbName.value;
        if (Convert.ToInt32(fightCountField.text) < 0) fightCountField.text = "1";
        new Thread(() => new MultiThreadSimHandler( gameMode, 
                                                    heroes, 
                                                    bossName.value, 
                                                    difficulty, 
                                                    ppt.GetPlayerNumber(),
                                                    Convert.ToInt32(fightCountField.text),
                                                    CallbackWinrate, 
                                                    CallbackSliderValue,
                                                    ShowError, 
                                                    GetCancelButtonState,
                                                    callback => { IsRunning = false; },
                                                    SimulationCompleted).LaunchSimulation(Convert.ToInt32(processorSlider.value))).Start();
    }

    //obsolete
    public void OnClickInitRaid()
    {
        ResetUI();
        IsRunning = true;
        MultiThreadSim = false;
        gameMode = GameMode.Raid;
        int difficulty = GetDifficulty(gameMode);
        int playerNumber = ppt.GetPlayerNumber();
        rdSim = new RaidSimulation(difficulty, playerNumber, heroes, 0);
        int bossType = 0;
        bossType = bossName.value;
        if (Convert.ToInt32(fightCountField.text) < 100) fightCountField.text = "100";
        StartCoroutine(rdSim.Simulation(Convert.ToInt32(fightCountField.text), bossType, callback => {IsRunning = false;}, InvokeStopSim));
        
    }
    //obsolete
    public void OnClickInitWB()
    {
        MultiThreadSim = false;
        ResetUI();
        int difficultyChecker = wbName.value * 100 + tier.value * 10 + wbDifficulty.value;

        gameMode = GameMode.WB;
        int playerNumber = ppt.GetPlayerNumber();
        if (wbName.value == 1)
        {
            if (playerNumber < 3)
            {
                //wbSim.heroes = new Character[playerNumber];
            }
            else
            {
                //wbSim.heroes = new Character[3];
                playerNumber = 3;
            }

        }
        wbSim = new WorldBossSimulation(WBDictionary[difficultyChecker], playerNumber, heroes, 0);
        if (Convert.ToInt32(fightCountField.text) < 100) fightCountField.text = "100";
        StartCoroutine(wbSim.Simulation(Convert.ToInt32(fightCountField.text), wbName.value, callback => { IsRunning = false; }, InvokeStopSim));
    }

    private void InitHeroArray()
    {
        heroes = new HeroPanel[5];
        for (int i = 0; i < heroContainers.Length; i++)
        {
            heroes[i] = heroContainers[i].GetComponentInChildren<HeroPanel>();
        }
    }

    public void OnClickCancelSim()
    {
        forceStopSimulation = true;
    }

    private void SimulationCompleted()
    {
        simulationsCompleted = true;
    }

    private bool InvokeStopSim(bool callFromSim)
    {
        if (forceStopSimulation || callFromSim)
        {
            //Debug.Log("stopSimulation is " + stopSimulation.ToString() + "and callFromSim is " + callFromSim.ToString());
            showError = true;
            forceStopSimulation = true;
            return true;
        }
        return false;
    }

    private void ShowError()
    {
        showError = true; ;
    }

    public void UpdateSlider()
    {
        slider.value = sliderValue;
    }

    private void ResetUI()
    {
        errorText.text = "";
        showError = false;
        forceStopSimulation = false;
        simulationsCompleted = false;
        sliderValue = 0;
        slider.value = 0;
        //simulationsWon = 0;
        InitHeroArray();
    }

    private void CallbackWinrate(float winrate)
    {
        winrateToShow = winrate;
    }

    private void CallbackSliderValue(int _sliderValue)
    {
        sliderValue = _sliderValue;
    }

    private bool GetCancelButtonState()
    {
        return forceStopSimulation;
    }

    private int GetDifficulty(GameMode gameMode)
    {
        int difficulty = 0;
        switch (gameMode)
        {
            case GameMode.Raid:
                difficulty = bossName.value * 10 + bossDifficulty.value;
                difficulty = RaidDictionary[difficulty];
                break;
            default:
                difficulty = wbName.value * 100 + tier.value * 10 + wbDifficulty.value;
                difficulty = WBDictionary[difficulty];
                break;
        }
        return difficulty;
    }

}


#region Previous code
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;


public class Launch : MonoBehaviour
{
    public delegate void RunEvent();
    public static event RunEvent onRun;
    public static Launch instance;
    WorldBossSimulation wbSim; //need to get rid off
    RaidSimulation rdSim;      //need to get rid off

    private bool stopSimulation;
    private bool updateSlider;
    private bool showError;
    private int gamesToSimulate;
    private int simulationsCompleted;
    private int simulationsWon;
    private int sliderValue;

    private float winrateToShow;

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


    public GameObject[] heroContainers;
    private HeroPanel[] heroes;
    public PlayerPanelToggle ppt;

    #region UI Objects
    public Text myText;
    public Text errorText;
    public Slider slider;
    public Dropdown bossName;
    public Dropdown bossDifficulty;
    public Dropdown wbName;
    public Dropdown tier;
    public Dropdown wbDifficulty;
    public InputField fightCountField;
    #endregion


    void Awake()
    {
        instance = this;
        stopSimulation = false;
        updateSlider = false;
        showError = false;

    }

    void Start()
    {
        errorText.text = "";
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
        if (updateSlider) UpdateSlider();
        if (showError) ShowError();
    }

    public void OnClickInitRaid()
    {
        ResetUI();
        IsRunning = true;
        gameMode = GameMode.Raid;
        int difficulty = 0;
        int playerNumber = ppt.GetPlayerNumber();
        int difficultyChecker = bossName.value * 10 + bossDifficulty.value;
        switch (difficultyChecker)
        {
            case 0:
                difficulty = 70;
                break;
            case 1:
                difficulty = 115;
                break;
            case 2:
                difficulty = 160;
                break;
            case 10:
                difficulty = 105;
                break;
            case 11:
                difficulty = 156;
                break;
            case 12:
                difficulty = 207;
                break;
            case 20:
                difficulty = 150;
                break;
            case 21:
                difficulty = 207;
                break;
            case 22:
                difficulty = 265;
                break;
            case 30:
                difficulty = 205; //to change
                break;
            case 31:
                difficulty = 310; //to change
                break;
            case 32:
                difficulty = 485; //to change
                break;
            default:
                break;
        }

        rdSim = new RaidSimulation(difficulty, playerNumber, heroes);

        int bossType = 0;
        bossType = bossName.value;
        if (Convert.ToInt32(fightCountField.text) < 100) fightCountField.text = "100";
        //current way to run sim, via coroutine to update loading bar every percent
        //StartCoroutine(rdSim.Simulation(Convert.ToInt32(fightCountField.text), bossType, callback => {IsRunning = false;}, InvokeStopSim));
        //using thread to update slider live using callback method
        gamesToSimulate = Convert.ToInt32(fightCountField.text);
        RaidSimulation raidSim;
        for (int i = 0; i < gamesToSimulate; i++)
        {
            raidSim = new RaidSimulation(difficulty, playerNumber, heroes);
            ThreadPool.QueueUserWorkItem(state => raidSim.Simulation(bossType, callback => { IsRunning = false; }, InvokeStopSim, SimulationOutcome));
        }
        int worker = 0;
        int io = 0;
        ThreadPool.GetAvailableThreads(out worker, out io);
        while (worker > 0 && !stopSimulation)
        {
            try
            {
                Thread.Sleep(100);

                ThreadPool.GetAvailableThreads(out worker, out io);
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
                stopSimulation = true;
            }
        }
    }

    public void OnClickInitWB()
    {
        ResetUI();
        int difficultyChecker = wbName.value * 100 + tier.value * 10 + wbDifficulty.value;

        gameMode = GameMode.Wb;
        int playerNumber = ppt.GetPlayerNumber();
        if (wbName.value == 1)
        {
            if (playerNumber < 3)
            {
                //wbSim.heroes = new Character[playerNumber];
            }
            else
            {
                //wbSim.heroes = new Character[3];
                playerNumber = 3;
            }

        }
        else
        {
            //wbSim.heroes = new Character[playerNumber];
            //for (int i = 0; i < playerNumber; i++)
            //{
            //    wbSim.heroes[i] = heroes[i].GetHero();
            //}
        }
        wbSim = new WorldBossSimulation(WBDictionary[difficultyChecker], playerNumber, heroes);
        if (Convert.ToInt32(fightCountField.text) < 100) fightCountField.text = "100";
        Debug.Log("launching sim");
        StartCoroutine(wbSim.Simulation(Convert.ToInt32(fightCountField.text), wbName.value, callback => { IsRunning = false; }, InvokeStopSim));
    }

    private void InitHeroArray()
    {
        heroes = new HeroPanel[5];
        for (int i = 0; i < heroContainers.Length; i++)
        {
            heroes[i] = heroContainers[i].GetComponentInChildren<HeroPanel>();
        }
    }

    public void OnClickCancelSim()
    {
        stopSimulation = true;
    }

    private bool InvokeStopSim(bool callFromSim)
    {
        if (stopSimulation || callFromSim)
        {
            //Debug.Log("stopSimulation is " + stopSimulation.ToString() + "and callFromSim is " + callFromSim.ToString());
            showError = true;
            stopSimulation = true;
            return true;
        }
        return false;
    }

    private void ShowError()
    {
        errorText.text = "Simulation has been forcibly stopped!";
        showError = false;
    }

    public void SimulationOutcome(bool win)
    {

        //Debug.Log("sim completed");
        simulationsCompleted++;
        if (win) simulationsWon++;
        winrateToShow = (float)simulationsWon * 100 / (float)simulationsCompleted;
        if (simulationsCompleted * 100 / gamesToSimulate >= sliderValue + 1 && slider.value < 100)
        {
            sliderValue++;
            updateSlider = true;
        }

    }

    public void UpdateSlider()
    {
        updateSlider = false;
        slider.value = sliderValue;
    }

    private void ResetUI()
    {
        errorText.text = "";
        stopSimulation = false;
        sliderValue = 0;
        slider.value = 0;
        simulationsWon = 0;
        InitHeroArray();
    }

}


*/
#endregion