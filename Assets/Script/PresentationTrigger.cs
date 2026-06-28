using UnityEngine;

public class PresentationTrigger : MonoBehaviour
{
    [SerializeField] private GameObject presentationMenu;
    [SerializeField]private GameObject presentationScreen;
public GameObject backButton1;
public GameObject backButton2;

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
    }
    public void freeSpeech()
    {
        presentationMenu.SetActive(false);
        backButton2.SetActive(true);
    }
    public void GoBackChoose()
    {
        presentationMenu.SetActive(true);
        backButton2.SetActive(false);
    }
}
