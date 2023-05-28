using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.UIManager.Containers;
using UnityEngine;

public class ShowPopup : MonoBehaviour
{
    public string PopupName;

    public void Show()
    {
        var popup = UIPopup.Get(PopupName);
        if (popup == null) return;
        popup.Show();
    }

}
