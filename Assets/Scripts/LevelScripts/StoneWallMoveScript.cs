using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneWallMoveScript : MonoBehaviour
{
    public GameObject lever, lever2, lever3;
    leverScript leverScript, leverScript2, leverScript3;
    public GameObject moveSpot;
    // Start is called before the first frame update
    void Start()
    {
        leverScript = lever.GetComponent<leverScript>();
        leverScript2 = lever2.GetComponent<leverScript>();
        leverScript3 = lever3.GetComponent<leverScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(leverScript.imActive && leverScript2.imActive && leverScript3.imActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpot.transform.position, 2 * Time.deltaTime);
        }
    }
}
