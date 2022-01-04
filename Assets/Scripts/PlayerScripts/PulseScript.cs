using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseScript : MonoBehaviour
{
    List<GameObject> enemyList;
    public GameObject pulseParticle;

    private float range;
    private float rangeMax;
    private bool canGrow = false;
    float pulseTimer;
    public float pulseRate;

    public AudioSource pulseSound;
    public AudioSource pulseTalkSound;

    PlayerSharedScript playerShared;
    GameObject tmpPulse;


    // Start is called before the first frame update
    void Start()
    {
        playerShared = transform.parent.GetComponentInParent<PlayerSharedScript>();
        enemyList = new List<GameObject>();
        rangeMax = 300f;
    }

    // Update is called once per frame
    void Update()
    {
        pulseTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q) && pulseTimer >= pulseRate && !playerShared.CheckCurrentState(NewPlayerStates.Attack) && !playerShared.CheckCurrentState(NewPlayerStates.Attack))
        {
            pulseTalkSound.Play();
            pulseSound.Play();

            tmpPulse = Instantiate(pulseParticle, transform.position, Quaternion.identity, this.transform);

            canGrow = true;
            pulseTimer = 0;
        }

        if (canGrow)
        {
            float rangeSpeed = 240f;
            range += rangeSpeed * Time.deltaTime;

            if (range > rangeMax)
            {
                range = rangeMax;
                Invoke("PulseEffect", 0.1f);
                canGrow = false;
            }

            tmpPulse.transform.GetChild(0).localScale = new Vector3(range, range);
        }
        else
        {
            Destroy(tmpPulse);
        }
    }
    private void PulseEffect()
    {
        range = 0;
    }

    public void AddForce(Collider2D collision)
    {
        for (int i = 0; i < 10; i++)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            Vector2 direction = collision.transform.position - this.transform.position;
            direction.Normalize();
            rb.AddForce(direction * 10, ForceMode2D.Force);
        }
    }

}
