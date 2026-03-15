using System.IO;
using UnityEngine;
using Zenject;

public class ConfigService : IInitializable
{
    private PlayerConfig _playerConfig;
    private WorldConfig _worldConfig;
    private EnemyConfig _enemyConfig;

    public PlayerConfig PlayerConfig => _playerConfig;
    public WorldConfig WorldConfig => _worldConfig;
    public EnemyConfig EnemyConfig => _enemyConfig;

    public void Initialize()
    {
        Load();
    }

    private void Load()
    {
        _playerConfig = LoadJson<PlayerConfig>("player.json");
        _worldConfig = LoadJson<WorldConfig>("world.json");
        _enemyConfig = LoadJson<EnemyConfig>("enemy.json");
    }

    private T LoadJson<T>(string fileName)
    {
        var path = Path.Combine(Application.streamingAssetsPath, "Configs", fileName);
        var json = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(json);
    }
}
