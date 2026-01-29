using System;
using UnityEngine;

namespace _Project.CodeBase.Core.Data.Cubes
{
    [Serializable]
    public class CubeModel
    {
        [SerializeField] private Sprite sprite;
        
        public Sprite Sprite => sprite;

        public CubeModel(Sprite sp)
        {
            sprite = sp;
        }
    }
}
