using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game3 : MonoBehaviour
{
    public Text txt_quest;
    public Text Collected;

    public string quest;

    private void Update()
    {
        txt_quest.text = quest;
    }
}
