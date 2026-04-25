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

    public void Initialize()
    {
        Debug.Log($"Max HP: {_configService.PlayerConfig.maxHealth}");
    }
}
