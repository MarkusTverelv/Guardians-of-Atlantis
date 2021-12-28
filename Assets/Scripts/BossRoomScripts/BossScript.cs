using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject[] Tentacles;
    public GameObject[] SpawnPoints;
    public GameObject[] movingBombSpawnPoints;
    public GameObject bomb;
    public GameObject indicator;
    public GameObject indicatorRune;
    public GameObject[] runeSpots;
    public GameObject[] activatedRuneSpots;
    public GameObject[] slamTentacles;
    public GameObject indicatorWarning, indicatorWarning2;
    public GameObject eyeTentacle, eyeTentacle2;
    public GameObject eye, eye2;
    public GameObject movingBomb;
    public GameObject leftMovingOilWall, rightMovingOilWall;

    public AudioSource audioSourceRune;
    public AudioSource audioBombExplosion;
    public AudioSource audioBossTalk;
    public AudioSource audioBossTalk2;

    public Transform bigBombIndicatorSpot;

    public Transform disappearSpot;
    public Transform bossSpot;
    public Transform phaseThreeBossSpot;

    private GameObject spawnedIndicator;

    private OilSpawner oilSpawner;
    float bombTimer = 0;
    float tentacleTimer = 10;
    float movingBombTimer = 0;

    public int amountOfTentaclesKilled = 0;
    public int totalTentaclesKilled = 0;
    public int runesActivated = 0;

    public int eyeHealth;

    bool phaseOneHasStarted = true;
    bool phaseTwoHasStarted = false;
    bool phaseThreeHasStarted = false;
    bool instantiatePhaseThree = false;

    bool shouldBombSpawn = true;
    bool shouldTentacleSpawn = true;

    bool bossMove = false;

    bool activateOnlyOnceTentacle = false;
    bool activateOnlyOnceTentacle2 = false;

    bool canSpawnMovingBombs;

    Vector2 bossPos;

    Coroutine lastRoutine = null;
    // Start is called before the first frame update
    void Start()
    {
        oilSpawner = GameObject.Find("OilSpawner").GetComponent<OilSpawner>();
        //bossPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        bombTimer += Time.deltaTime;
        tentacleTimer += Time.deltaTime;
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
            if (lastRoutine != null)
            {
                StopCoroutine(lastRoutine);
            }
            StartCoroutine(InstantiatePhaseThree());
            instantiatePhaseThree = false;
        }

        if (phaseThreeHasStarted)
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

        if (totalTentaclesKilled == 2)
        {
            instantiatePhaseThree = true;
            totalTentaclesKilled = 0;
        }


        if (bossMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, disappearSpot.position, 50 * Time.deltaTime);
        }

        if (!bossMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, phaseThreeBossSpot.position, 50 * Time.deltaTime);
        }

        if(!bossMove && !phaseThreeHasStarted)
        {
            transform.position = Vector2.MoveTowards(transform.position, bossSpot.position, 50 * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(activateRuneOrder());
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(activateTentacles());
        }

        if (runesActivated == 4)
        {
            Destroy(spawnedIndicator);
            runesActivated = 0;
        }

        if (eye != null)
        {
            if (eye.GetComponent<EyeScript>().health < 2 && !activateOnlyOnceTentacle)
            {
                eyeTentacle.GetComponent<EyeTentacles>().imActive = true;
                eye.GetComponent<CircleCollider2D>().enabled = false;
                activateOnlyOnceTentacle = true;
                StopAllCoroutines();
                StartCoroutine(activateRuneOrder());
            }
        }

        if (eye2 != null)
        {
            if (eye2.GetComponent<EyeScript>().health < 2 && !activateOnlyOnceTentacle2)
            {
                eyeTentacle2.GetComponent<EyeTentacles>().imActive = true;
                eye2.GetComponent<CircleCollider2D>().enabled = false;
                activateOnlyOnceTentacle2 = true;
                StopAllCoroutines();
                StartCoroutine(activateRuneOrder());
            }
        }

        if (!eyeTentacle.GetComponent<EyeTentacles>().imActive)
        {
            eye.GetComponent<CircleCollider2D>().enabled = true;
        }

        if (!eyeTentacle2.GetComponent<EyeTentacles>().imActive)
        {
            eye2.GetComponent<CircleCollider2D>().enabled = true;
        }


        if (canSpawnMovingBombs)
        {
            movingBombTimer += Time.deltaTime;
            if (movingBombTimer > 1.5F)
            {
                activateMovingBombs();
                movingBombTimer = 0;
            }
        }
    }

    void ActivateTentacles()
    {
        for (int i = 0; i < Tentacles.Length; i++)
        {
            int rnd = Random.Range(0, Tentacles.Length);
            GameObject tempGO = Tentacles[rnd];
            Tentacles[rnd] = Tentacles[i];
            Tentacles[i] = tempGO;
        }

        Tentacles[0].GetComponent<TentacleScript>().ImActive = true;
        Tentacles[1].GetComponent<TentacleScript>().ImActive = true;
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
        StartCoroutine(oilSpawner.oilTimer());
        yield return new WaitForSeconds(20);
        StartCoroutine(oilSpawner.fluidOil());
        yield return new WaitForSeconds(2);
        phaseOneHasStarted = true;
    }

    private IEnumerator BossPhaseTwo()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine(oilSpawner.oilTimer());
        yield return new WaitForSeconds(5);
        StartCoroutine(oilSpawner.OilSquare());
        yield return new WaitForSeconds(24);
        StartCoroutine(oilSpawner.OilSquareHard());
        yield return new WaitForSeconds(16);
        StartCoroutine(oilSpawner.oilTimer());
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
        transform.localScale = new Vector3(6, 6, 0);
        yield return new WaitForSeconds(4);
        phaseThreeHasStarted = true;
        bossMove = false;



    }

    private IEnumerator BossPhaseThree()
    {
        canSpawnMovingBombs = true;
        yield return new WaitForSeconds(4);
        audioBossTalk2.Play();
        leftMovingOilWall.GetComponent<moveLeftIndicatorScript>().canImoveLeft = true;
        rightMovingOilWall.GetComponent<moveIndicatorScript>().canImoveRight = true;
        yield return new WaitForSeconds(16);
        StartCoroutine(activateTentacles());
        yield return new WaitForSeconds(6);
        rightMovingOilWall.GetComponent<moveIndicatorScript>().startedMovingRight = true;
        rightMovingOilWall.GetComponent<moveIndicatorScript>().canImoveRight = true;
        yield return new WaitForSeconds(6);
        leftMovingOilWall.GetComponent<moveLeftIndicatorScript>().startedMovingLeft = true;
        leftMovingOilWall.GetComponent<moveLeftIndicatorScript>().canImoveLeft = true;
        yield return new WaitForSeconds(6);
        StartCoroutine(activateRuneOrder());
        yield return new WaitForSeconds(32);
        StartCoroutine(activateTentacles());
        yield return new WaitForSeconds(6);
        leftMovingOilWall.GetComponent<moveLeftIndicatorScript>().startedMovingLeft = true;
        leftMovingOilWall.GetComponent<moveLeftIndicatorScript>().canImoveLeft = true;
        rightMovingOilWall.GetComponent<moveIndicatorScript>().startedMovingRight = true;
        rightMovingOilWall.GetComponent<moveIndicatorScript>().canImoveRight = true;
        yield return new WaitForSeconds(4);
        phaseThreeHasStarted = true;
    }


    private IEnumerator activateRuneOrder()
    {
        audioSourceRune.Play();
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

        foreach (GameObject rune in activatedRuneSpots)
        {
            rune.GetComponent<SpriteRenderer>().enabled = true;
            rune.GetComponent<CircleCollider2D>().enabled = true;
            rune.GetComponent<runeScript>().myNumber = runesActivated;
            runesActivated++;
        }

        runesActivated = 0;

        spawnedIndicator = Instantiate(indicatorRune, bigBombIndicatorSpot.position, Quaternion.identity);
    }


    private IEnumerator activateTentacles()
    {
        indicatorWarning.SetActive(true);
        indicatorWarning2.SetActive(true);

        audioBossTalk.Play();

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
        yield return new WaitForSeconds(0.2f);
        slamTentacles[0].GetComponent<SlammingTentacleScript>().playSoundSlam();
        yield return new WaitForSeconds(2f);
        slamTentacles[2].GetComponent<SlammingTentacleScript>().imActive = true;
        slamTentacles[3].GetComponent<SlammingTentacleScript>().imActive = true;
        yield return new WaitForSeconds(0.2f);
        slamTentacles[0].GetComponent<SlammingTentacleScript>().playSoundSlam();
        yield return new WaitForSeconds(2f);
        slamTentacles[4].GetComponent<SlammingTentacleScript>().imActive = true;
        slamTentacles[5].GetComponent<SlammingTentacleScript>().imActive = true;
        yield return new WaitForSeconds(0.2f);
        slamTentacles[0].GetComponent<SlammingTentacleScript>().playSoundSlam();
    }

    //private IEnumerator activateEyeTentacles()
    //{
    //    eye.GetComponent<EyeTentacles>().imActive = true;
    //    eye2.GetComponent<EyeTentacles>().imActive = true;
    //}

    private void activateMovingBombs()
    {

        int rndNmb = Random.Range(0, 22);
        Instantiate(movingBomb, movingBombSpawnPoints[rndNmb].transform.position, Quaternion.identity);


    }

    public void playBombSound()
    {
        audioBombExplosion.Play();
    }

}
