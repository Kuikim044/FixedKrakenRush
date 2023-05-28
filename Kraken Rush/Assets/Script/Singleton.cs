using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }


    public int characterNum { get; private set; }
    public int coin { get; private set; }
    public int scorePlayer { get; private set; }

    public int isCloseFirstpage { get; private set; }
    public int dayReward { get; private set; }


    #region Item Life Time

    public float multiplierScore { get; set; }
    public float multiplierCoin { get; set; }
    public float magnet { get; set; }
    public float protection { get; set; }
    public float bonusMode { get; set; }

    #endregion

    public bool isMultiplierScore { get; set; }
    public bool isMultiplierCoin{ get; set; }


    public int lvProtection { get; set; }
    public int lvScoreMultiplier { get; set; }
    public int lvCorecurrencyMultiplier { get; set; }
    public int lvCoinMagnet { get; set; }
    public int lvBonusmode { get; set; }

    public bool isReadyToPlay { get; set; }
    private void Awake()
    {
        CreateSingleton();

    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        isCloseFirstpage = 0;


        multiplierCoin = 7f;
        multiplierScore = 7f;
        magnet = 7f;
        protection = 7f;
        bonusMode = 7f;
    }
    public void CloseFirstPage(int numClose)
    {
        isCloseFirstpage = numClose;
    }
    public void DayReward(int numDay)
    {
        dayReward += numDay;
    }

    public void SentScore(int score)
    {
        scorePlayer = score;
        SaveScore();
    }


    public void SaveUpgradeLevel()
    {
        PlayerPrefs.SetFloat("MultiplierCoinUpgradeLevel", multiplierCoin);
        PlayerPrefs.SetFloat("MultiplierScoreUpgradeLevel", multiplierScore);
        PlayerPrefs.SetFloat("MagnetUpgradeLevel", magnet);
        PlayerPrefs.SetFloat("ProtectionUpgradeLevel", protection);
        PlayerPrefs.SetFloat("BonusModeUpgradeLevel", bonusMode);
        PlayerPrefs.Save();
    }
    public void LoadUpgradeLevel()
    {
        multiplierCoin = PlayerPrefs.GetFloat("MultiplierCoinUpgradeLevel", 0);
        multiplierScore = PlayerPrefs.GetFloat("MultiplierScoreUpgradeLevel", 0);
        magnet = PlayerPrefs.GetFloat("MagnetUpgradeLevel", 0);
        protection = PlayerPrefs.GetFloat("ProtectionUpgradeLevel", 0);
        bonusMode = PlayerPrefs.GetFloat("BonusModeUpgradeLevel", 0);
    }
    public void SaveScore()
    {
        PlayerPrefs.SetInt("HightScore", scorePlayer);
        
    }
    public void LoadScore()
    {
       scorePlayer =  PlayerPrefs.GetInt("HightScore", 0);

    }



}
