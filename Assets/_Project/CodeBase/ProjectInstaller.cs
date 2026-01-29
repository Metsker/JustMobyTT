using _Project.CodeBase.Core.Data.Tweens;
using _Project.CodeBase.Core.SaveLoad;
using _Project.CodeBase.Core.Utils;
using Reflex.Core;
using Reflex.Enums;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

namespace _Project.CodeBase
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private TweenSettingsLibrary tweenSettingsLib;
        
        public void InstallBindings(ContainerBuilder builder) =>
            builder
                .RegisterValue(tweenSettingsLib)
                .RegisterType(typeof(SaveLoadService), typeof(ISaveLoadService).ToArray(), Lifetime.Singleton, Resolution.Lazy);
    }
}
