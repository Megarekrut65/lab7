using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class raund : MonoBehaviour
{
    public Text round_text;
    private int number_of_rounds;
    public void next_round(ref int number_of_rounds)
    {
        number_of_rounds++;
        this.number_of_rounds = number_of_rounds;        
        GetComponent<Animation>().Play("raund");
    }
    void set_text()
    {
        round_text.text = number_of_rounds.ToString();
    }
}
