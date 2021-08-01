using System;
using DefaultNamespace;
using UnityEngine;

namespace ItemInteraction
{
    public class DishManager : MonoCache
    {
        [SerializeField] private Dish[] dishes;

        private int _totalDishes;
        private int _currentDish = 0; 

        private void Start()
        {
            dishes = GetComponentsInChildren<Dish>(true);
            Array.ForEach(dishes, dish => dish.OnFullCleaning += UpdateDish);
            _totalDishes = dishes.Length;
            Debug.Log(_totalDishes);
            ObjectController.Instance.SetObject(dishes[_currentDish]);
        }

        private void UpdateDish()
        {
            Debug.Log(_currentDish);
            dishes[_currentDish].SetActive(false);
            _currentDish = (_currentDish + 1) % _totalDishes;
            Debug.Log(_currentDish);

            ObjectController.Instance.SetObject(dishes[_currentDish]);
            
        }
    }
}