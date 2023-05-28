using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActivateObjectAfterDelay : MonoBehaviour
{
    public GameObject objectToActivate;
    public float delayInSeconds = 5f;

    private void OnEnable()
    {
        Invoke(nameof(ActivateObject), delayInSeconds);
    }

    private void ActivateObject()
    {
        objectToActivate.SetActive(false);
    }
}
