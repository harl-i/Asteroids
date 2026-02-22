using System.IO;
using UnityEngine;
using Zenject;

public class ConfigService : IInitializable
{
    private PlayerConfig _playerConfig;
    private WorldConfig _worldConfig;

    public PlayerConfig PlayerConfig => _playerConfig;
    public WorldConfig WorldConfig => _worldConfig;

    public void Initialize()
    {
        Load();
    }

    private void Load()
    {
        _playerConfig = LoadJson<PlayerConfig>("player.json");
        _worldConfig = LoadJson<WorldConfig>("world.json");
    }

    private T LoadJson<T>(string fileName)
    {
        var path = Path.Combine(Application.streamingAssetsPath, "Configs", fileName);
        var json = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(json);
    }
}
