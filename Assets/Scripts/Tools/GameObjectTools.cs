using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectTools
{
    public static GameObject[] GetChildren(GameObject parent)
    {
        int childCount = parent.transform.childCount;
        GameObject[] children = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
        {
            children[i] = parent.transform.GetChild(i).gameObject;
        }

        return children;
    }
}
