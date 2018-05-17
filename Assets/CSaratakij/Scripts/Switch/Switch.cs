using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CSaratakij
{
    public class Switch : MonoBehaviour
    {
        [SerializeField]
        bool isTurnOn;

        [SerializeField]
        UnityEvent OnSwitchTurnOn;

        [SerializeField]
        UnityEvent OnSwitchTurnOff;


        bool previousState;


        public bool IsTurnOn { get { return isTurnOn; } }


        void Awake()
        {
            _SwitchSignalHandler();
        }

        void Update()
        {
            if (isTurnOn != previousState) {
                previousState = isTurnOn;
                _SwitchSignalHandler();
            }
        }

        void _SwitchSignalHandler()
        {
            if (isTurnOn) {
                _FireEvent_OnSwitchTurnOn();
            }
            else {
                _FireEvent_OnSwitchTurnOff();
            }
        }

        void _FireEvent_OnSwitchTurnOn()
        {
            if (OnSwitchTurnOn != null) {
                OnSwitchTurnOn.Invoke();
            }
        }

        void _FireEvent_OnSwitchTurnOff()
        {
            if (OnSwitchTurnOff != null) {
                OnSwitchTurnOff.Invoke();
            }
        }

        public void Toggle()
        {
            isTurnOn = !isTurnOn;
        }

        public void TurnOn()
        {
            isTurnOn = true;
        }

        public void TurnOff()
        {
            isTurnOn = false;
        }
    }
}
