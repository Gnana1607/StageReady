using System;

[Serializable]
public class AnalysisResult
{
    public string transcript;
    public string confidence;
    public string speakingPace;
    public string fillerWords;
    public string suggestions;
}