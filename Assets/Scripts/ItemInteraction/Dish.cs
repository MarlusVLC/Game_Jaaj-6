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

        private bool isClean = false;
        
        

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

            isClean = stains.Count < 1;
            Debug.Log("this dish is now clean!");
        }
    }
}