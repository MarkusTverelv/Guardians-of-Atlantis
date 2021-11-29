using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public GameObject healthbar;
    GameObject pinko, yello, pinkoHealthbar, yelloHealthbar;
    PlayerScript yelloPlayerScript, pinkoPlayerScript;
    Image pinkoHealthbarImage, yelloHealthbarImage;
    // Start is called before the first frame update
    void Start()
    {
        pinko = GameObject.Find("Pinko");
        yello = GameObject.Find("Yello");

        pinkoHealthbar = Instantiate(healthbar, pinko.transform.position, Quaternion.identity);
        pinkoHealthbar.transform.parent = transform;
        pinkoHealthbarImage = pinkoHealthbar.GetComponent<Image>();
        pinkoPlayerScript = pinko.GetComponent<PlayerScript>();

        yelloHealthbar = Instantiate(healthbar, pinko.transform.position, Quaternion.identity);
        yelloHealthbar.transform.parent = transform;
        yelloHealthbarImage = yelloHealthbar.GetComponent<Image>();
        yelloPlayerScript = yello.GetComponent<PlayerScript>();
        yelloHealthbar.gameObject.tag = "YelloHB";
    }

    // Update is called once per frame
    void Update()
    {
        yelloHealthbar.transform.position = yello.transform.position;
        yelloHealthbarImage.fillAmount = (float)yelloPlayerScript.currentHealth / yelloPlayerScript.maxHealth;
        pinkoHealthbar.transform.position = pinko.transform.position;
        pinkoHealthbarImage.fillAmount = (float)pinkoPlayerScript.currentHealth / pinkoPlayerScript.maxHealth;
        
    }
    
}
