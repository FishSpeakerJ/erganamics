using UnityEngine;

public class Settings : MonoBehaviour {
    public static Settings Instance { get; private set; }

    public Material quadMaterial;

    private void Awake() {
        if( Instance == null ) {
            Instance = this;
        } else {
            Debug.LogError( "Attempting to create more than one Settings object." );
        }
    }
}