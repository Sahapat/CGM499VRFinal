using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game4 : MonoBehaviour
{
    public Text txt_quest;
    public Text txt_collected;

    public string quest;

    private void Update()
    {
        txt_quest.text = quest;
    }
}
