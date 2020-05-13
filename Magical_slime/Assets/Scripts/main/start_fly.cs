using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class start_fly : MonoBehaviour
{
    public string fly = "none";
    public Image[] slimes;

    void if_showed()
    {
        GetComponent<Animation>().Play(fly);
    }
    void OnMouseDown()
    {
        int index = Random.Range(0, 6);
        GetComponent<Image>().sprite = slimes[index].sprite;
    }
}
