using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
public class DialogueManager : MonoBehaviour
{
    [SerializeField] BetterDialogue dialogue;
    [SerializeField] TMP_Text textBox;
    [SerializeField] UnityEvent onCloseDialogue;
    [SerializeField] Button nextButton;
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] TMP_Text nameText;
    string[] dialogueLines = new string[0];
    int lineIndex = 0;
    bool canSkipLine = true;
    public void SetDialogue(BetterDialogue dialogue)
    {
        this.dialogue = dialogue;
        dialogueLines = dialogue.ReadLines();
        lineIndex = 0;
        NextLine();
    }
    public void EnableSpace() { canSkipLine = true; }
    public void DisableSpace() { canSkipLine = false; }
    public void NextLine()
    {
        if (lineIndex >= dialogueLines.Length) onCloseDialogue?.Invoke();

        else
        {
            DisplayLine(lineIndex);
            PlayVoiceLine(lineIndex);
        }

        lineIndex++;
    }
    public void OnNextLine()
    {
        if (canSkipLine) NextLine();
    }
    void DisplayLine(int index)
    {
        if (nameText != null) nameText.text = dialogue.NPCName;
        textBox.text = dialogueLines[index];
    }
    void PlayVoiceLine(int index)
    {
        if (index < dialogue.VoiceLines.Count)
        {
            AudioClip clip = dialogue.VoiceLines[index];
            audioPlayer.PlayOneShot(clip);
            if (canSkipLine) nextButton.gameObject.SetActive(false);
            if (canSkipLine) StartCoroutine(EnableButtonAfterDelay(clip.length));
        }
    }
    IEnumerator EnableButtonAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        nextButton.gameObject.SetActive(true);
    }
}