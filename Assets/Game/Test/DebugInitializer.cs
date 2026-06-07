using Cysharp.Threading.Tasks;
using Game.Infrastructure.Services;
using UnityEngine;
using Zenject;

public class DebugInitializer : IInitializable
{
    private ConfigRepository _configRepository;

    public DebugInitializer(ConfigRepository configRepository)
    {
        _configRepository = configRepository;
    }

    public async void Initialize()
    {
        await _configRepository.LoadAsync();
        Debug.Log($"Max HP: {_configRepository.PlayerConfig.MaxHealth}");
    }
}
