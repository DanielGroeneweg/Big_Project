using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class RandomDialoguePlayer : MonoBehaviour
{
    [SerializeField] float timeInterval;
    [SerializeField][Range(0f,1f)] float dialogueChance;
    [SerializeField] List<BetterDialogue> dutchDialogues = new();
    [SerializeField] List<BetterDialogue> englishDialogues = new();
    [SerializeField] UnityEvent<BetterDialogue> onPlayDialogue;
    List<BetterDialogue> available = new();
    Languages language = Languages.English;
    public void ChangeLanguage(int lang)
    {
        language = (Languages)lang;

        List<BetterDialogue> listToCheck = language == Languages.English ? dutchDialogues : englishDialogues;
        List<int> ints = new List<int>();
        for (int i = 0; i < listToCheck.Count; i++)
        {
            if (available.Contains(listToCheck[i])) ints.Add(i);
        }

        available.Clear();
        List<BetterDialogue> listToPullFrom = language == Languages.English ? englishDialogues : dutchDialogues;
        foreach (int i in ints)
        {
            available.Add(listToPullFrom[i]);
        }
    }
    private void Start()
    {
        FillDialogueList(null);
    }
    public void Enable()
    {
        StartCoroutine(Dialogue());
    }
    public void Disable()
    {
        StopAllCoroutines();
    }
    IEnumerator Dialogue()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeInterval);
            if (Random.Range(0f, 1f) <= dialogueChance)
            {
                BetterDialogue dialogue = available[Random.Range(0, available.Count)];
                onPlayDialogue?.Invoke(dialogue);
                available.Remove(dialogue);
                if (available.Count == 0) FillDialogueList(dialogue);
            }
        }
    }
    void FillDialogueList(BetterDialogue lastDialogue)
    {
        List<BetterDialogue> dialogues = language == Languages.English ? englishDialogues : dutchDialogues;
        foreach (BetterDialogue dialogue in dialogues)
            if (lastDialogue != dialogue) available.Add(dialogue);
    }
}