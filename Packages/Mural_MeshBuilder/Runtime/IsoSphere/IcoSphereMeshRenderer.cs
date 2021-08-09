using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Mural.Caestus
{
    [ExecuteInEditMode]
    public class IcoSphereMeshRenderer : MonoBehaviour
    {
        [SerializeField]
        IcosphereMesh _mesh;

        [Space]
        [SerializeField]
        ShadowCastingMode _castShadows;

        [SerializeField]
        bool _receiveShadows;

        [SerializeField]
        Material _surfaceMaterial;

        MaterialPropertyBlock _materialProperties;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Graphics.DrawMesh(
                _mesh.sharedMesh, transform.localToWorldMatrix,
                _surfaceMaterial, 0, null, 0, _materialProperties,
                _castShadows, _receiveShadows, transform
            );
        }
    }
}
