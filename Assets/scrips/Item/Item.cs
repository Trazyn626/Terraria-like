using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu(menuName = "GameObject/Item")]
public class Item : ScriptableObject
{
    public float damage;
    public float usingSpeed;
    public int miningPower;
    public int minestable;
    public GameObject ammo;
    public float manacost;
    public TileBase tile;
    public ItemType type;
    public ActionType actionType;
    public Vector2 range = new Vector2(5, 4);
    public bool stackable = true;
    public Sprite image;
    public float iteamscale;

}
public enum ItemType
{
    BuildingBlock,
    Tool,
   meleeweapon,
   rangeweapon

}
public enum ActionType
{
    dig,
    mine,
    meleeattack,
    openfire

}

