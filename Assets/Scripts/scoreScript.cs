using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class scoreScript : MonoBehaviour
{
    TextMeshProUGUI tmp;
    ScoreCounterScript counterScript;
    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
        tmp = GetComponent<TextMeshProUGUI>();
        //tmp.enabled = SceneManager.GetActiveScene().name == "Level";
        counterScript = GameObject.Find("ScoreCounter").GetComponent<ScoreCounterScript>();

    }
    
    public int score
    {
        get { return counterScript.Score; }
        set 
        {
            tmp.text = "Score: " + value;
            counterScript.Score = value;
        }
    }
}
