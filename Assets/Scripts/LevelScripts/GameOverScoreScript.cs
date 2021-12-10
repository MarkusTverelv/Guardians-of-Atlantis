using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScoreScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Score: " + GameObject.Find("ScoreCounter").GetComponent<ScoreCounterScript>().Score;
    }

    
}
