using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1 : MonoBehaviour
{
    public Text currentQuest;
    public Text CollectedQuest;
    public string TxtQuest;
    private void Update()
    {
        currentQuest.text = TxtQuest;
    }
}
