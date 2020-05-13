using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class end_of_fight : MonoBehaviour
{
    public GameObject victory;
    public GameObject defeat;
    public string player_left_path;
    public string player_right_path;
    public string date_path;
    public string new_date_path;
    public string account_path;
    public string victory_path;
    private string nickname_of_winner;
    public Text game_text;
    public Text info_text;
    private Fight_player left_player;
    private Fight_player right_player;
    private const int number_of_items = 3;
    private int points_of_player = 0;
    private int coins_of_player = 0;
    private bool is_draw = false;
    public Image backgraund;
    public Image[] backgraunds;

    void read_victory()
    {
        FileStream file = new FileStream(victory_path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        if(reader.EndOfStream)
        {
            nickname_of_winner = "";
            file.Close();
            return;
        }
        nickname_of_winner = reader.ReadLine();
        if (nickname_of_winner == "Draw")
        {
            is_draw = true;
        }
        else nickname_of_winner = nickname_of_winner.Substring(7);
        int index_of_backgraund = 0;
        string index = reader.ReadLine();
        if (index.Length > 11) index_of_backgraund = Convert.ToInt32(index.Substring(11));
        backgraund.sprite = backgraunds[index_of_backgraund].sprite;
        reader.Close();
    }
    void calculate_the_prize(Fight_player winner, Fight_player loser)
    {
        points_of_player = 0;
        if (winner.points > loser.points)
        {
            points_of_player = 10 + 6 / (winner.points - loser.points) + UnityEngine.Random.Range(1, 5);
        }
        else
        {
            points_of_player = 15 + (loser.points - winner.points) / 10 + UnityEngine.Random.Range(2, 10);
        }
        coins_of_player = UnityEngine.Random.Range(points_of_player * 5 - 1, points_of_player * 10 + 1);
    }
    void calculate_the_losses()
    {
        points_of_player = -1 * points_of_player / 3;
        coins_of_player = coins_of_player / 10;
    }
    void set_winner(Fight_player winner, Fight_player loser)
    {
        victory.SetActive(true);
        game_text.text = "Victory!";
        info_text.text = "Congratulations on your victory, " + winner.nickname + ".\n\n" +
            loser.nickname + " was your enemy and you destroyed him!\n\n" +
            "For winning you get " + points_of_player.ToString() +
            " points and " + coins_of_player.ToString() + " coins.";
    }
    void set_draw()
    {
        victory.SetActive(true);
        points_of_player -= 5;
        coins_of_player -= 10;
        game_text.text = "Victory!";
        info_text.text = "Congratulations on your draw, " + left_player.nickname + ".\n\n" +
            right_player.nickname + " was your enemy.\n\n"+
            "You fought hard, but your strength was equal.\n\n" +
            "For the draw you get " + points_of_player.ToString() +
            " points and " + coins_of_player.ToString() + " coins.";
    }
    void set_loser(Fight_player loser, Fight_player winner)
    {
        defeat.SetActive(true);
        game_text.text = "Defeat!";
        info_text.text = "Sorry but you lost, " + loser.nickname + ".\n\n" +
            winner.nickname + " was your enemy and you couldn't defeat him!\n\n" +
            "You get " + coins_of_player.ToString() + " coins and lose " + (-1 * points_of_player).ToString() + " points.";       
    }       
    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        player_left_path = Path.Combine(Application.persistentDataPath,  player_left_path);
        player_right_path = Path.Combine(Application.persistentDataPath, player_right_path);
        date_path = Path.Combine(Application.persistentDataPath,  date_path);
        new_date_path = Path.Combine(Application.persistentDataPath, new_date_path);
        account_path = Path.Combine(Application.persistentDataPath,  account_path);
        victory_path = Path.Combine(Application.persistentDataPath, victory_path);
#else
        player_left_path = Path.Combine(Application.dataPath, player_left_path);
        player_right_path = Path.Combine(Application.dataPath, player_right_path);
        date_path = Path.Combine(Application.dataPath, date_path);
        new_date_path = Path.Combine(Application.dataPath, new_date_path);
        account_path = Path.Combine(Application.dataPath, account_path);
        victory_path = Path.Combine(Application.dataPath, victory_path);
#endif
        victory.SetActive(false);
        defeat.SetActive(false);
        left_player = new Fight_player();
        right_player = new Fight_player();
        left_player.read_from_file(player_left_path);
        right_player.read_from_file(player_right_path);
        read_victory();
        if ((nickname_of_winner == left_player.nickname)||is_draw)
        {
            calculate_the_prize(left_player, right_player);
            if(is_draw) set_draw();
            else set_winner(left_player, right_player);
        }
        else if(nickname_of_winner == right_player.nickname)
        {
            calculate_the_prize(left_player, right_player);
            calculate_the_losses();
            set_loser(left_player, right_player);
        }
        else
        {
            set_winner(left_player, right_player);
        }
        Player player = new Player();
        player.read_from_file(account_path);
        player.points += points_of_player;
        player.coins += coins_of_player;
        player.save_player(date_path, new_date_path, account_path);
    }
}
