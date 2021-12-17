using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    GameObject spawn;
    public GameObject spawnPoint, checkPoint;
    public GameObject pinko, yello;
    public AudioClip hurtSound;
    public UnityEvent onCheckpointSet = new UnityEvent();
    public int maxHealth;
    [HideInInspector] public int currentHealth;
    public float moveSpeed, turnSpeed, maxMoveSpeed, invTime;

    DashScriptNew dashScriptNew;
    PlayerSpecificScript pinkoMovement, yelloMovement;

    Rigidbody2D yelloRigidbody;
    Rigidbody2D pinkoRigidbody;
    ShootScriptNew shootScriptNew;

    Vector3 midPoint;

    float distance = 0.0f;
    bool shoot;
    bool shootPower = true;
    bool Shield = false;
    bool shootTimerBool = false;

    float shootTimer = 0;
    float dashTimer = 0;

    public int dashCharges = 3;

    NewPlayerStates currentState = NewPlayerStates.Moving;
    public GameObject shieldPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(yello.GetComponent<Collider2D>(), pinko.GetComponent<Collider2D>());
        yelloMovement = yello.GetComponent<PlayerSpecificScript>();
        pinkoMovement = pinko.GetComponent<PlayerSpecificScript>();

        dashScriptNew = yello.GetComponent<DashScriptNew>();
        shootScriptNew = pinko.GetComponent<ShootScriptNew>();

        yelloRigidbody = yello.GetComponent<Rigidbody2D>();
        pinkoRigidbody = pinko.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad2) && !shootTimerBool)
            currentState = NewPlayerStates.Attack;

        if (Input.GetKey(KeyCode.Return))
            shootPower = false;

        if (Input.GetKeyUp(KeyCode.Return))
            shoot = true;

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentState = NewPlayerStates.Shield;
            Shield = true;
        }

        if(Input.GetKeyDown(KeyCode.Keypad1) && dashCharges != 0)
        {
            currentState = NewPlayerStates.Dash;
            dashCharges -= 1;
        }

        if(shootTimerBool)
        {
            shootTimer += Time.deltaTime;

            if(shootTimer > 8)
            {
                shootTimerBool = false;
                shoot = false;
                shootTimer = 0;
            }
        }

        if(dashCharges < 3)
        {
            dashTimer += Time.deltaTime;
            if(dashTimer > 4)
            {
                dashCharges++;
                dashTimer = 0;
            }
        }

        yelloMovement.Turn();
        pinkoMovement.Turn();

        
    }

    //M=(2x1​+x2​​,2y1​+y2​​)

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
                yelloMovement.Move();
                yelloMovement.Pull(distance, pinkoRigidbody);
                if (Shield)
                    StartCoroutine(DeployShield());
                break;
            case NewPlayerStates.Attack:
                pinkoMovement.Move();
                if (shootScriptNew.Shoot(distance, yelloRigidbody, shoot, shootPower))
                {
                    shootTimerBool = true;
                    shootPower = true;
                    currentState = NewPlayerStates.Moving;
                }
                break;
            case NewPlayerStates.Dash:
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
        Shield = false;
        yello.gameObject.tag = "Gem";
        yield return new WaitForSeconds(5);
        yello.gameObject.tag = "Yello";
        Destroy(shield);
        currentState = NewPlayerStates.Moving;
    }
    public void SetCheckPoint(GameObject gameObject)
    {
        spawn.transform.position = gameObject.transform.position;
        onCheckpointSet.Invoke();
        checkPoint = gameObject;
        Debug.Log("checkpoint set: " + gameObject);
    }
}
