using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public GameObject currentObject;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)//draggable bir obje varsa eventData da true döner ve pozisyonlar setlenir
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            currentObject = eventData.pointerDrag;
        }
    }
}
