using System.Collections;
using UnityEngine;
using Utils;

public class GameOverTitle : MonoBehaviour
{
    [Header(Keyword.References)]
    [SerializeField] private GameObject[] letters;
    
    [Header(Keyword.Values)]
    [SerializeField] private float showDuration;


    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }
    

    private void OnGameOver(Game.EventResponse response)
    {
        Show();
    }
    

    private void Show()
    {
        StartCoroutine(Routine());
        
        
        IEnumerator Routine()
        {
            int lenght = 1;
            int targetLength = letters.Length;
            var intervalPerLetter = showDuration / targetLength;
            var interval = new WaitForSecondsRealtime(intervalPerLetter);

            while (lenght <= targetLength)
            {
                for (int i = 0; i < letters.Length; i++)
                {
                    var shouldActive = i < lenght;
                    letters[i].SetActive(shouldActive);
                }

                lenght++;
                yield return interval;
            }
        }
    }


    private void AddListeners()
    {
        Game.OnGameOver += OnGameOver;
    }

    private void RemoveListeners()
    {
        Game.OnGameOver -= OnGameOver;
    }
}