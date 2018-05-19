using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CSaratakij
{
    public enum QuestType
    {
        CollectAllObject,
        ReachTarget
    }

    public class QuestController : MonoBehaviour
    {
        [SerializeField]
        QuestType questType;

        [SerializeField]
        Transform target;

        [SerializeField]
        Transform targetDestination;

        [SerializeField]
        GameObject[] objectRequirements;

        [SerializeField]
        UnityEvent onQuestCompleted;

        public bool isCompleted;

        [SerializeField] private Game1 game1;
        [SerializeField] private Game2 game2;
        [SerializeField] private Game3 game3;
        [SerializeField] private Game4 game4;

        private bool isGame1;
        private bool isGame2;
        private bool isGame3;
        private bool isGame4;

        private void Start()
        {
            if(game1 != null)
            {
                isGame1 = true;
            }
            else if(game2 != null)
            {
                isGame2 = true;
            }
            else if(game3 != null)
            {
                isGame3 = true;
            }
            else if(game4 != null)
            {
                isGame4 = true;
                game4.quest = "Collect Gem";
                game4.txt_collected.text = "";
            }
        }

        void Update()
        {
            if (!isCompleted) {
                _QuestHandler();
            }
        }

        void _QuestHandler()
        {
            switch (questType) {
                case QuestType.CollectAllObject:
                    _CollectAllObject_Handler();
                    break;

                case QuestType.ReachTarget:
                    _ReachTarget_Handler();
                    break;
            }
        }

        void _CollectAllObject_Handler()
        {
            var isAllObjectDisable = true;
            int current = 0;
            foreach (GameObject obj in objectRequirements) {
                if (obj.activeSelf) {
                    isAllObjectDisable = false;
                }
                else
                {
                    current++;
                }
            }
            if(isGame1)
            {
                game1.TxtQuest = "Find banana";
                game1.CollectedQuest.text = current + "/" + objectRequirements.Length;
            }
            else if(isGame2)
            {
                game2.quest = "Find plank";
                game2.txt_count.text = current + "/" + objectRequirements.Length;
            }

            if (isAllObjectDisable) {
                isCompleted = true;
                if(isGame1)
                {
                    game1.CollectedQuest.text = " ";
                    game1.TxtQuest = "Find the way out";
                }
                else if(isGame2)
                {
                    game2.quest = "Reach to the boat";
                    game2.txt_count.text = "";
                }
                _FireEvent_OnQuestCompleted();
            }
        }

        void _ReachTarget_Handler()
        {
            var distance = Vector3.Distance(target.position, targetDestination.position);
            if(isGame3 && target.CompareTag("Finish"))
            {
                game3.quest = "Activate bridge";
                game3.Collected.text = " ";
            }
            if (distance <= 1.0f) {
                isCompleted = true;
                _FireEvent_OnQuestCompleted();
                if (isGame3)
                {
                    game3.quest = "Place skull\nOn platform";
                    game3.Collected.text = " ";
                }
            }
        }

        void _FireEvent_OnQuestCompleted()
        {
            onQuestCompleted.Invoke();
        }
    }
}
