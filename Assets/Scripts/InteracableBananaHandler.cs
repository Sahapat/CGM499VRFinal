using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;

public class InteracableBananaHandler : MonoBehaviour
{
    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    [SerializeField] private VRInput m_VRInput;
    [SerializeField] private Text text;
    private bool m_GazeOver;

    private AudioSource audioSource;
    private MeshRenderer meshRenderer;

    [SerializeField] private float respawnTime;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void OnEnable()
    {
        m_VRInput.OnDown += HandleDown;
        m_VRInput.OnUp += HandleUp;

        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
    }

    private void OnDisable()
    {
        m_VRInput.OnDown -= HandleDown;
        m_VRInput.OnUp -= HandleUp;

        m_InteractiveItem.OnOver -= HandleOver;
        m_InteractiveItem.OnOut -= HandleOut;
    }
    private void HandleOut()
    {
        m_GazeOver = false;
    }

    private void HandleOver()
    {
        m_GazeOver = true;
    }

    private void HandleUp()
    {
        text.text = "Up";
        text.color = Color.red;
    }

    private void HandleDown()
    {
        /*if(m_GazeOver)
        {
            audioSource.Play();
            meshRenderer.enabled = false;

            Invoke("RespawnObject", respawnTime);
        }*/
        text.text = "Down";
        text.color = Color.green;
    }
    private void RespawnObject()
    {
        meshRenderer.enabled = true;
        CancelInvoke();
    }
}
