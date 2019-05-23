using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SliderEvents : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] UnityEvent onPointerDownAction;
    [SerializeField] UnityEvent onPointerUpAction;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Sliding started");
        onPointerDownAction.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Sliding finished");
        onPointerUpAction.Invoke();
    }
}