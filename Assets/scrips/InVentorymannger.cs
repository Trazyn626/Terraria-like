using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InVentorymannger : MonoBehaviour
{
    public Canvas canvastrans;                   //UI����
    public static Transform canvas1;             //UI����λ�ã���������קitem�ű�������Ļ���λ��

    public Item[] startitems;                    //�����ʼ��Ʒ������
    public static InVentorymannger instance;     //ʵ�����洢������
   
    public int maxstackitem = 999 ;              //���ѵ�����
    public GameObject InVentoryItemPrefab;       //��ƷԤ�Ƽ�
    public InvenToryslot[] invenToryslots;       //������Ʒ��������
    public static Item selecteditem;             //ѡ�е���Ʒ
    
    int selectedslot = -1;                       //��ʼѡ����Ʒ�����
    int selectedslotidex;


    public static  Item checkeditem;
    public Image infoimage;

    private void Awake()
    {
        instance = this;                       //ʵ������Ʒ��
    }
    private void Start()
    {
        canvas1 = canvastrans.transform;       //����UIλ��

        changeselect(0);                       //������Ʒ��ѡ��

        foreach (var item in startitems)       //��ӳ�ʼ��Ʒ
        {
            AddItem(item);    
        }
        
    }
    private void Update()
    {
        if (checkeditem != null)
        {   infoimage.sprite = checkeditem.image;}
       
        
          selecteditem = GetselectedItem(false );//����ѡ����Ʒ���е���Ʒ

      
        if (Input.GetKeyDown(KeyCode.Alpha1))    //�л�ѡ�е���Ʒ��
        { changeselect(0); }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        { changeselect(1);  }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        { changeselect(2);  }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        { changeselect(3);  }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        { changeselect(4);  }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        { changeselect(5); }
        if (Input.GetKeyDown(KeyCode.Alpha7))
       { changeselect(6); }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        { changeselect(7); }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        { changeselect(8); }
    }


    void changeselect(int newValue)               //���ݱ���л�ѡ�е���Ʒ��

    {
        if (selectedslot >= 0)
        {
            invenToryslots[selectedslot].deselect();
        }
        invenToryslots[newValue].Select();
        selectedslot = newValue;

    }
    public bool AddItem(Item item)                  //�򱳰��������Ʒ
    {
        for (int i = 0; i < invenToryslots.Length; i++)//����Ƿ���δ�ﵽ���ѵ��������ظ���Ʒ�������
        {
            InvenToryslot slot = invenToryslots[i];
            InVentoryItem itemInSlot = slot.GetComponentInChildren<InVentoryItem>();
            if (itemInSlot != null && itemInSlot.item == item  && itemInSlot.count<maxstackitem && itemInSlot.item.stackable==true)
            {
                itemInSlot.count++;
                itemInSlot.Refreshcount();
                return true;
            }

        }
        for (int i = 0; i < invenToryslots.Length; i++)//����Ƿ��п�λ�������
        {
            InvenToryslot slot = invenToryslots[i];
            InVentoryItem itemInSlot = slot.GetComponentInChildren<InVentoryItem>();
            if (itemInSlot == null)
            {
                spawnNewItem(item, slot);
                return true;
             }
           
        }
 return false;

    }
    void spawnNewItem(Item item,InvenToryslot slot)                   //ͨ��Ԥ�Ƽ���������Ʒ����ʼ������
        {

        GameObject newItemGo = Instantiate(InVentoryItemPrefab, slot.transform);
        InVentoryItem inVentoryItem = newItemGo.GetComponent<InVentoryItem>();
        inVentoryItem.InitializesItem(item);

        }

    public Item GetselectedItem(bool use)                           //��ȡ��Ʒ���е���Ʒ��Ϣ
    {
        InvenToryslot slot = invenToryslots[selectedslot];
        InVentoryItem itemInSlot = slot.GetComponentInChildren<InVentoryItem>();
        if (itemInSlot != null)
        {
            Item item=itemInSlot.item;

            if (use == true)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                    return null;
                }
                else
                {
                    itemInSlot.Refreshcount();
                
                }

            }
            return item;
        }
        return null;
    }
        
  
}
