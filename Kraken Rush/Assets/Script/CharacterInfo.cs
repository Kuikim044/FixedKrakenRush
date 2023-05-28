using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public string name;
    public bool characterUnlocked;
    public float actionSpeed;
    public int life;
    public int scoreMultiplier;
    public int price;
}

public class CharacterInfo : MonoBehaviour
{
    public TextAsset jsonFile;
    public CharacterData characterData;

    private void Start()
    {
        if (jsonFile != null)
        {
            string jsonText = jsonFile.text;

            // แปลง JSON เป็นออบเจ็กต์
            characterData = JsonUtility.FromJson<CharacterData>(jsonText);

            // เข้าถึงค่าในออบเจ็กต์
            string name = characterData.name;
            bool characterUnlocked = characterData.characterUnlocked;
            float actionSpeed = characterData.actionSpeed;
            int life = characterData.life;
            int scoreMultiplier = characterData.scoreMultiplier;
            int price = characterData.price;


            // แสดงผลค่าที่ได้
           

            // ทำสิ่งอื่นๆตามต้องการเพื่อควบคุมตัวละครในเกม
        }
        else
        {
            Debug.LogError("Please assign a JSON file in the Inspector.");
        }
    }

    public void SaveCharacterDataToJson(CharacterData data)
    {
        // แปลงออบเจ็กต์ CharacterData เป็น JSON
        string jsonText = JsonUtility.ToJson(data);

        // บันทึก JSON ลงในไฟล์
        string filePath = Application.dataPath + "/File Data/Main Character.json";
        File.WriteAllText(filePath, jsonText);
    }
}
