using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class the_start : MonoBehaviour
{
    public GameObject place;
    public Text coins_text;
    public int number_of_slimes = 7;
    public string path_account;
    public string path;
    public string new_path;
    public int[] prices;
    public Player player;

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path_account = Path.Combine(Application.persistentDataPath, path_account);
        path = Path.Combine(Application.persistentDataPath, path);
        new_path = Path.Combine(Application.persistentDataPath, new_path);
#else
        path_account = Path.Combine(Application.dataPath, path_account);
        path = Path.Combine(Application.dataPath, path);
        new_path = Path.Combine(Application.dataPath, new_path);
#endif
        player = new Player();
        player.read_from_file(path_account);
        if(!player.is_read)
        {
            SceneManager.LoadScene("main", LoadSceneMode.Single);
            return;
        }
        coins_text.text = player.coins.ToString();
        prices = new int[number_of_slimes];
        int value = 100;
        for (int i = 0; i < number_of_slimes; i++)
        {
            prices[i] = value;
            value = 2 * value + 100;
        }
        place.GetComponent<index>().setting();
    }  
    public void save_info()
    {
        player.save_player(path, new_path, path_account);
    }
}
