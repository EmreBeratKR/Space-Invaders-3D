using UnityEngine;
using Utils;
using Utils.ModularBehaviour;

namespace MeshNumberSystem
{
    public class MeshDigit : BehaviourModule<MeshNumber>
    {
        [Header(Keyword.References)]
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private Mesh[] digitMeshes;


        private int Index => transform.GetSiblingIndex();
        

        private void Awake()
        {
            AddListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }


        private void OnMeshNumberChanged(MeshNumber.EventResponse response)
        {
            var digitAsString = response.numberAsString.Substring(Index, 1);
            int.TryParse(digitAsString, out var digitAsInt);
            UpdateMesh(digitAsInt);
        }


        private void UpdateMesh(int digit)
        {
            var mesh = DigitToMesh(digit);
            meshFilter.mesh = mesh;
        }

        private Mesh DigitToMesh(int digit)
        {
            return digitMeshes[digit];
        }
        
        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnChanged += OnMeshNumberChanged;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnChanged -= OnMeshNumberChanged;
            }
        }
    }
}