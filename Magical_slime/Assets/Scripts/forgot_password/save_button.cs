using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save_button : MonoBehaviour
{
    public GameObject items;

    void OnMouseDown()
    {
        items.GetComponent<edit_password>().button();
    }
}
