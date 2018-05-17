using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

namespace CSaratakij
{
    public class InteracableSwitchHandler : MonoBehaviour
    {
        [SerializeField]
        VRInteractiveItem interactiveItem;

        [SerializeField]
        VRInput vrInput;


        bool isGazeOver;


        Switch swichComponent;
        AudioSource audioSource;


        void OnEnable()
        {
            _SubscribeEvents();
        }

        void OnDisable()
        {
            _UnsubscribeEvents();
        }

        void Awake()
        {
            _Initialize();
        }

        void _Initialize()
        {
            swichComponent = GetComponent<Switch>();
            audioSource = GetComponent<AudioSource>();
        }

        void _SubscribeEvents()
        {
            vrInput.OnDown += _OnDown;
            vrInput.OnUp += _OnUp;

            interactiveItem.OnOver += _OnOver;
            interactiveItem.OnOut += _OnOut;
        }

        void _UnsubscribeEvents()
        {
            vrInput.OnDown -= _OnDown;
            vrInput.OnUp -= _OnUp;

            interactiveItem.OnOver -= _OnOver;
            interactiveItem.OnOut -= _OnOut;
        }

        void _OnDown()
        {
            if (isGazeOver) {
                audioSource.Play();
                swichComponent.Toggle();
            }
        }

        void _OnUp()
        {
        }

        void _OnOver()
        {
            isGazeOver = true;
        }

        void _OnOut()
        {
            isGazeOver = false;
        }
    }
}
