using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class RandomDialoguePlayer : MonoBehaviour
{
    [SerializeField] float timeInterval;
    [SerializeField][Range(0f,1f)] float dialogueChance;
    [SerializeField] List<BetterDialogue> dialogues = new();
    [SerializeField] UnityEvent<BetterDialogue> onPlayDialogue;
    List<BetterDialogue> available = new();
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
        foreach (BetterDialogue dialogue in dialogues)
            if (lastDialogue != dialogue) available.Add(dialogue);
    }
}