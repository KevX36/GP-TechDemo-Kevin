using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        Debug.Log("loaded main menu");
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadLevelSelect()
    {
        Debug.Log("loaded level Select");
        SceneManager.LoadScene("Level Select");
    }
    public void ReloadLevel()
    {
        Debug.Log("reloaded level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadLevel1()
    {
        Debug.Log("loaded level 1");
        //SceneManager.LoadScene("Level1");
    }
    public void LoadLevel2()
    {
        Debug.Log("loaded level 2");
        //SceneManager.LoadScene("Level2");
    }
    public void LoadLevel3()
    {
        Debug.Log("loaded level 3");
        //SceneManager.LoadScene("Level3");
    }
    public void LoadLevel4()
    {
        Debug.Log("loaded level 4");
        //SceneManager.LoadScene("Level4");
    }
    public void LoadLevel5()
    {
        Debug.Log("loaded level 5");
        //SceneManager.LoadScene("Level5");
    }
    public void LoadTestLevel()
    {
        SceneManager.LoadScene("TestLevel");
    }
    public void Quit()
    {
        Debug.Log("quit game");
        Application.Quit();

    }
}
