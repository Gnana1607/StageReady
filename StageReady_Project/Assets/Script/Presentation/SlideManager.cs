using UnityEngine;

public class SlideManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private ScreenManager screenManager;
    [SerializeField] private Texture[] slide;
    private int currentSlide=0;
    void Start()
    {
        screenManager.showSlide(slide[currentSlide]);
    }
    public void nextSlide()
    {
        if(currentSlide< slide.Length - 1)
        {
            currentSlide++;
            screenManager.showSlide(slide[currentSlide]);
        }
    }
    public void previousSlide()
    {
        if (currentSlide > 0)
        {
            currentSlide--;
            screenManager.showSlide(slide[currentSlide]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
