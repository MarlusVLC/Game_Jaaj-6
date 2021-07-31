using DefaultNamespace;
using UnityEngine;

namespace ItemInteraction
{
    public class Dish : MonoCache
    {
        [field: SerializeField] public CleanerType AdequateCleaner { get; private set; }
        
    }
}