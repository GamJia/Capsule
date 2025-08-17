using UnityEngine;
using UnityEngine.EventSystems;

public class ClawManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public static ClawManager Instance => instance;
    private static ClawManager instance;

    [SerializeField] private Claw claw;
    private bool touchEnabled = true;
    
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        
    }

    void Start()
    {
        EnableTouchEvents(true);
    }

    public void EnableTouchEvents(bool enable)
    {
        touchEnabled = enable;
        if (enable)
        {
            claw.CreateCapsule(true);
        }
        
        else
        {
            claw.DestroyCapsule();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!touchEnabled) return;
        claw.BeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!touchEnabled) return;
        claw.OnDrag();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!touchEnabled) return;
        claw.EndDrag();
    }
}
