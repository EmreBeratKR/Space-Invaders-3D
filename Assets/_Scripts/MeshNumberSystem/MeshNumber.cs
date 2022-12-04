using System;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using Utils.ModularBehaviour;

namespace MeshNumberSystem
{
    public class MeshNumber : MonoBehaviour, IMainBehaviour
    {
        public UnityAction<EventResponse> OnChanged;


        private MeshDigit[] MeshDigits
        {
            get
            {
                if (m_MeshDigits == null)
                {
                    m_MeshDigits = GetComponentsInChildren<MeshDigit>(true);
                }

                return m_MeshDigits;
            }
        }

        private int DigitCount => MeshDigits.Length;
        

        private MeshDigit[] m_MeshDigits;
        

        public void Set(int number)
        {
            var numberAsString = IntToString(number);

            var response = new EventResponse()
            {
                numberAsString = numberAsString
            };
            
            OnChanged?.Invoke(response);
        }


        private string IntToString(int number)
        {
            const string zero = "0";
            
            var stringBuilder = new StringBuilder();
            var numberAsString = number.ToString();
            var digitCount = DigitCount;
            var implicitZerosCount = digitCount - numberAsString.Length;

            for (int i = 0; i < implicitZerosCount; i++)
            {
                stringBuilder.Append(zero);
            }

            stringBuilder.Append(numberAsString);

            return stringBuilder.ToString();
        }
        
        
        [Serializable]
        public struct EventResponse
        {
            public string numberAsString;
        }
    }
}