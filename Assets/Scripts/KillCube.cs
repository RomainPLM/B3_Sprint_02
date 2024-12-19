using System;
using UnityEngine;

public class KillCube : MonoBehaviour
{
    private void OnEnable()
    {
        DestroyObjectsWithLayer(10);
    }

    private void DestroyObjectsWithLayer(int layer)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == layer)
            {
                Destroy(obj);
            }
        }
    }
}