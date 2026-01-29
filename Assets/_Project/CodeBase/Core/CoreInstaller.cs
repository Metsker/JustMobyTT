using System;
using _Project.CodeBase.Core.Data.Cubes;
using _Project.CodeBase.Core.Entities.Cubes;
using _Project.CodeBase.Core.Logger;
using _Project.CodeBase.Core.Utils;
using Reflex.Core;
using Reflex.Enums;
using Reflex.Injectors;
using UnityEngine;
using UnityEngine.UI;
using Resolution = Reflex.Enums.Resolution;

namespace _Project.CodeBase.Core
{
    public class CoreInstaller : MonoBehaviour, IInstaller
    {
        [Header("CubeFactory")]
        [SerializeField] private Cube cubePrefab;
        [SerializeField] private RectTransform rootLayout;
        [Header("ActionLogger")]
        [SerializeField] private string localizationTable;
        [SerializeField] private RectTransform logLayout;
        [SerializeField] private LocalizedLogMessage logMessagePrefab;
        [Header("Shared")]
        [SerializeField] private CubeModelsProviderSO cubeModelsProvider;
        [SerializeField] private ScrollRect previewsScrollRect;
        [SerializeField] private Canvas canvas;
        [SerializeField] private GraphicRaycaster raycaster;
        
        public void InstallBindings(ContainerBuilder builder) =>
            builder
                .RegisterValue(cubeModelsProvider, typeof(ICubeModelsProvider).ToArray())
                .RegisterValue(previewsScrollRect)
                .RegisterValue(canvas)
                .RegisterValue(raycaster)
                .RegisterFactory(LogFactory(), Lifetime.Singleton, Resolution.Lazy)
                .RegisterFactory(CubeFactory(), Lifetime.Singleton, Resolution.Lazy);

        private Func<Container, IActionLogger> LogFactory() =>
            container =>
            {
                LocalizedLogFactory localizedLogFactory = new (localizationTable, logMessagePrefab, logLayout);
                AttributeInjector.Inject(localizedLogFactory, container);
                return localizedLogFactory;
            };

        private Func<Container, ICubeFactory> CubeFactory() =>
            container =>
            {
                CubeFactory cubeFactory =  new (cubePrefab, rootLayout, container);
                AttributeInjector.Inject(cubeFactory, container);
                return cubeFactory;
            };
    }
}
