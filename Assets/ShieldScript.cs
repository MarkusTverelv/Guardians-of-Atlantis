using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldScript : MonoBehaviour
{
    GameObject pinko, line;
    public GameObject pinkoProjectile;
    GameObject pinkoProjectile2;
    bool canMove = false;
    // Start is called before the first frame update
    void Start()
    {
        pinko = GameObject.Find("Pinko");
        line = GameObject.Find("Line");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(ActivateShield());
        }

        if(canMove)
        {
            pinkoProjectile2.transform.position = Vector2.MoveTowards(pinkoProjectile2.transform.position, this.transform.position, 10 * Time.deltaTime);
        }
    }

    IEnumerator ActivateShield()
    {
        GetComponent<PlayerScript>().TurnOffOnComponents(pinko, line, false);
        pinkoProjectile2 = Instantiate(pinkoProjectile, pinko.transform.position, Quaternion.identity);
        GameObject.FindGameObjectWithTag("PinkoHB").GetComponent<Image>().enabled = false;
        canMove = true;
        yield return new WaitForSeconds(2);
        
    }
}
