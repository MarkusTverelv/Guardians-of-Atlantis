using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleScript : MonoBehaviour
{
    float posY;
    public bool ImActive = false;
    // Start is called before the first frame update
    void Start()
    {
        posY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (ImActive)
        {
            Move();
            Invoke("MoveDown", 4);
        }

        this.transform.position = new Vector2(transform.position.x, posY);

       
    }

    private void Move()
    {
        if(posY < 5)
        posY += 0.1f;
    }

    private void MoveDown()
    {
        ImActive = false;
        if (posY > -26)
            posY -= 0.1f;
    }
}
