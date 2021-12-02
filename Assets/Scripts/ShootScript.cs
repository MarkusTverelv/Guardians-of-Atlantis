using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class ShootScript : MonoBehaviour
{
    GameObject yello, pinko, arrowDirection, jellowProjectileObject;
    PlayerScript pinkoPlayerScript, yellloPlayerScript;
    
    [SerializeField] GameObject jellowProjectilePrefab;
    SpriteRenderer arrowSpriteRenderer;
    bool canShoot;
    List<SpriteRenderer> lineSprites = new List<SpriteRenderer>();
    CircleCollider2D yelloCircleCollider2D;
    SpriteRenderer pinkoSpriteRenderer;
    JellowProjectileScript jellowPro1jectileScript;
    Image yelloHB;

    // Start is called before the first frame update
    void Start()
    {
        
        try 
        {
            yelloHB = GameObject.FindGameObjectWithTag("YelloHB").GetComponent<Image>();
        }
        catch{ }
        
        pinkoPlayerScript = GetComponent<PlayerScript>();
        arrowDirection = GameObject.Find("ArrowAim");
        arrowSpriteRenderer = arrowDirection.GetComponent<SpriteRenderer>();
        yello = GameObject.Find("Yello");
        pinko = GameObject.Find("Pinko");
        yelloCircleCollider2D = yello.GetComponent<CircleCollider2D>();
        pinkoSpriteRenderer = pinko.GetComponent<SpriteRenderer>();
        yellloPlayerScript = yello.GetComponent<PlayerScript>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && yelloCircleCollider2D.enabled && pinkoSpriteRenderer.enabled)
        {
            //line.SetActive(false);
            StartCoroutine(AttachTimer());
        }
            

        if (Input.GetKeyDown(KeyCode.Return) && canShoot)
        {
            jellowPro1jectileScript.currentState = JellowProjectileScript.ProjectileState.fire;
            canShoot = false;
            Invoke("ActivateCollider", 0.25f);
        }
    }
    IEnumerator AttachTimer()
    {
        Debug.Log("AttachTimer");
        yellloPlayerScript.TurnOffOnComponents(false);
        jellowProjectileObject = Instantiate(jellowProjectilePrefab, yello.transform.position, Quaternion.identity);
        jellowPro1jectileScript = jellowProjectileObject.GetComponent<JellowProjectileScript>();
        if(yelloHB!=null)
            yelloHB.enabled = false;

        yield return new WaitForSeconds(2);

        jellowPro1jectileScript.currentState = JellowProjectileScript.ProjectileState.idle;
        canShoot = true;
        arrowSpriteRenderer.enabled = true;

    }
    private void ActivateCollider()
    {
        jellowProjectileObject.GetComponent<CircleCollider2D>().enabled = true;
        //line.SetActive(true);
    }

}
