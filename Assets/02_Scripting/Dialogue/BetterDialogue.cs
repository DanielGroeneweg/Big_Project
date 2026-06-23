using System.Collections.Generic;
using System.IO;
using UnityEngine;
[CreateAssetMenu(menuName = "Dialogue/BetterDialogueData")]
public class BetterDialogue : ScriptableObject
{
    [SerializeField] string npcName;
    [SerializeField] List<AudioClip> voiceLines = new();
    [SerializeField] TextAsset text;
    [SerializeField] Languages Language;
    public List<AudioClip> VoiceLines => voiceLines;
    public string NPCName => npcName;
    public string[] ReadLines()
    {
        string lang = Language == Languages.English ? "EN" : "NL";
        string path = Application.dataPath + $"/06_DialogueText/Text_{lang}/{text.name}.txt";
        string[] lines = File.ReadAllLines(path);
        return lines;
    }
}