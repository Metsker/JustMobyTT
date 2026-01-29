using System.Collections;
using _Project.CodeBase.Core.Entities.Cubes;
using _Project.CodeBase.Core.Entities.Cubes.States.Draggable;
using _Project.CodeBase.Core.Logger;
using _Project.CodeBase.Core.SaveLoad;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace _Project.CodeBase.Core
{
    public class CoreBehaviour : MonoBehaviour
    {
        private ICubeFactory _cubeFactory;
        private ScrollRect _previewsScrollRect;
        private ISaveLoadService _saveLoadService;
        private IActionLogger _actionLogger;

        [Inject]
        private void Construct(
            ICubeFactory cubeFactory,
            ScrollRect previewsScrollRect,
            ISaveLoadService saveLoadService,
            IActionLogger actionLogger)
        {
            _previewsScrollRect = previewsScrollRect;
            _cubeFactory = cubeFactory;
            _saveLoadService = saveLoadService;
            _actionLogger = actionLogger;
        }
        
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
        private void Awake()
        {
            Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;       
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
#endif
        private IEnumerator Start()
        {
            _saveLoadService.Load();
            
            CreatePreviews();
            
            yield return LocalizationSettings.InitializationOperation;

            _actionLogger.IsEnabled = true;
        }

        private void CreatePreviews()
        {
            foreach (Cube preview in _cubeFactory.CreateFromModels(_previewsScrollRect.content))
                preview.Fsm.SetState<PreviewState>();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                _saveLoadService.Save();
        }


#if UNITY_EDITOR
        private void OnApplicationQuit() =>
            _saveLoadService.Save();
#endif
    }
}
