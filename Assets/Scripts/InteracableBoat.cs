using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using CSaratakij;

public class InteracableBoat : MonoBehaviour
{
    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    [SerializeField] private VRInput m_VRInput;
    private bool m_GazeOver;
    private MeshRenderer meshRenderer;
    [SerializeField]private GameObject fixedBoat;

    //Hacks
    [SerializeField]
    QuestLogic questLogic;


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        fixedBoat.SetActive(false);
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
    }

    private void HandleDown()
    {
        if (m_GazeOver)
        {
            /* fixedBoat.SetActive(true); */
            /* meshRenderer.enabled = false; */
            if (!questLogic) { return; }
            questLogic.GoToScene(4);
        }
    }
}
