using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FaceChoiceDisplay : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject buttonPrefab;
    private List<GameObject> buttonPrefabs;

    public void Populate(List<string> allowedFaces)
    {
        if(buttonPrefabs == null)
            buttonPrefabs = new List<GameObject>();

        buttonPrefab.SetActive(false);
        foreach (var button in buttonPrefabs)
        {
            if(button != null)
                Destroy(button.gameObject);
        }

        buttonPrefabs.Clear();
        for (int i = 0; i < allowedFaces.Count; i++)
        {
            var button = Instantiate(buttonPrefab, transform);
            button.SetActive(true);
            button.GetComponentInChildren<TMP_Text>().text = allowedFaces[i];
            var face = allowedFaces[i];

            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                gameManager.ChooseFace(face);
            });

            buttonPrefabs.Add(button);
        }
    }
}
