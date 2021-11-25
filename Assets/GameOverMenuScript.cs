using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverMenuScript : MonoBehaviour
{

    public void Restart()
    {
        GameObject.Find("ScoreCounter").GetComponent<ScoreCounterScript>().Score = 0;
        Destroy(GameObject.Find("Music"));
        SceneManager.LoadScene("Level");
        
    }
}
