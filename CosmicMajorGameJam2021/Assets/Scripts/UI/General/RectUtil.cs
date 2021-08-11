using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectUtil
{
    public static bool Overlaps(this RectTransform a, RectTransform b)
    {
        return (checkX(a,b) && checkY(a,b));
    }

    static bool checkX(RectTransform a, RectTransform b)
    {
        if (b.TopRight().x > a.BottomLeft().x)
        {
            if (b.BottomLeft().x < a.TopRight().x)
            {
                return true;
            }
        }

        return false;
    }
    
    static bool checkY(RectTransform a, RectTransform b)
    {
        if (b.TopRight().y > a.BottomLeft().y)
        {
            if (b.BottomLeft().y < a.TopRight().y)
            {
                return true;
            }
        }
        
        return false;
    }

    static Vector3 TopRight(this RectTransform a)
    {
        Vector3[] array = new Vector3[4];
        a.GetWorldCorners(array);
        return array[2];
    }
    
    static Vector3 BottomLeft(this RectTransform a)
    {
        Vector3[] array = new Vector3[4];
        a.GetWorldCorners(array);
        return array[0];
    }
}
