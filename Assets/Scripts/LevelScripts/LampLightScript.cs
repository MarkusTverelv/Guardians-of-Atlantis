using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampLightScript : MonoBehaviour
{
    [SerializeField] GameObject lampPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = lampPosition.transform.position;
    }
}
