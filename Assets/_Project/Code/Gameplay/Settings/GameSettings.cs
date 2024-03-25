using System;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public partial class GameSettings
    {
        public ParticleSystem particle;

        public RocketSettings rocket;
        public SpawnerSettings spawner;
    }

    [Serializable]
    public partial class RocketSettings
    {
        public float enginePower = 500;
        public float rotateSpeed = 2;

        public WeaponSettings weapon;
    }

    [Serializable]
    public class WeaponSettings
    {
        public PistolSettings pistol;
        public LaserSettings laser;
    }

    [Serializable]
    public partial class PistolSettings
    {
        public float cooldown;
        public float speed;
        public float offset;
        public float radius;
    }

    [Serializable]
    public partial class LaserSettings
    {
        public int charges;
        public float cooldown;
    }

    [Serializable]
    public partial class SpawnerSettings
    {
        public float delay = 5;
        public int startAsteroidsAmount = 5;
        public int maxAsteroids = 10;

        public AsteroidInfo[] asteroids;
    }

    [Serializable]
    public partial struct AsteroidInfo
    {
        public int health;
        
        public float minSpeed;
        public float maxSpeed;

        public float radius;

        public int[] asteroidsAfterDestroy;
    }
}