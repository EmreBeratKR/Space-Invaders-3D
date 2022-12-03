using BulletSystem;
using Utils.ModularBehaviour;

namespace ShieldSystem
{
    public class ShieldChunk : BehaviourModule<Shield>, IHittableByBullet
    {
        public void Hit()
        {
            Disable();
        }

        public void Restore()
        {
            Enable();
        }
        

        private void Enable()
        {
            gameObject.SetActive(true);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}