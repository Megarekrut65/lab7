using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class send_mail : MonoBehaviour
{
    public InputField input_code;
    private Player player;
    public string path;
    public GameObject items;
    private Sender sender;

    public void send_message(Player player)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, path);
#else
        path = Path.Combine(Application.dataPath, path);
#endif
        GetComponent<AudioSource>().Play();
        this.player = player;
        items.SetActive(false);
        sender = new Sender(player.mail);
        sender.send_to_mail(player.nickname);
    }
    void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
        if (sender.check_code(input_code.text))
        {
            player.append_to_file(path);
            SceneManager.LoadScene("sign_in_scene", LoadSceneMode.Single);
        }
        else
        {
            Text text = input_code.transform.Find("Text").GetComponent<Text>();
            text.color = Color.white;
            input_code.image.color = Color.red;
            input_code.text = "The code is incorrect!";
        }
    }
}
public class Sender
{
    private string mail_of_sender;
    private string password;
    private string mail_of_player;
    private int port;
    private int code;

    public Sender()
    {
        mail_of_sender = "magicalslimesboa@gmail.com";
        password = "9NvZZpZz1s";
        mail_of_player = "magicalslimesboa@gmail.com";
        port = 587;
        code = 0;
    }
    public Sender(string mail)
    {
        mail_of_sender = "magicalslimesboa@gmail.com";
        password = "9NvZZpZz1s";
        mail_of_player = mail;
        port = 587;
        code = UnityEngine.Random.Range(10000, 99999);
    }
    private void send(string message, string title)
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(mail_of_sender);
        mail.To.Add(mail_of_player);
        mail.Subject = "Code " + title;
        mail.Body = message;
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = port;
        smtpServer.Credentials = new System.Net.NetworkCredential(mail_of_sender, password) as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };
        smtpServer.Send(mail);
    }
    public void send_to_mail(string nickname)
    {
        string title = "Magical slimes";
        string message = "Hello, " + nickname + ". Your code: " + code.ToString();
        if (code == 0) return;
        send(message, title);
    }
    public bool check_code(string code)
    {
        int number;
        bool is_number = int.TryParse(code, out number);
        if(!is_number) return false;
        if (code.Length != 5) return false;
        if (this.code == Convert.ToInt32(code))
        {
            return true;
        }
        return false;
    }
}