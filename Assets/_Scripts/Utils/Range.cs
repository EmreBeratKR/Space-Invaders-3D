using System;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public struct RangeInt
    {
        public int min, max;


        public int Random => UnityEngine.Random.Range(min, max + 1);
    }
    
    [Serializable]
    public struct RangeFloat
    {
        public float min, max;


        public float Random => UnityEngine.Random.Range(min, max);


        public float Lerp(float t)
        {
            return Mathf.Lerp(min, max, t);
        }

        public float LerpUnclamped(float t)
        {
            return Mathf.LerpUnclamped(min, max, t);
        }
    }
}