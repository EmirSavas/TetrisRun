using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [HideInInspector]public Vector2 firstPoint;
    [HideInInspector]public Vector2 releasePoint;
    [HideInInspector]public bool isDraging = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPoint = Input.mousePosition;
            isDraging = true;
        }
    }


    public void Reset()
    {
        isDraging = false;
        firstPoint = Vector2.zero;
        releasePoint = Vector2.zero;
    }
}
