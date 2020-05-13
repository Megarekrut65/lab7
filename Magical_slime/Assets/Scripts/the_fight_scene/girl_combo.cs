using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class girl_combo : MonoBehaviour
{
    public GameObject my_camera;
    public Slider hp_slider;
    public string name_of_fly;
    private int hp = 0;
    private int index = 0;
    private bool right = false;
    public GameObject the_slime;
    public Vector3 away_position;
    public Vector3 start_position;
    public Vector3 end_position;
    public Vector3 save_position;
    public float step;
    public float progress;
    private bool stop;
    private bool die = true;

    void FixedUpdate()
    {
        if (stop) return;
        transform.position = Vector3.Lerp(start_position, end_position, progress);
        progress += step;
        if (transform.position == save_position)
        {
            stop = true;
            if(!die) GetComponent<Animation>().Play(name_of_fly);
            else gameObject.SetActive(false);
        }
        save_position = transform.position;
    }
    public void getting_damage(int damage)
    {
        hp -= damage;
        hp_slider.value = hp;
        if (hp <= 0)
        {
            start_position = transform.position;
            end_position = away_position;
            save_position = end_position;
            progress = 0;
            stop = false;
            die = true;
            my_camera.GetComponent<the_begin>().remove_girl(index, right);
        }
    }
    public void show(int index_of_girl, bool side, int damage)
    {
        index = index_of_girl;
        right = side;
        hp = damage;
        hp_slider.value = hp;
        if (!die) return;
        start_position = away_position;
        end_position = the_slime.GetComponent<Transform>().position;
        if (side)
        {
            start_position.x += 15f;
            end_position.x -= 3f;
        }
        else
        {
            start_position.x -= 15f;
            end_position.x += 3f;
        }
        end_position.y -= 0.5f;
        save_position = end_position;
        progress = 0;
        stop = false;
        die = false;
    }
}
