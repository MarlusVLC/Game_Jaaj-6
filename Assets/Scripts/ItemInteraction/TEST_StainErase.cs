using System;
using UnityEngine;

namespace ItemInteraction
{
    public class TEST_StainErase : MonoBehaviour
    {

        [Range(0,1)][SerializeField] float erasingFactor = 0.1f;
        
        private Material _decalMAT;
        private Vector4 _alphaRemap;

        private void Awake()
        {
            _decalMAT = GetComponent<MeshRenderer>().material;
            _alphaRemap = _decalMAT.GetVector("_AlphaRemap");
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("CleaningTool"))
            {
                ReduceAlpha();
                if (_alphaRemap.y <= -0.9f)
                {
                    Destroy(gameObject);
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