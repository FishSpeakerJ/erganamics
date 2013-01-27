using UnityEngine;
using System.Collections;

public class CoroutineHelper : MonoBehaviour {}

public class MusicTrack {
    public AudioSource primaryMusicSource;
    public AudioSource secondaryMusicSource;

    private AudioManager audioManager;

    private CoroutineHelper primaryCoroutineHelper { get { return primaryMusicSource.gameObject.GetComponent<CoroutineHelper>(); } }
    private CoroutineHelper secondaryCoroutineHelper { get { return secondaryMusicSource.gameObject.GetComponent<CoroutineHelper>(); } }

    public MusicTrack( int index, AudioManager audioManager ) {
        this.audioManager = audioManager;

        GameObject go = new GameObject( string.Format( "Track{0}_PrimaryMusicSource", index ), typeof( CoroutineHelper ) );
        go.transform.parent = audioManager.transform;
        primaryMusicSource = go.AddComponent<AudioSource>();
        primaryMusicSource.playOnAwake = false;
        primaryMusicSource.loop = true;

        go = new GameObject( string.Format( "Track{0}_SecondaryMusicSource", index ), typeof( CoroutineHelper ) );
        go.transform.parent = audioManager.transform;
        secondaryMusicSource = go.AddComponent<AudioSource>();
        secondaryMusicSource.playOnAwake = false;
        secondaryMusicSource.loop = true;
    }

    public void FadeToMusic( string musicName ) {
        AudioClip newMusicClip = null;
        foreach( var musicClip in audioManager.musicClips ) {
            if( musicClip.name == musicName ) {
                newMusicClip = musicClip;
                break;
            }
        }

        if( (newMusicClip == null) && (musicName != null) ) {
            Debug.LogWarning( string.Format( "Couldn't find musicClip: {0}", musicName ) );
        } else {
            primaryCoroutineHelper.StopAllCoroutines();
            secondaryMusicSource.volume = 0f;  // Cancel whatever might be fading out.
            secondaryMusicSource.Stop(); 
            secondaryCoroutineHelper.StartCoroutine( FadeMusic( newMusicClip ) );
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
        while( Time.time - startTime < audioManager.fadeDuration ) {
            var portion = (Time.time - startTime) / audioManager.fadeDuration;
            primaryMusicSource.volume = portion;
            secondaryMusicSource.volume = Mathf.Lerp( secondaryMusicStartVolume, 0f, portion );
            yield return null;
        }

        primaryMusicSource.volume = 1f;
        secondaryMusicSource.Stop();

        yield break;
    }
}