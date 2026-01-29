using _Project.CodeBase.Core.Data.Tweens;
using Reflex.Core;

namespace _Project.CodeBase.Core.Entities.Cubes.States.Base
{
    public abstract class CubeStateBase : MonoStateBase<Cube>
    {
        protected readonly TweenSettingsLibrary tweenSettingsLib;

        protected CubeStateBase(Cube owner) : base(owner)
        {
            tweenSettingsLib = Container.RootContainer.Resolve<TweenSettingsLibrary>();
        }
    }
}
