using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YelloFollow : MonoBehaviour
{
    public float rotateSpeed;
    public float distance;

    Vector3 _centre;

    private float angle;
    private float direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal") * rotateSpeed;

        angle += direction * Time.deltaTime;

        Vector3 offset = new Vector3(Mathf.Sin(angle) * distance, Mathf.Cos(angle) * distance, 0);
        transform.position = transform.parent.position + offset;
    }
}
