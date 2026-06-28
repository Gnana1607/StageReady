using TMPro;
using UnityEngine;

public class PresentationTrigger : MonoBehaviour
{
    [SerializeField] private GameObject presentationMenu;
    [SerializeField]private GameObject presentationScreen;
    [SerializeField] private TMP_Text endPresentationText;
    [SerializeField] private AudioSource clappingSource;
public GameObject backButton1;
public GameObject backButton2;
public GameObject endPresentation;

    private bool hasShownMenu = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "XR Origin (XR Rig)" && !hasShownMenu)
        {
            presentationMenu.SetActive(true);
            backButton1.SetActive(false);
            hasShownMenu = true;
        }
    }
    public void presentationStart()
    {
        presentationMenu.SetActive(false);
        presentationScreen.SetActive(true);

        backButton2.SetActive(true);

        endPresentation.SetActive(true);
        endPresentationText.text = "End Presentation";
    }
     public void freeSpeech()
    {
        presentationMenu.SetActive(false);

        backButton2.SetActive(true);

        endPresentation.SetActive(true);
        endPresentationText.text = "End Speech";
    }
    public void EndPresentation()
    {
        clappingSource.Play();

        presentationScreen.SetActive(false);
        endPresentation.SetActive(false);
        backButton2.SetActive(false);
    }
    public void GoBackChoose()
    {
        presentationMenu.SetActive(true);
        backButton2.SetActive(false);
    }
}
