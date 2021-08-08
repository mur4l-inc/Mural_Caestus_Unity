using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mural.MeshBuilder;

namespace NoisySphere
{
    public class NoiseBallMesh : ScriptableObject
    {
        [SerializeField]
        int _subdivisionLevel = 1;

        public int subdivisionLevel
        {
            get { return _subdivisionLevel; }
        }

        [SerializeField]
        Mesh _mesh;

        public Mesh sharedMesh
        {
            get { return _mesh; }
        }

        public void RebuildMesh()
        {
            if(_mesh == null)
            {
                Debug.LogError("Mesh Asset is missing.");
                return;
            }

            _mesh.Clear();

            var builder = new IcosphereBuilder();
            for(var i = 0; i < _subdivisionLevel; i++)
            {
                builder.Subdivide();
            }

            var vcache = builder.vertexCache;
            var vcount = vcache.triangles.Count * 3;

            var varray1 = new List<Vector3>(vcount);
            var varray2 = new List<Vector3>(vcount);
            var varray3 = new List<Vector3>(vcount);

            foreach(var t in vcache.triangles)
            {
                var v1 = vcache.vertices[t.i1];
                var v2 = vcache.vertices[t.i2];
                var v3 = vcache.vertices[t.i3];

                varray1.Add(v1);
                varray2.Add(v2);
                varray3.Add(v3);

                varray1.Add(v2);
                varray2.Add(v3);
                varray3.Add(v1);

                varray1.Add(v3);
                varray2.Add(v1);
                varray3.Add(v2);
            }

            var iarray = new int[vcount * 2];

            for(var vi = 0; vi < vcount; vi += 3)
            {
                var i = vi * 2;

                iarray[i++] = vi;
                iarray[i++] = vi + 1;

                iarray[i++] = vi + 1;
                iarray[i++] = vi + 2;

                iarray[i++] = vi + 2;
                iarray[i++] = vi;
            }

            _mesh.SetVertices(varray1);
            _mesh.SetNormals(varray1);
            _mesh.SetUVs(0, varray2);
            _mesh.SetUVs(1, varray3);

            _mesh.subMeshCount = 2;
            _mesh.SetIndices(vcache.MakeIndexArrayForFlatMesh(), MeshTopology.Triangles, 0);
            _mesh.SetIndices(iarray, MeshTopology.Lines, 1);

            _mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 100);
            _mesh.Optimize();
            _mesh.UploadMeshData(true); //Expose Mesh Data to GPU
        }

        void OnEnable()
        {
            if(_mesh == null)
            {
                _mesh = new Mesh();
                _mesh.name = "NoisySphere";
            }    
        }
    }
}

