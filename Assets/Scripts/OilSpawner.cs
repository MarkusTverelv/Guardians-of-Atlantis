using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OilSpawner : MonoBehaviour
{
    float width, height;
    public GameObject oil;
    public GameObject indicator;
    public List<Vector2> randomPos;
    
    // Start is called before the first frame update
    void Start()
    {
        width = Camera.main.orthographicSize * Camera.main.aspect;
        height = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {

            StartCoroutine(oilTimer());

        }
    }

    IEnumerator oilTimer()
    {
        
        for (int i = 0; i < 20; i++)
        {
            randomPos.Add(new Vector2(Random.Range(-width, width), Random.Range(-height, height)));
            Instantiate(indicator, randomPos[i], Quaternion.identity);
        }

        Debug.Log(randomPos.Count);
        yield return new WaitForSeconds(2);

        for (int i = 0; i < randomPos.Count; i++)
        {
            Instantiate(oil, randomPos[i], Quaternion.identity);
            
        }

        randomPos.Clear();


    }
}
