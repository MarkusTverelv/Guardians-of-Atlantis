using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum NewPlayerStates
{
    Idle,
    Moving,
    Shield,
    Attack,
    Dash
}

public class PlayerSharedScript : MonoBehaviour
{
    public float moveSpeed, turnSpeed, maxMoveSpeed;

    public UnityEvent onCheckpointSet = new UnityEvent();

    public GameObject pinko, yello;
    public ParticleSystem swooschEffect;

    public AudioClip hurtSound;

    public AudioSource dashSound;
    public AudioSource shieldSound;
    public AudioSource talkSource;
    public AudioSource talkSource2;
    public AudioSource damageSource;
    public AudioSource shootSource;

    public SpriteRenderer yelloSprite;
    public SpriteRenderer pinkoSprite;

    GameMaster gm;
    DashScriptNew dashScriptNew;
    PlayerSpecificScript pinkoMovement, yelloMovement;
    ShootScriptNew shootScriptNew;

    Rigidbody2D yelloRigidbody;
    Rigidbody2D pinkoRigidbody;

    Vector3 midPoint;

    float invTime = 2.0f;
    float distance = 0.0f;

    float stayTimer = 0.0f;
    float stayTime = 10.0f;
    float shootTimer = 0;
    float dashTimer = 0;

    bool inv = false;
    bool Shield = false;

    bool shoot;
    bool shootPower = true;
    bool shootTimerBool = false;

    public float timerShield = 0.0f;
    public float cooldownShield = 0.0f;

    private static int dashCharges = 1;
    public static int maxDashCharges = 1;

    [HideInInspector]
    public float currentHealth;

    [HideInInspector]
    public float maxHealth = 3;
    public static float savedHealth = 3;

    NewPlayerStates currentState = NewPlayerStates.Moving;
    public GameObject shieldPrefab;

    string sceneName;
    Scene currentScene;
    LevelChangerScript lcs;

    // Start is called before the first frame update
    void Start()
    {
        swooschEffect.Stop();

        timerShield = cooldownShield;

        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;

        maxHealth = savedHealth;

        Physics2D.IgnoreCollision(yello.GetComponent<Collider2D>(), pinko.GetComponent<Collider2D>());
        yelloMovement = yello.GetComponent<PlayerSpecificScript>();
        pinkoMovement = pinko.GetComponent<PlayerSpecificScript>();

        dashScriptNew = yello.GetComponent<DashScriptNew>();
        shootScriptNew = pinko.GetComponent<ShootScriptNew>();

        yelloRigidbody = yello.GetComponent<Rigidbody2D>();
        pinkoRigidbody = pinko.GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        currentScene = SceneManager.GetActiveScene();
        lcs = GameObject.Find("LevelChanger").GetComponent<LevelChangerScript>();
        // Retrieve the name of this scene.
        sceneName = currentScene.name;
    }

    private void Update()
    {
        #region Shoot

        if (Input.GetKeyDown(KeyCode.Keypad2) && !shootTimerBool && CheckCurrentState(NewPlayerStates.Moving))
            currentState = NewPlayerStates.Attack;

        if (CheckCurrentState(NewPlayerStates.Attack))
        {
            if (Input.GetKey(KeyCode.Keypad3))
                shootPower = false;

            if (Input.GetKeyUp(KeyCode.Keypad3))
            {
                shoot = true;
            }
        }

        if (shootTimerBool)
        {
            shootTimer += Time.deltaTime;

            if (shootTimer > 8)
            {
                shootTimerBool = false;
                shoot = false;
                shootTimer = 0;
            }
        }
        #endregion

        #region Shield

        timerShield += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!CheckCurrentState(NewPlayerStates.Shield) && CheckCurrentState(NewPlayerStates.Moving))
            {
                if (timerShield >= cooldownShield)
                {
                    talkSource2.Play();
                    shieldSound.Play();

                    Shield = true;
                }
            }
        }

        if (Shield)
        {
            currentState = NewPlayerStates.Shield;
        }

        #endregion

        #region Dash

        if (Input.GetKeyDown(KeyCode.Keypad1) && dashCharges != 0 && CheckCurrentState(NewPlayerStates.Moving))
        {
            currentState = NewPlayerStates.Dash;
            dashCharges -= 1;
        }

        if (dashCharges < maxDashCharges)
        {
            dashTimer += Time.deltaTime;

            if (dashTimer > 4)
            {
                dashCharges++;
                dashTimer = 0;
            }
        }

        #endregion

        yelloMovement.Turn();
        pinkoMovement.Turn();
    }

    public bool CheckCurrentState(NewPlayerStates stateToBeIn)
    {
        if (currentState == stateToBeIn)
            return true;

        return false;
    }

    private void FixedUpdate()
    {
        distance = (yelloRigidbody.position - pinkoRigidbody.position).magnitude;
        midPoint = (yelloRigidbody.position + pinkoRigidbody.position) / 2;

        StateSwitcher();
    }

    private void Attraction()
    {
        if (distance > 3.0f)
        {
            yelloRigidbody.position = Vector3.Slerp(yelloRigidbody.position, midPoint, (distance / 10) * Time.deltaTime);
            pinkoRigidbody.position = Vector3.Slerp(pinkoRigidbody.position, midPoint, (distance / 10) * Time.deltaTime);
        }
        else
            return;
    }

    private void StateSwitcher()
    {
        switch (currentState)
        {
            case NewPlayerStates.Idle:
                Attraction();
                break;
            case NewPlayerStates.Moving:

                Attraction();

                yelloMovement.Move();
                pinkoMovement.Move();

                break;
            case NewPlayerStates.Shield:

                yelloMovement.Pull(distance, pinkoRigidbody);
                yelloMovement.Move();

                if (Shield)     //Have to exist for the coroutine to run once not every frame
                    StartCoroutine(DeployShield());

                break;
            case NewPlayerStates.Attack:

                pinkoMovement.Move();
                stayTimer += Time.fixedDeltaTime;

                if (stayTimer >= stayTime)
                {
                    currentState = NewPlayerStates.Moving;
                    stayTimer = 0.0f;
                }

                if (shootScriptNew.Shoot(distance, yelloRigidbody, shoot, shootPower))
                {
                    StartCoroutine(PlaySwooschEffect(1.5f));
                    shootSource.Play();
                    shootTimerBool = true;
                    shootPower = true;
                    currentState = NewPlayerStates.Moving;
                }

                break;
            case NewPlayerStates.Dash:
                StartCoroutine(PlaySwooschEffect(.5f));

                dashSound.Play();
                talkSource.Play();

                dashScriptNew.Dash(pinkoRigidbody);

                currentState = NewPlayerStates.Moving;
                break;
            default:
                break;
        }
    }

    IEnumerator DeployShield()
    {
        GameObject shield = Instantiate(shieldPrefab, yelloRigidbody.position, Quaternion.identity, yello.transform);
        inv = true;
        timerShield = 0.0f;
        Shield = false;

        pinko.gameObject.tag = "Gem";
        yield return new WaitForSeconds(5);
        pinko.gameObject.tag = "Pinko";

        Destroy(shield);
        inv = false;
        currentState = NewPlayerStates.Moving;
    }

    public void AddDash()
    {
        maxDashCharges += 1;
    }
    public void AddMaxHealth()
    {
        maxHealth++;
        savedHealth++;
        currentHealth = maxHealth;
    }

    IEnumerator InvTimer(float time)
    {
        inv = true;
        yield return new WaitForSeconds(time);
        inv = false;
    }
    IEnumerator DamageFlash(float time)
    {
        for (int i = 0; i < time * 10; i++)
        {
            pinkoSprite.enabled = !pinkoSprite.enabled;
            yelloSprite.enabled = !yelloSprite.enabled;
            yield return new WaitForSeconds(0.1f);
        }

        pinkoSprite.enabled = true;
        yelloSprite.enabled = true;

    }
    public bool TakeDamage()
    {
        if (!inv)
        {
            damageSource.Play();
            if (currentHealth > 1)
            {
                StartCoroutine(InvTimer(invTime));
                StartCoroutine(DamageFlash(invTime));
                currentHealth--;
            }

            else
            {
                if (sceneName == "Level")
                {
                    lcs.fadeToLevel("GameOver");

                }
                else if (sceneName == "Fluid")
                {
                    lcs.fadeToLevel("GameOverBoss");
                }

            }
        }

        return !inv;
    }

    public int ReturnDash()
    {
        return dashCharges;
    }

    IEnumerator PlaySwooschEffect(float playTime)
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < playTime)
        {
            swooschEffect.Play();

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        swooschEffect.Stop();
    }
}
