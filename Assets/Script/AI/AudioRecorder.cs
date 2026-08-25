using UnityEngine;

public class AudioRecorder : MonoBehaviour
{
    private AudioClip recordedClip;
    private string microphoneName;

    [Header("Gemini Manager")]
    [SerializeField] private GeminiManager geminiManager;

    private void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            microphoneName = Microphone.devices[0];
            Debug.Log("Microphone Found: " + microphoneName);
        }
        else
        {
            Debug.LogError("No microphone detected.");
        }
    }

    public void StartRecording()
    {
        if (microphoneName == null)
            return;

        recordedClip = Microphone.Start(
            microphoneName,
            false,
            300,
            44100
        );

        Debug.Log("Recording Started");
    }

    public void StopRecording()
    {
        if (microphoneName == null)
            return;

        // Stop the microphone recording
        Microphone.End(microphoneName);

        // Save the recording as speech.wav
        string filePath =
            Application.persistentDataPath + "/speech.wav";

        SavWav.Save(filePath, recordedClip);

        Debug.Log("Recording Saved At: " + filePath);
        Debug.Log("Recording Stopped");

        // Send the saved audio to Gemini
        if (geminiManager != null)
        {
            Debug.Log("Sending audio to Gemini...");
            geminiManager.AnalyzeAudio();
        }
        else
        {
            Debug.LogError("GeminiManager reference is missing!");
        }
    }
}