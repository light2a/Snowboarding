using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject menuSceen;
    [SerializeField]
    GameObject listSceen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        menuSceen.SetActive(true);
        listSceen.SetActive(false);
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level1");
        
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level2");
        
    }

    public void Level()
    {
        listSceen.SetActive(true);
        menuSceen.SetActive(false);
    }
    void Start()
    {
        listSceen.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
