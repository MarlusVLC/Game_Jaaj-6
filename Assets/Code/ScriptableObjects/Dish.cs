using UnityEngine;

namespace Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New CleaningTool", menuName = "CleaningTool", order = 0)]
    public class Dish : ScriptableObject
    {
        [SerializeField] private Utensil[] cleanerUtensils;
    }
}