using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{   
    public static bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return (layerMask & (1 << layer)) != 0;
    }
}
