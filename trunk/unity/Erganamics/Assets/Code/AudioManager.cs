using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }

    public AudioClip[] soundEffects;
    public AudioClip[] musicClips;
    public float fadeDuration = 3f;
    public bool showTestGUI;

    public AudioSource soundEffectSource;
    public int numMusicTracks = 2;
    public MusicTrack[] musicTracks;

    private void Awake() {
        if( Instance == null ) {
            Instance = this;
        } else {
            Debug.LogError( "Attempting to create more than one AudioManager object." );
        }
    }

    private void Start() {
        GameObject go = new GameObject();
        go.name = "SoundEffectSource";
        go.transform.parent = transform;
        soundEffectSource = go.AddComponent<AudioSource>();
        soundEffectSource.playOnAwake = false;

        musicTracks = new MusicTrack[numMusicTracks];
        for( int i = 0; i < numMusicTracks; i++ ) {
            musicTracks[i] = new MusicTrack( i, this );
        }
    }

    public void PlaySoundEffect( string soundName ) {
        foreach( var soundEffect in soundEffects ) {
            if( soundEffect.name == soundName ) {
                soundEffectSource.PlayOneShot( soundEffect );
                break;
            }
        }
    }

    public void FadeToMusic( int track, string musicName ) {
        Debug.Log(musicName);
        musicTracks[track].FadeToMusic( musicName );
    }

    private void OnGUI() {
        if( showTestGUI ) {
            var y = 0f;
            foreach( var soundEffect in soundEffects ) {
                if( GUI.Button( new Rect( 10f, y, 200f, 45f ), string.Format( "Play {0}", soundEffect.name ) ) ) {
                    PlaySoundEffect( soundEffect.name );
                }
                y += 50f;
            }

            var x = 210f;
            for( int i = 0; i < numMusicTracks; i++ ) {
                y = 0f;
                foreach( var musicClip in musicClips ) {
                    if( GUI.Button( new Rect( x, y, 250f, 45f ), string.Format( "Switch to music: {0}", musicClip.name ) ) ) {
                        FadeToMusic( i, musicClip.name );
                    }
                    y += 50f;
                }
                if( GUI.Button( new Rect( x, y, 250f, 45f ), "Fade out music" ) ) {
                    FadeToMusic( i, null );
                }
                x += 260;
            }
        }
    }
}

