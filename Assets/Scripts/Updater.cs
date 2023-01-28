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
    private string _livesText = "Lives: ";
    private Text _lives;
    private Text _livesLeft;
    private Text _result;

    // variables for Elvis
    public int elvisHit=1;
    private int _currentElvisHits=0;
    private int _elvisLives;

    // Elvis is dead when hit 5 times
    private int _elvisDead = 5;

    // variables for Fan
    public int fanHit;
    private int _currentFanHits;
    public GameObject Fan;
    private int _fanDead = 3;

    // variables for visual feedback
    public ParticleSystem ParticleSystemPlayerWin;
    public ParticleSystem ParticleSystemPlayerHurt;

    // variables for game over
    public bool elvisHome;
    public bool gameOver;

    //variable for lost Game
    private bool _gameLost;

    //link to PlayerController-Script
    private PlayerController _PlayerController;

    // Start is called before the first frame update
    void Start()
    {
        //find PlayerController-Script
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

        //find UI
        _playGameUI = GameObject.Find ("Play");
        _gameOverUI = GameObject.Find ("GameOver");

        // activate playGameUI
        _playGameUI.SetActive(true);

        // deactivate playGameUI
        _gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //run script when _currentFanHits is the sme as _fanDead
        if (_currentFanHits == _fanDead)
        {
            // Fan gets destroyed
            Destroy(Fan);
        }

        // calculate how many lives Elvis has left
        _elvisLives = _elvisDead-_currentElvisHits;

        // print how many lives Elvis has left on screen
        _lives.text = _livesText + _elvisLives.ToString();

        //run script if game is over
        if (gameOver) 
        {
            // exchange text in UI
            _livesLeft.text = "Lives left: " + _elvisLives;

            // run script if game is lost
            if (_gameLost)
            {
                // exchange text in UI
                _result.text = "You Lost! Try it again!";
            }

            //run script if game is not lost
            else 
            {
                // exchange text in UI
                _result.text = "Congratulations, you won!";
            }
        }
    }

    // run script if method UpdateElvis is called
    public void UpdateElvis(int elvisHit)
    {
        //adds currentElvisHits to ElvisHits
        _currentElvisHits += elvisHit;

        // visual feedback
        ParticleSystemPlayerHurt.Emit(5);
   
        // call method CheckGameOver
        CheckGameOver(elvisHome);
    }

    // run Script if method UpdateFan is called
    public void UpdateFan(int fanHit)
    {
        //adds currentFanHits to fanHits
        _currentFanHits += fanHit;

        // visual feedback
        ParticleSystemPlayerWin.Emit(5);

        // call method CheckGameOver
        CheckGameOver(elvisHome);
    }

    // run Script if method CheckGameOver is called
    public void CheckGameOver(bool elvisHome)
    {
        // runs script if _currentElvisHits is the same number as _elvisDead
        if (_currentElvisHits == _elvisDead)
        {
            // set values to game over 
            gameOver = true;

            // set value to game lost
            _gameLost = true;

            // run method gameOff in script playerController
            _PlayerController.gameOff();
        }

        // runs script if elvis reaches goal
        if (elvisHome)
        {
            //sets value to game is over
            gameOver = true;
        }

        // runs script if game is over
        if (gameOver)
        {
            // panels change
            _playGameUI.SetActive(false);
            _gameOverUI.SetActive(true);
        }
    }
    
}
