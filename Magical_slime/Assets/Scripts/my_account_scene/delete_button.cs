using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete_button : MonoBehaviour
{
    public GameObject items;
    public GameObject delete;

    void OnMouseDown()
    {
        delete.SetActive(true);
        delete.GetComponent<AudioSource>().Play();
        items.SetActive(false);
    }
}
