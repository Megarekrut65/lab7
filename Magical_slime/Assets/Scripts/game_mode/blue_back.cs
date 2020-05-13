using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blue_back : MonoBehaviour
{
    public GameObject my_camera;
    public GameObject backgraund;

    void OnMouseDown()
    {
        backgraund.GetComponent<AudioSource>().Play();
        my_camera.GetComponent<start_mode>().set_active_items(true);
        my_camera.GetComponent<start_mode>().set_active_slimes(false);
        gameObject.SetActive(false);
    }
}
