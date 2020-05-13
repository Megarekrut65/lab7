using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_slime : MonoBehaviour
{
    public GameObject off_button;
    public int index = 0;

    void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
        off_button.GetComponent<offline_game>().set_element(index);
    }
}
