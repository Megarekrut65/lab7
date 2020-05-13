using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class delete_account : MonoBehaviour
{
    public InputField input_password;
    public GameObject my_camera;
    public string name_of_scene;
    public string path;
    public string new_path;
    public string account_path;

    void OnMouseDown()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, path);
        new_path = Path.Combine(Application.persistentDataPath, new_path);
        account_path = Path.Combine(Application.persistentDataPath, account_path);
#else
        path = Path.Combine(Application.dataPath, path);
        new_path = Path.Combine(Application.dataPath, new_path);
        account_path = Path.Combine(Application.dataPath, account_path);
#endif
        if (my_camera.GetComponent<my_account_date>().player.check_password(input_password.text))
        {
            my_camera.GetComponent<my_account_date>().player.delete_account(path, new_path,account_path);
            SceneManager.LoadScene(name_of_scene, LoadSceneMode.Single);
        }
        else
        {
            Text text = input_password.transform.Find("Text").GetComponent<Text>();
            text.color = Color.white;
            input_password.image.color = Color.red;
            input_password.text = "Passwords do not match!";
        }
    }
}
