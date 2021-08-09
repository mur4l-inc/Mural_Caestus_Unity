using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mural.Caestus
{
    [System.Serializable]
    public class VertexCache
    {
        #region Triangle Struct
        public struct RawTriangles
        {
            public Vector3 v1, v2, v3;

            public RawTriangles(Vector3 v1, Vector3 v2, Vector3 v3)
            {
                this.v1 = v1;
                this.v2 = v2;
                this.v3 = v3;
            }
        }
        public struct IndexedTriangles
        {
            public int i1, i2, i3;

            public IndexedTriangles(int i1, int i2, int i3)
            {
                this.i1 = i1;
                this.i2 = i2;
                this.i3 = i3;
            }
        }
        #endregion

        #region Public Member Viriables
        public List<Vector3> vertices;
        public List<Vector2> uvs;
        public List<IndexedTriangles> triangles;
        #endregion

        #region Constructor
        public VertexCache()
        {
            vertices = new List<Vector3>();
            uvs = new List<Vector2>();
            triangles = new List<IndexedTriangles>();
        }

        public VertexCache(VertexCache source)
        {
            vertices = source.vertices;
            uvs = source.uvs;
            triangles = source.triangles;
        }
        #endregion

        #region VertexAndUV Related Subroutines
        public int AddVertex(Vector3 v)
        {
            int i = vertices.Count;
            vertices.Add(v);

            return i;
        }
        public int AddVertexAndUV(Vector3 v, Vector2 uv)
        {
            int i = vertices.Count;
            vertices.Add(v);
            uvs.Add(uv);

            return i;
        }

        public int LookUpVertex(Vector3 v)
        {
            for(int i = 0; i < vertices.Count; i++)
            {
                if((vertices[i] - v).sqrMagnitude < 0.0001f)
                {
                    return i;
                }
            }
            return -1;
        }

        public int LookUpAndAddVertexAndUV(Vector3 v, Vector2 uv)
        {
            int i = LookUpVertex(v);
            if(i >= 0)
            {
                return i;
            }
            AddVertexAndUV(v, uv);

            return vertices.Count - 1;
        }
        #endregion

        #region Indexed Triangle Subroutines
        public void AddTriangle(int i1, int i2, int i3)
        {
            triangles.Add(new IndexedTriangles(i1, i2, i3));
        }

        public void LookUpAndAddTriangle(Vector3 v1, Vector3 v2, Vector3 v3, Vector2 uv1, Vector2 uv2, Vector2 uv3)
        {
            triangles.Add(new IndexedTriangles(
                LookUpAndAddVertexAndUV(v1, uv1),
                LookUpAndAddVertexAndUV(v2, uv2),
                LookUpAndAddVertexAndUV(v3, uv3)
            ));
        }
        #endregion

        #region Make Array For Mesh Subroutines

        #region Flat Mesh
        public Vector3[] MakeVertexArrayForFlatMesh()
        {
            var vertices = new Vector3[triangles.Count * 3];
            var i = 0;
            foreach (var t in triangles)
            {
                vertices[i++] = this.vertices[t.i1];
                vertices[i++] = this.vertices[t.i2];
                vertices[i++] = this.vertices[t.i3];
            }
            return vertices;
        }

        public Vector2[] MakeUVArrayForFlatMesh()
        {
            var uvs = new Vector2[triangles.Count * 3];
            var i = 0;
            foreach (var t in triangles)
            {
                uvs[i++] = this.uvs[t.i1];
                uvs[i++] = this.uvs[t.i2];
                uvs[i++] = this.uvs[t.i3];
            }
            return uvs;
        }

        public int[] MakeIndexArrayForFlatMesh()
        {
            var indices = new int[3 * triangles.Count];
            for (var i = 0; i < indices.Length; i++) indices[i] = i;
            return indices;
        }
        #endregion

        #region Smooth Mesh
        public Vector3[] MakeVertexArrayForSmoothMesh()
        {
            return vertices.ToArray();
        }

        public Vector2[] MakeUVArrayForSmoothMesh()
        {
            return uvs.ToArray();
        }

        public int[] MakeIndexArrayForSmoothMesh()
        {
            var indices = new int[3 * triangles.Count];
            var i = 0;
            foreach(var t in triangles)
            {
                indices[i++] = t.i1;
                indices[i++] = t.i2;
                indices[i++] = t.i3;
            }
            return indices;
        }
        #endregion

        #region Line
        public Vector3[] MakeVertexArrayForLine()
        {
            return this.vertices.ToArray();
        }

        public Vector2[] MakeUVArrayForLine()
        {
            return this.uvs.ToArray();
        }

        public int[] MakeIndexArrayForLine()
        {
            var indeces = new int[this.triangles.Count * 6];
            var i = 0;
            foreach(var t in this.triangles)
            {
                indeces[i++] = t.i1;
                indeces[i++] = t.i2;
                indeces[i++] = t.i2;
                indeces[i++] = t.i3;
                indeces[i++] = t.i3;
                indeces[i++] = t.i1;
            }
            return indeces;
        }
        #endregion

        #endregion

        #region Build Mesh Subroutines
        public Mesh BuildFlatMesh(bool hasUV = true, bool optimize = false)
        {
            var mesh = new Mesh();
            mesh.vertices = MakeVertexArrayForFlatMesh();
            if (hasUV)
            {
                mesh.uv = MakeUVArrayForFlatMesh();
            }
            mesh.SetIndices(MakeIndexArrayForFlatMesh(), MeshTopology.Triangles, 0);
            mesh.RecalculateNormals();
            return mesh;
        }

        public Mesh BuildSmoothMesh(bool hasUV = true)
        {
            var mesh = new Mesh();
            mesh.vertices = MakeVertexArrayForSmoothMesh();
            if (hasUV)
            {
                mesh.uv = MakeUVArrayForSmoothMesh();
            }
            mesh.SetIndices(MakeIndexArrayForSmoothMesh(), MeshTopology.Triangles, 0);
            mesh.RecalculateNormals();
            return mesh;

        }

        public Mesh BuildLine(bool hasUV = true)
        {
            var mesh = new Mesh();
            mesh.vertices = MakeVertexArrayForLine();
            if (hasUV)
            {
                mesh.uv = MakeUVArrayForLine();
            }
            mesh.SetIndices(MakeIndexArrayForLine(), MeshTopology.Lines, 0);
            return mesh;
        }
        #endregion

        #region CacheClear Subroutines
        public void ClearCache()
        {
            vertices.Clear();
            uvs.Clear();
            triangles.Clear();
        }
        #endregion
    }
}
