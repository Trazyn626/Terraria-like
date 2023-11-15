using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InvenToryslot : MonoBehaviour, IDropHandler
{


    public Image image;
    public Color Selectcolor,noselectcolor;
    private void Awake()
    {
        deselect();
    }
    public void Select()
    {

        image.color = Selectcolor;
    
    }
   public  void deselect()
    {
        image.color = noselectcolor;

    }
    public void OnDrop(PointerEventData eventData)
    {
       if (transform.childCount == 0)
              {
          
            InVentoryItem inVentoryItem = eventData.pointerDrag.GetComponent<InVentoryItem>();
         inVentoryItem.parentAfterDrag = transform;
               }
        else
        {
         
            InVentoryItem inVentoryItem = eventData.pointerDrag.GetComponent<InVentoryItem>();
            transform.GetChild(0).transform.SetParent(inVentoryItem.parentAfterDrag);
            inVentoryItem.parentAfterDrag = transform;
        }
    }
   // public void OnMouseOver(PointerEventData data)
  //  {
     //   if (Input.GetMouseButton(0))
    // {
       //  InVentorymannger.checkeditem =data.pointerCurrentRaycast.gameObject.GetComponent<InVentoryItem>().item;       
     //  }

    // }
}
