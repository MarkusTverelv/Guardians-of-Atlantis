using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    public void textAnimate()
    {
        GetComponent<Animator>().Play("TextFadeInAndOut");
    }

}
