using System.Collections;
using UnityEngine;

public class FadeToBlack : MonoBehaviour {
    private static FadeToBlack instance = null;
    public static FadeToBlack Instance {
        get {
            if( instance == null ) {
                GameObject go = new GameObject( "FadeToBlack" );
                instance = go.AddComponent<FadeToBlack>();
            }
            return instance;
        }
    }

    public float alpha;

    private Texture2D fadeTexture;
    private GUIStyle fadeStyle = new GUIStyle();

    public IEnumerator FadeOut( float duration ) {
        Debug.Log( string.Format( "FadeOut: {0}", duration ) );
        yield return StartCoroutine( FadeTo( 1f, duration ) );
    }

    public IEnumerator FadeIn( float duration ) {
        yield return StartCoroutine( FadeTo( 0f, duration ) );
    }

    public IEnumerator FadeTo( float targetOpacity, float duration ) {
        targetOpacity = Mathf.Clamp01( targetOpacity );

        float startOpacity = alpha;
        float startTime = Time.time;

        while( Time.time - startTime < duration ) {
            float portion = (Time.time - startTime) / duration;
            alpha = Mathf.Lerp( startOpacity, targetOpacity, portion );
            yield return null;
        }
        alpha = targetOpacity;

        yield break;
    }

    private void Awake() {
        fadeTexture = new Texture2D( 1, 1 );
        fadeTexture.SetPixel( 0, 0, Color.black );
        fadeTexture.Apply();
        fadeStyle.normal.background = fadeTexture;
    }

    private void OnGUI() {
        if( alpha > 0f ) {
            var screenRect = new Rect( 0, 0, Screen.width, Screen.height );
            GUI.color = new Color( 1f, 1f, 1f, alpha );
            GUI.depth = -1000;
            GUI.DrawTexture( screenRect, fadeTexture );
        }
    }
}
