using System;
using System.Threading;
using System.Threading.Tasks;

public enum GameMode
{
    None,
    Raid,
    WB
}

public class MultiThreadSimHandler
{
    public static readonly object SyncObj = new object();

    private GameMode gameMode;
    private HeroPanel[] heroPanels;
    private int simulationDifficulty;
    private int playerNumber;
    private int bossType;

    private bool simRunning;
    //private bool stopSimulation;

    private int simulationsToRun;
    private int simulationsCompleted;
    private int simulationsWon;
    private int sliderValue;

    private float winrateToShow;

    private Action<float> CallBackWinrate;
    private Action<int> CallBackSliderValue;
    private Action CallbackShowError;
    private Action CallbackSimulationCompleted;
    private Action <float> WeirdCallback; 

    private Func<bool> GetCancelButtonState;


    public MultiThreadSimHandler(GameMode _gameMode, HeroPanel[] _heroPanels, int _bossType, int _simulationDifficulty, int _playerNumber, int _gamesToSimulate, Action<float> _callbackWinrate, Action<int> _callbackSliderValue, Action _callbackShowError, Func<bool> _getCancelButtonState, Action<float> _weirdCallback, Action _callbackSimulationsCompleted)
    {
        gameMode = _gameMode;
        heroPanels = _heroPanels;
        simulationDifficulty = _simulationDifficulty;
        playerNumber = _playerNumber;
        bossType = _bossType;

        simRunning = false;

        simulationsToRun = _gamesToSimulate;
        sliderValue = 0;
        simulationsWon = 0;
        simulationsCompleted = 0;
        winrateToShow = 0;

        CallBackWinrate = _callbackWinrate;
        CallBackSliderValue = _callbackSliderValue;
        CallbackShowError = _callbackShowError;
        GetCancelButtonState = _getCancelButtonState;
        WeirdCallback = _weirdCallback;
        CallbackSimulationCompleted = _callbackSimulationsCompleted;
    }

    public void LaunchSimulation(int processorCount)
    {
        simRunning = true;
        Parallel.For(0, simulationsToRun, new ParallelOptions { MaxDegreeOfParallelism = processorCount }, (x, state) =>
        {
            if (!simRunning || GetCancelButtonState())
            {
                CallbackShowError();
                state.Break();
            }
            switch (gameMode)
            {
                case GameMode.Raid:
                    new RaidSimulation(simulationDifficulty, playerNumber, heroPanels, x).Run(bossType, WeirdCallback, InvokeStopSim, SimulationOutcome);
                    break;
                default:
                    new WorldBossSimulation(simulationDifficulty, playerNumber, heroPanels, x).Run(bossType, WeirdCallback, InvokeStopSim, SimulationOutcome);
                    break;
            }
        });

        CallBackWinrate(winrateToShow);
        CallBackSliderValue(sliderValue);
    }
    public void SimulationOutcome(bool win)
    {
        lock (SyncObj)
        {
            simulationsCompleted++;
            if (win) simulationsWon++;
            winrateToShow = (float)simulationsWon * 100 / (float)simulationsCompleted;
            if (simulationsCompleted * 100 / simulationsToRun >= sliderValue + 1 && sliderValue < 100)
            {
                sliderValue = Convert.ToInt32(simulationsCompleted * 100 / simulationsToRun);
                CallBackWinrate(winrateToShow);
                CallBackSliderValue(sliderValue);
            }
            if (simulationsCompleted >= simulationsToRun)
            {
                simRunning = false;
                CallbackSimulationCompleted();
            }
        }
    }
    private bool InvokeStopSim(bool callFromSim)
    {
        if (callFromSim)
        {
            CallbackShowError();
            simRunning = false;
            return true;
        }
        return false;
    }
}
