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
            SetStains();
        }

        private void EliminateStain(Stain stain)
        {
            if (stains.Contains(stain))
            {
                stain.OnErase -= EliminateStain;
                stains.Remove(stain);
            }

            if (stains.Count < 1)
            {
                _cleanLevel = 1;
            }
        }

        private void SetStains()
        {
            stains = GetComponentsInChildren<Stain>().ToList();
            stains.ForEach(s => s.OnErase += EliminateStain);
        }

        public void EndCleaning()
        {
            OnFullCleaning();
        }

        public void ResetDirtness()
        {
            stains = GetComponentsInChildren<Stain>().ToList();
            foreach (var stain in stains)
            {
                stain.OnErase += EliminateStain;
                stain.ResetStain();
            }
            _cleanLevel = 0;
        }
    }
}