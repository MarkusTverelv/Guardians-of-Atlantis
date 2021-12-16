using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverScript : MonoBehaviour
{
    public bool imActive;
    float leverTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(imActive)
        {
            if (transform.rotation.z > -50 && leverTimer < 0.5f)
            {
                leverTimer += Time.deltaTime;
                transform.Rotate(0, 0, -1);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.F))
        {
            imActive = true;
        }
    }
}
