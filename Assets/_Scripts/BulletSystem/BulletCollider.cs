using Utils.ModularBehaviour;

namespace BulletSystem
{
    public abstract class BulletCollider<T> : BehaviourModule<Bullet<T>>, IBulletCollider
        where T : IMainBehaviour
    {
        public void Release()
        {
            MainBehaviour.Release();
        }
    }
}