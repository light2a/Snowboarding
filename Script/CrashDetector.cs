using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] 
    float loadDelay = 2.5f;
    [SerializeField]
    ParticleSystem CrashEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            CrashEffect.Play();
            Invoke("ReloadScene", loadDelay);
            Debug.Log("done");
        }
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
