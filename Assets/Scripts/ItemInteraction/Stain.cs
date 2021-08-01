using System;
using Code.ScriptableObjects;
using DefaultNamespace;
using UnityEngine;

namespace ItemInteraction
{
    public class Stain : MonoCache
    {

        [Range(0,1)][SerializeField] float erasingFactor = 0.1f;
        
        private Material _decalMAT;
        private Vector4 _alphaRemap;
        private CleanerType _adequateCleaner;
        private CleaningTool _interactingCleaningTool;
        
        public event Action<Stain> OnErase = delegate(Stain stain) {  };    

        private void Awake()
        {
            _decalMAT = GetComponent<MeshRenderer>().material;
            _alphaRemap = _decalMAT.GetVector("_AlphaRemap");
        }

        private void Start()
        {
            _adequateCleaner = GetComponentInParent<Dish>().AdequateCleaner;
        }

        private void OnCollisionEnter(Collision other)
        {
            _interactingCleaningTool = other.transform.GetComponentInChildren<CleaningTool>();
            if (_interactingCleaningTool)
            {
                if (_interactingCleaningTool.CleanerType == _adequateCleaner)
                {
                    ReduceAlpha();
                    if (_alphaRemap.y <= -0.9f)
                    {
                        OnErase(this);
                        Destroy(gameObject);
                    }
                }
            }
        }

        private void ReduceAlpha()
        {
            _alphaRemap.y -= erasingFactor;
            _decalMAT.SetVector("_AlphaRemap", _alphaRemap);
        }
    }
}