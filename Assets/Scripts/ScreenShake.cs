using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Camera mainCam;

    public float strength;
    public float frequency;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(Shake(frequency));
        }
    }

    IEnumerator Shake(float time)
    {
        ShakeScreen();
        yield return new WaitForSeconds(time);
        ShakeScreen();
        yield return new WaitForSeconds(time);
        ShakeScreen();
        yield return new WaitForSeconds(time);
        ShakeScreen();
        yield return new WaitForSeconds(time);
        ShakeScreen();
        yield return new WaitForSeconds(time);
        ShakeScreen();
        yield return new WaitForSeconds(time);
        ShakeScreen();
    }
    private void ShakeScreen()
    {
        Vector3 newShakeValue = new Vector3(
            Mathf.PerlinNoise(-strength, strength),
            Mathf.PerlinNoise(-strength, strength), -15) * 2;

        mainCam.transform.localPosition += newShakeValue;
    }
}
