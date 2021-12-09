using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject[] Tentacles;
    public GameObject[] SpawnPoints;
    public GameObject bomb;
    public GameObject indicator;
    private OilSpawner oilSpawner;
    float bombTimer = 0;
    float tentacleTimer = 0;
    public int amountOfTentaclesKilled = 0;


    bool phaseTwoHasStarted = false;
    bool phaseOneHasStarted = true;
    bool phaseThreeHasStarted = false;
    bool shouldBombSpawn = true;
    bool shouldTentacleSpawn = true;

    Coroutine lastRoutine = null;
    // Start is called before the first frame update
    void Start()
    {
        oilSpawner = GameObject.Find("OilSpawner").GetComponent<OilSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        bombTimer += Time.deltaTime;
        tentacleTimer += Time.deltaTime;

        Debug.Log(phaseOneHasStarted);
        //Spawning bombs every 2 seconds when phase one is active.
        if (bombTimer > 2 && shouldBombSpawn)
        {
            SpawnBomb();
            bombTimer = 0;
        }

        if(tentacleTimer > 12 && shouldTentacleSpawn)
        {
            ActivateTentacles();
            tentacleTimer = 0;
        }

        if(phaseOneHasStarted && !phaseTwoHasStarted)
        {
            shouldBombSpawn = true;
            shouldTentacleSpawn = true;
            lastRoutine = StartCoroutine(BossPhaseOne());
            phaseOneHasStarted = false;
        }

        if (phaseTwoHasStarted)
        {
            StopCoroutine(lastRoutine);
            shouldBombSpawn = false;
            StartCoroutine(BossPhaseTwo());
            phaseTwoHasStarted = false;
        }

        if (phaseThreeHasStarted)
        {
            
        }

        if(amountOfTentaclesKilled == 2)
        {
            shouldTentacleSpawn = false;
            phaseTwoHasStarted = true;
            amountOfTentaclesKilled = 0;
        }

        if(amountOfTentaclesKilled > 2)
        {
            amountOfTentaclesKilled = 2;
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
    }

    private void SpawnBomb()
    {
        int rndNmb = Random.Range(0, 7);

        Vector2 indicatorPos = SpawnPoints[rndNmb].transform.position;

        Instantiate(indicator, new Vector2(indicatorPos.x, indicatorPos.y - 15), Quaternion.identity);

        
        Instantiate(bomb, SpawnPoints[rndNmb].transform.position, Quaternion.identity);
    }

    private IEnumerator BossPhaseOne()
    {
        yield return new WaitForSeconds(10);
        shouldBombSpawn = false;
        StartCoroutine(oilSpawner.fluidOil());
        yield return new WaitForSeconds(10);
        shouldBombSpawn = true;
        yield return new WaitForSeconds(2);
        phaseOneHasStarted = true;
    }

    private IEnumerator BossPhaseTwo()
    {
        yield return new WaitForSeconds(4);
        StartCoroutine(oilSpawner.oilTimer());
        yield return new WaitForSeconds(5);
        StartCoroutine(oilSpawner.OilInRoom());
        yield return new WaitForSeconds(16);
        StartCoroutine(oilSpawner.OilZigZag());
        yield return new WaitForSeconds(6);
        StartCoroutine(oilSpawner.BigOilInRoom());
        yield return new WaitForSeconds(3);

        if (amountOfTentaclesKilled < 8)
        {
            phaseOneHasStarted = true;
            bombTimer = 0;
            tentacleTimer = 0;
        }

        else
        {
            phaseThreeHasStarted = true;
        }
    }
}
