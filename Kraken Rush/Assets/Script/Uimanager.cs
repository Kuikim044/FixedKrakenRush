using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Uimanager : MonoBehaviour
{
    [Header("Page In Game")]
    public GameObject rewardPage;
    public GameObject leaderBoardPage;
    public GameObject missionsPage;
    public GameObject shopPage;

    public GameObject settingPage;
    public GameObject emailPage;
    public GameObject topUpPage;

    [Header("Runner Page")]
    public GameObject runnerPage;
    public GameObject skillPage;
    public GameObject upgradeItemPage;

    [Header("Curency")]
    public TextMeshProUGUI[] coinText;
    public TextMeshProUGUI[] starText;

    [Header("Collect Day")]
    public Button[] btnCollectDay;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



        foreach (TextMeshProUGUI txt in coinText) 
        { 
            txt.text = Player.player.playerData.coin.ToString();
        }
        foreach (TextMeshProUGUI txt in starText)
        {
            txt.text = Player.player.playerData.star.ToString();
        }

        /*  #region Check Collect Day

          if (Singleton.Instance.dayReward == 7)
          {
              btnCollectDay[0].interactable = true;
          }
          else
          {
              btnCollectDay[0].interactable = false;
          }
          if (Singleton.Instance.dayReward == 14)
          {
              btnCollectDay[1].interactable = true;
          }
          else
          {
              btnCollectDay[1].interactable = false;
          }

          if (Singleton.Instance.dayReward == 21)
          {
              btnCollectDay[2].interactable = true;
          }
          else
          {
              btnCollectDay[2].interactable = false;
          }

          if (Singleton.Instance.dayReward == 28)
          {
              btnCollectDay[3].interactable = true;
          }
          else
          {
              btnCollectDay[3].interactable = false;
          }
          #endregion */


    }

    public void RewardPage()
    {
        rewardPage.SetActive(true);
    }
    public void CloseRewardPage()
    {
        rewardPage.SetActive(false);
    }

    public void LeaderPage()
    {
        leaderBoardPage.SetActive(true);
    }

    public void CloseLeaderPage()
    {
        leaderBoardPage.SetActive(false);
    }


    public void MissionPage()
    {
        missionsPage.SetActive(true);
    }

    public void CloseMissionPage()
    {
        missionsPage.SetActive(false);
    }


    public void SettingPage()
    {
        settingPage.SetActive(true);
    }

    public void CloseSettingPage()
    {
        settingPage.SetActive(false);
    }
    #region Runner Page
    public void RunnerPage()
    {
        runnerPage.SetActive(true);
    }

    public void CloseRunnerPage()
    {
        runnerPage.SetActive(false);
    }

    public void SkillPage()
    {
        if (upgradeItemPage.activeSelf)
            upgradeItemPage.SetActive(false);

        skillPage.SetActive(true);

    }
    public void UpgradeItemPage()
    {
        if (skillPage.activeSelf)
            skillPage.SetActive(false);

        upgradeItemPage.SetActive(true);
    }
    #endregion
}
