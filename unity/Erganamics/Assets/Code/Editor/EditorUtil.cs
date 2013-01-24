using UnityEditor;

namespace Erganamics.Editor {
    public class EditorUtil {
        [MenuItem( "Erganamics/Generate/Quad" )]
        public static void GenerateQuad() {
            Util.CreateQuad();
        }
    }
}