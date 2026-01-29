using _Project.CodeBase.Core.Entities.Cubes.States.Base;
using PrimeTween;
using UnityEngine;

namespace _Project.CodeBase.Core.Entities.Cubes.States
{
    public class HoleState : CubeStateBase
    {
        public HoleState(Cube owner) : base(owner)
        {
        }
        
        public override void OnEnter()
        {
            Tween.StopAll(owner.transform);

            TweenSettings<Vector3> missSettings = tweenSettingsLib.missSettings;
            missSettings.settings.duration = tweenSettingsLib.holeSettings.settings.duration;
            
            Sequence.Create()
                .Group(Tween.Scale(owner.transform, missSettings))
                .Group(Tween.Rotation(owner.transform, tweenSettingsLib.holeSettings))
                .OnComplete(() => Object.Destroy(owner.gameObject));
        }
    }
}
