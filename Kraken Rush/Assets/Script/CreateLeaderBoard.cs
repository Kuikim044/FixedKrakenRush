using Firebase;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CreateLeaderBoard : MonoBehaviour
{
    public GameObject leaderboardItemPrefab;
    public Transform contentContainer;
    public TextMeshProUGUI[] LeaderTexts;
    private DatabaseReference databaseReference;
    public GameObject leaderboard;
    private bool isFetchingLeaderboard = false;

    private void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }
  
    private void Update()
    {
        if (leaderboard.activeSelf)
        {
            // เรียกใช้ FetchLeaderboard() เมื่อต้องการอัพเดตข้อมูล
            Invoke(nameof(FetchLeaderboard), 0f);
        }

    }
    private async Task FetchLeaderboard()
    {
        TaskCompletionSource<DataSnapshot> tcs = new TaskCompletionSource<DataSnapshot>();

        Task task = databaseReference.Child("leaderBoard").OrderByChild("score").LimitToLast(10).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to fetch leaderboard: " + task.Exception);
                tcs.SetException(task.Exception);
                return;
            }
            if (task.IsCompleted)
            {
                tcs.SetResult(task.Result);
            }
        });

        DataSnapshot snapshot = await tcs.Task;

        List<string> leaderboardData = new List<string>();

        foreach (DataSnapshot childSnapshot in snapshot.Children)
        {
            string playerName = childSnapshot.Child("Name").Value.ToString();
            int score = int.Parse(childSnapshot.Child("score").Value.ToString());

            string leaderboardEntry = "Name: " + playerName + "\n" + "Score: " + score.ToString();
            leaderboardData.Add(leaderboardEntry);
        }

        UpdateLeaderboardUI(leaderboardData);

        Debug.Log("Fetched leaderboard successfully.");
    }

    private void UpdateLeaderboardUI(List<string> leaderboardData)
    {
        int numEntries = Mathf.Min(leaderboardData.Count, LeaderTexts.Length);

        for (int i = 0; i < numEntries; i++)
        {
            if (LeaderTexts[i] != null)
            {
                string leaderboardText = leaderboardData[i] + "\n\n";
                LeaderTexts[i].text = leaderboardText;
            }
        }
    }
    


}
