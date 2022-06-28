using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public static PlaySound _instance;
    public static PlaySound GetInstance()=>_instance;
    public AudioSource soundPlayer;

private void Awake() {
    if(_instance==null)
    {
        _instance=this;
    }
    else
    {Destroy(_instance);}
}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playThisSoundEffect()
    {
        soundPlayer.Play();
    }
}
