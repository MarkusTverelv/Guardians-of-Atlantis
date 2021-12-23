using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGem : MonoBehaviour
{
    public GameObject explosion;
    public Text healthText;
    private static bool imAlive = true;

    private void Start()
    {
        if (!imAlive)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pinko") || other.gameObject.CompareTag("Yello"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            other.gameObject.transform.parent.GetComponent<PlayerSharedScript>().AddMaxHealth();
            healthText.GetComponent<TextScript>().textAnimate();

            imAlive = false;
            Destroy(gameObject);
        }
    }
}
