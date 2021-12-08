using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OilSpawner : MonoBehaviour
{
    float width, height;

    public GameObject oil, oil2, bigOil, biggestOil;

    public GameObject indicator, bigIndicator, biggestIndicator;
    public GameObject biggestOilSpot;
    public GameObject[] IndicatorList;
    public GameObject indicatorWarning;


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

        if (Input.GetKeyDown(KeyCode.J))
        {

            spawnOil();

        }

        if (Input.GetKeyDown(KeyCode.M))
        {

            StartCoroutine(OilInRoom());

        }

        if (Input.GetKeyDown(KeyCode.B))
        {

            StartCoroutine(BigOilInRoom());

        }

        if (Input.GetKeyDown(KeyCode.V))
        {

            StartCoroutine(OilZigZag());

        }

    }

    public IEnumerator oilTimer()
    {
        
        for (int i = 0; i < 20; i++)
        {
            randomPos.Add(new Vector2(Random.Range(-width - 10, width - 20), Random.Range(-height - 10, height + 10)));
            Instantiate(indicator, randomPos[i], Quaternion.identity);
        }

        yield return new WaitForSeconds(2);

        for (int i = 0; i < randomPos.Count; i++)
        {
            var oilCopy = Instantiate(oil, randomPos[i], Quaternion.identity);
            Destroy(oilCopy, 0.5f);
        }

        randomPos.Clear();


    }

    void spawnOil()
    {
        var oilCopy = Instantiate(oil2, new Vector3(-width - 10, height + 20), Quaternion.identity);
        Destroy(oilCopy, 10);
    }
    public IEnumerator fluidOil()
    {
        indicatorWarning.SetActive(true);
        yield return new WaitForSeconds(2f);
        indicatorWarning.SetActive(false);
        var oilCopy = Instantiate(oil2, new Vector3(-width - 10, height + 20), Quaternion.identity);
        Destroy(oilCopy, 10);
    }


    public IEnumerator OilInRoom()
    {
        Instantiate(bigIndicator, IndicatorList[0].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(bigOil, IndicatorList[0].transform.position, Quaternion.identity);
        Instantiate(bigIndicator, IndicatorList[1].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(bigOil, IndicatorList[1].transform.position, Quaternion.identity);
        Instantiate(bigIndicator, IndicatorList[2].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(bigOil, IndicatorList[2].transform.position, Quaternion.identity);
        Instantiate(bigIndicator, IndicatorList[3].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(bigOil, IndicatorList[3].transform.position, Quaternion.identity);
        //Go Reverse
        Instantiate(bigIndicator, IndicatorList[2].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(bigOil, IndicatorList[2].transform.position, Quaternion.identity);
        Instantiate(bigIndicator, IndicatorList[1].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(bigOil, IndicatorList[1].transform.position, Quaternion.identity);
        Instantiate(bigIndicator, IndicatorList[0].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(bigOil, IndicatorList[0].transform.position, Quaternion.identity);

    }

    public IEnumerator OilZigZag()
    {
        Instantiate(bigIndicator, IndicatorList[0].transform.position, Quaternion.identity);
        Instantiate(bigIndicator, IndicatorList[2].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(bigOil, IndicatorList[0].transform.position, Quaternion.identity);
        Instantiate(bigOil, IndicatorList[2].transform.position, Quaternion.identity);
        Instantiate(bigIndicator, IndicatorList[1].transform.position, Quaternion.identity);
        Instantiate(bigIndicator, IndicatorList[3].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(bigOil, IndicatorList[1].transform.position, Quaternion.identity);
        Instantiate(bigOil, IndicatorList[3].transform.position, Quaternion.identity);
    }

    public IEnumerator BigOilInRoom()
    {
        Instantiate(biggestIndicator, biggestOilSpot.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(biggestOil, biggestOilSpot.transform.position, Quaternion.identity);
    }
}
