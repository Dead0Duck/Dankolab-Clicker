using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UpgradeButtonSizer : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnSizeChanged()
    {
        Vector2 size = rectTransform.sizeDelta;
        size.y = Screen.safeArea.width + size.x;
        rectTransform.sizeDelta = size;
    }
}
