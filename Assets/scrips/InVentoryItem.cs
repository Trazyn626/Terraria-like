using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InVentoryItem : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler, IPointerDownHandler


{
 

    [Header("UI")]
    public Image image;
    public TextMeshProUGUI counttext;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Transform parentinDrag;
    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;
   
    public void InitializesItem(Item newitem)
    {
        item = newitem;
        image.sprite = newitem.image;
        if (item.type == ItemType.BuildingBlock)
        {
            image.transform.localScale = new Vector3 (0.5f,0.5f,1);

        
        }
        Refreshcount();
    
    }
    public void Refreshcount()
     {
        counttext.text = count.ToString();
        bool textactive = count > 1;
        counttext.gameObject.SetActive(textactive);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {   //BuildingSystem.canbulid = false;
        
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        parentinDrag = parentAfterDrag.parent;
        transform.SetParent(parentinDrag.parent);
        counttext.gameObject.SetActive(false);
       
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 _pos = Vector2.one;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(InVentorymannger.canvas1 as RectTransform , Input.mousePosition, null, out _pos);
        transform.position = _pos + new Vector2(Screen.width / 2F,Screen.height/2f);
      
    }

     public void OnPointerDown(PointerEventData eventData)
    { 
     InVentorymannger.checkeditem = item;
    }




    public void OnEndDrag(PointerEventData eventData)
    {
        // if  (BuildingSystem.inventoryopen) 
        //  {  
        //     BuildingSystem.canbulid = false;
        // }
        // else
        //  {
        //   BuildingSystem.canbulid = true;
        //  }
       
        image.raycastTarget = true;
        counttext.gameObject.SetActive(true);
        transform.SetParent(parentAfterDrag);
        Refreshcount();
        
    }



}
