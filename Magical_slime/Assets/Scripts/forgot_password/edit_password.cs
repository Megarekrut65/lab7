using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class edit_password : MonoBehaviour
{
    public InputField input_password;
    public InputField input_password_again;
    private string nickname;
    public string path;
    public string new_path;
    public string account_path;
    public string name_of_scene;
    private bool is_click;

    void Start()
    {
        is_click = true;
    }
    public void set_nickname(string nickname)
    {
        this.nickname = "Nickname=" + nickname;
    }
    void edit_player(string new_password)
    {
        Player player = new Player();
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (line == nickname)
            {
                string old_password = reader.ReadLine();
                player = new Player(nickname, "Password=" + new_password, reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), reader.ReadLine());
                break;
            }
        }
        reader.Close();
        player.save_player(path, new_path, account_path);
        SceneManager.LoadScene(name_of_scene, LoadSceneMode.Single);
    }
    public void button()
    {
        if (!is_click) return;
        is_click = false;
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, path);
        new_path = Path.Combine(Application.persistentDataPath, new_path);
        account_path = Path.Combine(Application.persistentDataPath, account_path);
#else
        path = Path.Combine(Application.dataPath, path);
        new_path = Path.Combine(Application.dataPath, new_path);
        account_path = Path.Combine(Application.dataPath, account_path);
#endif
        if (input_password.text == input_password_again.text)
        {
            if(input_password.text.Length < 4)
            {
                Text text = input_password.transform.Find("Text").GetComponent<Text>();
                text.color = Color.white;
                text.horizontalOverflow = HorizontalWrapMode.Wrap;
                input_password.image.color = Color.red;
                input_password.text = "Password must be longer than 3 characters!";
                is_click = true;
                return;
            }
            is_click = true;
            edit_player(input_password.text);
        }
        else
        {
            Text text = input_password.transform.Find("Text").GetComponent<Text>();
            text.color = Color.white;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            input_password.image.color = Color.red;
            input_password.text = "Passwords do not match!";
            is_click = true;
        }
    }
}
