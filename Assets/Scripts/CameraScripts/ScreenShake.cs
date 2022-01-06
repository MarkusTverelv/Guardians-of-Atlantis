using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Camera mainCam;

    public float strength;
    public float length;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(Shake(length, strength));
        }
    }

    IEnumerator Shake(float duration, float strength)
    {
        Vector3 origin = mainCam.transform.localPosition;

        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float x = Mathf.PerlinNoise(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(-strength, strength);
            float y = Mathf.PerlinNoise(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(-strength, strength);

            mainCam.transform.localPosition += new Vector3(x, y, origin.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        mainCam.transform.localPosition = origin;
    }
}
