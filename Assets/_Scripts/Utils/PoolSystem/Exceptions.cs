using UnityEngine;

namespace Utils.PoolSystem
{
    public class InvalidPoolCapacityException : UnityException
    {
        public InvalidPoolCapacityException()
            : base("Invalid Pool Capacity!")
        {
            HResult = -2147467261;
        }
    }
}