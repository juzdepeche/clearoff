using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    [Header("Health")]
    public float StartHealth = 100f;
    private float CurrentHealth;

    [Header("Dammages")]
    public float DammageByEnvironmentForce = 15f;
    public float DammageByEnvironmentDone = 10f;
    public float DammageByPlayerForce = 50f;
    public float DammageByPlayerDone = 15f;

    [Header("Audio")]
    public AudioClip[] Impacts;
    public AudioClip[] Hits;
    public AudioSource SoundSource;

    enum SoundType
    {
        Hit,
        Impact
    } 

    private void Start() {
        CurrentHealth = StartHealth;
    }

    void OnCollisionEnter(Collision col)
	{
        if (col.collider.tag == "Player" && col.relativeVelocity.magnitude >= DammageByPlayerForce) 
        {
            Debug.Log(col.relativeVelocity.magnitude);
            Dammage(DammageByPlayerDone, SoundType.Hit);
        }
		else if(col.collider.tag != "Player" && col.relativeVelocity.magnitude >= DammageByEnvironmentForce)
		{
            Dammage(DammageByEnvironmentDone, SoundType.Impact);
		}
    }

    private void Dammage(float dammageDone, SoundType soundType) 
    {
        CurrentHealth -= dammageDone;
        PlaySound(soundType);
    }

    private void PlaySound(SoundType soundType) 
    {
        AudioClip[] SoundLibrary = null;
        switch(soundType)
        {
            case SoundType.Hit:
                SoundLibrary = Hits;
                break;
            case SoundType.Impact:
                SoundLibrary = Impacts;
                break;
        }
        
        if(SoundLibrary != null && !SoundSource.isPlaying)
        {
            int i = Random.Range(0, SoundLibrary.Length);
            SoundSource.clip = SoundLibrary[i];
            SoundSource.Play();
        }
    }

    public float GetPoints()
    {
        return GetComponent<Rigidbody>().mass * (CurrentHealth / StartHealth);
    }
}
