using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject[] Tentacles;
    public GameObject[] SpawnPoints;
    public GameObject bomb;
    public GameObject indicator;

    public GameObject eyebrow, eyebrow2;

    public Transform disappearSpot;
    public Transform bossSpot;

    private OilSpawner oilSpawner;
    float bombTimer = 0;
    float tentacleTimer = 0;

    public int amountOfTentaclesKilled = 0;
    public int totalTentaclesKilled = 0;

    public int eyeHealth;

    bool phaseTwoHasStarted = false;
    bool phaseOneHasStarted = true;
    bool phaseThreeHasStarted = false;
    bool instantiatePhaseThree = false;
    bool shouldBombSpawn = true;
    bool shouldTentacleSpawn = true;
    bool bossMove = false;

    Vector2 bossPos;

    Coroutine lastRoutine = null;
    // Start is called before the first frame update
    void Start()
    {
        oilSpawner = GameObject.Find("OilSpawner").GetComponent<OilSpawner>();
        bossPos = transform.position;
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

        if (tentacleTimer > 12 && shouldTentacleSpawn)
        {
            ActivateTentacles();
            tentacleTimer = 0;
        }

        if (phaseOneHasStarted && !phaseTwoHasStarted)
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
            lastRoutine = StartCoroutine(BossPhaseTwo());
            phaseTwoHasStarted = false;
        }

        if (instantiatePhaseThree)
        {
            shouldBombSpawn = false;
            shouldTentacleSpawn = false;
            StopCoroutine(lastRoutine);
            StartCoroutine(InstantiatePhaseThree());
            instantiatePhaseThree = false;
        }

        if(phaseThreeHasStarted)
        {
            StartCoroutine(BossPhaseThree());
            phaseThreeHasStarted = false;
        }

        if (amountOfTentaclesKilled == 3 && totalTentaclesKilled != 2)
        {
            shouldTentacleSpawn = false;
            phaseTwoHasStarted = true;
            amountOfTentaclesKilled = 0;
            totalTentaclesKilled++;
        }

        if (amountOfTentaclesKilled > 3)
        {
            amountOfTentaclesKilled = 3;
        }

        if(totalTentaclesKilled == 2)
        {
            instantiatePhaseThree = true;
            totalTentaclesKilled = 0;
        }


        if(bossMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, disappearSpot.position, 50 * Time.deltaTime);
        }

        if (!bossMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, bossSpot.position, 50 * Time.deltaTime);
        }
    }

    void ActivateTentacles()
    {
        float rndNmb1 = Random.Range(0, Tentacles.Length);
        float rndNmb2 = Random.Range(0, Tentacles.Length);

        if (rndNmb1 == rndNmb2)
        {
            if (rndNmb1 >= 4)
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
            if (i == rndNmb1 || i == rndNmb2)
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
        yield return new WaitForSeconds(20);
        StartCoroutine(oilSpawner.fluidOil());
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

    private IEnumerator InstantiatePhaseThree()
    {
        
        yield return new WaitForSeconds(4);
        bossMove = true;
        yield return new WaitForSeconds(4);
        transform.localScale = new Vector3(40, 40, 0);
        eyebrow.GetComponent<SpriteRenderer>().enabled = true;
        eyebrow2.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(4);
        phaseThreeHasStarted = true;
        bossMove = false;


    }

    private IEnumerator BossPhaseThree()
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
    }
}
