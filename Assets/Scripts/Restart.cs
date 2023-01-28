using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    // variable for butoon
    public Button restartButton;
    
    // Start is called before the first frame update
    void Start()
    {
        // when button is clicked RestartScene Method is called
        restartButton.onClick.AddListener(RestartScene);
    }

    public void RestartScene()
    {
        // restarts scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}