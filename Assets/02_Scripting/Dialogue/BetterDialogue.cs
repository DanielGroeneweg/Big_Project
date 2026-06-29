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
        return text.text.Split(
            new[] { "\r\n", "\n" },
            System.StringSplitOptions.None);
    }
}