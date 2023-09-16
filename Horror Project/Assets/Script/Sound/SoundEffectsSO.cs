using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New_SoundEffect", menuName = "Audio/New Sound Effect")]
public class SoundEffectsSO : ScriptableObject
{
    /// Configuración general del audio
    #region config
    
    public AudioClip[] clips;

    //[Range(0.0f, 1.0f)]
    //public Vector2 volume = new Vector2(0.5f, 0.5f);
    //[Range(0.0f, 1.0f)]
    public Vector2 pitch = new Vector2(1, 1);
    /*
    [SerializeField] private int playIndex;
    [SerializeField] public SoundClipPlayOrder playOrder;
    */
    #endregion


    /// Preview del sonido añadido
    #region previewer

        #if UNITY_EDITOR
        private AudioSource previewer;

        private void OnEnable()
        {
            previewer = EditorUtility
                .CreateGameObjectWithHideFlags("AudioPreview", HideFlags.HideAndDontSave,
                    typeof(AudioSource)).GetComponent<AudioSource>();
        }

        private void OnDisable()
        {
            DestroyImmediate(previewer.gameObject);
        }
    /*
    [ButtonGroup("previewControls")]
    [GUIColor(.3f, .1f, .3f)]
    [Button(ButtonSizes.Gigantic)]
    */
        private void PlayPreview()
        {
            Play(previewer);
        }
    /*
    [ButtonGroup("previewControls")]
    [GUIColor(.3f, .1f, .3f)]
    [Button(ButtonSizes.Gigantic)]
    [EnableIf("@previewer.isPlaying")]
    */
        private void StopPreview()
        {
            previewer.Stop();
        }
        #endif
    #endregion
    /*
    private AudioClip GetAudioClip()
    {
        // Obtengo el clip actual
        var clip = clips[playIndex];

        // Encuentro el siguiente clip
        switch (playOrder)
        {
            case SoundClipPlayOrder.in_order:
                playIndex = (playIndex + 1) % clips.Length;
                break;
            case SoundClipPlayOrder.random:
                playIndex = Random.Range(0, clips.Length);
                break;
            case SoundClipPlayOrder.reverse:
                playIndex = (playIndex + clips.Length - 1) % clips.Length;
                break;
        }

        // Devuelvo el clip que cogí
        return clip;
    }
    */

    public AudioSource Play(AudioSource audioSourceParam = null)
    {
        if(clips.Length == 0)
        {
            Debug.LogWarning($"Missing sound clips for {name}");
            return null;
        }

        var source = audioSourceParam;
        if(source == null)
        {
            var obj = new GameObject("Sound", typeof(AudioSource));
            source = obj.GetComponent<AudioSource>();
        }

        // Establecer los valores d la configuración
        source.clip = clips[0]; // Obtener clips al azar
        //source.volume = Random.Range(volume.x, volume.y);
        source.pitch = Random.Range(pitch.x, pitch.y);

        source.Play();

        #if UNITY_EDITOR
            // Previewer
            // Prevengo que la función "Play()" destruya el previewer
            if (source != previewer)
            {
                Destroy(source.gameObject, source.clip.length / source.pitch);
            }
        #else
            Destroy(source.gameObject, source.clip.length / source.pitch);
        #endif
        return source;
    }

    public enum SoundClipPlayOrder
    {
        random,
        in_order,
        reverse
    }
    
}
