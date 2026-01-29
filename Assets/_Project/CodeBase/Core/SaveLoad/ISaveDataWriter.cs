using _Project.CodeBase.Core.Data.Save;

namespace _Project.CodeBase.Core.SaveLoad
{
    public interface ISaveDataWriter
    {
        void Write(SaveData saveData);
    }
}
