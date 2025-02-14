using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Riferimento al VideoPlayer
    public float delayBeforeQuit = 3f; // Tempo di attesa prima di chiudere l'app

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>(); // Prova ad assegnarlo automaticamente
        }

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd; // Assegna l'evento
        }
        else
        {
            Debug.LogError("VideoPlayer non assegnato!");
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("Video terminato, chiusura in " + delayBeforeQuit + " secondi...");
        StartCoroutine(QuitAfterDelay());
    }

    IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeQuit);
        Debug.Log("Chiusura applicazione...");
        Application.Quit();

        // Per funzionare in editor (solo per debug)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
