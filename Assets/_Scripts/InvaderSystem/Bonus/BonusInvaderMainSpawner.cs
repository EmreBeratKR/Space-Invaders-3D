using UnityEngine;
using Utils;

namespace InvaderSystem
{
    public class BonusInvaderMainSpawner : MonoBehaviour
    {
        [Header(Keyword.References)]
        [SerializeField] private BonusInvaderSpawner ufoBonusInvaderSpawner;


        public UfoBonusInvader SpawnUfo()
        {
            return (UfoBonusInvader) ufoBonusInvaderSpawner.Spawn();
        }
    }
}