using System.Collections;
using UnityEngine;
using Utils;

public abstract class WritableMeshText : MonoBehaviour
{
    [Header(Keyword.Values)]
    [SerializeField] private float showDuration;


    private MeshLetter[] Letters
    {
        get
        {
            if (m_Letters == null)
            {
                m_Letters = GetComponentsInChildren<MeshLetter>(true);
            }

            return m_Letters;
        }
    }
    

    private MeshLetter[] m_Letters;
    
    
    public Coroutine Show()
    {
        return StartCoroutine(Routine());
        
        
        IEnumerator Routine()
        {
            int lenght = 1;
            int targetLength = Letters.Length;
            var intervalPerLetter = showDuration / targetLength;
            var interval = new WaitForSecondsRealtime(intervalPerLetter);

            while (lenght <= targetLength)
            {
                for (int i = 0; i < Letters.Length; i++)
                {
                    var shouldShow = i < lenght;
                    var letter = Letters[i];
                    
                    if (shouldShow) letter.Show();
                    
                    else letter.Hide();
                }

                yield return interval;
                lenght++;
            }
        }
    }

    public void Hide()
    {
        foreach (var letter in Letters)
        {
            letter.Hide();
        }
    }
}