using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check_button : MonoBehaviour
{
    public GameObject items;

    void OnMouseDown()
    {
        items.GetComponent<check_code>().button();
    }
}
