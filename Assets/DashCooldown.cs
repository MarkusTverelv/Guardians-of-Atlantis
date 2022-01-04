using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DashCooldown : MonoBehaviour
{
    [SerializeField]
    private Image imageCooldown;
    [SerializeField]
    private TMP_Text textCooldown;

    private bool isCooldown = false;
    [SerializeField]
    private float cooldownTime = 4.0f;
    [SerializeField]
    private float cooldownTimer = 0.0f;

    PlayerSharedScript ps;
    public int dashCharges;
    public KeyCode letter;
    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.Find("NewPlayers").GetComponent<PlayerSharedScript>();
        imageCooldown.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        dashCharges = ps.ReturnDash();
        textCooldown.text = dashCharges.ToString();
        if (isCooldown)
        {
            ApplyCooldown();
        }

        if (dashCharges == 0)
        {

            if (Input.GetKey(letter))
            {
                UseSpell();
            }
        }
    }

    void ApplyCooldown()
    {

        cooldownTimer -= Time.deltaTime;


        if (cooldownTimer < 0.0f)
        {
            isCooldown = false;
            imageCooldown.fillAmount = 0.0f;
        }
        else
        {
            textCooldown.text = Mathf.RoundToInt(cooldownTimer).ToString();
            imageCooldown.fillAmount = cooldownTimer / cooldownTime;
        }

    }
    public void UseSpell()
    {
        if (isCooldown)
        {
        }
        else
        {
            isCooldown = true;
            cooldownTimer = cooldownTime;
        }
    }
}
