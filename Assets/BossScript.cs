using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject[] Tentacles;
    public GameObject[] SpawnPoints;
    public GameObject bomb;
    public GameObject indicator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            ActivateTentacles();
            SpawnBomb();
        }
    }

    void ActivateTentacles()
    {
        float rndNmb1 = Random.Range(0, Tentacles.Length);
        float rndNmb2 = Random.Range(0, Tentacles.Length);

        if(rndNmb1 == rndNmb2)
        {
            if(rndNmb1 >= 4)
            {
                rndNmb2 = Random.Range(0, 3);
            }

            else
            {
                rndNmb2 = Random.Range(5, Tentacles.Length - 1);
            }
        }

        for (int i = 0; i < Tentacles.Length; i++)
        {
            if(i == rndNmb1 || i == rndNmb2)
            {
                Tentacles[i].GetComponent<TentacleScript>().ImActive = true;
            }
        }

        Debug.Log(rndNmb1);
        Debug.Log(rndNmb2);
    }

    private void SpawnBomb()
    {
        int rndNmb = Random.Range(0, 7);

        Vector2 indicatorPos = SpawnPoints[rndNmb].transform.position;

        Instantiate(indicator, new Vector2(indicatorPos.x, indicatorPos.y - 15), Quaternion.identity);

        
        Instantiate(bomb, SpawnPoints[rndNmb].transform.position, Quaternion.identity);
    }
}
