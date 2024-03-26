using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public interface IWeightable
    {
        public int weight { get; }
    }

    public static class RollByWeightUtility
    {
        public static bool TryRollByWeight<T>(IReadOnlyList<T> variants,
            out T element) where T : IWeightable
        {
            var index = 0;
            element = default;

            if (variants.Count >= 1)
            {
                var firstElement = variants[0];
                if (variants.Count == 1 && firstElement.weight > 0)
                {
                    index = 0;
                    element = firstElement;
                    return true;
                }

                var weightsSum = 0;

                foreach (var x in variants)
                {
                    var xWeight = x.weight;

                    weightsSum += xWeight;
                }

                if (weightsSum <= 0)
                {
                    return false;
                }

                var roll = Random.Range(0, weightsSum);

                index = GetElementIndexByRoll(variants, roll, index);
                element = variants[index];
                return true;
            }

            return false;
        }

        private static int GetElementIndexByRoll<T>(IReadOnlyList<T> variants, int roll, int index)
            where T : IWeightable
        {
            var targetWeightByElement = 0;
            foreach (var y in variants)
            {
                targetWeightByElement += y.weight;

                if (roll < targetWeightByElement)
                {
                    break;
                }

                index++;
            }

            return index;
        }
    }
}