using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class index : MonoBehaviour
{
    public GameObject my_camera;
    public GameObject buy;
    public GameObject start_button;
    public Text buy_text;
    private int number_of_slimes;
    public int i = 0;
    public GameObject[] slimes;
    public GameObject[] all_items;
    public Vector3 vector;

    public void setting()
    {
        number_of_slimes = my_camera.GetComponent<the_start>().number_of_slimes;
        for (int j = 1; j < number_of_slimes; j++)
        {
            all_items[j].SetActive(false);
        }
        purchase();
    }
    void purchase()
    {
        if (my_camera.GetComponent<the_start>().player.slimes.check_slime(i))
        {
            buy.SetActive(false);
            start_button.SetActive(true);
        }
        else
        {
            buy.SetActive(true);
            start_button.SetActive(false);
            buy_text.text = my_camera.GetComponent<the_start>().prices[i].ToString();
        }
    }
    public void right()
    {
        if (i < 0 || i == number_of_slimes) return;
        slimes[i].transform.position = vector + new Vector3(0f, 0.1f, 0f);
        all_items[i].SetActive(false);
        if (i == number_of_slimes - 1)  i = 0;        
        else i++;
        purchase();
        all_items[i].SetActive(true);
    }
    public void left()
    {
        if (i < 0 || i == number_of_slimes) return;
        slimes[i].transform.position = vector + new Vector3(0f, 0.1f, 0f);
        all_items[i].SetActive(false);
        if (i == 0) i = number_of_slimes - 1;
        else  i--;
        purchase();
        all_items[i].SetActive(true);
    }
    public void buying()
    {
        GetComponent<AudioSource>().Play();
    }
}
