using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreScript : MonoBehaviour
{
    TextMeshProUGUI tmp;
    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }
    int _score = 0;
    public int score
    {
        get { return _score; }
        set 
        {
            tmp.text = "Score: " + value;
            _score = value;
        }
    }
}
