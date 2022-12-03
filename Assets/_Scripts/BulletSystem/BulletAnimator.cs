using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace BulletSystem
{
    public class BulletAnimator : MonoBehaviour
    {
        [Header(Keyword.References)]
        [SerializeField] private GameObject baseMesh;
        [SerializeField] private GameObject blastMesh;
        
        
        [Header(Keyword.Values)]
        [SerializeField, Min(0f)] private float blastAnimationDuration;


        public void PlayIdle()
        {
            baseMesh.SetActive(true);
            blastMesh.SetActive(false);
        }
        
        
        public void PlayBlast(UnityAction onComplete)
        {
            StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                baseMesh.SetActive(false);
                blastMesh.SetActive(true);
                
                yield return new WaitForSecondsRealtime(blastAnimationDuration);

                onComplete?.Invoke();
            }
        }
    }
}