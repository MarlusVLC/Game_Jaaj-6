using System;
using Code.ScriptableObjects;
using DefaultNamespace;
using UnityEngine;

namespace ItemInteraction
{
    public class TEST_StainErase : MonoCache
    {

        [Range(0,1)][SerializeField] float erasingFactor = 0.1f;
        
        private Material _decalMAT;
        private Vector4 _alphaRemap;
        private CleanerType _adequateCleaner;
        private CleaningTool _interactingCleaningTool;

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
            if (other.transform.TryGetComponent(out _interactingCleaningTool))
            {
                if (_interactingCleaningTool.CleanerType == _adequateCleaner)
                {
                    ReduceAlpha();
                    if (_alphaRemap.y <= -0.9f)
                    {
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