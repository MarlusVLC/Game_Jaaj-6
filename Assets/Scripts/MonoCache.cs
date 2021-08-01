using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class MonoCache : MonoBehaviour
    {
        private Transform _transform;
        public Transform Transform => _transform;

        private void Awake()
        {
            CacheTransform();
        }

        private void OnValidate()
        {
            if (!_transform)
            {
                CacheTransform();
            }
        }

        private void CacheTransform()
        {
            TryGetComponent(out _transform);
        }
        
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}