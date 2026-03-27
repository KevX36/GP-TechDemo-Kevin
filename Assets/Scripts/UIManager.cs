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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        Cursor.visible = false;
        gameplayUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ShowPauseUI()
    {
        CloseAllUI();
        Time.timeScale = 0.0f;
        pauseUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    





    public void CloseAllUI()
    {
        
        gameplayUI.SetActive(false);
        
        pauseUI.SetActive(false);
        
        
    }
    
    
    // Update is called once per frame
    
}
