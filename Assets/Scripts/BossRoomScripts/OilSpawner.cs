using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OilSpawner : MonoBehaviour
{
    float width, height;

    public GameObject oil, oil2, bigOil, biggestOil;

    public GameObject indicator, bigIndicator, biggestIndicator, squareIndicator, fasterSquareIndicator;
    public GameObject biggestOilSpot;
    public GameObject[] IndicatorList;
    public GameObject[] squareIndicatorList;
    public GameObject[] fasterSquareIndicatorList;
    public GameObject[] squareOilSpots;
    public GameObject indicatorWarning;

    public GameObject[] fasterSquareOilSpots, fasterSquareOilSpots1, fasterSquareOilSpots2, fasterSquareOilSpots3,
        fasterSquareOilSpots4, fasterSquareOilSpots5, fasterSquareOilSpots6, fasterSquareOilSpots7, fasterSquareOilSpots8,
        fasterSquareOilSpots9, fasterSquareOilSpots10, fasterSquareOilSpots11, fasterSquareOilSpots12, fasterSquareOilSpots13,
        fasterSquareOilSpots14, fasterSquareOilSpots15, fasterSquareOilSpots16, fasterSquareOilSpots17;
    


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


    //public IEnumerator OilInRoom()
    //{
    //    Instantiate(bigIndicator, IndicatorList[0].transform.position, Quaternion.identity);
    //    yield return new WaitForSeconds(2f);
    //    Instantiate(bigOil, IndicatorList[0].transform.position, Quaternion.identity);
    //    Instantiate(bigIndicator, IndicatorList[1].transform.position, Quaternion.identity);
    //    yield return new WaitForSeconds(2f);
    //    Instantiate(bigOil, IndicatorList[1].transform.position, Quaternion.identity);
    //    Instantiate(bigIndicator, IndicatorList[2].transform.position, Quaternion.identity);
    //    yield return new WaitForSeconds(2f);
    //    Instantiate(bigOil, IndicatorList[2].transform.position, Quaternion.identity);
    //    Instantiate(bigIndicator, IndicatorList[3].transform.position, Quaternion.identity);
    //    yield return new WaitForSeconds(2f);
    //    Instantiate(bigOil, IndicatorList[3].transform.position, Quaternion.identity);
    //    //Go Reverse
    //    Instantiate(bigIndicator, IndicatorList[2].transform.position, Quaternion.identity);
    //    yield return new WaitForSeconds(2f);
    //    Instantiate(bigOil, IndicatorList[2].transform.position, Quaternion.identity);
    //    Instantiate(bigIndicator, IndicatorList[1].transform.position, Quaternion.identity);
    //    yield return new WaitForSeconds(2f);
    //    Instantiate(bigOil, IndicatorList[1].transform.position, Quaternion.identity);
    //    Instantiate(bigIndicator, IndicatorList[0].transform.position, Quaternion.identity);
    //    yield return new WaitForSeconds(2f);
    //    Instantiate(bigOil, IndicatorList[0].transform.position, Quaternion.identity);

    //}

    //public IEnumerator OilZigZag()
    //{
    //    Instantiate(bigIndicator, IndicatorList[0].transform.position, Quaternion.identity, IndicatorList[0].transform);
    //    Instantiate(bigIndicator, IndicatorList[2].transform.position, Quaternion.identity);
    //    yield return new WaitForSeconds(2f);
    //    Instantiate(bigOil, IndicatorList[0].transform.position, Quaternion.identity);
    //    Instantiate(bigOil, IndicatorList[2].transform.position, Quaternion.identity);
    //    Instantiate(bigIndicator, IndicatorList[1].transform.position, Quaternion.identity);
    //    Instantiate(bigIndicator, IndicatorList[3].transform.position, Quaternion.identity);
    //    yield return new WaitForSeconds(2f);
    //    Instantiate(bigOil, IndicatorList[1].transform.position, Quaternion.identity);
    //    Instantiate(bigOil, IndicatorList[3].transform.position, Quaternion.identity);
    //}

    public IEnumerator OilSquare()
    {
        Instantiate(squareIndicator, squareIndicatorList[0].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(4f);
        Instantiate(squareIndicator, squareIndicatorList[1].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(bigOil, squareOilSpots[0].transform.position, Quaternion.identity);
        Instantiate(bigOil, squareOilSpots[1].transform.position, Quaternion.identity);
        Instantiate(bigOil, squareOilSpots[2].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(4f);
        Instantiate(bigOil, squareOilSpots[3].transform.position, Quaternion.identity);
        Instantiate(bigOil, squareOilSpots[4].transform.position, Quaternion.identity);
        Instantiate(bigOil, squareOilSpots[5].transform.position, Quaternion.identity);
        Instantiate(squareIndicator, squareIndicatorList[2].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(squareIndicator, squareIndicatorList[0].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(4f);
        Instantiate(bigOil, squareOilSpots[6].transform.position, Quaternion.identity);
        Instantiate(bigOil, squareOilSpots[7].transform.position, Quaternion.identity);
        Instantiate(bigOil, squareOilSpots[8].transform.position, Quaternion.identity);
        Instantiate(squareIndicator, squareIndicatorList[1].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(bigOil, squareOilSpots[0].transform.position, Quaternion.identity);
        Instantiate(bigOil, squareOilSpots[1].transform.position, Quaternion.identity);
        Instantiate(bigOil, squareOilSpots[2].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(4f);
        Instantiate(bigOil, squareOilSpots[3].transform.position, Quaternion.identity);
        Instantiate(bigOil, squareOilSpots[4].transform.position, Quaternion.identity);
        Instantiate(bigOil, squareOilSpots[5].transform.position, Quaternion.identity);

    }

    public IEnumerator oilWall()
    {
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[0].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach(GameObject l in fasterSquareOilSpots)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[1].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots1)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[2].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots2)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[3].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots3)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[4].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots4)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[5].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots5)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[6].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots6)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[7].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots7)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[8].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots8)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[9].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots9)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[10].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots10)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[11].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots11)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[12].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots12)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[13].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots13)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[14].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots14)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[15].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots15)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[16].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots16)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[17].transform.position, Quaternion.identity);

    }

    public IEnumerator reverseOilWall()
    {
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[17].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots16)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[16].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots15)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[15].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots14)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[14].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots13)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[13].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots12)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[12].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots11)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[11].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots10)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[10].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots9)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[9].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots8)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[8].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots7)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[7].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots6)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[6].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots5)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[5].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots4)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[4].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots3)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[3].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots2)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[2].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots1)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[1].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject l in fasterSquareOilSpots)
        {
            Instantiate(oil, l.transform.position, Quaternion.identity);
        }
        Instantiate(fasterSquareIndicator, fasterSquareIndicatorList[0].transform.position, Quaternion.identity);

    }

    //public IEnumerator BigOilInRoom()
    //{
    //    Instantiate(biggestIndicator, biggestOilSpot.transform.position, Quaternion.identity);
    //    yield return new WaitForSeconds(2f);
    //    Instantiate(biggestOil, biggestOilSpot.transform.position, Quaternion.identity);
    //}
}
