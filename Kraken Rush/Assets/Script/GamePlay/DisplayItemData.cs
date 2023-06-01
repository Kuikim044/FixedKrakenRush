using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDisplay Data", menuName = "Game Data/ItemDisplayData", order = 1)]
 
public class DisplayItemData : ScriptableObject
{
    public Sprite itemSprite;
    public Sprite timeBar;

    public enum ItemName
    {
        Scoremultiplier,
        CurrencyMultiplier,
        CoinMagnet,
        BonusMode,
        Protection
    }
    public ItemName itemName;
}
