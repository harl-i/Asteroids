using Cysharp.Threading.Tasks;
using Game.Infrastructure.Services;
using UnityEngine;
using Zenject;

public class DebugInitializer : IInitializable
{
    private ConfigService _configService;

    public DebugInitializer(ConfigService configService)
    {
        _configService = configService;
    }

    public async void Initialize()
    {
        await _configService.LoadAsync();
        Debug.Log($"Max HP: {_configService.PlayerConfig.MaxHealth}");
    }
}
