using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlammingTentacleScript : MonoBehaviour
{
    GameObject player;
    Vector3 lookDirection;
    public bool imActive = false;
    bool moving = false;

    float tentacleTimer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Yello");
    }


    // Update is called once per frame
    void Update()
    {
        if (!imActive && !moving)
        {
            Look();
        }

        if(imActive)
        {
            moving = true;
            Move();
            tentacleTimer += Time.deltaTime;
            if (tentacleTimer > 3)
            {
                imActive = false;
                tentacleTimer = 0;
            }
        }

        if(!imActive && moving)
        {
            tentacleTimer += Time.deltaTime;
            MoveDown();
            if (tentacleTimer > 3)
            {
                moving = false; ;
                tentacleTimer = 0;
            }
        }

        
    }

    public void Look()
    {
        lookDirection = player.transform.position - transform.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, lookAngle);
        //transform.right = new Vector3(0, 0, lookAngle);
    }

    private void Move()
    {
        transform.position += transform.right * 20 * Time.deltaTime;
    }

    public void MoveDown()
    {
        transform.position += -transform.right * 20 * Time.deltaTime;
    }


}
