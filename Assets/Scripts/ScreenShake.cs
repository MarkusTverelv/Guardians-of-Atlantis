using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Camera mainCam;

    public float strength;
    public float length;

    float timer = 0.0f;
    bool applyShake = false;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            applyShake = true;    
        }

        if (applyShake)
        {
            timer += Time.deltaTime;
            ShakeScreen(strength, length);
        }
    }

    public bool ShakeScreen(float shakeAmount, float timeToShake)
    {
        if (timer < timeToShake)
        {
            Vector3 newShakeValue = new Vector3(
                Mathf.PerlinNoise(-shakeAmount, shakeAmount),
                Mathf.PerlinNoise(-shakeAmount, shakeAmount), 0) * 10;

            mainCam.transform.localPosition += newShakeValue;

            return true;
        }
        else if (timer >= timeToShake)
        {
            timer = 0.0f;
            applyShake = false;
        }

        return false;
    }
}
