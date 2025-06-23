using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] 
    float loadDelay = 0.1f;
    [SerializeField]
    ParticleSystem finishEffect;
    [SerializeField]
    GameObject gameWinScreen;
    bool gameWin = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            finishEffect.Play();
            GameWin();
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Level1");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameWinScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GameWin()
    {
        Time.timeScale = 0;
        gameWin = true;
        gameWinScreen.SetActive(true);
    }
}
