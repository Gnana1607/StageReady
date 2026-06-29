using System;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject aboutPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject completionPanel;
    [SerializeField] private GameObject roleSelectionPanel;
    public GameObject backToMain;

    private void Start()
    {
        ShowMainMenu();
    }

    private void HideAllPanels()
{
    mainPanel.SetActive(false);
    aboutPanel.SetActive(false);
    creditsPanel.SetActive(false);
    completionPanel.SetActive(false);

    if (roleSelectionPanel != null)
        roleSelectionPanel.SetActive(false);

    if (backToMain != null)
        backToMain.SetActive(false);
}

    public void ShowMainMenu()
    {
        HideAllPanels();
        backToMain.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void OpenAbout()
    {
        HideAllPanels();
        aboutPanel.SetActive(true);
    }

    public void OpenCredits()
    {
        HideAllPanels();
        creditsPanel.SetActive(true);
    }

    public void OpenCompletion()
    {
        HideAllPanels();
        completionPanel.SetActive(true);
    }

    public void OpenRoleSelection()
    {
        HideAllPanels();
        backToMain.SetActive(true);
        roleSelectionPanel.SetActive(true);
    }
    public void BackToMainMenu()
    {
        ShowMainMenu();
    }

    public void ExitApplication()
    {
        Debug.Log("Exiting Application");
        Application.Quit();
    }
}