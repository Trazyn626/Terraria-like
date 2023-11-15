using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu(menuName = "Tiles/breakableTile")]
public class BreakableTile :  RuleTile<BreakableTile>
{
    public Item Lootitem;
    public TileBase[] groundTile;
    public Tilemap thetilemap;
    public class Neighbor : RuleTile.TilingRule.Neighbor
    {
        public const int Any = 3;
        public const int Specifiec = 4;
        public const int notspecifiec = 5;
    }

    public override bool RuleMatch(int neighbor, TileBase other)
    {
        if (neighbor == 3)
        { return Checkany(other); }
        else if (neighbor == 4)
        { return CheckSpecified(other); }
        else if (neighbor == 5)
        { return Checknotspecific(other); }
        else if (neighbor == 6)
        { return checkair(other); }
        return base.RuleMatch(neighbor, other);
    }
    private bool checkGround(TileBase other)
    {
        int length = groundTile.Length;

        if (length > 0)
        {
            for (int i = 0; i <= length - 1; i++)
            {
                if (groundTile[i] != other)
                {    
                   
               
                }
            }

        }
        
        return true ;


    }
    private bool CheckSpecified(TileBase other)
    {
        int length = groundTile.Length;

        if (length > 0)
        {
            for (int i = 0; i <= length - 1; i++)
            {
                if (groundTile [i]== other)
                {
                    return true;
                }
            }

        }
        return false;
    }
    private bool Checkany(TileBase other)
    {

        if (other == this)
        {
            return true;
        }
        else
        {
            int length = groundTile.Length;

            if (length > 0)
            {
                for (int i = 0; i <= length - 1; i++)
                {
                    if (groundTile[i] == other)
                    {
                        return true;
                    }
                }

            }
        }

        return false;
    }

    private bool Checknotspecific(TileBase other)
    {
        int length = groundTile.Length;
        if (other == this)
        {
            return false;
        }
        if (length > 0)
        {
            for (int i = 0; i <= length - 1; i++)
            {
                if (groundTile[i] == other)
                {
                    return false;
                }
            }

        }

        return true;

    }
    private bool checkair(TileBase other)
    {



        if (other == null) return true;



        return false;

    }

}
