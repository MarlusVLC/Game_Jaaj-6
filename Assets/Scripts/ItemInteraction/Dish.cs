using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ItemInteraction
{
    public class Dish : MonoCache
    {
        [field: SerializeField] public CleanerType AdequateCleaner { get; private set; }
        [SerializeField] private List<Stain> stains;

        private short _cleanLevel = 0;

        public short CleanLevel
        {
            get => _cleanLevel;
            set => _cleanLevel = value;
        }
        
        public event Action OnFullCleaning = delegate {  };


        private void Start()
        {
            stains = GetComponentsInChildren<Stain>().ToList();
            stains.ForEach(s => s.OnErase += EliminateStain);
        }

        private void EliminateStain(Stain stain)
        {
            if (stains.Contains(stain))
            {
                stains.Remove(stain);
            }

            if (stains.Count < 1)
            {
                _cleanLevel = 1;
                Debug.Log("this dish is 2 steps to be cleansed");
            }
        }

        public void EndCleaning()
        {
            OnFullCleaning();
        }
    }
}