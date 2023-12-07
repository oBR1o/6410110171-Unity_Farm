using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHoldClick : MonoBehaviour
{
    FarmManager fm;

    public bool isMouseHold = false;

    private void Start()
    {
        fm = FindObjectOfType<FarmManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(fm.isPlanting|| fm.isSelecting)
        {
            if(Input.GetMouseButtonDown(0))
            {
                isMouseHold = true;
            }
            if(Input.GetMouseButtonUp(0))
            {
                isMouseHold = false;
            }
        }
    }
}
