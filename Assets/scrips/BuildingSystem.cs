using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class BuildingSystem : MonoBehaviour
{

    [SerializeField] private TileBase highligheTile;
    [SerializeField] private Tilemap mainTilemap;
    [SerializeField] private Tilemap tempTilemap;
    [SerializeField] private GameObject lootprefab;
    private Vector3 playerPos;
    private Vector3Int highlightedTilepos;
    private bool highlighted;
    private Item item;
    public LayerMask raycastlayermask;
    public static Vector3 mousePos;
    public bool autoAimng ;
    private Vector3Int autoaimgridpos;
    private float blockDiggingtime= 0.5f;
    private float blockplacingtime;
    private float lastDiggingtime;
    public static bool canaction;
    public static  bool inventoryopen;
    public void OnStartButtonClick()
    {
        if (inventoryopen == false)
        {
           
            inventoryopen = true;
        }
        else
        {
           
            inventoryopen = false;
            canaction = true;
        }

    }

    private Vector3Int GetMouseOnGridpos()
    {

        
        Vector3Int mouseCellpos = mainTilemap.WorldToCell(mousePos);
        mouseCellpos.z = 0;
        return mouseCellpos;
    }
    private Vector3Int Getautoaming()
    {
      
        RaycastHit2D hits = Physics2D.Raycast(new Vector2(playerPos.x, playerPos.y), mousePos - playerPos, Mathf.Sqrt((mousePos.x - playerPos.x) * (mousePos.x - playerPos.x) + (mousePos.y - playerPos.y) * (mousePos.y - playerPos.y)), raycastlayermask);
        Vector3 hitspo= hits.point;
        Vector3 posinside =(mousePos - playerPos) * 0.1f+hitspo;
        Vector3Int mouseCellpos = mainTilemap.WorldToCell(posinside);
        mouseCellpos.z = 0;
        return mouseCellpos;
    }
    private void Start()
    {
        canaction = true;
        inventoryopen = false;
    }
    private void Update()
    {
        if (inventoryopen == true)
        {
            canaction = false;

        }
        
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        item = InVentorymannger.selecteditem;
        if (canaction)
        {
            buliding();
        
        }
       
        
        
     
       
                
               
      

    }
    private void buliding()
    { 
      if (item != null)
        {
            // playerPos = mainTilemap.WorldToCell(transform.position);
            playerPos = transform.position;
            if (item.type != ItemType.Tool || item.type != ItemType.BuildingBlock)
            {
                tempTilemap.SetTile(highlightedTilepos, null);
            }
            if (item.type == ItemType.Tool || item.type == ItemType.BuildingBlock)
            {
                HighLightTile(item);
            }
            if (Input.GetMouseButton(0))
            {
                
                if (highlighted)
                    
                {
                     
                    if (lastDiggingtime == 0)
                    { 
                        lastDiggingtime = Time.time ;
                    }
                   
                    if (item.type == ItemType.BuildingBlock)
                    {
                        blockplacingtime = item.usingSpeed;
                        if((Time.time - lastDiggingtime) > blockplacingtime)
                        { 
                            Build(highlightedTilepos, item);
                            lastDiggingtime = 0;
                        } 
                        
                    }
                    else if (item.type == ItemType.Tool)
                    {
                         RuleTileWithData tile = mainTilemap.GetTile<RuleTileWithData>(highlightedTilepos);
                        if (item.miningPower>tile.item.minestable)
                        {                  
                          blockDiggingtime =  item.usingSpeed;
                          if ((Time.time - lastDiggingtime) > blockDiggingtime)
                          {
                            
                            Destroy(highlightedTilepos);
                            lastDiggingtime = 0;
                          }
                        }

                    }
                }

            }
        }
        if (item == null)
        {
            tempTilemap.SetTile(highlightedTilepos, null);

        }
    
    }
    private void HighLightTile(Item currentTtem)
    {
  
       Vector3Int mouseGridpos = GetMouseOnGridpos();
     
        if (InRange(playerPos, mouseGridpos, currentTtem.range))
        {

          if (autoAimng == true)
          {
                if (item.type == ItemType.Tool)
                {
                    autoaimgridpos = Getautoaming();
                    if (CheckCondition(mainTilemap.GetTile<RuleTileWithData>(autoaimgridpos), currentTtem))
                    {


                        tempTilemap.SetTile(autoaimgridpos, highligheTile);

                        highlightedTilepos = autoaimgridpos;
                        highlighted = true;
                    }
                    else { highlighted = false; }
                }
                else
                {
                    if (CheckCondition(mainTilemap.GetTile<RuleTileWithData>(mouseGridpos), currentTtem) && (Checksurrand(mainTilemap.GetTile<RuleTileWithData>(mouseGridpos + Vector3Int.up), currentTtem) || Checksurrand(mainTilemap.GetTile<RuleTileWithData>(mouseGridpos - Vector3Int.up), currentTtem) || Checksurrand(mainTilemap.GetTile<RuleTileWithData>(mouseGridpos + Vector3Int.left), currentTtem) || Checksurrand(mainTilemap.GetTile<RuleTileWithData>(mouseGridpos - Vector3Int.left), currentTtem)))
                    {
                       tempTilemap.SetTile(mouseGridpos, item.tile);

                        highlightedTilepos = mouseGridpos;
                        highlighted = true;
                    }
                    else 
                    { highlighted = false; }

                }
          }
          else
          {
               
                    if (item.type == ItemType.BuildingBlock)
                    { 
                       if (CheckCondition(mainTilemap.GetTile<RuleTileWithData>(mouseGridpos), currentTtem)&&(Checksurrand(mainTilemap.GetTile<RuleTileWithData>(mouseGridpos+Vector3Int.up), currentTtem)|| Checksurrand(mainTilemap.GetTile<RuleTileWithData>(mouseGridpos -Vector3Int.up), currentTtem)|| Checksurrand(mainTilemap.GetTile<RuleTileWithData>(mouseGridpos + Vector3Int.left), currentTtem)|| Checksurrand(mainTilemap.GetTile<RuleTileWithData>(mouseGridpos - Vector3Int.left), currentTtem)))
                       { 
                        tempTilemap.SetTile(mouseGridpos, item.tile);
                        highlightedTilepos = mouseGridpos;
                         highlighted = true;
                       }
                       else
                       {  
                        highlighted = false; 
                       }
                    }
                    else
                    {
                       if (CheckCondition(mainTilemap.GetTile<RuleTileWithData>(mouseGridpos), currentTtem))
                       { 
                        tempTilemap.SetTile(mouseGridpos, highligheTile);
                        }
                        else { highlighted = false; }
                    }
                  
               
               
          }
          
        }
        else
        {
            highlighted = false;
        }

    }
    private bool InRange(Vector3 postionA, Vector3 postionB, Vector2 Range)
    {

        Vector3 distance = postionA - postionB;
        if (Mathf.Abs(distance.x) >= Range.x || Mathf.Abs(distance.y) >= Range.y)
        {
            return false;
        }
        if (Mathf.Abs(distance.x) < 1&& Mathf.Abs(distance.y) < 1.5)
        {
            return false;
        }
        return true;

    }

    private bool CheckCondition(RuleTileWithData tile, Item currentItem)
    {
        if (currentItem.type == ItemType.BuildingBlock)
        {
            if (!tile)
            {
                return true;
            }
        }
        else if (currentItem.type == ItemType.Tool)
        {
            if (tile)
            {
                if (tile.item.actionType == currentItem.actionType)
                {
                    return true;
                }
            }

        }


        return false;

    }
    private bool Checksurrand(RuleTileWithData tile, Item currentItem)
    {
        if (currentItem.type == ItemType.BuildingBlock)
        {
            if (tile)
            {
                return true;
            }
        }
       

        return false;

    }


    private void Build(Vector3Int position, Item itemToBuild)
    {
        InVentorymannger.instance.GetselectedItem(true);

        tempTilemap.SetTile(position, null);

        highlighted = false;

        mainTilemap.SetTile(position, itemToBuild.tile);
    }
    private void Destroy(Vector3Int position)
    {
        tempTilemap.SetTile(position, null);
        highlighted = false;
        RuleTileWithData tile = mainTilemap.GetTile<RuleTileWithData>(position);
        mainTilemap.SetTile(position, null);
        Vector3 pos = mainTilemap.GetCellCenterWorld(position);
        GameObject loot = Instantiate(lootprefab, pos, Quaternion.identity);
        loot.GetComponent<Loot>().Initalize(tile.item);


    }
    
}
