using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    public TextScript textCS;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pinko") || other.CompareTag("Yello"))
        {
            textCS.textAnimate();
        }
    }
}