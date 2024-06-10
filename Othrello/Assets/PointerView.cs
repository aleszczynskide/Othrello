using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PointerView : MonoBehaviour
{
    void Update()
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = -2f;
        transform.position = mouseWorldPos;
    }
}
