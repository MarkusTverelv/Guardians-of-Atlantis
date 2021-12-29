using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePlayer : MonoBehaviour
{
    [SerializeField]
    private string player;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find(player).GetComponent<Collider2D>());
    }
}
