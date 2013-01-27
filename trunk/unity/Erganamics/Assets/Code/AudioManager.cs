using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }

    public AudioClip[] soundEffects;
    public AudioClip[] musicClips;
    public float fadeDuration = 3f;
    public bool showTestGUI;

    public AudioSource soundEffectSource;
    public AudioSource primaryMusicSource;
    public AudioSource secondaryMusicSource;

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

        go = new GameObject();
        go.name = "PrimaryMusicSource";
        go.transform.parent = transform;
        primaryMusicSource = go.AddComponent<AudioSource>();
        primaryMusicSource.playOnAwake = false;
        primaryMusicSource.loop = true;

        go = new GameObject();
        go.name = "SecondaryMusicSource";
        go.transform.parent = transform;
        secondaryMusicSource = go.AddComponent<AudioSource>();
        secondaryMusicSource.playOnAwake = false;
        secondaryMusicSource.loop = true;
    }

    public void PlaySoundEffect( string soundName ) {
        foreach( var soundEffect in soundEffects ) {
            if( soundEffect.name == soundName ) {
                soundEffectSource.PlayOneShot( soundEffect );
                break;
            }
        }
    }

    public void FadeToMusic( string musicName ) {
        AudioClip newMusicClip = null;
        foreach( var musicClip in musicClips ) {
            if( musicClip.name == musicName ) {
                newMusicClip = musicClip;
                break;
            }
        }
        Debug.Log( string.Format( "newMusicClip: {0}", newMusicClip.name ) );

        if( newMusicClip != null ) {
            StopAllCoroutines();
            secondaryMusicSource.Stop(); // Cancel whatever might be fading out.
            StartCoroutine( FadeMusic( newMusicClip ) );
        } else {
            Debug.LogWarning( string.Format( "Couldn't find musicClip: {0}", musicName ) );
        }
    }

    public IEnumerator FadeMusic( AudioClip newMusicClip ) {
        AudioSource temp = primaryMusicSource;
        primaryMusicSource = secondaryMusicSource;
        secondaryMusicSource = temp;

        primaryMusicSource.clip = newMusicClip;
        primaryMusicSource.volume = 0f;
        primaryMusicSource.Play();
        var secondaryMusicStartVolume = secondaryMusicSource.volume;

        var startTime = Time.time;
        while( Time.time - startTime < fadeDuration ) {
            var portion = (Time.time - startTime)/fadeDuration;
            primaryMusicSource.volume = portion;
            secondaryMusicSource.volume = Mathf.Lerp( secondaryMusicStartVolume, 0f, portion );
            Debug.Log( string.Format( "portion: {0}", portion ) );
            yield return null;
        }

        primaryMusicSource.volume = 1f;
        secondaryMusicSource.Stop();

        yield break;
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

            y = 0f;
            foreach( var musicClip in musicClips ) {
                if( GUI.Button( new Rect( 210f, y, 300f, 45f ), string.Format( "Switch to music: {0}", musicClip.name ) ) ) {
                    FadeToMusic( musicClip.name );
                }
                y += 50f;
            }
        }
    }
}
