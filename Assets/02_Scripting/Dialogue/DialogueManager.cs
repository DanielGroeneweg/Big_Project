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
    string[] dialogueLines;
    int lineIndex = -1;
    private void Start()
    {
        dialogueLines = dialogue.ReadLines();
    }
    public void NextLine()
    {
        lineIndex++;

        if (lineIndex >= dialogueLines.Length) onCloseDialogue?.Invoke();

        else
        {
            DisplayLine(lineIndex);
            PlayVoiceLine(lineIndex);
        }
    }
    void DisplayLine(int index)
    {
        nameText.text = dialogue.NPCName;
        textBox.text = dialogueLines[index];
    }
    void PlayVoiceLine(int index)
    {
        if (index < dialogue.VoiceLines.Count)
        {
            AudioClip clip = dialogue.VoiceLines[index];
            GetComponent<AudioSource>().PlayOneShot(clip);
            nextButton.gameObject.SetActive(false);
            StartCoroutine(EnableButtonAfterDelay(clip.length));
        }
    }
    IEnumerator EnableButtonAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        nextButton.gameObject.SetActive(true);
    }
}