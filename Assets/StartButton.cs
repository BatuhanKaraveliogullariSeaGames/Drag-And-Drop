using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private InventoryObject inventory;

    [SerializeField] private GameObject inventoryUI;//buttona basılınca setactive false yapmak için inventory gameobjecti

    [SerializeField] private List<InteractableSlot> interactableSlots = new List<InteractableSlot>();//selected olbjeleri liste yerleştirmek için slots list onları slotindex fieldlarını kullanmak için

    [SerializeField] private List<SlotButton> slotButtons = new List<SlotButton>();//Active deactive ve gun a değer atama için açılmış field

    public void StartButtonUI()
    {
        for (int i = 0; i < interactableSlots.Count; i++)
        {
            if(interactableSlots[i].currentObject != null)
            {
                inventory.itemHolder.Insert(interactableSlots[i].slotIndex, interactableSlots[i].currentObject.GetComponent<Item>().item);
            }
            else
            {
                var go = new Item();

                inventory.itemHolder.Insert(interactableSlots[i].slotIndex, go.item);
            }
        }

        /*
         *Üstteki döngüde slotların fieldindeki obje null mu değilmi o kontrol ediliyor.
         *O obje ancak atama yoksa null olabilir. 
         *eğer bir atama yoksa yani null sa inventorydeki list boş index olamıycağı için 
         *boş bir item variable ı oluşturulup boş olması gereken indexe atanıyor.
         */

        if(inventoryUI.activeInHierarchy)
        {
            if(inventoryUI != null)
                inventoryUI.SetActive(false);
        }

        //inventory i deactive ediyor.

        float j = -2f;

        for (int i = 0; i < inventory.itemHolder.Count; i++)
        {
            if(inventory.itemHolder[i] == null)
            {
                for (int k = 0; k < interactableSlots.Count; k++)
                {
                    if(interactableSlots[k].slotIndex == i)
                    {
                        if (slotButtons[k] != null)
                            slotButtons[k].gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                if(inventory.itemHolder[i] != null)
                {
                    for (int k = 0; k < interactableSlots.Count; k++)
                    {
                        if (interactableSlots[k].slotIndex == i)
                        {
                            if(slotButtons[k] != null)
                            {
                                slotButtons[k].gun = Instantiate(inventory.itemHolder[i].prefab, new Vector3(j, 0, 0), Quaternion.identity);

                                slotButtons[k].gun.SetActive(false);
                            }
                        }
                    }
                }
            }

            j += 2f;
        }

        /*
         * invertory objelerinin tutulduğu list insert işlemi tamamlanınca boş indexlere bakılıyor. 
         * eğer boş index varsa o indexe karşılık gelen slot button deactive oluyor.
         * eğer o indexte bir obje varsa o obje instantiate ediliyor ve slotbutton ile ilişkilendiriliyor.
         */
    }

    private void OnApplicationQuit()
    {
        inventory.itemHolder.Clear();
    }
}
