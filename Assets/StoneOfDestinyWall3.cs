using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneOfDestinyWall3 : MonoBehaviour
{
    public static bool shouldIExist = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldIExist)
        {
            Destroy(gameObject);
        }
    }

    public void stopExisting()
    {
        shouldIExist = false;
    }
}
