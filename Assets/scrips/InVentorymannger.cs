using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InVentorymannger : MonoBehaviour
{
    public Canvas canvastrans;                   //UI画布
    public static Transform canvas1;             //UI画布位置，用于在拖拽item脚本计算屏幕相对位置

    public Item[] startitems;                    //储存初始物品的数组
    public static InVentorymannger instance;     //实例化存储管理器
   
    public int maxstackitem = 999 ;              //最大堆叠数量
    public GameObject InVentoryItemPrefab;       //物品预制件
    public InvenToryslot[] invenToryslots;       //储存物品栏的数组
    public static Item selecteditem;             //选中的物品
    
    int selectedslot = -1;                       //初始选择物品栏编号
    int selectedslotidex;


    public static  Item checkeditem;
    public Image infoimage;

    private void Awake()
    {
        instance = this;                       //实例化物品栏
    }
    private void Start()
    {
        canvas1 = canvastrans.transform;       //传输UI位置

        changeselect(0);                       //重置物品栏选择

        foreach (var item in startitems)       //添加初始物品
        {
            AddItem(item);    
        }
        
    }
    private void Update()
    {
        if (checkeditem != null)
        {   infoimage.sprite = checkeditem.image;}
       
        
          selecteditem = GetselectedItem(false );//更新选中物品栏中的物品

      
        if (Input.GetKeyDown(KeyCode.Alpha1))    //切换选中的物品栏
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


    void changeselect(int newValue)               //根据编号切换选中的物品栏

    {
        if (selectedslot >= 0)
        {
            invenToryslots[selectedslot].deselect();
        }
        invenToryslots[newValue].Select();
        selectedslot = newValue;

    }
    public bool AddItem(Item item)                  //向背包中添加物品
    {
        for (int i = 0; i < invenToryslots.Length; i++)//检查是否有未达到最大堆叠数量的重复物品，并添加
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
        for (int i = 0; i < invenToryslots.Length; i++)//检查是否有空位，并添加
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
    void spawnNewItem(Item item,InvenToryslot slot)                   //通过预制件生成新物品并初始化属性
        {

        GameObject newItemGo = Instantiate(InVentoryItemPrefab, slot.transform);
        InVentoryItem inVentoryItem = newItemGo.GetComponent<InVentoryItem>();
        inVentoryItem.InitializesItem(item);

        }

    public Item GetselectedItem(bool use)                           //获取物品栏中的物品信息
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
