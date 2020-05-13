using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class my_account_date : MonoBehaviour
{
    public Text nickname;
    public Text mail;
    public Text points;
    public Text coins;
    public string path_account;
    public Player player;

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path_account = Path.Combine(Application.persistentDataPath, path_account);
#else
        path_account = Path.Combine(Application.dataPath, path_account);
#endif
        player = new Player();
        player.read_from_file(path_account);
        if (!player.is_read)
        {
            SceneManager.LoadScene("main", LoadSceneMode.Single);
            return;
        }
        nickname.text += " " + player.nickname;
        mail.text += " " + player.mail;
        points.text += " " + player.points.ToString();
        coins.text += " " + player.coins.ToString();
    }
}
