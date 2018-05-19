using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlider : MonoBehaviour
{
    [SerializeField] private Sprite[] images;
    [SerializeField] private GameObject[] textShows;
    [SerializeField] private Animator anim;
    [SerializeField] private float timePerimage;

    private WaitForSeconds waitTime;
    private WaitForSeconds waitFade;
    private Image m_image;
    private byte showindex = 0;
    private bool isFinalImage;

    public bool isEnd;

    private void Awake()
    {
        m_image = GetComponent<Image>();
        waitTime = new WaitForSeconds(timePerimage);
        waitFade = new WaitForSeconds(1f);
    }
    private void Start()
    {
        StartCoroutine(SlideImage());
    }
    private void Update()
    {
        if((OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTrackedRemote)
                                         || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote)||
                                         Input.GetButtonDown("Fire1"))
                                         && isFinalImage)
        {
            ToGameScene();
        }
    }
    private void ToGameScene()
    {
        if (!isEnd)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
    private IEnumerator SlideImage()
    {
        m_image.sprite = images[0];
        textShows[0].SetActive(true);
        textShows[1].SetActive(false);
        yield return waitTime;
        anim.SetTrigger("FadeOut");
        yield return waitFade;
        m_image.sprite = images[1];
        textShows[0].SetActive(false);
        textShows[1].SetActive(true);
        anim.SetTrigger("FadeIN");
        yield return waitTime;
        isFinalImage = true;
    }
}
