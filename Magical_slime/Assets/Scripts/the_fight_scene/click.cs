using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class click : MonoBehaviour
{
    public GameObject my_camera;
    public GameObject first_dice;
    public GameObject second_dice;
    public GameObject text_click;
    public GameObject time_slider;
    public GameObject slime;
    public bool right = false;
    public int attack;
    public bool combo;
    public bool is_clicked = false;
    private int first_attack;
    private int second_attack;

    IEnumerator show_random_dices()
    {
        int number_of_shows = 10;
        for (int i = 0; i < number_of_shows; i++)
        {
            int first_index = Random.Range(0, 5);
            int second_index = Random.Range(0, 5);
            first_dice.GetComponent<Image>().sprite = my_camera.GetComponent<the_begin>().dices[first_index].GetComponent<Image>().sprite;
            second_dice.GetComponent<Image>().sprite = my_camera.GetComponent<the_begin>().dices[second_index].GetComponent<Image>().sprite;
            yield return new WaitForSeconds(0.12f);
        }
        StopCoroutine("show_random_dices");
        first_dice.GetComponent<Image>().sprite = my_camera.GetComponent<the_begin>().dices[first_attack - 1].GetComponent<Image>().sprite;
        second_dice.GetComponent<Image>().sprite = my_camera.GetComponent<the_begin>().dices[second_attack - 1].GetComponent<Image>().sprite;
        set_animation(false);
        if (my_camera.GetComponent<the_begin>().is_click)
        {
            my_camera.GetComponent<the_begin>().start_fight(right);
            my_camera.GetComponent<the_begin>().is_click = false;
        }
        else
        {
            my_camera.GetComponent<the_begin>().is_click = true;
        }
    }
    public void set_animation(bool show)
    {
        if(show)
        {
            first_dice.GetComponent<Animation>().Play("dices");
            second_dice.GetComponent<Animation>().Play("dices");
        }
        else
        {
            first_dice.GetComponent<Animation>().Stop("dices");
            first_dice.GetComponent<Transform>().rotation = new Quaternion(0f, 0f, 0f, 0f);
            second_dice.GetComponent<Animation>().Stop("dices");
            second_dice.GetComponent<Transform>().rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }
    public void can_click(bool can)
    {
        text_click.SetActive(can);
        time_slider.SetActive(can);
        is_clicked = !can;
        if(can) time_slider.GetComponent<time_slider>().start_time();
    }
    public void OnMouseDown()
    {       
        if (is_clicked) return;
        can_click(false);
        set_animation(true);
        GetComponent<AudioSource>().Play();       
        first_attack = Random.Range(1, 6);
        second_attack = Random.Range(1, 6);
        attack = first_attack + second_attack;
        combo = false;
        if (first_attack == second_attack) combo = true;
        if (right)
        {
            my_camera.GetComponent<the_begin>().right_attack = attack;
            my_camera.GetComponent<the_begin>().right_combo = combo;
        }
        else
        {
            my_camera.GetComponent<the_begin>().left_attack = attack;
            my_camera.GetComponent<the_begin>().left_combo = combo;
        }
        StartCoroutine("show_random_dices");       
    }
}
