using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class data_base : MonoBehaviour
{
    public InputField input_nickname;
    public InputField input_password;
    public string path;
    public string account_path;
    public string name_of_scene;
    Player player;

    void incorrect_date()
    {
        Text text = input_nickname.transform.Find("Text").GetComponent<Text>();
        text.color = Color.white;
        text.fontSize = 6;
        text = input_password.transform.Find("Text").GetComponent<Text>();
        text.color = Color.white;
        text.fontSize = 6;
        input_nickname.image.color = Color.red;
        input_password.image.color = Color.red;
        input_nickname.text = "Nickname or password are incorrect!";
    }
    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, path);
        account_path = Path.Combine(Application.persistentDataPath, account_path);
#else
        path = Path.Combine(Application.dataPath, path);
        account_path = Path.Combine(Application.dataPath, account_path);
#endif
    }
    bool find_player(string nickname, string password)
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
    void OnMouseDown()
    {
        string nickname = "Nickname=" + input_nickname.text;
        string password = "Password=" + input_password.text;
        if(!find_player(nickname, password))
        {
            incorrect_date();
        }
        else
        {            
            SceneManager.LoadScene(name_of_scene, LoadSceneMode.Single);          
        }      
    }
}
public class Player
{
    public string nickname;
    private string password;
    public string mail;
    public int points;
    public int coins;
    public Slimes slimes;
    private const int number_of_slimes = 7;
    public bool is_read;
    public Player()
    {
        nickname = "nickname";
        password = "password";
        mail = "mail";        
        points = 0;
        coins = 0;
        slimes = new Slimes();
        is_read = false;
    }
    public Player(string nickname, string password, string mail, int points, int coins, Slimes slimes)
    {
        this.nickname = nickname;
        this.password = password;
        this.mail = mail;
        this.points = points;
        this.coins = coins;
        this.slimes = slimes;
        is_read = false;
    }
    public Player(string nickname, string password, string mail, string points, string coins, string slimes)
    {
        this.nickname = nickname.Substring(9);
        this.password = password.Substring(9);
        this.mail = mail.Substring(5);
        this.points = Convert.ToInt32(points.Substring(7));
        this.coins = Convert.ToInt32(coins.Substring(6));
        this.slimes = new Slimes();
        this.slimes.read_slimes(slimes.Substring(7));
        is_read = false;
    }
    public bool check_password(string password)
    {
        return (this.password == password);
    }
    public void create_file(string path)
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamWriter writer = new StreamWriter(file);
        writer.WriteLine("Nickname=" + nickname);
        writer.WriteLine("Password=" + password);
        writer.WriteLine("Mail=" + mail);
        writer.WriteLine("Points=" + points.ToString());
        writer.WriteLine("Coins=" + coins.ToString());
        writer.WriteLine(slimes.create_string());
        writer.Close();
    }
    public void append_to_file(string path)
    {
        FileStream file = new FileStream(path, FileMode.Append);
        StreamWriter writer = new StreamWriter(file);
        writer.WriteLine("Nickname=" + nickname);
        writer.WriteLine("Password=" + password);
        writer.WriteLine("Mail=" + mail);
        writer.WriteLine("Points=" + points.ToString());
        writer.WriteLine("Coins=" + coins.ToString());
        writer.WriteLine(slimes.create_string());
        writer.Close();
    }
    void refresh_file(string path)
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        reader.Close();
    }
    public void read_from_file(string path)
    {
        is_read = false;
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        if (reader.EndOfStream) return;
        nickname = reader.ReadLine().Substring(9);
        if (reader.EndOfStream) return;
        password = reader.ReadLine().Substring(9);
        if (reader.EndOfStream) return;
        mail = reader.ReadLine().Substring(5);
        if (reader.EndOfStream) return;
        points = Convert.ToInt32(reader.ReadLine().Substring(7));
        if (reader.EndOfStream) return;
        coins = Convert.ToInt32(reader.ReadLine().Substring(6));
        if (reader.EndOfStream) return;
        slimes.read_slimes(reader.ReadLine().Substring(7));
        reader.Close();
        is_read = true;
    }
    public void save_player(string path, string new_path, string account_path)
    {
        create_file(account_path);
        refresh_file(new_path);
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        while (!reader.EndOfStream)
        {
            string file_nickname = reader.ReadLine();
            if ((file_nickname.Length <= 9) || (file_nickname.Substring(0, 9) != "Nickname="))
            {
                continue;
            }
            if (file_nickname.Substring(9) == nickname)
            {
                append_to_file(new_path);
            }
            else
            {
                Player new_player = new Player(file_nickname, reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), reader.ReadLine());
                new_player.append_to_file(new_path);
            }
        }
        reader.Close();
        File.Delete(path);
        File.Move(new_path, path);
    }
    public void delete_account(string path, string new_path, string account_path)
    {
        File.Delete(account_path);
        refresh_file(new_path);
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        while (!reader.EndOfStream)
        {
            string file_nickname = reader.ReadLine();
            if ((file_nickname.Length <= 9) || (file_nickname.Substring(0, 9) != "Nickname="))
            {
                continue;
            }
            if (file_nickname.Substring(9) == nickname)
            {
                continue;
            }
            else
            {
                Player new_player = new Player(file_nickname, reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), reader.ReadLine());
                new_player.append_to_file(new_path);
            }
        }
        reader.Close();
        File.Delete(path);
        File.Move(new_path, path);
    }
}
public class Slimes
{
    public bool[] was_bought;
    private const int number_of_slimes = 7;
    public Slimes()
    {
        was_bought = new bool[number_of_slimes];
        for (int i = 0; i < number_of_slimes; i++) was_bought[i] = false;
    }
    public string create_string()
    {
        string slimes = "Slimes=";
        for(int i = 0; i < number_of_slimes; i++)
        {
            if (was_bought[i]) slimes += "1";
            else slimes += "0";
        }
        return slimes;
    }
    public void read_slimes(string slimes)
    {
        for (int i = 0; i < number_of_slimes; i++)
        {
            if (slimes[i] == '1') was_bought[i] = true;
            else was_bought[i] = false;
        }
    }
    public void buy_slime(int index)
    {
        was_bought[index] = true;
    }
    public bool check_slime(int index)
    {
        return was_bought[index];
    }
}