using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Firebase.Database;

public class leaderBoard : MonoBehaviour
{
    private string userName;
    private string userID;
    private int previousScorePlayer;
    private DatabaseReference dbReference;
    private void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void CreateLeaderBoardUser()
    {
        if (string.IsNullOrEmpty(userID))
        {
            Debug.LogWarning("Player name is not set.");
            return;
        }
        if (Singleton.Instance.scorePlayer <= 0 || Singleton.Instance.scorePlayer <= previousScorePlayer)
        {
            Debug.Log("New score is lower than or equal to previous score. Skip sending to Firebase.");
            return;
        }
        userName = Player.player.playerData.playerName;
        LeaderBoaderUser setScoreUser = new LeaderBoaderUser(userName, Singleton.Instance.scorePlayer);
        string json = JsonUtility.ToJson(setScoreUser);
        // dbReference.Child("leaderBoard").Push().SetRawJsonValueAsync(json);
        dbReference.Child("leaderBoard").Child(userID).SetRawJsonValueAsync(json);
        previousScorePlayer = Singleton.Instance.scorePlayer;

    }

}

