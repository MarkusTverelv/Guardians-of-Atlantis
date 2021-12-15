using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveLeftIndicatorScript : MonoBehaviour
{
    bool canImoveRight;
    public bool canImoveLeft = false;
    public bool startedMovingLeft = true;
    public GameObject[] spawnOilSpots;
    public GameObject oilExplosion;
    float posX;
    // Start is called before the first frame update
    void Start()
    {
        posX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(posX, transform.position.y, 0);
        if (canImoveLeft)
        {
            moveLeft();
            if (startedMovingLeft)
            {
                InvokeRepeating("SpawnOil", 1, 0.4f);
                startedMovingLeft = false;
            }


        }

        if (canImoveRight)
        {
            moveRight();
            CancelInvoke("SpawnOil");
        }
    }

    private void moveRight()
    {
        if (transform.position.x < 35)
        {
            posX += 1f;
        }

        else
        {
            canImoveRight = false;
        }
    }

    private void moveLeft()
    {
        if (transform.position.x > -75)
        {
            posX -= 0.05f;
        }

        else
        {
            canImoveLeft = false;
            startedMovingLeft = true;
            canImoveRight = true;
            
        }
    }

    private void SpawnOil()
    {
        for (int i = 0; i < spawnOilSpots.Length; i++)
        {
            GameObject oilRef = Instantiate(oilExplosion, spawnOilSpots[i].transform.position, Quaternion.identity);
            Destroy(oilRef, 1);
        }

    }
}