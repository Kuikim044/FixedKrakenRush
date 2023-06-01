using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProtection : MonoBehaviour
{
    public GameObject Protection;

    // Start is called before the first frame update
    void Start()
    {
        Protection = GameObject.FindGameObjectWithTag("Protection");
        Protection.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Protection.SetActive(true);
            Destroy(gameObject);
        }
    }
}
