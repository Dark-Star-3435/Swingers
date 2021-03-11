using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    public GrapplingRope rope;

    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) //locates the mouse position 
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rope.setStart(worldPos);
        }
    }
}
