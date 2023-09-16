using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startMusic : MonoBehaviour
{
    void Start()
    {
        PlayerMusicManager.instance.PlayMusic("menu");
        PlayerMusicManager.instance.Loop();
    }
}
