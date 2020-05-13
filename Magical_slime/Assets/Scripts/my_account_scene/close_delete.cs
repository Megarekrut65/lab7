using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class close_delete : MonoBehaviour
{
    public GameObject items;
    public GameObject delete;

    void OnMouseDown()
    {
        items.SetActive(true);
        items.GetComponent<AudioSource>().Play();
        delete.SetActive(false);
    }
}
