using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class the_boll : MonoBehaviour
{
    public GameObject my_camera;
    public GameObject enemy_slime;
    public GameObject the_slime;
    public int damage = 0;
    private bool double_attack = false;
    private bool right = true;
    private bool stop = true;
    public Vector3 start_position;
    public Vector3 end_position;
    public Vector3 save_position;
    public float step;
    public float progress;

    void FixedUpdate()
    {
        if (stop) return;
        transform.position = Vector3.Lerp(start_position, end_position, progress);
        progress += step;       
        if (transform.position == save_position)
        {
            stop = true;
            StartCoroutine("hit");
        }
        save_position = transform.position;
    }
    IEnumerator hit()
    {       
        transform.localScale = 2f * transform.localScale;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = transform.localScale / 2f;
        enemy_slime.GetComponent<slime>().getting_damage(damage);
        active_false();
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        StopCoroutine("hit");
    }
    public void boll_fly(int attack, bool combo, bool side)
    {
        damage = attack;
        double_attack = combo;
        right = side;        
        transform.position = the_slime.GetComponent<Transform>().position;
        start_position = transform.position;
        end_position = enemy_slime.GetComponent<Transform>().position;
        save_position = end_position;
        GetComponent<AudioSource>().Play();
        progress = 0;
        stop = false;
    }
    void active_false()
    {
        if (!double_attack)
        {
            my_camera.GetComponent<the_begin>().finish_fight(right);
        }            
    }
}
