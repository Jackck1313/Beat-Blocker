using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource hitSFX;
    public AudioSource missSFX;
    public TMPro.TextMeshPro comboText;
    public TMPro.TextMeshPro scoreText;
    public static int comboScore;
    public static int score;


    public GameStart SceneMove;

    public bool ShowResults { get; set; } = false;

    private float _waitTime = 0.5f;
    private bool _done = false;
    private float _returnTime = 5f;

    [SerializeField] private TMPro.TextMeshPro _scoreText;
    [SerializeField] private AudioSource _success;
    [SerializeField] private AudioSource _failure;
    private int suckScore;
    private int ehScore;
    private int nbScore;
    private int niceScore;
    private int specScore;



    //public float sfxDelay;
    void Start()
    {

        if (SongManager.SongNum == 0)
        {
            suckScore = 10000;
            ehScore = 30000;
            nbScore = 70000;
            niceScore = 140000;
            specScore = 244205;
        }
        else if (SongManager.SongNum == 1)
        {
            suckScore = 10000;
            ehScore = 20000;
            nbScore = 60000;
            niceScore = 120000;
            specScore = 172601;
        }
        Instance = this;
        comboScore = 0;
        score = 0;
        ShowResults = false;
        _waitTime = 0.5f;
        _done = false;
        _returnTime = 2f;
    }
    public static void Hit()
    {
        comboScore += 1;
        Instance.hitSFX.PlayDelayed(0.15f);
        score += (17 * comboScore);
        ShakeBehaviour.Instance.TriggerShake();
    }
    public static void Miss()
    {
        if(comboScore >= 5)
            Instance.missSFX.PlayDelayed(0f);
        comboScore = 0;
    }
    private void Update()
    {

        scoreText.text = score.ToString();
        comboText.text = comboScore.ToString() + "x";
        if (ShowResults)
            Results();
        if (_done)
            Return();

        
    }
    public void Return()
    {
        _returnTime -= Time.deltaTime;
        if (_returnTime < 0)
        {
            SceneMove.SceneToGame(1);
            
        }
    }
    public void Results()
    {
        _waitTime -= Time.deltaTime;
        if (_waitTime < 0 && !_done)
        {
            if (score < suckScore)
            {
                _failure.Play();
               
                _scoreText.text = "you suck...";
            }
            else if (score < ehScore)
            {
                _failure.Play();
               
                _scoreText.text = "yikes";
            }
            else if (score < nbScore)
            {
                _success.Play();
                
                _scoreText.text = "not bad.";
            }
            else if (score < niceScore)
            {
                _success.Play();
                
                _scoreText.text = "nice job!";
            }
            else if (score < specScore)
            {
                _success.Play();
               
                _scoreText.text = "spectacular";
            }
            else
            {
                _success.Play();
              
                _scoreText.text = "perfection";
            }
            ShowResults = false;
            _done = true;
        }
    }
}
