using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
	void Start()
    {
        
    }

	void Update()
    {
        
    }

    public AudioClip auidoClip;

    int i = 0;
    private void OnTriggerEnter(Collider other)
    {
        //AudioSource audio = GetComponent<AudioSource>();
        //audio.PlayOneShot(auidoClip);

        i++;

        if(i % 2 == 0)
            Managers.Sound.Play("UnityChan/univ0001", Define.Sound.Effect);
        else
            Managers.Sound.Play("UnityChan/univ0002", Define.Sound.Effect);
    }
}
