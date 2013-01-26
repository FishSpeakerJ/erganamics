using UnityEngine;
using System.Collections;

namespace Erganamics {
    public class Util {
        public static readonly int screenWidth = 1280;
        public static readonly int screenHeight = 752;

        // This should be called to create a quad at runtime.
        public static GameObject CreateQuadAtRuntime() {
            return CreateQuad( Settings.Instance.quadMaterial );
        }

        public static GameObject CreateQuad( Material material ) {
            GameObject quad = new GameObject( "Quad", typeof( MeshFilter ), typeof( MeshRenderer ) );
            Mesh mesh = new Mesh();

            mesh.vertices = new Vector3[] {
                new Vector3( 0f, 0f, 0f ),
                new Vector3( 0f, 1f, 0f ),
                new Vector3( 1f, 1f, 0f ),
                new Vector3( 1f, 0f, 0f ),
            };

            mesh.normals = new Vector3[] {
                new Vector3( 0f, 0f, 1f ),
                new Vector3( 0f, 0f, 1f ),
                new Vector3( 0f, 0f, 1f ),
                new Vector3( 0f, 0f, 1f ),
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

            mesh.RecalculateBounds();

            quad.GetComponent<MeshFilter>().mesh = mesh;

            var meshCollider = quad.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = mesh;

            quad.renderer.sharedMaterial = material;

            quad.transform.localScale = new Vector3( 100f, 100f, 1f );

            return quad;
        }
    }
}