using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounterScript : MonoBehaviour
{
    public int Score;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
