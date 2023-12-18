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
        // 현재 비율과 원래 비율 간의 차이를 계산하고, 각각을 소수 한자리까지 살려둠
        float currentAspectRatio = Mathf.Round((float)Screen.height / Screen.width * 10) / 10;
        float targetAspectRatio = Mathf.Round((16f / 9f) * 10) / 10;

        int ratioInt = Mathf.RoundToInt((currentAspectRatio - targetAspectRatio) * 10);

        rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -(initialTop+ratioInt*60));
            
    }

    private void Update() {
        AdjustTop();
    }
}
