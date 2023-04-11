using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldCordninates : MonoBehaviour
{
    [SerializeField] TextMeshPro tmPRO;

    public void Setup(Vector3Int cords)
    {
            tmPRO.text = "(" + cords.x + "," + cords.y +")";
    }
}
