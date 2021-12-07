using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidParticleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("DestroyMe", 7);
    }

    void DestroyMe()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        Destroy(gameObject, 3);
    }
}
