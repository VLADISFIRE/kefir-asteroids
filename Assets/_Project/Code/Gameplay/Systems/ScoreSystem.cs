using System;
using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class ScoreSystem : BaseSystem
    {
        private Mask _mask;
        private Mask _rockets;

        private int _score;

        public int score { get { return _score; } }

        public event Action<int> scoreChanged;

        protected override void OnInitialize()
        {
            Mask<DestroyEvent, ScoreComponent>().Build(out _mask);
        }

        protected override void OnPlayed()
        {
            UpdateScore(0);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var destroy = ref entity.GetComponent<DestroyEvent>();
                
                if (destroy.auto) continue;

                ref var score = ref entity.GetComponent<ScoreComponent>();
                AddScore(score.value);
            }
        }

        private void AddScore(int score)
        {
            UpdateScore(_score + score);
        }

        private void UpdateScore(int value)
        {
            _score = value;
            scoreChanged?.Invoke(_score);
        }
    }
}