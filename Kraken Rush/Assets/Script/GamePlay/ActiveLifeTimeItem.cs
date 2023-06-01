using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ITEM
{
    Magnet,
    Projection,
    BonusMode
}
public class ActiveLifeTimeItem : MonoBehaviour
{
    public ITEM itemName;
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        item = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (item.activeSelf)
        {
            if (itemName == ITEM.Magnet)
                StartCoroutine(ActivateMagnet());
            if (itemName == ITEM.Projection)
                StartCoroutine(ActivateProjection());
            if (itemName == ITEM.BonusMode)
                StartCoroutine(ActivateBonusMode());
        }
    }

    IEnumerator ActivateMagnet()
    {
        yield return new WaitForSeconds(Singleton.Instance.magnet);
        item.SetActive(false);
    }
    IEnumerator ActivateProjection()
    {
        PlayerController.isProtection = true;
        yield return new WaitForSeconds(Singleton.Instance.protection);
        PlayerController.isProtection = false;
        item.SetActive(false);
    }
    IEnumerator ActivateBonusMode()
    {
        yield return new WaitForSeconds(Singleton.Instance.bonusMode);
        item.SetActive(false);
    }
}
