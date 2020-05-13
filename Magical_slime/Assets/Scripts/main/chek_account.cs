using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class chek_account : MonoBehaviour
{
    public string name_of_scene;
    public string path;
    bool is_empty()
    {
        string[] file = File.ReadAllLines(path);
        if (file.Length == 0) return true;
        return false;
    }
void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, path);
#else
        path = Path.Combine(Application.dataPath, path);
#endif
        Player player = new Player();
        player.read_from_file(path);
        if (player.is_read) return;
        else
        {
            SceneManager.LoadScene(name_of_scene, LoadSceneMode.Single);
        }
    }
}
