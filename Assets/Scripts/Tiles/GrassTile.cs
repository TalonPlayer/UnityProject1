using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : Tile
{
    [SerializeField] private Color offsetColor, inRangeOffsetColor;
    private bool isOffset;
    public override void Init(int x, int y)
    {                
        isOffset = (x + y) % 2 == 1;
        render.color = isOffset ? offsetColor : baseColor;
    }

    public override void SetRangeBool(bool active){
        InRange = active;
        if (isOffset) render.color = active ? inRangeOffsetColor: offsetColor;
        else{
            render.color = active ? inRangeColor: baseColor;
        }
    }
}
