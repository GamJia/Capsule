using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    private RectTransform rectTransform;
    private float initialTop = 60f;
    private float topIncreasePerRatio = 60f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        AdjustTop();
    }

    void AdjustTop()
    {
        float currentAspectRatio = Mathf.Round((float)Screen.height / Screen.width * 10) / 10;
        float targetAspectRatio = Mathf.Round((16f / 9f) * 10) / 10;

        int ratioInt = Mathf.RoundToInt((currentAspectRatio - targetAspectRatio) * 10);

        rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -(initialTop+ratioInt*60));
    }

    private void Update() {
        AdjustTop();
    }
}