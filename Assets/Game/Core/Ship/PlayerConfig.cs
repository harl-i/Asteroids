[System.Serializable]
public class PlayerConfig
{
    public int maxHealth;
    public float acceleration;
    public float maxSpeed;
    public float bulletSpeed;
    public int laserCharges;
    public float laserCooldown;
    public float radius;
    public float mass;
    public float turnSpeedDeg;
    public float fireRate;
    public float laserRange;
    public bool useMobileInput;

    public int MaxHealth => maxHealth;
    public float Acceleration => acceleration;
    public float MaxSpeed => maxSpeed;
    public float BulletSpeed => bulletSpeed;
    public int LaserCharges => laserCharges;
    public float LaserCooldown => laserCooldown;
    public float Radius => radius;
    public float Mass => mass;
    public float TurnSpeedDeg => turnSpeedDeg;
    public float FireRate => fireRate;
    public float LaserRange => laserRange;
    public bool UseMobileInput => useMobileInput;
} 
