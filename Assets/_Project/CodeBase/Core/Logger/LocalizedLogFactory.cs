using _Project.CodeBase.Core.Data.Tweens;
using PrimeTween;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.Core.Logger
{
    public class LocalizedLogFactory : IActionLogger
    {
        [Inject] private TweenSettingsLibrary tweenSettingsLibrary;
        
        public bool IsEnabled { get; set; }
        
        private readonly string _table;
        private readonly LocalizedLogMessage _localizedLogMessagePrefab;
        private readonly RectTransform _layout;

        public LocalizedLogFactory(string table, LocalizedLogMessage localizedLogMessagePrefab, RectTransform layout)
        {
            _table = table;
            _localizedLogMessagePrefab = localizedLogMessagePrefab;
            _layout = layout;
        }

        public void Log(string key)
        {
            if (!IsEnabled)
                return;
            
            // TODO: object pooling
            LocalizedLogMessage localizedLogMessage = Object.Instantiate(_localizedLogMessagePrefab, _layout);
            
            localizedLogMessage.SetText(_table, key);

            LayoutRebuilder.ForceRebuildLayoutImmediate(_layout);
            
            Sequence.Create()
                .Chain(Tween.Scale(localizedLogMessage.transform, tweenSettingsLibrary.logInSettings))
                .ChainDelay(tweenSettingsLibrary.logLifetime)
                .Chain(Tween.Scale(localizedLogMessage.transform, tweenSettingsLibrary.logOutSettings))
                .OnComplete(() => Object.Destroy(localizedLogMessage.gameObject));
        }
    }
}
