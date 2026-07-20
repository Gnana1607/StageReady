using TMPro;
using UnityEngine;

public class PresentationTrigger : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject presentationMenu;
    [SerializeField] private GameObject readyPanel;
    [SerializeField] private GameObject presentationScreen;

    [Header("Buttons")]
    public GameObject backButton1;          // Back to Role Selection
    public GameObject backButton2;          // Back during Presentation
    public GameObject backButtonReady;      // Back on Ready Panel
    public GameObject endPresentation;

    [Header("UI Text")]
    [SerializeField] private TMP_Text endPresentationText;

    [Header("Audio")]
    [SerializeField] private AudioSource clappingSource;
    [SerializeField] private AudienceNoise audienceNoise;

    [Header("Managers")]
    [SerializeField] private MainMenuManager mainMenuManager;

    private bool isPresentationMode;
    private bool hasShownMenu = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "XR Origin (XR Rig)" && !hasShownMenu)
        {
            presentationMenu.SetActive(true);

            backButton1.SetActive(false);
            backButton2.SetActive(false);
            backButtonReady.SetActive(false);
            endPresentation.SetActive(false);

            hasShownMenu = true;
        }
    }

    // -----------------------
    // Presentation Mode
    // -----------------------

    public void presentationStart()
    {
        isPresentationMode = true;

        presentationMenu.SetActive(false);

        readyPanel.SetActive(true);
        backButtonReady.SetActive(true);

        backButton2.SetActive(false);
        endPresentation.SetActive(false);
    }

    // -----------------------
    // Free Speech Mode
    // -----------------------

    public void freeSpeech()
    {
        isPresentationMode = false;

        presentationMenu.SetActive(false);

        readyPanel.SetActive(true);
        backButtonReady.SetActive(true);

        backButton2.SetActive(false);
        endPresentation.SetActive(false);
    }

    // -----------------------
    // Ready Panel -> START
    // -----------------------

    public void StartPresentation()
    {
        readyPanel.SetActive(false);
        backButtonReady.SetActive(false);

        backButton2.SetActive(true);
        endPresentation.SetActive(true);

        if (isPresentationMode)
        {
            presentationScreen.SetActive(true);
            endPresentationText.text = "End Presentation";
        }
        else
        {
            presentationScreen.SetActive(false);
            endPresentationText.text = "End Speech";
        }
    }

    // -----------------------
    // Ready Panel -> Back
    // -----------------------

    public void BackToPresentationMenu()
    {
        readyPanel.SetActive(false);
        backButtonReady.SetActive(false);

        presentationMenu.SetActive(true);

        backButton2.SetActive(false);
        endPresentation.SetActive(false);
    }

    // -----------------------
    // End Presentation
    // -----------------------

    public void EndPresentation()
    {
        audienceNoise.StopAudienceNoise();

        clappingSource.Play();

        Animator[] audienceAnimators =
            GameObject.Find("Students").GetComponentsInChildren<Animator>();

        foreach (Animator animator in audienceAnimators)
        {
            animator.SetBool("Clap", true);
        }

        presentationScreen.SetActive(false);
        presentationMenu.SetActive(false);
        readyPanel.SetActive(false);

        backButtonReady.SetActive(false);
        backButton2.SetActive(false);
        endPresentation.SetActive(false);

        Invoke(nameof(StopClapping), clappingSource.clip.length);
    }

    private void StopClapping()
    {
        Animator[] audienceAnimators =
            GameObject.Find("Students").GetComponentsInChildren<Animator>();

        foreach (Animator animator in audienceAnimators)
        {
            animator.SetBool("Clap", false);
        }

        mainMenuManager.OpenCompletion();
    }

    // -----------------------
    // Back while Presenting
    // -----------------------

    public void GoBackChoose()
    {
        presentationScreen.SetActive(false);

        backButton2.SetActive(false);
        endPresentation.SetActive(false);

        presentationMenu.SetActive(true);
    }
}