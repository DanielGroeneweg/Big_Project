using System.Collections.Generic;
using System.IO;
using UnityEngine;
[CreateAssetMenu(menuName = "Dialogue/BetterDialogueData")]
public class BetterDialogue : ScriptableObject
{
    [SerializeField] List<AudioClip> voiceLines = new();
    [SerializeField] TextAsset text;
    public List<AudioClip> VoiceLines => voiceLines;
    public string[] ReadLines()
    {
        string path = Application.dataPath + "/06_DialogueText/" + text.name + ".txt";
        string[] lines = File.ReadAllLines(path);
        return lines;
    }
}