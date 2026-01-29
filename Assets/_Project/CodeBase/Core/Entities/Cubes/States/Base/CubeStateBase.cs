using _Project.CodeBase.Core.Data.Tweens;
using _Project.CodeBase.Core.Logger;
using Reflex.Attributes;

namespace _Project.CodeBase.Core.Entities.Cubes.States.Base
{
    public abstract class CubeStateBase : MonoStateBase<Cube>
    {
        [Inject] protected TweenSettingsLibrary tweenSettingsLib;
        [Inject] protected IActionLogger actionLogger;

        protected CubeStateBase(Cube owner) : base(owner)
        {
        }
    }
}
