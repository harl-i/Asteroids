[System.Serializable]
public class EnemyConfig
{
    public float asteroidSpawnInterval;
    public float asteroidMinSpeed;
    public float asteroidMaxSpeed;
    public float asteroidFragmentSpeed;
    public float spawnPadding;
    public float ufoSpawnInterval;
    public float ufoSpawnSpeed;

    public float AsteroidSpawnInterval => asteroidSpawnInterval;
    public float AsteroidMinSpeed => asteroidMinSpeed;
    public float AsteroidMaxSpeed => asteroidMaxSpeed;
    public float AsteroidFragmentSpeed => asteroidFragmentSpeed;
    public float SpawnPadding => spawnPadding;
    public float UfoSpawnInterval => ufoSpawnInterval;
    public float UfoSpawnSpeed => ufoSpawnSpeed;
}
