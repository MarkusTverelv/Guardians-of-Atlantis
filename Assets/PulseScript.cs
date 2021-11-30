using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseScript : MonoBehaviour
{
    List<GameObject> enemyList;
    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            addForce();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            enemyList.Add(collision.gameObject);
        Debug.Log(enemyList.Count);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            enemyList.Remove(collision.gameObject);
        Debug.Log(enemyList.Count);
    }

    private void addForce()
    {
        for (int i = 0; i < 10; i++)
        {
            foreach (GameObject l in enemyList)
            {
                Rigidbody2D rb = l.GetComponent<Rigidbody2D>();
                Vector2 position = l.transform.position - this.transform.position;
                rb.AddForce(position, ForceMode2D.Impulse);
            }
        }
    }
}
