using BulletSystem;
using InvaderSystem;
using Utils.ModularBehaviour;

namespace ShieldSystem
{
    public class ShieldChunk : BehaviourModule<Shield>, ITriggerEnterByBullet, ITriggerEnterByInvader
    {
        public void TriggerEnter()
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