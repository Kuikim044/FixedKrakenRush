using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        playerData.LoadPlayerData();
        List<CharacterDataForGame> purchasedCharacters = playerData.characters;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnApplicationQuit()
    {
        playerData.SavePlayerData();
    }
}
