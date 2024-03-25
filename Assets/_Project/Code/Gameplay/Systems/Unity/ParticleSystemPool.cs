using UnityEngine;
using Utility.Pooling;

namespace Gameplay
{
    public class ParticleSystemPool : ObjectPool<ParticleSystem>
    {
        public ParticleSystemPool(ParticleSystem prefab, int capacity) : base(new Policy(prefab), true, capacity: capacity)
        {
        }

        public class Policy : DefaultObjectPoolPolicy<ParticleSystem>
        {
            private const string NAME = "[ " + nameof(ParticleSystemPool) + " ] {0}";

            private int _count = -1;

            private ParticleSystem _prefab;

            public Policy(ParticleSystem prefab)
            {
                _prefab = prefab;
            }

            public override ParticleSystem Create()
            {
                _count++;

                var particleSystem = Object.Instantiate(_prefab);
                particleSystem.name = string.Format(NAME, _count);
                return particleSystem.GetComponent<ParticleSystem>();
            }

            public override void OnGet(ParticleSystem obj)
            {
                obj.gameObject.SetActive(true);
            }

            public override void OnReturn(ParticleSystem obj)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }
}