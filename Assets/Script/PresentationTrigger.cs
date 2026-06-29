using TMPro;
using UnityEngine;

public class PresentationTrigger : MonoBehaviour
{
    [SerializeField] private GameObject presentationMenu;
    [SerializeField] private GameObject presentationScreen;

    [SerializeField] private TMP_Text endPresentationText;

    [SerializeField] private AudioSource clappingSource;
    [SerializeField] private AudienceNoise audienceNoise;

    [SerializeField] private MainMenuManager mainMenuManager;

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
        presentationScreen.SetActive(false);
        presentationMenu.SetActive(false);

        backButton2.SetActive(true);

        endPresentation.SetActive(true);
        endPresentationText.text = "End Speech";
    }

    public void EndPresentation()
    {
        // Stop audience talking noise
        audienceNoise.StopAudienceNoise();

        // Play clap sound
        clappingSource.Play();

        // Make all students clap
        Animator[] audienceAnimators = GameObject.Find("Students").GetComponentsInChildren<Animator>();

        foreach (Animator animator in audienceAnimators)
        {
            animator.SetBool("Clap", true);
        }

        // Hide presentation UI
        presentationScreen.SetActive(false);
        presentationMenu.SetActive(false);
        endPresentation.SetActive(false);
        backButton2.SetActive(false);

        // Stop clapping after the audio finishes
        Invoke(nameof(StopClapping), clappingSource.clip.length);
    }

    private void StopClapping()
    {
        Animator[] audienceAnimators = GameObject.Find("Students").GetComponentsInChildren<Animator>();

        foreach (Animator animator in audienceAnimators)
        {
            animator.SetBool("Clap", false);
        }

        // Open completion panel after clapping
        mainMenuManager.OpenCompletion();
    }

    public void GoBackChoose()
    {
        presentationScreen.SetActive(false);
        presentationMenu.SetActive(true);
        backButton2.SetActive(false);
    }
}