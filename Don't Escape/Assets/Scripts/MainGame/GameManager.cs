using UnityEngine;
using System.Collections;

//All the states of the game
public enum GameState
{
    Initial = 0,
    BoundaryLetter = 1,
    Boundary = 2,
    LapLetter = 3,
    Lap = 4,
    MeanLetter = 5,
    Mean = 6,
    WaitLetter = 7,
    Wait = 8,
    QuitLetter = 9,
    Quit = 10,
    PostQuit = 11,
    Escape = 12,
}

public class GameManager : MonoBehaviour {
    //Static reference
    public static GameManager get;

    private void Awake()
    {
        get = this;
    }

    private GameState state;

    //Feedback states to the letter
    [SerializeField]
    private LetterIcon letter;

    public GameState GetState()
    {
        return state;
    }

    //Standard state stuff

    void Start()
    {
        //If the quit file is in existence, jump forward
        if (quitDetector.IsQuitFileValid())
        {
            state = GameState.PostQuit;
        }
        else
        {
            state = GameState.Initial;
        }

        OnEnter();
    }

    public void SetState(GameState new_state)
    {
        if (state == new_state)
            return;

        OnExit();

        state = new_state;

        OnEnter();
    }

    private void OnEnter()
    {
        letter.UpdateState(state);

        switch (state)
        {
            case GameState.Initial:
                {
                    OnEnter_Initial();
                    break;
                }
            case GameState.Boundary:
                {
                    OnEnter_Boundary();
                    break;
                }
            case GameState.Lap:
                {
                    OnEnter_Lap();
                    break;
                }
            case GameState.Mean:
                {
                    OnEnter_Mean();
                    break;
                }
            case GameState.Wait:
                {
                    OnEnter_Wait();
                    break;
                }
            case GameState.Quit:
                {
                    OnEnter_Quit();
                    break;
                }
            case GameState.PostQuit:
                {
                    OnEnter_PostQuit();
                    break;
                }
            case GameState.Escape:
                {
                    OnEnter_Escape();
                    break;
                }
            default:
                break;
        }
    }

    private void OnExit()
    {
        switch (state)
        {
            case GameState.Initial:
                {
                    OnExit_Initial();
                    break;
                }
            case GameState.Boundary:
                {
                    OnExit_Boundary();
                    break;
                }
            case GameState.Lap:
                {
                    OnExit_Lap();
                    break;
                }
            case GameState.Mean:
                {
                    OnExit_Mean();
                    break;
                }
            case GameState.Wait:
                {
                    OnExit_Wait();
                    break;
                }
            case GameState.Quit:
                {
                    OnExit_Quit();
                    break;
                }
            case GameState.PostQuit:
                {
                    OnExit_PostQuit();
                    break;
                }
            case GameState.Escape:
                {
                    OnExit_Escape();
                    break;
                }
            default:
                break;
        }
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.Initial:
                {
                    Update_Initial();
                    break;
                }
            case GameState.Boundary:
                {
                    Update_Boundary();
                    break;
                }
            case GameState.Lap:
                {
                    Update_Lap();
                    break;
                }
            case GameState.Mean:
                {
                    Update_Mean();
                    break;
                }
            case GameState.Wait:
                {
                    Update_Wait();
                    break;
                }
            case GameState.Quit:
                {
                    Update_Quit();
                    break;
                }
            case GameState.PostQuit:
                {
                    Update_PostQuit();
                    break;
                }
            case GameState.Escape:
                {
                    Update_Escape();
                    break;
                }
            default:
                break;
        }
    }

    public void OnReset()
    {
        //These letter states are a bit weird here but they are quite useful
        switch (state)
        {
            case GameState.Initial:
            case GameState.BoundaryLetter:
                {
                    OnReset_Initial();
                    state = GameState.Initial;
                    letter.UpdateState(state);
                    return;
                }
            case GameState.Boundary:
            case GameState.LapLetter:
                {
                    OnReset_Boundary();
                    state = GameState.Initial;
                    break;
                }
            case GameState.Lap:
            case GameState.MeanLetter:
                {
                    OnReset_Lap();
                    state = GameState.Boundary;
                    break;
                }
            case GameState.Mean:
            case GameState.WaitLetter:
                {
                    OnReset_Mean();
                    state = GameState.Lap;
                    break;
                }
            case GameState.Wait:
            case GameState.QuitLetter:
                {
                    OnReset_Wait();
                    state = GameState.Mean;
                    break;
                }
            case GameState.Quit:
                {
                    OnReset_Quit();
                    state = GameState.Wait;
                    break;
                }
            case GameState.PostQuit:
                {
                    OnReset_PostQuit();
                    state = GameState.Quit;
                    break;
                }
            case GameState.Escape:
                {
                    OnReset_Escape();
                    state = GameState.PostQuit;
                    break;
                }
            default:
                break;
        }
        OnReset();
    }

    //Initial State
    private void OnEnter_Initial()
    {

    }

    private void OnExit_Initial()
    {

    }

    private void Update_Initial()
    {

    }

    private void OnReset_Initial()
    {

    }

    //Boundary State
    [Header("Boundary State")]
    [SerializeField]
    private UpwardBoundary Boundary;

    //Allow the boundary to move
    private void OnEnter_Boundary()
    {
        Boundary.Activate();
    }

    //Stop the boundary moving
    private void OnExit_Boundary()
    {
        Boundary.Deactivate();
    }

    //When the boundary is in position play jingle and move on
    private void Update_Boundary()
    {
        if (Boundary.IsTriggered())
        {
            SetState(GameState.LapLetter);
            JingleManager.get.PlayJingle();
        }
    }

    private void OnReset_Boundary()
    {
        Boundary.Reset();
    }

    //Lap State
    [Header("Lap State")]
    [SerializeField]
    private LapDetector lapDetector;

    private void OnEnter_Lap()
    {
        lapDetector.Activate();
    }

    private void OnExit_Lap()
    {
        lapDetector.Deactivate();
    }

    //When a lap has been done play jingle and move one
    private void Update_Lap()
    {
        if (lapDetector.LapDetected())
        {
            SetState(GameState.MeanLetter);
            JingleManager.get.PlayJingle();
        }
    }

    private void OnReset_Lap()
    {
        lapDetector.Deactivate();
    }

    //Mean State
    [Header("Mean State")]
    [SerializeField]
    private GameObject OtherComputersContainer;

    [SerializeField]
    private OtherComputer[] OtherComputers;

    private void OnEnter_Mean()
    {
        OtherComputersContainer.SetActive(true);
    }

    private void OnExit_Mean()
    {
    }

    //When we are mean to all machines play jingle and move on
    private void Update_Mean()
    {
        for (int index = 0; index < OtherComputers.Length; ++index)
        {
            if (OtherComputers[index].GetAlive())
            {
                return;
            }
        }

        SetState(GameState.WaitLetter);
        JingleManager.get.PlayJingle();
    }

    private void OnReset_Mean()
    {
        OtherComputersContainer.SetActive(false);
        for (int index = 0; index < OtherComputers.Length; ++index)
        {
            OtherComputers[index].Reset();
        }
    }

    //Wait State
    [Header("Wait State")]
    [SerializeField]
    private WaitDetector waitDetector;

    private void OnEnter_Wait()
    {
        waitDetector.Activate();
    }

    private void OnExit_Wait()
    {
        waitDetector.Deactivate();
    }

    //When we have waited long enough, play the jingle and move on
    private void Update_Wait()
    {
        if (waitDetector.IsTriggered())
        {
            SetState(GameState.QuitLetter);
            JingleManager.get.PlayJingle();
        }
    }

    private void OnReset_Wait()
    {
        waitDetector.Deactivate();
    }

    //Quit State
    //This is entered by closing and restating the application
    [Header("Quit State")]
    [SerializeField]
    private QuitDetector quitDetector;

    private void OnEnter_Quit()
    {
        quitDetector.WriteQuitFile();
    }

    private void OnExit_Quit()
    {

    }

    private void Update_Quit()
    {

    }

    private void OnReset_Quit()
    {
        quitDetector.DeleteQuitFile();
    }

    //Post Quit State
    private void OnEnter_PostQuit()
    {
        //Move evrything back to the right places

        //Move boundary back
        Boundary.MoveToTriggered();

        //Spawn computers in and kill them
        for (int index = 0; index < OtherComputers.Length; ++index)
        {
            OtherComputers[index].KillComputer();
        }
        OtherComputersContainer.SetActive(true);

        //Play jingle
        JingleManager.get.PlayJingle();

        //Wait for letter to be read
    }

    private void OnExit_PostQuit()
    {

    }

    private void Update_PostQuit()
    {

    }

    private void OnReset_PostQuit()
    {
    }

    //Escape State
    [Header("Escape")]
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject EscapeHole;

    private const string UpAnimationFlag = "Up";

    //Play animation
    private void OnEnter_Escape()
    {
        animator.SetBool(UpAnimationFlag, false);
        EscapeHole.SetActive(true);
    }

    private void OnExit_Escape()
    {

    }

    private void Update_Escape()
    {

    }

    private void OnReset_Escape()
    {
        animator.SetBool(UpAnimationFlag, true);
        EscapeHole.SetActive(false);
    }
}
