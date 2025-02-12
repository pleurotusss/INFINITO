using System.Collections;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip musicClip;
    public float fadeDuration = 1f;  // Durata del fade
    private bool isFading = false;

    void Start()
    {
        audioSource.loop = false;  // Disabilita il loop
        audioSource.clip = musicClip;
        audioSource.Play();
    }

    void Update()
    {
        // Verifica se la traccia sta per finire
        if (!audioSource.isPlaying && audioSource.time >= audioSource.clip.length - 1)
        {
            if (!isFading)  // Evita che la coroutine venga chiamata più volte
            {
                StartCoroutine(FadeOutAndPlayAgain());  // Avvia la coroutine
            }
        }
    }

    private IEnumerator FadeOutAndPlayAgain()
    {
        isFading = true;

        // Salva il volume di partenza
        float startVolume = audioSource.volume;

        // Fase di fade out (decadimento esponenziale)
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            // Applicazione di un decadimento esponenziale
            float fadeOutFactor = 1 - Mathf.Exp(-t / fadeDuration);  // Esponenziale decrescente
            audioSource.volume = startVolume * (1 - fadeOutFactor);  // Decadimento volume
            yield return null;  // Aspetta il prossimo frame
        }

        // Imposta il volume a zero e riprendi la traccia
        audioSource.volume = 0;
        audioSource.Play();

        // Fase di fade in (decadimento esponenziale inverso)
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            // Applicazione di un decadimento esponenziale inverso
            float fadeInFactor = 1 - Mathf.Exp(-t / fadeDuration);  // Esponenziale crescente
            audioSource.volume = startVolume * fadeInFactor;  // Incremento volume
            yield return null;  // Aspetta il prossimo frame
        }

        // Ripristina il volume originale
        audioSource.volume = startVolume;
        isFading = false;
    }
}
