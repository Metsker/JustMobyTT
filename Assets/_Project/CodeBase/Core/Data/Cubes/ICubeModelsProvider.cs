using System.Collections.Generic;

namespace _Project.CodeBase.Core.Data.Cubes
{
    public interface ICubeModelsProvider
    {
        int Count { get; }
        CubeModel AtIndex(int index);
        IEnumerable<CubeModel> Enumerate();
    }
}
