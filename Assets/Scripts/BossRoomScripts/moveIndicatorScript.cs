using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveIndicatorScript : MonoBehaviour
{
    public bool canImoveRight = false;
    bool canImoveLeft;
    public bool startedMovingRight = true;
    public GameObject[] spawnOilSpots;
    public GameObject oilExplosion;
    float posX;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        posX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(posX, transform.position.y, 0);
        if (canImoveRight)
        {
            moveRight();
            if (startedMovingRight)
            {
                InvokeRepeating("SpawnOil", 1, 0.4f);
                startedMovingRight = false;
            }


        }

        if(canImoveLeft)
        {
            moveLeft();
            CancelInvoke("SpawnOil");
        }
    }

    private void moveRight()
    {
        if (transform.position.x < 35)
        {
            posX += 6f * Time.deltaTime;
        }

        else
        {
            canImoveRight = false;
            startedMovingRight = true;
            canImoveLeft = true;
        }
    }

    private void moveLeft()
    {
        if (transform.position.x > -75)
        {
            posX -= 1f;
        }

        else
        {
            canImoveLeft = false;
        }
    }

    private void SpawnOil()
    {
        audioSource.Play();
        for (int i = 0; i < spawnOilSpots.Length; i++)
        {
            GameObject oilRef = Instantiate(oilExplosion, spawnOilSpots[i].transform.position, Quaternion.identity);
            Destroy(oilRef, 2);
        }
    }
}
