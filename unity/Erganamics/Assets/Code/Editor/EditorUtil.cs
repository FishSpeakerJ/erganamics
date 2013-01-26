using UnityEditor;
using UnityEngine;

namespace Erganamics.Editor {
    public class EditorUtil {
        [MenuItem( "Erganamics/Generate/Quad" )]
        public static void GenerateQuad() {
            var quadMaterial = (Material)AssetDatabase.LoadAssetAtPath( "Assets/Materials/ErgUnlitAlpha.mat", typeof( Material ) );
            Util.CreateQuad( quadMaterial );
        }
    }
}