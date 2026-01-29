using _Project.CodeBase.Core.Entities.Cubes.States.Base;
using PrimeTween;
using UnityEngine;

namespace _Project.CodeBase.Core.Entities.Cubes.States
{
    public class MissState : CubeStateBase
    {
        public MissState(Cube owner) : base(owner)
        {
        }

        public override void OnEnter()
        {
            Tween.StopAll(owner.transform);
            Tween
                .Scale(owner.transform, tweenSettingsLib.missSettings)
                .OnComplete(() => Object.Destroy(owner.gameObject));
        }
    }
}
