using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Updater : MonoBehaviour
{
    // UI
    //private GameObject _startGameUI;
    //private GameObject _playGameUI;
    //private GameObject _gameOverUI;

    // variables for Elvis
    public int elvisHit;
    private int _currentElvisHits;
    private int _elvisDead = 3;

    // variables for Fan
    public int fanHit;
    private int _currentFanHits;
    private int _fanDead = 3;

    public GameObject Fan;

    // audiovisual feedback
    public ParticleSystem ParticleSystemPlayerWin;
    public ParticleSystem ParticleSystemPlayerHurt;
    public ParticleSystem ParticleSystemEnemyHurt;

    // variables for game over
    public bool elvisHome;
    public bool gameOver;
    private bool gameWon;
    private bool gameLost;

    // Start is called before the first frame update
    void Start()
    {
        // find particle systems
        ParticleSystemPlayerWin = GameObject.Find("ParticleSystemPlayerWin").GetComponent<ParticleSystem>();
        ParticleSystemPlayerHurt = GameObject.Find("ParticleSystemPlayerHurt").GetComponent<ParticleSystem>();
        ParticleSystemEnemyHurt = GameObject.Find("ParticleSystemEnemyHurt").GetComponent<ParticleSystem>();

        // find Enemy Fan
        Fan = GameObject.FindWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameStart();
        
        if (_currentFanHits == _fanDead)
        {
            print("Fand died!");
            Destroy(Fan);
        }
    }

    private void CheckGameStart()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            UpdateElvis(elvisHit);
            UpdateFan(fanHit);
        }
    }

   
    public void UpdateElvis(int elvisHit)
    {
        _currentElvisHits += elvisHit;

        print("Elvis Hits: " + _currentElvisHits);

        // audiovisual feedback
        ParticleSystemPlayerHurt.Emit(5);
        // audio

        CheckGameOver(elvisHome);
    }

    public void UpdateFan(int fanHit)
    {
        _currentFanHits += fanHit;

        print("Fan Hits: " + _currentFanHits);

        // audiovisual feedback
        ParticleSystemPlayerWin.Emit(5);
        ParticleSystemEnemyHurt.Emit(5);
        // audio

        CheckGameOver(elvisHome);
    }

    public void CheckGameOver(bool elvisHome)
    {
        // Game Over LOST
        if (_currentElvisHits == _elvisDead)
        {
            print("Elvis died!");
            gameOver = true;
            gameLost = true;
        }

        // Game Over WIN
        if (elvisHome)
        {
            gameOver = true;
            gameWon = true;
        }

        // Game OVER
        if (gameOver)
        {
            // the game is over, panels change
        }
    }
}
