using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttons : MonoBehaviour
{
    public bool sound = true;

    void OnMouseDown()
    {
        if(sound) GetComponent<AudioSource>().Play();
        transform.localScale = 1.1f * transform.localScale;
    }
    void OnMouseUp()
    {
        transform.localScale = transform.localScale / 1.1f;
    }
}
