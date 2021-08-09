using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Mural.MeshBuilder
{
    public class PlaneMesh : ScriptableObject
    {
        public enum RenderType
        {
            Flat, Smooth, Line
        };

        [SerializeField, Range(2, 50)]
        int _widthSegments = 5;

        [SerializeField, Range(2, 50)]
        int _heightSegments = 5;

        [SerializeField, Range(0.1f, 10f)]
        float _width = 1.0f;

        [SerializeField, Range(0.1f, 10f)]
        float _height = 1.0f;

        [SerializeField]
        RenderType _renderType = RenderType.Flat;

        public RenderType renderType
        {
            get { return _renderType; }
        }

        [SerializeField]
        Mesh _mesh;

        public Mesh sharedMesh
        {
            get { return _mesh; }
        }

        public void RebuildMesh()
        {
            if (_mesh == null)
            {
                Debug.LogError("Plane Mesh is null.");
                return;
            }

            _mesh.Clear();

            var builder = new PlaneBuilder(_widthSegments, _heightSegments, _width, _height);

            var vc = builder.vertexCache;

            switch (renderType)
            {
                case RenderType.Flat:
                    _mesh.vertices = vc.MakeVertexArrayForFlatMesh();
                    _mesh.uv = vc.MakeUVArrayForFlatMesh();
                    _mesh.SetIndices(vc.MakeIndexArrayForFlatMesh(), MeshTopology.Triangles, 0);
                    _mesh.RecalculateNormals();
                    _mesh.RecalculateBounds();
                    break;
                case RenderType.Smooth:
                    _mesh.vertices = vc.MakeVertexArrayForSmoothMesh();
                    _mesh.uv = vc.MakeUVArrayForSmoothMesh();
                    _mesh.SetIndices(vc.MakeIndexArrayForSmoothMesh(), MeshTopology.Triangles, 0);
                    _mesh.RecalculateNormals();
                    _mesh.RecalculateBounds();
                    break;
                case RenderType.Line:
                    _mesh.vertices = vc.MakeVertexArrayForLine();
                    _mesh.uv = vc.MakeUVArrayForLine();
                    _mesh.SetIndices(vc.MakeIndexArrayForLine(), MeshTopology.Lines, 0);
                    _mesh.RecalculateBounds();
                    break;
            }
        }

        void OnEnable()
        {
            if (_mesh == null)
            {
                _mesh = new Mesh();
                _mesh.name = "Plane Mesh";
            }
        }
    }
}

