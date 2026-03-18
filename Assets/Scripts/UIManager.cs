using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class UIManager : MonoBehaviour
{
    [SerializeField] private SurviceHub surviceHub;
    
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] public GameObject player;
    [SerializeField] private GameObject pauseUI;
    


    private void Awake()
    {
        player = surviceHub.player;
        
    }
    public void ShowMenu()
    {
        CloseAllUI();
        
    }
    public void ShowGameplayUI()
    {
        CloseAllUI();
        Time.timeScale = 1f;
        player.SetActive(true);
        
        gameplayUI.SetActive(true);
    }
    public void ShowPauseUI()
    {
        CloseAllUI();
        Time.timeScale = 0.0f;
        pauseUI.SetActive(true);
    }
    





    public void CloseAllUI()
    {
        
        gameplayUI.SetActive(false);
        
        pauseUI.SetActive(false);
        
        
    }
    
    
    // Update is called once per frame
    
}
