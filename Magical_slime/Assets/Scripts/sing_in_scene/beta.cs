using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class beta : MonoBehaviour
{
    private string account_path = "account.txt";
    private string path = "data.txt";
    bool find_beta(string nickname, string password)
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        while (!reader.EndOfStream)
        {
            string file_nickname = reader.ReadLine();
            if (file_nickname == nickname)
            {
                string file_password = reader.ReadLine();
                if (file_password == password)
                {
                    Player player = new Player(nickname, password, reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), reader.ReadLine());
                    reader.Close();
                    player.create_file(account_path);
                    return true;
                }
                else
                {
                    reader.Close();
                    return false;
                }
            }
        }
        reader.Close();
        return false;
    }
    void create_account()
    {
        if (find_beta("Nickname=Beta tester", "Password=uiiu")) return;
        Player player = new Player("Beta tester", "uiiu", "magicalslimesboa@gmail.com", 100, 30000, new Slimes());
        player.create_file(account_path);
        player.append_to_file(path);
    }
    void OnMouseDown()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        account_path = Path.Combine(Application.persistentDataPath, account_path);
        path = Path.Combine(Application.persistentDataPath, path);
#else
        account_path = Path.Combine(Application.dataPath, account_path);
        path = Path.Combine(Application.dataPath, path);
#endif
        create_account();
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }
}
