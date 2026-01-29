using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.CodeBase.Core.Data.Cubes
{
    [CreateAssetMenu(fileName = "CubeModelsData", menuName = "Data/CubeModelsData")]
    public class CubeModelsProviderSO : ScriptableObject, ICubeModelsProvider
    {
        [SerializeField] private List<CubeModel> cubeModels;

        public int Count => cubeModels.Count;
        public CubeModel AtIndex(int index) => cubeModels[index];
        public IEnumerable<CubeModel> Enumerate() => cubeModels;

        [Button]
        private void CreateFromSprites(Sprite[] sprites)
        {
            cubeModels = new List<CubeModel>();
            cubeModels = sprites.Select(s => new CubeModel(s)).ToList();
        }
    }
}
