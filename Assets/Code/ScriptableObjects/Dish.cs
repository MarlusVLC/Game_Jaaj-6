using UnityEngine;

namespace Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Utensil", menuName = "Utensil", order = 0)]
    public class Dish : ScriptableObject
    {
        [SerializeField] private Utensil[] cleanerUtensils;
    }
}