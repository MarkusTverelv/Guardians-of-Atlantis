using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer.enabled)
        {
            transform.Rotate(Vector3.forward, -Input.GetAxisRaw("Horizontal") * 160 * Time.deltaTime);
        }

        else
        {
            return;
        }
    }
}
