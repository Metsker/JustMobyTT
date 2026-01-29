using System.Collections.Generic;
using System.IO;
using _Project.CodeBase.Core.Data.Save;
using Sirenix.Serialization;
using UnityEngine;

namespace _Project.CodeBase.Core.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private static readonly string SavePath = $"{Application.persistentDataPath}/save.dat";
        
        public SaveData SaveData { get; private set; } = new();
        
        private readonly List<ISaveDataWriter> _writers = new ();
        private readonly List<ISaveDataReader> _readers = new ();

        public void RegisterReader<T>(T reader) where T : ISaveDataReader =>
            _readers.Add(reader);

        public void RegisterWriter<T>(T writer) where T : ISaveDataWriter =>
            _writers.Add(writer);

        public void Save()
        {
            foreach (ISaveDataWriter writer in _writers)
                writer.Write(SaveData);
            
            byte[] bytes = SerializationUtility.SerializeValue(SaveData, DataFormat.Binary);
            File.WriteAllBytes(SavePath, bytes);
        }

        public void Load()
        {
            if (!File.Exists(SavePath))
                return;
            
            byte[] bytes = File.ReadAllBytes(SavePath);
            SaveData saveData = SerializationUtility.DeserializeValue<SaveData>(bytes, DataFormat.Binary);
            
            SaveData = saveData ?? new SaveData();
            
            foreach (var reader in _readers)
                reader.Read(SaveData);
        }

        public void Clear()
        {
            if (File.Exists(SavePath))
                File.Delete(SavePath);
        }
    }
}
