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


        bool isCompleted;


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

            foreach (GameObject obj in objectRequirements) {
                if (obj.activeSelf) {
                    isAllObjectDisable = false;
                }
            }

            if (isAllObjectDisable) {
                isCompleted = true;
                _FireEvent_OnQuestCompleted();
            }
        }

        void _ReachTarget_Handler()
        {
            var distance = Vector3.Distance(target.position, targetDestination.position);

            if (distance <= 1.0f) {
                isCompleted = true;
                _FireEvent_OnQuestCompleted();
            }
        }

        void _FireEvent_OnQuestCompleted()
        {
            onQuestCompleted.Invoke();
        }
    }
}
