using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CSaratakij
{
    public class QuestLogic : MonoBehaviour
    {
        public void GoToScene(int index)
        {
            SceneManager.LoadScene(index);
        }
    }
}
