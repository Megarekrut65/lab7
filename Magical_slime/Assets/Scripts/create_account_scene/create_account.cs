using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class create_account : MonoBehaviour
{
    public InputField input_nickname;
    public InputField input_mail;
    public InputField input_password;
    public InputField input_password_again;
    public GameObject incorrect_text;
    public GameObject error_message;
    public GameObject send_mail;
    public string path;

    bool find_player(string nickname, string mail)
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (line == nickname || line == mail)
            {
                reader.Close();
                return true;
            }
        }
        reader.Close();
        return false;
    }
    void exists_player()
    {
        Text text = input_nickname.transform.Find("Text").GetComponent<Text>();
        text.color = Color.white;
        input_nickname.image.color = Color.red;
        input_nickname.text = "Nickname or mail already exist!";
    }
    void incorrect_password()
    {
        incorrect_text.SetActive(true);
        input_password.image.color = Color.red;
        input_password_again.image.color = Color.red;
    }
    bool empty_input(InputField input)
    {
        if (input.text.Length > 3) return false;
        input.image.color = Color.red;
        input.text = "";
        error_message.SetActive(true);
        return true;
    }
    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, path);
#else
        path = Path.Combine(Application.dataPath, path);
#endif
    }
    void OnMouseDown()
    {
        incorrect_text.SetActive(false);        
        if(empty_input(input_nickname)|| empty_input(input_mail)|| empty_input(input_password)|| empty_input(input_password_again)) return;
        Player player;
        string nickname = input_nickname.text;
        string mail = input_mail.text;
        if (find_player("Nickname=" + nickname, "Mail=" + mail))
        {
            exists_player();
        }
        else
        {        
            string password = input_password.text;         
            string password_again = input_password_again.text;         
            if (password == password_again)
            {
                Slimes slimes = new Slimes();
                player = new Player(nickname, password, mail, 100, 300, slimes);
                send_mail.SetActive(true);
                send_mail.GetComponent<send_mail>().send_message(player);
            }
            else
            {
                incorrect_password();
            }
        }
    }
}