﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;//drag edilecek transform
    private CanvasGroup canvasGroup;//drag edilecek transformun alpha değeriyle oynamak için component 
                                    //birde düzgün drop edilmesi için componentte bulunan blockraycast 
                                    //bollenını kollanmak için örneklendirilmiş bir component
    //private Vector3 initialPosition;//transformun başlangıç value sunu tutmak için bir variable
    [HideInInspector] public Transform baseSlot;//asıl yerinin positionı
    [SerializeField] private Canvas canvas;//transform düzgün hareket etmesi için canvas scalefactörünü kullanmak 
                                           //adına örneklendirme

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;//drag başladığı andaki alpha değeri
        canvasGroup.blocksRaycasts = false;//tekrar seçimi engellemek için bool ataması
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;//mouse tracking
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!GetHitCondition(eventData))//transformu bırakacağımız yerdeki object kontrolü
            rectTransform.anchoredPosition = baseSlot.localPosition;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        //object brakıldıktan sonraki setlemeler
    }

    private bool GetHitCondition(PointerEventData eventData)//transformun bırakılacağı yerde stot var mı yok mu kontrolü
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject.CompareTag("Slot"))
            {
                return true;
            }
        }
        return false;

        /*
         * bütün eventData verilri resultun içine atanır. 
         * ardından bu verilerdeki gamobjectler kontrol edilir 
         * ve eğer eventdata ucundaki gameobject slot tagindeyse true döner değilse false  
         */
    }
}
