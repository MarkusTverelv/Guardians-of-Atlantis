using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldScript : MonoBehaviour
{
    //Attached to Yello

    GameObject pinko, line, pinkoProjectile2, shieldPreFab2;

    Image pinkoHBImage;
    public GameObject pinkoProjectile, shieldPreFab;
    SpriteRenderer spriteRenderer;
    
    
    bool canMove = false;
    PlayerScript yelloPlayerScript, pinkoPlayerScript;
    // Start is called before the first frame update
    void Start()
    {
        yelloPlayerScript = GetComponent<PlayerScript>();
        pinkoPlayerScript = GameObject.Find("Pinko").GetComponent<PlayerScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        pinko = GameObject.Find("Pinko");
        line = GameObject.Find("Line");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && spriteRenderer.enabled)
        {
            StartCoroutine(ActivateShield());
        }

        if(canMove)
        {
            pinkoProjectile2.transform.position = Vector2.MoveTowards(pinkoProjectile2.transform.position, transform.position, 10 * Time.deltaTime);
        }

        else if (!canMove && pinkoProjectile2 != null)
        {
            pinkoProjectile2.transform.position = Vector2.MoveTowards(pinkoProjectile2.transform.position, pinko.transform.position, 30 * Time.deltaTime);
        }
    }

    IEnumerator ActivateShield()
    {
        pinkoPlayerScript.TurnOffOnComponents(false);
        pinkoProjectile2 = Instantiate(pinkoProjectile, pinko.transform.position, Quaternion.identity);
        pinkoHBImage = GameObject.FindGameObjectWithTag("PinkoHB").GetComponent<Image>();
        pinkoHBImage.enabled = false;
        canMove = true;

        yield return new WaitForSeconds(2);

        shieldPreFab2 = Instantiate(shieldPreFab, transform.position, Quaternion.identity, transform);

        yield return new WaitForSeconds(5);

        Destroy(shieldPreFab2);
        canMove = false;

        yield return new WaitForSeconds(1);

        pinkoPlayerScript.TurnOffOnComponents(true);
        pinkoHBImage.enabled = true;
        Destroy(pinkoProjectile2);


    }
}
