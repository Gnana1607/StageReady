using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]private Renderer presentationScreen;
    public void showSlide(Texture slide)
    {
        presentationScreen.material.mainTexture = slide;
    }
}
