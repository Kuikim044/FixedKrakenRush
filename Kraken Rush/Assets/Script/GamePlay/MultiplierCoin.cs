using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierCoin : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(CheckLifeItem());
        }
    }

    IEnumerator CheckLifeItem()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }
}
