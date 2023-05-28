using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnLockCharacterSystem : MonoBehaviour
{
    public PlayerData playerData;
    public CharacterInfo characterInfo;
    public void OnCharacterPurchase(int characterIndex)
    {
        if (characterInfo.characterData.characterUnlocked)
            return;

        if (characterInfo.characterData.price > Player.player.coin)
        {
            Debug.Log("not enough money");
            return;
        }


        // เรียกใช้ UnlockCharacter ใน PlayerData เพื่อปลดล็อคตัวละครที่ดัชนีที่กำหนด
        playerData.UnlockCharacter(characterIndex);
        playerData.AddCharacter(characterInfo.characterData.name, characterInfo.characterData.characterUnlocked = true);
        playerData.coin -= characterInfo.characterData.price;

        characterInfo.characterData.characterUnlocked = true; // เปลี่ยนสถานะปลดล็อก
        characterInfo.SaveCharacterDataToJson(characterInfo.characterData);


        // ทำสิ่งอื่นๆที่ต้องการหลังจากปลดล็อคตัวละคร เช่นอัปเดตสถานะของตัวละครในเกม
    }
}
