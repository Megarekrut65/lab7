﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buying : MonoBehaviour
{
    public GameObject my_camera;
    public GameObject place;
    public GameObject buy;
    public Text coins_text;
    public GameObject start_button;

    void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
        int i = place.GetComponent<index>().i;       
        int price = my_camera.GetComponent<the_start>().prices[i];
        int coins = my_camera.GetComponent<the_start>().player.coins;
        if(coins >= price)
        {
            place.GetComponent<index>().buying();
            my_camera.GetComponent<the_start>().player.slimes.buy_slime(i);
            my_camera.GetComponent<the_start>().player.coins -= price;
            coins_text.text = my_camera.GetComponent<the_start>().player.coins.ToString();
            my_camera.GetComponent<the_start>().save_info();          
            buy.SetActive(false);
            start_button.SetActive(true);
        }
    }
}
