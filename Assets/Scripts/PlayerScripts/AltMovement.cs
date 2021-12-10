using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NewPlayerStates
{
    Idle,
    Moving,
    Shield,
    Attack,
    Dash

}

public class AltMovement : MonoBehaviour
{
    public GameObject pinko;
    public GameObject yello;

    NewPinkoMovement pinkoMove;
    NewYelloMovement yelloMove;

    Rigidbody2D yelloRigidbody;
    Rigidbody2D pinkoRigidbody;

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


        pinkoMove = pinko.GetComponent<NewPinkoMovement>();
        yelloMove = yello.GetComponent<NewYelloMovement>();

        yelloRigidbody = yello.GetComponent<Rigidbody2D>();
        pinkoRigidbody = pinko.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !shootTimerBool)
            currentState = NewPlayerStates.Attack;

        if (Input.GetKey(KeyCode.Return))
            shootPower = false;

        if (Input.GetKeyUp(KeyCode.Return))
            shoot = true;

        if (Input.GetKeyDown(KeyCode.L))

        {
            currentState = NewPlayerStates.Shield;
            Shield = true;
        }

        if(Input.GetKeyDown(KeyCode.J) && dashCharges != 0)
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

        yelloMove.Turn();
        pinkoMove.Turn();

        
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
        if (distance > 2.0f)
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
                yelloMove.Move();
                pinkoMove.Move();
                break;
            case NewPlayerStates.Shield:
                yelloMove.Move();
                yelloMove.Pull(distance, pinkoRigidbody);
                if (Shield)
                {
                    StartCoroutine(DeployShield());
                    
                }
                break;
            case NewPlayerStates.Attack:
                pinkoMove.Move();
                if (pinkoMove.Shoot(distance, yelloRigidbody, shoot, shootPower))
                {
                    shootTimerBool = true;
                    shootPower = true;
                    currentState = NewPlayerStates.Moving;
                }
                break;
            case NewPlayerStates.Dash:
                yelloMove.Dash(pinkoRigidbody);
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
        yield return new WaitForSeconds(5);
        Destroy(shield);
        currentState = NewPlayerStates.Moving;
    }
}
