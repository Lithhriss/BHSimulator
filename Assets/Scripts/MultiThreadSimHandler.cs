using UnityEngine;
using System;
using System.Threading;

public class MultiThreadSimHandler
{
    private static readonly object SyncObj = new object();

    private Launch.GameMode gameMode;
    private HeroPanel[] heroPanels;
    private int simulationDifficulty;
    private int playerNumber;
    private int bossType;

    private bool simRunning;
    private bool stopSimulation;

    private int simulationsToRun;
    private int simulationsCompleted;
    private int simulationsWon;
    private int sliderValue;

    private float winrateToShow;

    private Action<float> CallBackWinrate;
    private Action<int> CallBackSliderValue;
    private Action CallbackShowError;

    private Func<bool> GetCancelButtonState;

    public MultiThreadSimHandler(Launch.GameMode _gameMode, HeroPanel[] _heroPanels, int _bossType, int _simulationDifficulty, int _playerNumber, int _gamesToSimulate, Action<float> _callbackWinrate, Action<int> _callbackSliderValue, Action _callbackShowError, Func<bool> _getCancelButtonState)
    {
        gameMode = _gameMode;
        heroPanels = _heroPanels;
        simulationDifficulty = _simulationDifficulty;
        playerNumber = _playerNumber;
        bossType = _bossType;

        simRunning = false;
        stopSimulation = false;

        simulationsToRun = _gamesToSimulate;
        sliderValue = 0;
        simulationsWon = 0;
        simulationsCompleted = 0;
        winrateToShow = 0;

        CallBackWinrate = _callbackWinrate;
        CallBackSliderValue = _callbackSliderValue;
        CallbackShowError = _callbackShowError;
        GetCancelButtonState = _getCancelButtonState;

        ThreadPool.SetMaxThreads(Environment.ProcessorCount, 0);
    }


    public void LaunchSimulation()
    {
        simRunning = true;
        Simulation simulation;
        CancellationTokenSource cts = new CancellationTokenSource();
        for (int i = 0; i < simulationsToRun; i++)
        {
            switch (gameMode)
            {
                case Launch.GameMode.Raid:
                    simulation = new RaidSimulation(simulationDifficulty, playerNumber, heroPanels);
                    break;
                default:
                    simulation = new WorldBossSimulation(simulationDifficulty, playerNumber, heroPanels);
                    break;
            }
            ThreadPool.QueueUserWorkItem(state => simulation.Run(bossType, Callback, InvokeStopSim, SimulationOutcome, cts.Token));
        }

        while (GetActiveThreadCount() > 0 && simRunning)
        {
            try
            {
                Thread.Sleep(100);
                Debug.Log("winrate is " + winrateToShow);
                CallBackWinrate(winrateToShow);
                Debug.Log("slider is" + sliderValue);
                CallBackSliderValue(sliderValue);
                if (stopSimulation || GetCancelButtonState())
                {
                    simRunning = false;
                    CallbackShowError();
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
                //simRunning = false;
            }
        }
        CallBackWinrate(winrateToShow);
        CallBackSliderValue(sliderValue);
    }
    public void Callback(float value)
    {
        //fill out later, not very important
    }
    public void SimulationOutcome(bool win)
    {
        lock (SyncObj)
        {
            simulationsCompleted++;
            if (win) simulationsWon++;
            winrateToShow = (float)simulationsWon * 100 / (float)simulationsCompleted;
            if (simulationsCompleted * 100 / simulationsToRun >= sliderValue + 1 && sliderValue < 100) sliderValue = Convert.ToInt32(simulationsCompleted * 100 / simulationsToRun);
            if (simulationsCompleted >= simulationsToRun) simRunning = false;
        }
    }
    private bool InvokeStopSim(bool callFromSim)
    {
        if (stopSimulation || callFromSim)
        {
            stopSimulation = true;
            return true;
        }
        return false;
    }
    private int GetActiveThreadCount()
    {
        int availableWorkers = 0;
        int maxWorkers = 0;
        int availableIo = 0;
        int maxIo = 0;
        ThreadPool.GetAvailableThreads(out availableWorkers, out availableIo);
        ThreadPool.GetMaxThreads(out maxWorkers, out maxIo);
        return maxWorkers - availableWorkers;
    }
}
