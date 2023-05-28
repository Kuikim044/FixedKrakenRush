using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class UpgradeItemManager : MonoBehaviour
{
    [Header("Image Upgrade")]
    public Image[] barFillScoreMultiplier;
    public Image[] barFillCurrencyMultiplier;
    public Image[] barFillCoinMagnet;
    public Image[] barFillBonusMode;
    public Image[] barFillProtection;


    public ItemUpgradeData scoreMultiplier;
    public ItemUpgradeData currencyMultiplier;
    public ItemUpgradeData coinMagnet;
    public ItemUpgradeData bonusMode;
    public ItemUpgradeData protection;

    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtCoin;
    public TextMeshProUGUI txtMagnet;
    public TextMeshProUGUI txtBonus;
    public TextMeshProUGUI txtProtect;


    public int[] prices; // ใช้เก็บราคาของไอเท็มแต่ละระดับ

    private void Start()
    {
        ResetUpgradeLevel(ref scoreMultiplier.currentUpgradeLevel);
        ResetUpgradeLevel(ref currencyMultiplier.currentUpgradeLevel);
        ResetUpgradeLevel(ref coinMagnet.currentUpgradeLevel);
        ResetUpgradeLevel(ref bonusMode.currentUpgradeLevel);
        ResetUpgradeLevel(ref protection.currentUpgradeLevel);

        /* LoadUpgradeLevel(ref scoreMultiplier.currentUpgradeLevel);
        LoadUpgradeLevel(ref currencyMultiplier.currentUpgradeLevel);
        LoadUpgradeLevel(ref coinMagnet.currentUpgradeLevel);
        LoadUpgradeLevel(ref bonusMode.currentUpgradeLevel);
        LoadUpgradeLevel(ref protection.currentUpgradeLevel);*/

    }

    private void Update()
    {
        #region ScoreMultiplier
        if (scoreMultiplier.currentUpgradeLevel >= 1)
            barFillScoreMultiplier[0].gameObject.SetActive(true);
        if (scoreMultiplier.currentUpgradeLevel >= 2)
            barFillScoreMultiplier[1].gameObject.SetActive(true);
        if (scoreMultiplier.currentUpgradeLevel >= 3)
            barFillScoreMultiplier[2].gameObject.SetActive(true);
        if (scoreMultiplier.currentUpgradeLevel >= 4)
            barFillScoreMultiplier[3].gameObject.SetActive(true);
        if (scoreMultiplier.currentUpgradeLevel >= 5)
            barFillScoreMultiplier[4].gameObject.SetActive(true);
        #endregion

        #region currencyMultiplier
        if (currencyMultiplier.currentUpgradeLevel >= 1)
            barFillCurrencyMultiplier[0].gameObject.SetActive(true);
        if (currencyMultiplier.currentUpgradeLevel >= 2)
            barFillCurrencyMultiplier[1].gameObject.SetActive(true);
        if (currencyMultiplier.currentUpgradeLevel >= 3)
            barFillCurrencyMultiplier[2].gameObject.SetActive(true);
        if (currencyMultiplier.currentUpgradeLevel >= 4)
            barFillCurrencyMultiplier[3].gameObject.SetActive(true);
        if (currencyMultiplier.currentUpgradeLevel >= 5)
            barFillCurrencyMultiplier[4].gameObject.SetActive(true);
        #endregion

        #region Coin Magnet
        if (coinMagnet.currentUpgradeLevel >= 1)
            barFillCoinMagnet[0].gameObject.SetActive(true);
        if (coinMagnet.currentUpgradeLevel >= 2)
            barFillCoinMagnet[1].gameObject.SetActive(true);
        if (coinMagnet.currentUpgradeLevel >= 3)
            barFillCoinMagnet[2].gameObject.SetActive(true);
        if (coinMagnet.currentUpgradeLevel >= 4)
            barFillCoinMagnet[3].gameObject.SetActive(true);
        if (coinMagnet.currentUpgradeLevel >= 5)
            barFillCoinMagnet[4].gameObject.SetActive(true);
        #endregion

        #region bonusMode
        if (bonusMode.currentUpgradeLevel >= 1)
            barFillBonusMode[0].gameObject.SetActive(true);
        if (bonusMode.currentUpgradeLevel >= 2)
            barFillBonusMode[1].gameObject.SetActive(true);
        if (bonusMode.currentUpgradeLevel >= 3)
            barFillBonusMode[2].gameObject.SetActive(true);
        if (bonusMode.currentUpgradeLevel >= 4)
            barFillBonusMode[3].gameObject.SetActive(true);
        if (bonusMode.currentUpgradeLevel >= 5)
            barFillBonusMode[4].gameObject.SetActive(true);
        #endregion

        #region Protection
        if (protection.currentUpgradeLevel >= 1)
            barFillProtection[0].gameObject.SetActive(true);
        if (protection.currentUpgradeLevel >= 2)
            barFillProtection[1].gameObject.SetActive(true);
        if (protection.currentUpgradeLevel >= 3)
            barFillProtection[2].gameObject.SetActive(true);
        if (protection.currentUpgradeLevel >= 4)
            barFillProtection[3].gameObject.SetActive(true);
        if (protection.currentUpgradeLevel >= 5)
            barFillProtection[4].gameObject.SetActive(true);
        #endregion

        Singleton.Instance.lvCorecurrencyMultiplier = currencyMultiplier.currentUpgradeLevel;
        Singleton.Instance.lvScoreMultiplier = scoreMultiplier.currentUpgradeLevel;
        Singleton.Instance.lvCoinMagnet = coinMagnet.currentUpgradeLevel;
        Singleton.Instance.lvBonusmode = bonusMode.currentUpgradeLevel;
        Singleton.Instance.lvProtection = protection.currentUpgradeLevel;

        



        txtScore.text = GetPrice(scoreMultiplier.currentUpgradeLevel).ToString();
        txtCoin.text = GetPrice(currencyMultiplier.currentUpgradeLevel).ToString();
        txtMagnet.text = GetPrice(coinMagnet.currentUpgradeLevel).ToString();
        txtBonus.text = GetPrice(bonusMode.currentUpgradeLevel).ToString();
        txtProtect.text = GetPrice(protection.currentUpgradeLevel).ToString();

    }
    public void UpgradeScoreMultiplier()
    {
        string price = GetPrice(scoreMultiplier.currentUpgradeLevel); // เรียกใช้เมธอด GetPrice() เพื่อตรวจสอบราคา
        if (price == "Max")
            return;
        int priceValue = int.Parse(price);
        if (Player.player.playerData.coin < priceValue)
            return;

        Player.player.playerData.coin -= priceValue; // หักเหรียญจากผู้เล่น
        if (scoreMultiplier.currentUpgradeLevel < scoreMultiplier.maxUpgradeLevel)
        {
            #region upgrade Time
            if (scoreMultiplier.currentUpgradeLevel == 0)
                Singleton.Instance.multiplierScore += 2;
            if (scoreMultiplier.currentUpgradeLevel == 1)
                Singleton.Instance.multiplierScore += 3;
            if (scoreMultiplier.currentUpgradeLevel == 2)
                Singleton.Instance.multiplierScore += 3;
            if (scoreMultiplier.currentUpgradeLevel == 3)
                Singleton.Instance.multiplierScore += 5;
            if (scoreMultiplier.currentUpgradeLevel == 4)
                Singleton.Instance.multiplierScore += 10;
            #endregion

            UpgradeItem(ref scoreMultiplier.currentUpgradeLevel);

            Debug.Log(scoreMultiplier.currentUpgradeLevel);

        }
    }
    public void UpgradeCurrencyMultiplier()
    {
        string price = GetPrice(currencyMultiplier.currentUpgradeLevel); // เรียกใช้เมธอด GetPrice() เพื่อตรวจสอบราคา
        if (price == "Max")
            return;
        int priceValue = int.Parse(price);
        if (Player.player.playerData.coin < priceValue)
            return;

        if (currencyMultiplier.currentUpgradeLevel < currencyMultiplier.maxUpgradeLevel)
        {
            #region upgrade Time
            if (currencyMultiplier.currentUpgradeLevel == 0)
                Singleton.Instance.multiplierCoin += 2;
            if (currencyMultiplier.currentUpgradeLevel == 1)
                Singleton.Instance.multiplierCoin += 3;
            if (currencyMultiplier.currentUpgradeLevel == 2)
                Singleton.Instance.multiplierCoin += 3;
            if (currencyMultiplier.currentUpgradeLevel == 3)
                Singleton.Instance.multiplierCoin += 5;
            if (currencyMultiplier.currentUpgradeLevel == 4)
                Singleton.Instance.multiplierCoin += 10;
            #endregion

            UpgradeItem(ref currencyMultiplier.currentUpgradeLevel);

            Debug.Log(currencyMultiplier.currentUpgradeLevel);

        }
    }
    public void UpgradeCoinMagnet()
    {
        string price = GetPrice(coinMagnet.currentUpgradeLevel); // เรียกใช้เมธอด GetPrice() เพื่อตรวจสอบราคา
        if (price == "Max")
            return;
        int priceValue = int.Parse(price);
        if (Player.player.playerData.coin < priceValue)
            return;

        if (coinMagnet.currentUpgradeLevel < coinMagnet.maxUpgradeLevel)
        {
            #region upgrade Time
            if (coinMagnet.currentUpgradeLevel == 0)
                Singleton.Instance.magnet += 2;
            if (coinMagnet.currentUpgradeLevel == 1)
                Singleton.Instance.magnet += 3;
            if (coinMagnet.currentUpgradeLevel == 2)
                Singleton.Instance.magnet += 3;
            if (coinMagnet.currentUpgradeLevel == 3)
                Singleton.Instance.magnet += 5;
            if (coinMagnet.currentUpgradeLevel == 4)
                Singleton.Instance.magnet += 10;
            #endregion

            UpgradeItem(ref coinMagnet.currentUpgradeLevel);

            Debug.Log(coinMagnet.currentUpgradeLevel);

        }
    }
    public void UpgradeBonusMode()
    {
        string price = GetPrice(bonusMode.currentUpgradeLevel); // เรียกใช้เมธอด GetPrice() เพื่อตรวจสอบราคา
        if (price == "Max")
            return;
        int priceValue = int.Parse(price);
        if (Player.player.playerData.coin < priceValue)
            return;

        if (bonusMode.currentUpgradeLevel < bonusMode.maxUpgradeLevel)
        {
            #region upgrade Time
            if (bonusMode.currentUpgradeLevel == 0)
                Singleton.Instance.bonusMode += 2;
            if (bonusMode.currentUpgradeLevel == 1)
                Singleton.Instance.bonusMode += 3;
            if (bonusMode.currentUpgradeLevel == 2)
                Singleton.Instance.bonusMode += 3;
            if (bonusMode.currentUpgradeLevel == 3)
                Singleton.Instance.bonusMode += 5;
            if (bonusMode.currentUpgradeLevel == 4)
                Singleton.Instance.bonusMode += 10;
            #endregion

            UpgradeItem(ref bonusMode.currentUpgradeLevel);

            Debug.Log(bonusMode.currentUpgradeLevel);

        }
    }
    public void UpgradeProtection()
    {
        string price = GetPrice(protection.currentUpgradeLevel); // เรียกใช้เมธอด GetPrice() เพื่อตรวจสอบราคา
        if (price == "Max")
            return;
        int priceValue = int.Parse(price);
        if (Player.player.playerData.coin < priceValue)
            return;

        if (protection.currentUpgradeLevel < protection.maxUpgradeLevel)
        {
            #region upgrade Time
            if (protection.currentUpgradeLevel == 0)
                Singleton.Instance.protection += 2;
            if (protection.currentUpgradeLevel == 1)
                Singleton.Instance.protection += 3;
            if (protection.currentUpgradeLevel == 2)
                Singleton.Instance.protection += 3;
            if (protection.currentUpgradeLevel == 3)
                Singleton.Instance.protection += 5;
            if (protection.currentUpgradeLevel == 4)
                Singleton.Instance.protection += 10;
            #endregion

            UpgradeItem(ref protection.currentUpgradeLevel);

            Debug.Log(protection.currentUpgradeLevel);

        }
    }

    public void UpgradeItem(ref int currentUpgradeItem)
    {
        if (currentUpgradeItem < 5)
        {
            string price = GetPrice(currentUpgradeItem); // เรียกใช้เมธอด GetPrice() เพื่อตรวจสอบราคา
            if (price == "Max")
                return;
            currentUpgradeItem++;

            SaveUpgradeLevel(currentUpgradeItem);
        }
        else
        {
            Debug.Log("ไอเทมอัพเกรดถึงระดับสูงสุดแล้ว");
        }
    }


    public void SaveUpgradeLevel(int currentUpgradeItem)
    {
        PlayerPrefs.SetInt("ItemUpgradeLevel", currentUpgradeItem);
        PlayerPrefs.Save();
    }

    public void LoadUpgradeLevel(ref int currentUpgradeItem)
    {
        currentUpgradeItem = PlayerPrefs.GetInt("ItemUpgradeLevel", 0);
    }

    public string GetPrice(int currentUpgradeLevel)
    {
        if (currentUpgradeLevel < prices.Length)
            return prices[currentUpgradeLevel].ToString();
        else
            return "Max";
    }



    public void ResetUpgradeLevel(ref int currentUpgradeItem)
    {
        currentUpgradeItem = 0;
        SaveUpgradeLevel(currentUpgradeItem);
    }
}