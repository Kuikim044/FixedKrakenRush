using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private int score;
    public PlayerData playerName;
    public UnityEvent<string, int> submitScoreEvent;

    // Start is called before the first frame update


    public void SubmitScore()
    {
        score = Singleton.Instance.scorePlayer;
        submitScoreEvent.Invoke(playerName.playerName, score);
        
    }
}
