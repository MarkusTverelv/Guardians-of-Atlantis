using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;




public class JellowProjectileScript : MonoBehaviour
{
    private GameObject player, line, yello, lineSegment;
    private Transform attachPoint, arrowAim;
    private LineRenderer lineRenderer;
    private SpriteRenderer yelloSpriteRenderer, arrowAimSpriteRenderer;
    private PolygonCollider2D yelloCollider;
    private Rigidbody2D rb, yellorb;
    private Light2D yelloLs;
    private TrailRenderer yelloTr;
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
        line = GameObject.Find("Line");
        lineRenderer = line.GetComponent<LineRenderer>();
        yello = GameObject.Find("Yello");
        yelloCollider = yello.GetComponent<PolygonCollider2D>();
        yelloSpriteRenderer = yello.GetComponent<SpriteRenderer>();
        yellorb = yello.GetComponent<Rigidbody2D>();
        yelloLs = yello.GetComponent<Light2D>();
        yelloTr = yello.GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currentState = ProjectileState.follow;
        GetComponent<CircleCollider2D>().enabled = false;
        arrowAim = GameObject.Find("ArrowAim").transform;
        arrowAimSpriteRenderer = arrowAim.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentState);
        switch (currentState)
        {
            case ProjectileState.follow:
                transform.position = Vector2.MoveTowards(transform.position, attachPoint.position, 10 * Time.deltaTime);
                break;
            case ProjectileState.idle:
                transform.position = attachPoint.position;
                break;
            case ProjectileState.fire:
                rb.AddForce(arrowAim.right * 20); 
                yellorb.position = Vector2.MoveTowards(yellorb.position, this.transform.position, 30 * Time.deltaTime);
                arrowAimSpriteRenderer.enabled = false;
                
                break;
            default:
                break;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            yelloSpriteRenderer.color = new Color(255, 255, 255, 255);
            yelloCollider.enabled = true;
            lineRenderer.enabled = true;
            yelloLs.enabled = true;
            yelloTr.enabled = true;
            GameObject.FindGameObjectWithTag("YelloHB").GetComponent<Image>().enabled = true;
            Destroy(gameObject);
        }
    }


}
