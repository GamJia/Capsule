using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SizeElement
{
    Top,
    Middle,
    Bottom,
    Claw,
}


public class SizeResizer : MonoBehaviour
{
    
    public RectTransform UIElement; // UI 요소의 RectTransform
    [SerializeField] private SizeElement sizeElement;
    void Start()
    {

        switch (sizeElement)
        {
            case SizeElement.Top:
                ResizeTop();
                break;

            case SizeElement.Middle:
                ResizeMiddle();
                break;

            case SizeElement.Bottom:
                ResizeBottom();
                break;

            case SizeElement.Claw:
                ResizeClaw();
                break;

        }
        
    }

    void ResizeTop()
    {
        Vector3 worldPosition = GetUIElementWorldPosition(UIElement);
        transform.position = worldPosition;

        Vector3 worldScale = GetUIElementWorldScale(UIElement) * 100f;
        transform.localScale = worldScale;
    }

    void ResizeMiddle()
    {
        Vector3 worldPosition = GetUIElementWorldPosition(UIElement);
        float ratio = 1+(float)Screen.width / (float)Screen.height;

        transform.position = worldPosition;
        Vector3 worldScale = GetUIElementWorldScale(UIElement);

        Vector3 localScaleDelta = new Vector3(worldScale.x * 100, ratio, worldScale.z * 100);
        transform.localScale = localScaleDelta;
    }

    void ResizeBottom()
    {
        // UI 요소의 월드 좌표를 구함
        Vector3 worldPosition = GetUIElementWorldPosition(UIElement);
        transform.position = worldPosition;

        Vector3 worldScale = GetUIElementWorldScale(UIElement) * 100f;
        transform.localScale = worldScale;
    }

    void ResizeClaw()
    {
        Vector3 worldPosition = GetUIElementWorldPosition(UIElement);
        transform.position = worldPosition;

        Vector3 worldScale = GetUIElementWorldScale(UIElement) * 100f;
        transform.localScale = worldScale;
    }

    Vector3 GetUIElementWorldPosition(RectTransform UIElement)
    {
        Vector3 worldPosition = UIElement.TransformPoint(Vector3.zero);
        return worldPosition;
    }

    Vector3 GetUIElementWorldScale(RectTransform UIElement)
    {
        Vector3 worldScale = UIElement.lossyScale;
        return worldScale;
    }

}
