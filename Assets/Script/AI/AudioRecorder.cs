using UnityEngine;

public class AudioRecorder : MonoBehaviour
{
    private AudioClip recordedClip;
    private string microphoneName;

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

        recordedClip = Microphone.Start(microphoneName, false, 300, 44100);

        Debug.Log("Recording Started");
    }

    public void StopRecording()
    {
        if (microphoneName == null)
            return;

        Microphone.End(microphoneName);

        string filePath = Application.persistentDataPath + "/speech.wav";

        SavWav.Save(filePath, recordedClip);

        Debug.Log("Recording Saved At : " + filePath);
        Debug.Log("Recording Stopped");
    }
}