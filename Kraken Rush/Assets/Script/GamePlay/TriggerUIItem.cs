using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUIItem : MonoBehaviour
{
    public DisplayItemUi ui;
    public DisplayItemData itemData;

    private void Start()
    {
        GameObject displayItemObject = GameObject.FindGameObjectWithTag("DisplayItem");

        if (displayItemObject != null)
        {
            ui = displayItemObject.GetComponent<DisplayItemUi>();
        }
        else
        {
            Debug.LogError("DisplayItem object not found!");
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            ui.CollectItem(itemData);
    }
}
