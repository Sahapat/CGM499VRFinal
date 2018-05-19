using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game2 : MonoBehaviour
{
    public Text txt_quest;
    public Text txt_count;

    public string quest;

    private void Update()
    {
        txt_quest.text = quest;
    }
}
