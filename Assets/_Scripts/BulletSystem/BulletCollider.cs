using Utils;
using Utils.ModularBehaviour;

namespace BulletSystem
{
    public abstract class BulletCollider<T> : BehaviourModule<Bullet<T>>, IReleasable
        where T : IMainBehaviour
    {
        public void Release()
        {
            MainBehaviour.Release();
        }
    }
}