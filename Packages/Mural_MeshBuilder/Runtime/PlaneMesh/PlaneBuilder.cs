using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mural.MeshBuilder
{
    public class PlaneBuilder
    {
        public VertexCache vertexCache
        {
            get;
            set;
        }

        public PlaneBuilder(int widthSegments, int heightSegments, float width, float height)
        {
            vertexCache = new VertexCache();

			var winv = 1f / (widthSegments - 1);
			var hinv = 1f / (heightSegments - 1);

			for (int y = 0; y < heightSegments; y++)
			{
				var ry = y * hinv;

				for (int x = 0; x < widthSegments; x++)
				{
					var rx = x * winv;

					Vector3 position = new Vector3(
						(rx - 0.5f) * width,
						0f,
						(0.5f - ry) * height
					);
					Vector2 uv = new Vector2(rx, ry);

					vertexCache.AddVertexAndUV(position, uv);
				}
			}

			for (int y = 0; y < heightSegments - 1; y++)
			{
				for (int x = 0; x < widthSegments - 1; x++)
				{
					int index = y * widthSegments + x;
					var a = index;
					var b = index + 1;
					var c = index + 1 + widthSegments;
					var d = index + widthSegments;

					vertexCache.AddTriangle(a, b, c);
					vertexCache.AddTriangle(c, d, a);
				}
			}
		}
    }
}
