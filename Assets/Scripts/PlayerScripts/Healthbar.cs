using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public GameObject healthbar;
    GameObject pinko, yello, pinkoHealthbar, yelloHealthbar;
    PlayerSpecificScript yelloPlayerScript, pinkoPlayerScript;
    Image pinkoHealthbarImage, yelloHealthbarImage;

    // Start is called before the first frame update
    void Start()
    {
        pinko = GameObject.Find("Pinko");
        yello = GameObject.Find("Yello");

        pinkoHealthbar = Instantiate(healthbar, pinko.transform.position, Quaternion.identity);
        pinkoHealthbar.name = "Pinko Healthbar";
        pinkoHealthbar.transform.parent = transform;
        pinkoHealthbarImage = pinkoHealthbar.GetComponent<Image>();
        pinkoPlayerScript = pinko.GetComponent<PlayerSpecificScript>();
        pinkoHealthbar.gameObject.tag = "PinkoHB";
        //pinkoPlayerScript.healthbar = pinkoHealthbar.GetComponent<Image>();

        yelloHealthbar = Instantiate(healthbar, yello.transform.position, Quaternion.identity);
        yelloHealthbar.name = "Yello Healthbar";
        yelloHealthbar.transform.parent = transform;
        yelloHealthbarImage = yelloHealthbar.GetComponent<Image>();
        yelloPlayerScript = yello.GetComponent<PlayerSpecificScript>();
        yelloHealthbar.gameObject.tag = "YelloHB";
        //yelloPlayerScript.healthbar = yelloHealthbar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (yelloHealthbarImage.isActiveAndEnabled)
        {
            yelloHealthbar.transform.position = yello.transform.position;
            yelloHealthbarImage.fillAmount = (float)yelloPlayerScript.currentHealth / yelloPlayerScript.maxHealth;
        }
        if (pinkoHealthbarImage.isActiveAndEnabled)
        {
            pinkoHealthbar.transform.position = pinko.transform.position;
            pinkoHealthbarImage.fillAmount = (float)pinkoPlayerScript.currentHealth / pinkoPlayerScript.maxHealth;
        }
        

    }

}
