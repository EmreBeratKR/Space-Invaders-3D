using Utils.ModularBehaviour;

namespace BulletSystem
{
    public abstract class BulletCollider<T> : BehaviourModule<Bullet<T>>, IBulletCollider
        where T : IMainBehaviour
    {
        public void Release()
        {
            if (!MainBehaviour) return;
            
            MainBehaviour.Release();
        }

        public void TriggerEnter()
        {
            Release();
        }
    }
}