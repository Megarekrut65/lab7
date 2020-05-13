using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class offline_game : MonoBehaviour
{
    public GameObject my_camera;
    public GameObject back;
    public string path_of_player1;
    public string path_of_player2;
    public string path_of_mode;
    public string account_path;
    private string element;
    public string name_of_scene;
    public Player player;
    public GameObject backgraund;

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path_of_player1 = Path.Combine(Application.persistentDataPath, path_of_player1);
        path_of_player2 = Path.Combine(Application.persistentDataPath, path_of_player2);
        path_of_mode = Path.Combine(Application.persistentDataPath, path_of_mode);
        account_path = Path.Combine(Application.persistentDataPath, account_path);
#else
        path_of_player1 = Path.Combine(Application.dataPath, path_of_player1);
        path_of_player2 = Path.Combine(Application.dataPath, path_of_player2);
        path_of_mode = Path.Combine(Application.dataPath, path_of_mode);
        account_path = Path.Combine(Application.dataPath, account_path);
#endif
    }
    void add_mode()
    {
        FileStream file = new FileStream(path_of_mode, FileMode.OpenOrCreate);
        StreamWriter writer = new StreamWriter(file);
        writer.WriteLine("Mode=offline");
        writer.Close();
    }
    void choose_element(int index)
    {
        switch (index)
        {
            case 0:
                element = "fire";
                break;
            case 1:
                element = "water";
                break;
            case 2:
                element = "poison";
                break;
            case 3:
                element = "wood";
                break;
            case 4:
                element = "dark";
                break;
            case 5:
                element = "girl";
                break;
            case 6:
                element = "white";
                break;
        }
    }
    public void set_element(int index)
    {
        choose_element(index);
        Fight_player player1 = new Fight_player();
        Fight_player player2 = new Fight_player();
        player1.read_from_file(path_of_player1);
        player2.points = player1.points;
        player2.element = element;
        player2.nickname = "Anti" + player1.nickname;
        player2.create_file(path_of_player2);
        add_mode();
        SceneManager.LoadScene(name_of_scene, LoadSceneMode.Single);
    }    
    void OnMouseDown()
    {
        backgraund.GetComponent<AudioSource>().Play();
        back.SetActive(true);
        player = new Player();
        player.read_from_file(account_path);
        my_camera.GetComponent<start_mode>().show_slimes(player.slimes.was_bought);
    }
}
