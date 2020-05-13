using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class start_button : MonoBehaviour
{
    public string name_of_scene;
    public GameObject my_camera;
    public GameObject place;
    public string slime_path;
    private Fight_player player;
    void choose_element(int index)
    {
        switch(index)
        {
            case 0:
                player.element = "fire";
                break;
            case 1:
                player.element = "water";
                break;
            case 2:
                player.element = "poison";
                break;
            case 3:
                player.element = "wood";
                break;
            case 4:
                player.element = "dark";
                break;
            case 5:
                player.element = "girl";
                break;
            case 6:
                player.element = "white";
                break;
        }
    }
    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        slime_path = Path.Combine(Application.persistentDataPath, slime_path);
#else
        slime_path = Path.Combine(Application.dataPath, slime_path);
#endif
    }
    void OnMouseDown()
    {
        player = new Fight_player();
        GetComponent<AudioSource>().Play();
        choose_element(place.GetComponent<index>().i);
        player.nickname = my_camera.GetComponent<the_start>().player.nickname;
        player.points = my_camera.GetComponent<the_start>().player.points;
        player.create_file(slime_path);
        SceneManager.LoadScene(name_of_scene, LoadSceneMode.Single);
    }
}
public class Fight_player
{
    public string nickname;
    public int points;
    public string element;

    public Fight_player()
    {
        nickname = "nickname";
        points = 0;
        element = "fire";
    }
    public void create_file(string path)
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamWriter writer = new StreamWriter(file);
        writer.WriteLine("Nickname=" + nickname);
        writer.WriteLine("Points=" + points.ToString());
        writer.WriteLine("Element=" + element);
        writer.Close();
    }
    public void read_from_file(string path)
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        nickname = reader.ReadLine().Substring(9);
        points = Convert.ToInt32(reader.ReadLine().Substring(7));
        element = reader.ReadLine().Substring(8);
        reader.Close();
    }
}