using TMPro;
using UnityEngine;

public class AnalysisUIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject analysisPanel;
    [SerializeField] private TMP_Text loadingText;
    [SerializeField] private GameObject resultPanel;

    [Header("Analysis Result")]
    [SerializeField] private TMP_Text analysisText;

    public void StartAnalysis()
    {
        analysisPanel.SetActive(true);

        loadingText.gameObject.SetActive(true);
        loadingText.text = "Analyzing your speech...";

        resultPanel.SetActive(false);
    }

    public void ShowResult(string result)
    {
        // Get only the feedback sections
        string confidence = GetSection(
            result,
            "CONFIDENCE:",
            "PACE:"
        );

        string pace = GetSection(
            result,
            "PACE:",
            "FILLER WORDS:"
        );

        string fillerWords = GetSection(
            result,
            "FILLER WORDS:",
            "SUGGESTIONS:"
        );

        string suggestions = GetSection(
            result,
            "SUGGESTIONS:",
            null
        );

        loadingText.gameObject.SetActive(false);
        resultPanel.SetActive(true);

        // Display ONLY feedback. Transcript is intentionally excluded.
        analysisText.text =
            "<b>Confidence</b>\n" + confidence +
            "\n\n<b>Speaking Pace</b>\n" + pace +
            "\n\n<b>Filler Words</b>\n" + fillerWords +
            "\n\n<b>Suggestions</b>\n" + suggestions;
    }

    public void ShowError(string message)
    {
        resultPanel.SetActive(false);
        loadingText.gameObject.SetActive(true);
        loadingText.text = message;
    }

    private string GetSection(
        string text,
        string startMarker,
        string endMarker)
    {
        int startIndex = text.IndexOf(startMarker);

        if (startIndex == -1)
            return "Not available";

        startIndex += startMarker.Length;

        int endIndex;

        if (endMarker != null)
        {
            endIndex = text.IndexOf(endMarker, startIndex);

            if (endIndex == -1)
                endIndex = text.Length;
        }
        else
        {
            endIndex = text.Length;
        }

        return text
            .Substring(startIndex, endIndex - startIndex)
            .Trim();
    }

    public void CloseAnalysis()
    {
        analysisPanel.SetActive(false);
    }
}