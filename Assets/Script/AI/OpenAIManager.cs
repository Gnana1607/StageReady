using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class OpenAIManager : MonoBehaviour
{
    [Header("OpenAI")]
    [SerializeField] private string apiKey;

    [Header("Whisper Model")]
    [SerializeField] private string model = "gpt-4o-mini-transcribe";

    private const string endpoint =
        "https://api.openai.com/v1/audio/transcriptions";
}