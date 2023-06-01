using Doozy.Runtime.UIManager.Layouts.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayItemUi : MonoBehaviour
{
    public GameObject[] itemDisplayTrans;
    public Image[] itemDisplayImage;
    public Image[] itemDisplayImageFill;

    private DisplayItemData displayItem;

    public void CollectItem(DisplayItemData displayItemData)
    {
        displayItem = displayItemData;

        for (int i = 0; i < itemDisplayTrans.Length; i++)
        {
            StartCoroutine(ActivateAndCountdown(i));
        }
    }

    private IEnumerator ActivateAndCountdown(int index)
    {
        itemDisplayTrans[index].SetActive(true);
        itemDisplayImage[index].sprite = displayItem.itemSprite;
        float itemDuration = Singleton.Instance.GetItemDuration(displayItem.itemName);

        while (itemDuration > 0f)
        {
            itemDisplayImageFill[index].fillAmount = itemDuration / Singleton.Instance.GetItemDuration(displayItem.itemName);
            yield return null;
            itemDuration -= Time.deltaTime;
        }

        itemDisplayTrans[index].SetActive(false);
    }

}



