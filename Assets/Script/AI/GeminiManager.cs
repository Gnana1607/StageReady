using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class GeminiManager : MonoBehaviour
{
    [Header("Gemini API")]
    [SerializeField] private string apiKey;

    [Header("Gemini Model")]
    [SerializeField] private string model = "gemini-2.5-flash";

    [Header("Managers")]
    [SerializeField] private AnalysisUIManager analysisUIManager;

    [Header("Retry Settings")]
    [SerializeField] private int maxRetries = 3;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void AnalyzeAudio()
    {
        if (analysisUIManager != null)
        {
            analysisUIManager.StartAnalysis();
        }

        StartCoroutine(SendAudioToGemini());
    }

    private IEnumerator SendAudioToGemini()
    {
        string filePath = Path.Combine(
            Application.persistentDataPath,
            "speech.wav"
        );

        if (!File.Exists(filePath))
        {
            Debug.LogError("speech.wav was not found.");

            if (analysisUIManager != null)
            {
                analysisUIManager.ShowError(
                    "Recording could not be found."
                );
            }

            yield break;
        }

        byte[] audioData = File.ReadAllBytes(filePath);
        string base64Audio =
            System.Convert.ToBase64String(audioData);

        string prompt =
            "You are a helpful public speaking coach analyzing a student's speech. " +
            "Listen carefully to the audio and analyze it. " +
            "Return your answer in EXACTLY the following format:\n\n" +
            "TRANSCRIPT:\n" +
            "The complete spoken transcript\n\n" +
            "CONFIDENCE:\n" +
            "Give a score out of 10 and a very short assessment\n\n" +
            "PACE:\n" +
            "Slow, Good, or Fast with a short explanation\n\n" +
            "FILLER WORDS:\n" +
            "Count and mention the filler words used\n\n" +
            "SUGGESTIONS:\n" +
            "• Give up to three short, practical suggestions\n\n" +
            "Do not add any headings or text other than this format.";

        string json =
            "{"
            + "\"contents\":[{"
            + "\"parts\":["
            + "{\"text\":\"" + EscapeJson(prompt) + "\"},"
            + "{\"inlineData\":{"
            + "\"mimeType\":\"audio/wav\","
            + "\"data\":\"" + base64Audio + "\""
            + "}}"
            + "]"
            + "}]"
            + "}";

        string url =
            "https://generativelanguage.googleapis.com/v1beta/models/"
            + model
            + ":generateContent";

        for (int attempt = 1;
             attempt <= maxRetries;
             attempt++)
        {
            Debug.Log(
                "Sending audio to Gemini... Attempt "
                + attempt + " of " + maxRetries
            );

            UnityWebRequest request =
                new UnityWebRequest(url, "POST");

            request.uploadHandler =
                new UploadHandlerRaw(
                    Encoding.UTF8.GetBytes(json)
                );

            request.downloadHandler =
                new DownloadHandlerBuffer();

            request.SetRequestHeader(
                "Content-Type",
                "application/json"
            );

            request.SetRequestHeader(
                "x-goog-api-key",
                apiKey
            );

            yield return request.SendWebRequest();

            if (request.result ==
                UnityWebRequest.Result.Success)
            {
                GeminiResponse response =
                    JsonUtility.FromJson<GeminiResponse>(
                        request.downloadHandler.text
                    );

                if (response.candidates != null &&
                    response.candidates.Length > 0 &&
                    response.candidates[0].content != null &&
                    response.candidates[0].content.parts != null &&
                    response.candidates[0].content.parts.Length > 0)
                {
                    string analysis =
                        response.candidates[0]
                            .content.parts[0].text;

                    Debug.Log(
                        "========== SPEECH ANALYSIS =========="
                    );
                    Debug.Log(analysis);

                    if (analysisUIManager != null)
                    {
                        analysisUIManager.ShowResult(
                            analysis
                        );
                    }

                    request.Dispose();
                    yield break;
                }

                request.Dispose();

                if (analysisUIManager != null)
                {
                    analysisUIManager.ShowError(
                        "Analysis could not be read."
                    );
                }

                yield break;
            }

            long responseCode =
                request.responseCode;

            string errorResponse =
                request.downloadHandler.text;

            request.Dispose();

            Debug.LogWarning(
                "Gemini request failed. HTTP Code: "
                + responseCode
            );

            Debug.LogWarning(errorResponse);

            if (attempt < maxRetries)
            {
                int waitTime = attempt * 3;

                Debug.Log(
                    "Retrying in "
                    + waitTime + " seconds..."
                );

                yield return new WaitForSeconds(
                    waitTime
                );
            }
            else
            {
                Debug.LogError(
                    "Gemini could not process the request."
                );

                if (analysisUIManager != null)
                {
                    analysisUIManager.ShowError(
                        "Unable to analyze speech. Please try again."
                    );
                }
            }
        }
    }

    private string EscapeJson(string text)
    {
        return text
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"")
            .Replace("\n", "\\n")
            .Replace("\r", "");
    }
}