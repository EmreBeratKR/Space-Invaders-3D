using System.Collections;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace InvaderSystem
{
    public class InvaderCommander : MonoBehaviour
    {
        [Header(Keyword.Values)]
        [SerializeField] private float rateOfFirePerSecond;


        private Invader[] Invaders => GetComponentsInChildren<Invader>();

        private Invader RandomInvader
        {
            get
            {
                var invaders = Invaders;
                var randomIndex = Random.Range(0, invaders.Length);
                return invaders.Length == 0 ? null : invaders[randomIndex];
            }
        }


        private void Start()
        {
            CommandShoot();
        }


        private void CommandShoot()
        {
            StartCoroutine(Routine());
            
            IEnumerator Routine()
            {
                var interval = new WaitForSeconds(1f / rateOfFirePerSecond);

                while (true)
                {
                    yield return interval;

                    var randomInvader = RandomInvader;
                    
                    if (!randomInvader) yield break;
                    
                    while (true)
                    {
                        var lowerNeighbour = randomInvader.LowerNeighbour;
                        
                        if (!lowerNeighbour) break;

                        randomInvader = lowerNeighbour;
                    }

                    var response = new Invader.EventResponse()
                    {

                    };

                    randomInvader.OnShootCommand?.Invoke(response);
                }
            }
        }
    }
}