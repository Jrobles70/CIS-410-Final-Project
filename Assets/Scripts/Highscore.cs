using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    private Transform container;
    private Transform template;
    private List<Transform> highschoreEntryTransformList;

    private void Awake()
    {
        container = transform.Find("HighscoreEntries");
        template = container.Find("HighscoreEntry");
        if (highschoreEntryTransformList == null)
            highschoreEntryTransformList = new List<Transform>();

        template.gameObject.SetActive(false);

    }

    public void AddNewEntry(string score)
    {
        float templateDistance = 95f;
        Transform entryTransform = Instantiate(template, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -120 + -templateDistance * highschoreEntryTransformList.Count);
        entryTransform.gameObject.SetActive(true);
        int rank = highschoreEntryTransformList.Count + 1;

        entryTransform.Find("Rank").GetComponent<UnityEngine.UI.Text>().text = rank + ")";
        entryTransform.Find("Score").GetComponent<UnityEngine.UI.Text>().text = score;

        highschoreEntryTransformList.Add(entryTransform);


    }
}
