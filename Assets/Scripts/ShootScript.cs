using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class ShootScript : MonoBehaviour
{
    GameObject yello, line, pinko, arrowDirection, jellowProjectileObject;
    PlayerScript playerScript;
    [SerializeField] GameObject jellowProjectilePrefab;
    SpriteRenderer arrowSpriteRenderer;
    bool canShoot;
    List<SpriteRenderer> lineSprites = new List<SpriteRenderer>();
    CircleCollider2D yelloCircleCollider2D;
    SpriteRenderer pinkoSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        arrowDirection = GameObject.Find("ArrowAim");
        arrowSpriteRenderer = arrowDirection.GetComponent<SpriteRenderer>();
        yello = GameObject.Find("Yello");
        pinko = GameObject.Find("Pinko");
        line = GameObject.Find("Line");
        yelloCircleCollider2D = yello.GetComponent<CircleCollider2D>();
        pinkoSpriteRenderer = pinko.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && yelloCircleCollider2D.enabled && pinkoSpriteRenderer.enabled)
        {
            line.SetActive(false);
            StartCoroutine(AttachTimer());
        }
            

        if (Input.GetKeyDown(KeyCode.Return) && canShoot)
        {
            jellowProjectileObject.GetComponent<JellowProjectileScript>().currentState = JellowProjectileScript.ProjectileState.fire;
            canShoot = false;
            Invoke("ActivateCollider", 0.25f);
        }
    }
    IEnumerator AttachTimer()
    {
        playerScript.TurnOffOnComponents(yello, line, false);
        jellowProjectileObject = Instantiate(jellowProjectilePrefab, yello.transform.position, Quaternion.identity);
        GameObject.FindGameObjectWithTag("YelloHB").GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(2);
        jellowProjectileObject.GetComponent<JellowProjectileScript>().currentState = JellowProjectileScript.ProjectileState.idle;
        canShoot = true;
        arrowSpriteRenderer.enabled = true;

    }

    private void ActivateCollider()
    {
        jellowProjectileObject.GetComponent<CircleCollider2D>().enabled = true;
        line.SetActive(true);
    }

}
