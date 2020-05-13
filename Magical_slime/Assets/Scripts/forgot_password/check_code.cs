using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class check_code : MonoBehaviour
{
    private Sender sender;
    public InputField input_code;
    public GameObject items;
    private string nickname;
    private bool is_click;

    void Start()
    {
        is_click = true;
    }
    public void mail_send(string mail, string nickname)
    {
        sender = new Sender(mail);
        sender.send_to_mail(nickname);
        this.nickname = nickname;
    }
    public void button()
    {
        if (!is_click) return;
        is_click = false;
        GetComponent<AudioSource>().Play();
        if (sender.check_code(input_code.text))
        {
            items.SetActive(true);
            items.GetComponent<edit_password>().set_nickname(nickname);
            is_click = true;
            gameObject.SetActive(false);
        }
        else
        {
            Text text = input_code.transform.Find("Text").GetComponent<Text>();
            text.color = Color.white;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            input_code.image.color = Color.red;
            input_code.text = "The code is incorrect!";
            is_click = true;
        }
    }
}
