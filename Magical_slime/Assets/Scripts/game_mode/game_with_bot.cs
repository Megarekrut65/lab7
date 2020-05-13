using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class game_with_bot : MonoBehaviour
{
    public string path_of_player1;
    public string path_of_player2;
    public string path_of_mode;
    private string element;
    public string name_of_scene;
    public GameObject backgraund;

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path_of_player1 = Path.Combine(Application.persistentDataPath, path_of_player1);
        path_of_player2 = Path.Combine(Application.persistentDataPath, path_of_player2);
        path_of_mode = Path.Combine(Application.persistentDataPath, path_of_mode);
#else
        path_of_player1 = Path.Combine(Application.dataPath, path_of_player1);
        path_of_player2 = Path.Combine(Application.dataPath, path_of_player2);
        path_of_mode = Path.Combine(Application.dataPath, path_of_mode);
#endif
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
    void add_mode()
    {
        FileStream file = new FileStream(path_of_mode, FileMode.OpenOrCreate);
        StreamWriter writer = new StreamWriter(file);
        writer.WriteLine("Mode=bot");
        writer.Close();
    }
    void OnMouseDown()
    {
        backgraund.GetComponent<AudioSource>().Play();
        int index = UnityEngine.Random.Range(0, 6);
        choose_element(index);
        Fight_player player = new Fight_player();
        Fight_player bot = new Fight_player();
        player.read_from_file(path_of_player1);
        bot.nickname = "The " + element;
        bot.points = player.points + UnityEngine.Random.Range(-40, 50);
        if (bot.points < 0) bot.points = 10;
        bot.element = element;
        bot.create_file(path_of_player2);
        add_mode();
        SceneManager.LoadScene(name_of_scene, LoadSceneMode.Single);
    }
}
