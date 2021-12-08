using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCounterScript : MonoBehaviour
{
    int _gemCount = 0;
    DoorScript door;
    public int gemCount
    {
        get { return _gemCount; }
        set
        {
            if (value < 1)
                door.Open();
            _gemCount = value;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        door = GameObject.Find("Door").GetComponent<DoorScript>();
        GameObject yellowGems = GameObject.Find("YellowGems");
        GameObject pinkGems = GameObject.Find("PinkGems");
        foreach (Transform t in yellowGems.transform)
            _gemCount++;
        foreach (Transform t in pinkGems.transform)
            _gemCount++;
    }

}
