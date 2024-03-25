using System.Collections.Generic;
using Infrastructure.ECS;
using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay
{
    public class ParticleSpawnSystem : BaseSystem
    {
        private ParticleSystemPool _pool;

        private Mask _mask;

        private List<ParticleSystem> _particles = new(8);

        public ParticleSpawnSystem(ParticleSystemPool pool)
        {
            _pool = pool;
        }

        protected override void OnInitialize()
        {
            Mask<ParticleEvent>().Build(out _mask);
        }

        protected override void OnLateUpdate()
        {
            TryRelease();

            foreach (var entity in _mask)
            {
                ref var data = ref entity.GetComponent<ParticleEvent>();

                var particle = _pool.Get();
                _particles.Add(particle);

                particle.gameObject.transform.position = data.position;
            }
        }

        private void TryRelease()
        {
            using (ListPool<ParticleSystem>.Get(out var list))
            {
                foreach (var particle in _particles)
                {
                    if (particle.isPlaying) continue;

                    list.Add(particle);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    var particleSystem = list[i];
                    _pool.Release(particleSystem);
                    _particles.Remove(particleSystem);
                }
            }
        }
    }
}