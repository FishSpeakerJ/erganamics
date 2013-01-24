using UnityEngine;
using System.Collections;

namespace Erganamics {
    public class Util {
        public static GameObject CreateQuad() {
            GameObject quad = new GameObject( "Quad", typeof( MeshFilter ), typeof( MeshRenderer ) );
            Mesh mesh = new Mesh();

            mesh.vertices = new Vector3[] {
                new Vector3( 0f, 0f, 0f ),
                new Vector3( 0f, 1f, 0f ),
                new Vector3( 1f, 1f, 0f ),
                new Vector3( 1f, 0f, 0f ),
            };

            mesh.uv = new Vector2[] {
                new Vector2( 0f, 0f ),
                new Vector2( 0f, 1f ),
                new Vector2( 1f, 1f ),
                new Vector2( 1f, 0f ),
            };

            mesh.triangles = new int[] {
                0, 1, 2,
                0, 2, 3,
            };

            quad.GetComponent<MeshFilter>().mesh = mesh;

            return quad;
        }
    }
}