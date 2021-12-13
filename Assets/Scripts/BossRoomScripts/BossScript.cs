using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject[] Tentacles;
    public GameObject[] SpawnPoints;
    public GameObject bomb;
    public GameObject indicator;
    public GameObject indicatorRune;
    public GameObject[] runeSpots;
    public GameObject[] activatedRuneSpots;
    public GameObject[] slamTentacles;
    public GameObject indicatorWarning, indicatorWarning2;
    public GameObject eyeTentacle, eyeTentacle2;
    public GameObject eye, eye2;


    public GameObject eyebrow, eyebrow2;

    public Transform disappearSpot;
    public Transform bossSpot;

    private GameObject spawnedIndicator;

    private OilSpawner oilSpawner;
    float bombTimer = 0;
    float tentacleTimer = 0;

    public int amountOfTentaclesKilled = 0;
    public int totalTentaclesKilled = 0;
    public int runesActivated = 0;

    public int eyeHealth;

    bool phaseTwoHasStarted = false;
    bool phaseOneHasStarted = true;
    bool phaseThreeHasStarted = false;
    bool instantiatePhaseThree = false;
    bool shouldBombSpawn = true;
    bool shouldTentacleSpawn = true;
    bool bossMove = false;
    bool activateOnlyOnce = false;

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

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(activateRuneOrder());
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(activateTentacles());
        }

        if (runesActivated == 4)
        {
            Destroy(spawnedIndicator);
        }

        if(eye.GetComponent<EyeScript>().health < 1 || eye2.GetComponent<EyeScript>().health < 1 && !activateOnlyOnce)
        {
            eyeTentacle.GetComponent<EyeTentacles>().imActive = true;
            eyeTentacle2.GetComponent<EyeTentacles>().imActive = true;
            activateOnlyOnce = true;
            StopAllCoroutines();
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


    private IEnumerator activateRuneOrder()
    {
        for (int i = 0; i < runeSpots.Length; i++)
        {
            int rnd = Random.Range(0, runeSpots.Length);
            GameObject tempGO = runeSpots[rnd];
            runeSpots[rnd] = runeSpots[i];
            runeSpots[i] = tempGO;

            GameObject tempGO2 = activatedRuneSpots[rnd];
            activatedRuneSpots[rnd] = activatedRuneSpots[i];
            activatedRuneSpots[i] = tempGO2;
        }



        yield return new WaitForSeconds(2);
        //Activate first rune
        runeSpots[0].GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        runeSpots[0].GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        runeSpots[0].GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        runeSpots[0].GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1);
        //Activate second rune
        runeSpots[1].GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        runeSpots[1].GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        runeSpots[1].GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        runeSpots[1].GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1);
        //Activate third rune
        runeSpots[2].GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        runeSpots[2].GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        runeSpots[2].GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        runeSpots[2].GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1);
        //Activate fourth rune
        runeSpots[3].GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        runeSpots[3].GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        runeSpots[3].GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        runeSpots[3].GetComponent<SpriteRenderer>().enabled = false;

        foreach(GameObject rune in activatedRuneSpots)
        {
            rune.GetComponent<SpriteRenderer>().enabled = true;
            rune.GetComponent<CircleCollider2D>().enabled = true;
            rune.GetComponent<runeScript>().myNumber = runesActivated;
            runesActivated++;
        }

        runesActivated = 0;

        spawnedIndicator = Instantiate(indicatorRune, transform.position, Quaternion.identity);
    }


    private IEnumerator activateTentacles()
    {
        indicatorWarning.SetActive(true);
        indicatorWarning2.SetActive(true);


        for (int i = 0; i < slamTentacles.Length; i++)
        {
            int rnd = Random.Range(0, slamTentacles.Length);
            GameObject tempGO = slamTentacles[rnd];
            slamTentacles[rnd] = slamTentacles[i];
            slamTentacles[i] = tempGO;
        }

        yield return new WaitForSeconds(2f);
        indicatorWarning.SetActive(false);
        indicatorWarning2.SetActive(false);
        slamTentacles[0].GetComponent<SlammingTentacleScript>().imActive = true;
        slamTentacles[1].GetComponent<SlammingTentacleScript>().imActive = true;
        yield return new WaitForSeconds(2f);
        slamTentacles[2].GetComponent<SlammingTentacleScript>().imActive = true;
        slamTentacles[3].GetComponent<SlammingTentacleScript>().imActive = true;
        yield return new WaitForSeconds(2f);
        slamTentacles[4].GetComponent<SlammingTentacleScript>().imActive = true;
        slamTentacles[5].GetComponent<SlammingTentacleScript>().imActive = true;
    }

    //private IEnumerator activateEyeTentacles()
    //{
    //    eye.GetComponent<EyeTentacles>().imActive = true;
    //    eye2.GetComponent<EyeTentacles>().imActive = true;
    //}

}
