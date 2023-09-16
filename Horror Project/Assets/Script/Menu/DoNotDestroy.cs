using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    public DoNotDestroy Instance;
    void Awake()
    {
        // Si no existe "SoundManager" en la escena ==> Crear "SoundManager" y nunca destruirlo("DontDestroyOnLoad")
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // Si existe ==> Destruir
        else
        {
            Destroy(gameObject);
        }
    }
}
