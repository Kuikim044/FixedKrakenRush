using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DailyReward : MonoBehaviour
{
    public TextMeshProUGUI time;
    public Button[] ClickButton;
    private ulong lastTimeClick;
    private int currentDay = 1;
    private const int maxDays = 30;

    private string[] originalButtonTexts; 
    private void Awake()
    {

        originalButtonTexts = new string[ClickButton.Length];
        for (int i = 0; i < ClickButton.Length; i++)
        {
            originalButtonTexts[i] = ClickButton[i].GetComponentInChildren<TextMeshProUGUI>().text;
        }

        if (originalButtonTexts != null && originalButtonTexts.Length > 0)
        {
            for (int i = 0; i < ClickButton.Length; i++)
            {
                if (i == 0)
                {
                    originalButtonTexts[i] = "Day1";
                }
                else
                {
                    originalButtonTexts[i] = "Day" + (i + 1);
                }
            }
        }

        if (PlayerPrefs.HasKey("LastTimeClicked"))
        {
            lastTimeClick = ulong.Parse(PlayerPrefs.GetString("LastTimeClicked"));
        }
        else
        {
            lastTimeClick = (ulong)DateTime.Now.Ticks;
            PlayerPrefs.SetString("LastTimeClicked", lastTimeClick.ToString());
        }

        if (PlayerPrefs.HasKey("CurrentDay"))
        {
            currentDay = PlayerPrefs.GetInt("CurrentDay");
            if (currentDay > maxDays || currentDay < 1) // ????????????? currentDay < 1
            {
                currentDay = 1;
                PlayerPrefs.SetInt("CurrentDay", currentDay);
            }
        }
        else
        {
            PlayerPrefs.SetInt("CurrentDay", currentDay);
        }
        if (currentDay == maxDays && Ready())
        {
            StartCoroutine(ResetButtonsAfterDelay());
        }

        UpdateButtonAvailability();

        ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClick);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float secondsPassed = (float)(m) / 1000.0f;
        if (currentDay > 1 && secondsPassed >= maxDays)
        {
            currentDay = 1;
            PlayerPrefs.SetInt("CurrentDay", currentDay);
            UpdateButtonAvailability();
        }
    }

    private void Update()
    {
        if (currentDay <= maxDays && !ClickButton[currentDay - 1].interactable)
        {
            if (Ready())
            {
                time.text = "Ready!";
                return;
            }

            ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClick);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float secondsLeft = (float)(GetMillisecondsToWait() - m) / 1000.0f;

            string r = "";
            r += ((int)secondsLeft / 3600).ToString() + "h";
            secondsLeft -= ((int)secondsLeft / 3600) * 3600;
            r += ((int)secondsLeft / 60).ToString("00") + "m ";
            r += (secondsLeft % 60).ToString("00") + "s";
            time.text = r;

            ClickButton[currentDay - 1].GetComponentInChildren<TextMeshProUGUI>().text = "Claimed";
        }

        Debug.Log(currentDay);
    }

    public void Click()
    {
        lastTimeClick = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("LastTimeClicked", lastTimeClick.ToString());
        ClickButton[currentDay - 1].interactable = false;

        if (currentDay == maxDays)
        {
            for (int i = 1; i <= maxDays; i++)
            {
                PlayerPrefs.DeleteKey("ClickedDay" + i);
            }

            StartCoroutine(ResetButtonsAfterDelay());
            StartCoroutine(ResetCurrentDayAfterDelay(1));

            UpdateButtonAvailability();

            ClickButton[0].GetComponentInChildren<TextMeshProUGUI>().text = "Claim";

            return;
        }

        
        if (currentDay < maxDays && HasClickedDay(currentDay))
        {
            currentDay++;
            PlayerPrefs.SetInt("CurrentDay", currentDay);
            if (currentDay == maxDays)
            {
                StartCoroutine(ResetButtonsAfterDelay());
            }
            else
            {
                UpdateButtonAvailability();
            }
        }

        PlayerPrefs.SetInt("ClickedDay" + currentDay, 1);

        if (currentDay == maxDays && Ready())
        {
            currentDay = 1;
            PlayerPrefs.SetInt("CurrentDay", currentDay);
            UpdateButtonAvailability();
        }
    }

    private bool Ready()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClick);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)(GetMillisecondsToWait() - m) / 1000.0f;

        if (secondsLeft <= 0)
        {
            ClickButton[currentDay - 1].interactable = true;
            if (currentDay < maxDays)
            {
                currentDay++;
                PlayerPrefs.SetInt("CurrentDay", currentDay);
            }
            UpdateButtonAvailability();
            return true;
        }

        return false;
    }

    private float GetMillisecondsToWait()
    {
        float millisecondsToWait = (float)TimeSpan.FromHours(24).TotalMilliseconds;

        return millisecondsToWait;
    }

    private void UpdateButtonAvailability()
    {
        for (int i = 0; i < ClickButton.Length; i++)
        {
            bool interactable = (i < currentDay && !HasClickedDay(i + 1));

            if (interactable)
            {
                ClickButton[i].GetComponentInChildren<TextMeshProUGUI>().text = "Claim";
            }
            else
            {
                if (HasClickedDay(i + 1))
                {
                    ClickButton[i].GetComponentInChildren<TextMeshProUGUI>().text = "Claimed";
                }
                else
                {
                    ClickButton[i].GetComponentInChildren<TextMeshProUGUI>().text = "Day" + (i + 1);
                }
            }

            ClickButton[i].interactable = interactable;
        }
    }

    private bool HasClickedDay(int day)
    {
        return PlayerPrefs.HasKey("ClickedDay" + day);
    }

    private IEnumerator ResetButtonsAfterDelay()
    {
        yield return new WaitForSeconds(GetMillisecondsToWait());

        if (Ready())
        {
            for (int i = 0; i < ClickButton.Length; i++)
            {
                ClickButton[i].interactable = false;
                ClickButton[i].GetComponentInChildren<TextMeshProUGUI>().text = originalButtonTexts[i];
            }

            UpdateButtonAvailability();
        }

    }
    private IEnumerator ResetCurrentDayAfterDelay(int newDay)
    {

        yield return new WaitForSeconds(GetMillisecondsToWait() / 1000.0f);

        currentDay = newDay;
        PlayerPrefs.SetInt("CurrentDay", currentDay);
    }
}