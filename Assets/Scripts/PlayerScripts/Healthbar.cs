using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public GameObject healthbar;
    GameObject pinko, yello, pinkoHealthbar, yelloHealthbar;
    PlayerSharedScript playerShared;
    Image pinkoHealthbarImage, yelloHealthbarImage;

    // Start is called before the first frame update
    void Start()
    {
        playerShared = FindObjectOfType<PlayerSharedScript>();
        pinko = GameObject.Find("Pinko");
        yello = GameObject.Find("Yello");

        pinkoHealthbar = Instantiate(healthbar, pinko.transform.position, Quaternion.identity);
        pinkoHealthbar.name = "Pinko Healthbar";
        pinkoHealthbar.transform.parent = transform;
        pinkoHealthbarImage = pinkoHealthbar.GetComponent<Image>();
        pinkoHealthbar.gameObject.tag = "PinkoHB";

        yelloHealthbar = Instantiate(healthbar, yello.transform.position, Quaternion.identity);
        yelloHealthbar.name = "Yello Healthbar";
        yelloHealthbar.transform.parent = transform;
        yelloHealthbarImage = yelloHealthbar.GetComponent<Image>();
        yelloHealthbar.gameObject.tag = "YelloHB";
    }

    // Update is called once per frame
    void Update()
    {
        yelloHealthbar.transform.position = yello.transform.position;
        yelloHealthbarImage.fillAmount = playerShared.currentHealth / playerShared.maxHealth;

        pinkoHealthbar.transform.position = pinko.transform.position;
        pinkoHealthbarImage.fillAmount = playerShared.currentHealth / playerShared.maxHealth;

        Debug.Log("Pinko" + pinkoHealthbarImage.fillAmount);
        Debug.Log("Yello" + yelloHealthbarImage.fillAmount);

    }

}
