using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewManager : MonoBehaviour
{
    public ScrollRect scrollView;

    public void ScrollViewSet()
    {

        scrollView.verticalNormalizedPosition = 1f;
    }

}
