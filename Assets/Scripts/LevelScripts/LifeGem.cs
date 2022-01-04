using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGem : MonoBehaviour

{
    AudioScript audioScript;
    public GameObject explosion;
    public Text healthText;
    private static bool imAlive = true;
    private static bool imAlive2 = true;
    private static bool imAlive3 = true;
    private static bool imAlive4 = true;
    public int lifeNmbr;
    public GameObject lifeGem;
    public GameObject lifeGem2;
    public GameObject lifeGem3;
    public GameObject lifeGem4;

    private void Start()
    {
        audioScript = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioScript>();
        if (!imAlive)
        {
            lifeGem.GetComponent<SpriteRenderer>().enabled = false;
            lifeGem.GetComponent<CircleCollider2D>().enabled = false;
        }
        if (!imAlive2)
        {
            lifeGem2.GetComponent<SpriteRenderer>().enabled = false;
            lifeGem2.GetComponent<CircleCollider2D>().enabled = false;
        }
        if (!imAlive3)
        {
            lifeGem3.GetComponent<SpriteRenderer>().enabled = false;
            lifeGem3.GetComponent<CircleCollider2D>().enabled = false;
        }
        if (!imAlive4)
        {
            lifeGem4.GetComponent<SpriteRenderer>().enabled = false;
            lifeGem4.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pinko") || other.gameObject.CompareTag("Yello"))
        {
            if (lifeNmbr == 1)
            {
                audioScript.playLifeSound();
                Instantiate(explosion, transform.position, Quaternion.identity);
                other.gameObject.transform.parent.GetComponent<PlayerSharedScript>().AddMaxHealth();
                healthText.GetComponent<TextScript>().textAnimate();

                imAlive = false;
                Destroy(gameObject);
            }

            if (lifeNmbr == 2)
            {
                audioScript.playLifeSound();
                Instantiate(explosion, transform.position, Quaternion.identity);
                other.gameObject.transform.parent.GetComponent<PlayerSharedScript>().AddMaxHealth();
                healthText.GetComponent<TextScript>().textAnimate();

                imAlive2 = false;
                Destroy(gameObject);
            }
            if (lifeNmbr == 3)
            {
                audioScript.playLifeSound();
                Instantiate(explosion, transform.position, Quaternion.identity);
                other.gameObject.transform.parent.GetComponent<PlayerSharedScript>().AddMaxHealth();
                healthText.GetComponent<TextScript>().textAnimate();

                imAlive3 = false;
                Destroy(gameObject);
            }
            if (lifeNmbr == 4)
            {
                audioScript.playLifeSound();
                Instantiate(explosion, transform.position, Quaternion.identity);
                other.gameObject.transform.parent.GetComponent<PlayerSharedScript>().AddMaxHealth();
                healthText.GetComponent<TextScript>().textAnimate();

                imAlive4 = false;
                Destroy(gameObject);
            }
        }
    }
}
