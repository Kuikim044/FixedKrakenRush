using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData playerData;
    public string playerName { get; private set; }
    public static Player player { get; private set; }
    public int coin { get; private set; }
    public int lv { get; private set; }

    private void Awake()
    {
        playerData.playerName = playerName;
        if(player == null)
        {
            player = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        coin = playerData.coin;
        lv = playerData.level;
    }

}
