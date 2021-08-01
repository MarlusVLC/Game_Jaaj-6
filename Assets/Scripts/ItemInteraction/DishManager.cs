using System;
using DefaultNamespace;
using UnityEngine;

namespace ItemInteraction
{
    public class DishManager : MonoCache
    {
        [SerializeField] private Dish[] dishes;

        private int _totalDishes;
        private int _currentDishIndex = 0; 

        private void Start()
        {
            dishes = GetComponentsInChildren<Dish>(true);
            Array.ForEach(dishes, dish => dish.OnFullCleaning += UpdateDish);
            _totalDishes = dishes.Length;
            ObjectController.Instance.SetObject(dishes[_currentDishIndex]);
        }

        private void UpdateDish()
        {
            dishes[_currentDishIndex].SetActive(false);
            _currentDishIndex = (_currentDishIndex + 1) % _totalDishes;
            ObjectController.Instance.SetObject(dishes[_currentDishIndex]);
            
        }
    }
}