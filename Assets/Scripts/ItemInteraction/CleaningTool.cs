using DefaultNamespace;
using UnityEngine;

namespace ItemInteraction
{
    public class CleaningTool : MonoCache
    {
        [field: SerializeField] public CleanerType CleanerType { get; private set; }
        
    }
}