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

    private bool m_GazeOver;
    private MeshRenderer meshRenderer;

    [SerializeField] private float respawnTime;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        m_VRInput.OnDown += HandleDown;

        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
    }

    private void OnDisable()
    {
        m_VRInput.OnDown -= HandleDown;

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
    private void HandleDown()
    {
        if (m_GazeOver)
        {
            meshRenderer.enabled = false;
            gameObject.SetActive(false);
            /* Invoke("RespawnObject", respawnTime); */
        }
    }

    private void RespawnObject()
    {
        meshRenderer.enabled = true;
        CancelInvoke();
    }
}
