using UnityEngine;
using Utils;
using Utils.ModularBehaviour;

namespace InvaderSystem
{
    public class InvaderAnimator : BehaviourModule<Invader>
    {
        [Header(Keyword.References)]
        [SerializeField] private Animator animator;
    }
}