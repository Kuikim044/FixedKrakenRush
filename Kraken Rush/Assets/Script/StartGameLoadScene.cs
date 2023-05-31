using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameLoadScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider LoadingBarfill;

    public string gameSceneName = "MainMenu"; // ชื่อซีนของเกมที่ต้องการโหลด

    private void Start()
    {
        // แสดงหรือซ่อน GameObject ที่เกี่ยวข้องกับหน้าจอโหลด
        LoadingScreen.SetActive(true);
        StartCoroutine(LoadGameSceneAsync());
    }
    private IEnumerator LoadGameSceneAsync()
    {
        // โหลดซีนเกมแบบไม่บล็อก
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(gameSceneName);
        asyncOperation.allowSceneActivation = false;

        // ดำเนินการโหลดเกมขณะที่แสดงสถานะการโหลด

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // คำนวณความคืบหน้าของการโหลด
            LoadingBarfill.value = progress;
            // ทำสิ่งที่คุณต้องการเพื่อแสดงสถานะการโหลด (เช่นอัพเดตแถบโหลดหรือข้อความ)

            if (progress >= 0.9f)
            {
                // หากความคืบหน้าเกิน 90% (0.9) ให้สลายสถานะการโหลด
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
        LoadingScreen.SetActive(false);
    }
}
