using UnityEngine;
public class CutSceneLanguageSetter : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] BetterDialogue[] dutchDialogues;
    [SerializeField] BetterDialogue[] englishDialogues;
    Languages language = Languages.English;
    public void SetDutch()
    {
        language = Languages.Dutch;
    }
    public void SetEnglish()
    {
        language = Languages.English;
    }
    public void PlayDialogue(int index)
    {
        if (index < 0) return;

        switch (language)
        {
            case Languages.English:
                if (index >= englishDialogues.Length) return;
                dialogueManager.SetDialogue(englishDialogues[index]);
                break;

            case Languages.Dutch:
                if (index >= dutchDialogues.Length) return;
                dialogueManager.SetDialogue(dutchDialogues[index]);
                break;
        }
    }
}