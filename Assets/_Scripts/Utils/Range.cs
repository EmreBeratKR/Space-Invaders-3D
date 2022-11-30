using System;

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
    }
}