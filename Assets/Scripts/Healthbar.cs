using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public GameObject healthbar;
    GameObject pinko, yello, pinkoHealthbar, yelloHealthbar;
    Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        pinko = GameObject.Find("Pinko");
        yello = GameObject.Find("Yello");
        pinkoHealthbar = Instantiate(healthbar, pinko.transform.position, Quaternion.identity);
        pinkoHealthbar.transform.parent = transform;
        yelloHealthbar = Instantiate(healthbar, pinko.transform.position, Quaternion.identity);
        yelloHealthbar.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        yelloHealthbar.transform.position = yello.transform.position;
        pinkoHealthbar.transform.position = pinko.transform.position;
        
    }
    
}
