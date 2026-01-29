using PrimeTween;
using UnityEngine;

namespace _Project.CodeBase.Core.Data.Tweens
{
    [CreateAssetMenu(fileName = "TweenSettingsLibrary", menuName = "Data/TweenSettingsLibrary")]
    public class TweenSettingsLibrary : ScriptableObject
    {
        [Header("Cube")]
        public float fallSpeed;
        public Ease fallEase = Ease.Default;
        public TweenSettings<Vector3> missSettings;
        public TweenSettings<Vector3> holeSettings;
        public TweenSettings<Vector3> scaleInSettings;
        public TweenSettings<Vector3> scaleOutSettings;
        [Header("Logger")]
        public TweenSettings<Vector3> logInSettings;
        public TweenSettings<Vector3> logOutSettings;
        [Min(0)] public float logLifetime = 1;
    }
}
