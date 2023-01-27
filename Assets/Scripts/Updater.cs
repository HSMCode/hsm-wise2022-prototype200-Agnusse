using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Updater : MonoBehaviour
{
    // UI
    private GameObject _playGameUI;
    private GameObject _gameOverUI;
    private string livesText = "Lives: ";
    private Text _lives;
    private Text _livesLeft;
    private Text _result;

    // variables for Elvis
    public int elvisHit=1;
    private int _currentElvisHits=0;
    private int _elvisDead = 5;
    private int elvisLives;

    // variables for Fan
    public int fanHit;
    private int _currentFanHits;
    private int _fanDead = 3;
    public GameObject Fan;

    // audiovisual feedback
    public ParticleSystem ParticleSystemPlayerWin;
    public ParticleSystem ParticleSystemPlayerHurt;
    // public ParticleSystem ParticleSystemEnemyHurt;

    // variables for game over
    public bool elvisHome;
    public bool gameOver;
    
    // private bool gameWon;
    private bool gameLost;
    private PlayerController _PlayerController;



    // Start is called before the first frame update
    void Start()
    {

         _PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();

        // find particle systems
        ParticleSystemPlayerWin = GameObject.Find("ParticleSystemPlayerWin").GetComponent<ParticleSystem>();
        ParticleSystemPlayerHurt = GameObject.Find("ParticleSystemPlayerHurt").GetComponent<ParticleSystem>();
        
        // find Enemy Fan
        Fan = GameObject.FindWithTag("Enemy");

        //find Text
        _lives = GameObject.Find ("Lives").GetComponent<Text>();
        _livesLeft = GameObject.Find ("LivesLeft").GetComponent<Text>();
        _result= GameObject.Find ("Result").GetComponent<Text>();

        //UI
        _playGameUI = GameObject.Find ("Play");
        _gameOverUI = GameObject.Find ("GameOver");

        _playGameUI.SetActive(true);
        _gameOverUI.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        
        if (_currentFanHits == _fanDead)
        {
            print("Fan died!");
            Destroy(Fan);
        }

        elvisLives = _elvisDead-_currentElvisHits;
        _lives.text = livesText + elvisLives.ToString();

        if (gameOver) 
        {
            _livesLeft.text = "Lives left: " + elvisLives;

            if (gameLost)
            {
                _result.text = "You Lost! Maybe you can save Elvis next time.";
            }
            else 
            {
                _result.text = "Congratulations, you won!";
            }
           
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

        // ParticleSystemEnemyHurt.Emit(5);
        // ParticleSystemPlayerHurt.Emit(5);
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
            _PlayerController.gameOff();
        }

        // Game Over WIN
        if (elvisHome)
        {
            gameOver = true;
            // gameWon = true;
        }

        // Game OVER
        if (gameOver)
        {
            // the game is over, panels change
            _playGameUI.SetActive(false);
            _gameOverUI.SetActive(true);
        }
    }
    
}
