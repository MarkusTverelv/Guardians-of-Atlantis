using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    Image healthbar;
    GameObject player;
    Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        healthbar = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        healthbar.fillAmount = (float)playerScript.currentHealth / playerScript.maxHealth;
        
    }
    
}
