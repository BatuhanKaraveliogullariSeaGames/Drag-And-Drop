using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotButton : MonoBehaviour
{
    [HideInInspector] public GameObject gun = null;

    public void SlotButtonUI()
    {
        if(gun.activeInHierarchy)
        {
            gun.SetActive(false);
        }
        else
        {
            gun.SetActive(true);
        }

        //burada ise silah set activation yapılıyor.
    }
}
