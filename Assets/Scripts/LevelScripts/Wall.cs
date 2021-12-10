using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    bool opening, open;

    public void Open()
    {
        if(!open)
        {
            Debug.Log("open");
            opening = true;
            open = true;
        }
    }

    private void FixedUpdate()
    {
        if(opening)
        {
            //Debug.Log("opening");
            transform.localScale += new Vector3(0, -0.05f, 0);
            if(transform.localScale.y<0)
            {
                opening = false;
            }
        }
        

    }
}
