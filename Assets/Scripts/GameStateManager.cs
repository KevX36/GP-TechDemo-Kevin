using Unity.VisualScripting;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameState staringState;
    public void Quit()
    {
        Application.Quit();
        
    }
    public enum GameState
    {

        Menu,
        Paused,
        GamePlay,
        




    }
    [SerializeField] private SurviceHub hub;
    [SerializeField] private UIManager UI;
    public GameState currentState { get; private set; }
    public GameState lastState { get; private set; }
    private string currentStateLog;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        UI = hub.uIManager;
        SetState(staringState);
        currentStateLog = currentState.ToString();
    }

    public void SetState(GameState state)
    {
        if (currentState == state) return;
        lastState = currentState;
        currentState = state;
        currentStateLog = currentState.ToString();
        OnStateChange(currentState);


    }

    private void OnStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.Menu:

                UI.ShowMenu();



                break;


            case GameState.GamePlay:

                UI.ShowGameplayUI();



                break;



            case GameState.Paused:


                UI.ShowPauseUI();


                break;




            



        }
    }

    // Update is called once per frame

    public void OnPause()
    {
        if (currentState == GameState.GamePlay)
        {
            SetState(GameState.Paused);
        }
        else if (currentState == GameState.Paused)
        {
            SetState(GameState.GamePlay);
        }
        
    }
    
    public void CloseSettings()
    {
        SetState(lastState);
    }
    public void ReturnToMainMenu()
    {
        SetState(GameState.Menu);
    }
    public void GamePlay()
    {
        SetState(GameState.GamePlay);
    }
}
