using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Main : MonoBehaviour
{
    public float w_timer;
    public float b_timer;
    public TextMeshProUGUI w_timer_text;
    public TextMeshProUGUI b_timer_text;
    public int turn;
    // Start is called before the first frame update
    void Start()
    {
        turn = (int)color.WHITE;
        update_timer(w_timer, w_timer_text);
        update_timer(b_timer, b_timer_text);

    }

    // Update is called once per frame
    void Update()
    {
        if (turn == (int)color.WHITE)
        {
            w_timer -= Time.deltaTime;
            update_timer(w_timer, w_timer_text);
        }
        else
        {
            b_timer -= Time.deltaTime;
            update_timer(b_timer, b_timer_text);
        }    
    }

    void update_timer(float timer, TextMeshProUGUI timer_text)
    {
        string minutes = ((int)(timer / 60)).ToString();
        string seconds = ((int)(timer % 60)).ToString();
        if (seconds == "0") seconds += "0";
        timer_text.text = $"{minutes}: {seconds}";
    }

}
