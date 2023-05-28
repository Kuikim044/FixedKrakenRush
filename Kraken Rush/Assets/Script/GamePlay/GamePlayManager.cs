using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    private int score;
    private int displayedScore;

    public static int coin;

    public static float multiplierScore = 1f;
    public static float multiplierCoin = 1f;

    private float timeElapsed;

    private float scoreIncreaseRate = 100f;

    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtCoin;
    public TextMeshProUGUI txtMultipilerScore;

    public TextMeshProUGUI txtWaitForPlay;
    public GameObject waitForplay;
    public GameObject startTxt;

    public float coolDownDuration = 3f;

    private bool isCoolingDown = false;
    private bool isPause = false;

    private DatabaseReference dbReference;

    [Header("PAGE")]
    [SerializeField] private GameObject pausePage;

    void Start()
    {
        score = 0;
        displayedScore = 0;
        coin = 0;

        waitForplay.SetActive(true);
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;

    }


    void FixedUpdate()
    {
        Singleton.Instance.LoadScore();
        Debug.Log(Singleton.Instance.scorePlayer);
        if (score >= Singleton.Instance.scorePlayer)
        {
            SendScore();
            
        }

        if (waitForplay.activeSelf)
        {
            Singleton.Instance.isReadyToPlay = false;
            isCoolingDown = true;
            PlayerController.forwardSpeed =  0f;
            if (isCoolingDown)
            {
                coolDownDuration -= Time.deltaTime;
                txtWaitForPlay.text = Mathf.RoundToInt(coolDownDuration).ToString();
                if (coolDownDuration <= 0)
                {
                    coolDownDuration = 0;
                    waitForplay.SetActive(false);
                    isCoolingDown = false;
                    PlayerController.forwardSpeed = 10f;
                    startTxt.SetActive(true);
                    Singleton.Instance.isReadyToPlay = true;
                }
            }
        }
        if (!Singleton.Instance.isReadyToPlay)
            return;

        txtMultipilerScore.text = "x"+ multiplierScore.ToString();
        timeElapsed += Time.deltaTime;
        // อัปเดตและแสดงค่า score ที่แสดงบน UI
        UpdateDisplayedScore();
        IncreaseScore();


        if (Singleton.Instance.isMultiplierScore)
            StartCoroutine(CheckLifeTimeMultiplierScore());
        if (Singleton.Instance.isMultiplierCoin)
            StartCoroutine(CheckLifeTimeMultiplierCoin());
   

    }

    IEnumerator CheckLifeTimeMultiplierScore()
    {
        yield return new WaitForSeconds(Singleton.Instance.multiplierScore);
        Singleton.Instance.isMultiplierScore = false;
        multiplierScore = 1f;
    }
    IEnumerator CheckLifeTimeMultiplierCoin()
    {
        yield return new WaitForSeconds(Singleton.Instance.multiplierScore);
        Singleton.Instance.isMultiplierCoin = false;
        multiplierCoin = 1f;
    }


    private void IncreaseScore()
    {
        score = Mathf.FloorToInt(timeElapsed * scoreIncreaseRate * multiplierScore);
    }

    public void IncreaseCoin()
    {
        Player.player.playerData.coin += coin;
        Player.player.playerData.SavePlayerData();
    }

    private void UpdateDisplayedScore()
    {
        displayedScore = (int)Mathf.Lerp(displayedScore, score, Time.deltaTime * 1f);
        txtScore.text = displayedScore.ToString("D10");

        txtCoin.text = coin.ToString("D7");
    }

    public void SendScore()
    {
        Singleton.Instance.SentScore(score);
    }



    public void Pause()
    {
        Time.timeScale = 0f;
        pausePage.SetActive(true);
        isPause = true;

    }
   
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePage.SetActive(false);
        isPause = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

    }

}
