using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class SafeArea : MonoBehaviour
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Rect lastSafeArea;

    [Serializable]
    public class SafeAreaChanged : UnityEvent { }

    [FormerlySerializedAs("onSizeChange")]
    [SerializeField]
    private SafeAreaChanged m_OnSizeChanged = new();

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    private void LateUpdate()
    {
        if (lastSafeArea != Screen.safeArea)
        {
            lastSafeArea = Screen.safeArea;
            Refresh();
        }
    }

    public void Refresh()
    {
        var inverseSize = new Vector2(1f, 1f) / canvas.pixelRect.size;
        var newAnchorMin = Vector2.Scale(lastSafeArea.position, inverseSize);
        var newAnchorMax = Vector2.Scale(lastSafeArea.position + lastSafeArea.size, inverseSize);

        rectTransform.anchorMin = newAnchorMin;
        rectTransform.anchorMax = newAnchorMax;

        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        m_OnSizeChanged.Invoke();
    }
}
