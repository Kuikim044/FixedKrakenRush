using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI scoreText;

    public void SetData(int rank, string playerName, int score)
    {
        rankText.text = "No. " + rank.ToString();
        playerNameText.text = playerName;
        scoreText.text = "Score: "+score.ToString();     
    }
}
