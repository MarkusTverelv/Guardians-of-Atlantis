using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class JellowProjectileScript : MonoBehaviour
{
    
    private Transform attachPoint;
    private GameObject player;
    private GameObject line;
    private GameObject yello;
    private Transform arrowAim;
    private Rigidbody2D rb;
    private GameObject lineSegment;
    private bool iArrived = false;
    public bool shoot = false;
    public ProjectileState currentState;

    public enum ProjectileState
    {
        follow,
        idle,
        fire
    }

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        attachPoint = GameObject.Find("JellowAttach").transform;
        line = FindInActiveObjectByName("Line");
        yello = FindInActiveObjectByName("Yello"); 
        rb = GetComponent<Rigidbody2D>();
        currentState = ProjectileState.follow;
        GetComponent<CircleCollider2D>().enabled = false;
        lineSegment = GameObject.Find("Linesegment 0");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentState);
        switch (currentState)
        {
            case ProjectileState.follow:
                transform.position = Vector2.MoveTowards(transform.position, attachPoint.position, 5 * Time.deltaTime);
                break;
            case ProjectileState.idle:
                transform.position = attachPoint.position;
                break;
            case ProjectileState.fire:
                arrowAim = GameObject.Find("ArrowAim(Clone)").transform;
                rb.AddForce(arrowAim.right * 1);
                break;
            default:
                break;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jellyfish"))
        {
            yello.SetActive(true);
            yello.transform.position = lineSegment.transform.position;
            line.GetComponent<LineRenderer>().enabled = true;
        }
    }

    GameObject FindInActiveObjectByName(string name)
    {
        GameObject[] objs = Resources.FindObjectsOfTypeAll<GameObject>() as GameObject[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
