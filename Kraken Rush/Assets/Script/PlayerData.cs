using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterDataForGame
{
    public string characterName;
    public bool characterUnlocked;
}

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game Data/Player Data")]
public class PlayerData : ScriptableObject
{
    public int coin;
    public int star;

    public int level;
    public string playerName;

    public List<CharacterDataForGame> characters = new List<CharacterDataForGame>();

    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("PlayerMoney", coin);
        PlayerPrefs.SetInt("PlayerStar", star);
        PlayerPrefs.SetInt("PlayerLevel", level);
        PlayerPrefs.SetString("PlayerName", playerName);

        PlayerPrefs.SetInt("CharacterCount", characters.Count);

        for (int i = 0; i < characters.Count; i++)
        {
            string key = "CharacterUnlocked_" + i;
            PlayerPrefs.SetInt(key, characters[i].characterUnlocked ? 1 : 0);

            string nameKey = "CharacterName_" + i;
            PlayerPrefs.SetString(nameKey, characters[i].characterName);
        }

        PlayerPrefs.Save();
    }



    public void LoadPlayerData()
    {
        coin = PlayerPrefs.GetInt("PlayerMoney", 0);
        star = PlayerPrefs.GetInt("PlayerStar", 0);
        level = PlayerPrefs.GetInt("PlayerLevel", 1);
        playerName = PlayerPrefs.GetString("PlayerName", playerName);
        characters.Clear();
        int characterCount = GetCharacterCount();

        for (int i = 0; i < characterCount; i++)
        {
            string key = "CharacterUnlocked_" + i;
            bool characterUnlocked = PlayerPrefs.GetInt(key, 0) == 1;

            string nameKey = "CharacterName_" + i;
            string characterName = PlayerPrefs.GetString(nameKey, "");

            CharacterDataForGame characterData = new CharacterDataForGame();
            characterData.characterUnlocked = characterUnlocked;
            characterData.characterName = characterName;

            characters.Add(characterData);
        }
    }

    private int GetCharacterCount()
    {
        // ??????????????????????????? PlayerPrefs
        return PlayerPrefs.GetInt("CharacterCount", 0);
    }
    public void UnlockCharacter(int characterIndex)
    {
        if (characterIndex >= 0 && characterIndex < characters.Count)
        {
            characters[characterIndex].characterUnlocked = true;
        }
    }

    public void AddCharacter(string characterName, bool characterUnlocked)
    {
        CharacterDataForGame characterData = new CharacterDataForGame();
        characterData.characterName = characterName;
        characterData.characterUnlocked = characterUnlocked;
        characters.Add(characterData);
    }

}
