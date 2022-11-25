using Utils;
using Utils.ModularBehaviour;

namespace BulletSystem
{
    public abstract class BulletReleaser<T> : Releaser<BulletCollider<T>>
        where T : IMainBehaviour
    {
        
    }
}