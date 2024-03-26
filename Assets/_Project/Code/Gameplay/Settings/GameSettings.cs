using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    [Serializable]
    public partial class GameSettings
    {
        public ParticleSystem particle;

        public RocketSettings rocket;

        [FormerlySerializedAs("spawner")]
        public AsteroidsSpawnSettings asteroids;

        public UfoSpawnSettings ufo;
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
    public partial class AsteroidsSpawnSettings
    {
        public float delay = 5;

        [FormerlySerializedAs("startAsteroidsAmount")]
        public int startAmount = 5;

        [FormerlySerializedAs("maxAsteroids")]
        public int maxAmount = 10;

        [FormerlySerializedAs("asteroids")]
        public AsteroidInfo[] asteroidVariants;
    }

    [Serializable]
    public partial class UfoSpawnSettings
    {
        public float startDelay;
        public float delay;
        public int maxAmount;

        public UfoInfo info;
    }

    [Serializable]
    public partial struct UfoInfo
    {
        public int health;
        public float radius;
        public float speed;
        public int score;
    }

    [Serializable]
    public partial struct AsteroidInfo : IWeightable
    {
        public int weight;

        [Space]
        public int health;

        public float minSpeed;
        public float maxSpeed;

        public float radius;

        public int[] asteroidsAfterDestroy;

        public int score;

        int IWeightable.weight { get { return weight; } }
    }
}