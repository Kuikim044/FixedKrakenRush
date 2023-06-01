using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GachaItemData", menuName = "Gacha System/Gacha Item Data", order = 1)]
public class GachaScriptable : ScriptableObject
{
    public List<GachaItem> itemData;
}
