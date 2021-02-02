using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractableSlot : MonoBehaviour, IDropHandler
{
    public GameObject currentObject;//Obje slota bırakıldıktan sonra atama o obje olur.
    public int slotIndex;//listenin indexlerini etkileyebilmek için kullanılacak bir sabit

    public void OnDrop(PointerEventData eventData)
    {
        if(currentObject == null)
        {
            if (eventData.pointerDrag != null)//draggable bir obje varsa eventData da true döner ve pozisyonlar setlenir
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                currentObject = eventData.pointerDrag;
            }
        }
        else
        {
            currentObject.GetComponent<RectTransform>().anchoredPosition = currentObject.GetComponent<Draggable>().baseSlot.GetComponent<RectTransform>().anchoredPosition;

            currentObject = null;

            if (eventData.pointerDrag != null)//draggable bir obje varsa eventData da true döner ve pozisyonlar setlenir
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                currentObject = eventData.pointerDrag;
            }
        }
    }
}
