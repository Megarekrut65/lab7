using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class send_code : MonoBehaviour
{
    public GameObject input_mail;
    public string path;
    public GameObject send_objects;
    private string nickname;
    private bool is_click;

    void Start()
    {
        is_click = true;
    }
    bool find_mail(string mail)
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (line.Length > 9 && line.Substring(0, 9) == "Nickname=") nickname = line.Substring(9);
            if (line == mail)
            {
                reader.Close();
                return true;
            }
        }
        reader.Close();
        return false;
    }
    void OnMouseDown()
    {
        if (!is_click) return;
        is_click = false;
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, path);
#else
        path = Path.Combine(Application.dataPath, path);
#endif
        GetComponent<AudioSource>().Play();
        string mail = input_mail.GetComponent<InputField>().text;
        if (find_mail("Mail=" + mail))
        {
            input_mail.SetActive(false);
            send_objects.SetActive(true);
            send_objects.GetComponent<check_code>().mail_send(mail, nickname);
            is_click = true;
            gameObject.SetActive(false);
        }
        else
        {
            Text text = input_mail.GetComponent<InputField>().transform.Find("Text").GetComponent<Text>();
            text.color = Color.white;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            input_mail.GetComponent<InputField>().image.color = Color.red;
            input_mail.GetComponent<InputField>().text = "There isn't entered mail!";
            is_click = true;
        }
    }
}
