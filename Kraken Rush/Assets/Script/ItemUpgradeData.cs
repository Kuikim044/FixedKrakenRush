using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Upgrade System/Item Data")]
public class ItemUpgradeData : ScriptableObject
{
    public int maxUpgradeLevel = 5;
    public int currentUpgradeLevel;
}
