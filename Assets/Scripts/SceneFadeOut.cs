using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFadeOut : MonoBehaviour
{
    [SerializeField] Fade fade;
    void Start()
    {
        // fade.FadeOut(1f);
        fade.FadeOut(1f);
    }
    
}
