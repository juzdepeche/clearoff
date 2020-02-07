using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    [Header("Health")]
    public int StartHealth = 100;
    private int CurrentHealth;

    public int PoseValue = 0;

    [Header("Damages")]
    public int DamageByEnvironmentForce = 15;
    public int DamageByEnvironmentDone = 10;
    public int DamageByPlayerForce = 50;
    public int DamageByPlayerDone = 15;

    [Header("Audio")]
    public AudioClip[] Impacts;
    public AudioClip[] Hits;
    public AudioSource SoundSource;

    public ParticleSystem SparkParticles;

    public bool HasBeenRewarded = false;

    List<Player> hookedPlayers;

    enum SoundType
    {
        Hit,
        Impact
    } 

    private void Start() {
        CurrentHealth = StartHealth;
        hookedPlayers = new List<Player>();
    }

    void OnCollisionEnter(Collision col)
	{
        if (col.collider.tag == "Player" && col.relativeVelocity.magnitude >= DamageByPlayerForce) 
        {
            Damage(DamageByPlayerDone, SoundType.Hit);
            Spark(col.transform);
        }
		else if(col.collider.tag != "Player" && col.relativeVelocity.magnitude >= DamageByEnvironmentForce)
		{
            Damage(DamageByEnvironmentDone, SoundType.Impact);
            Spark(col.transform);
		}
    }

    public void Pose(int value)
    {
        HasBeenRewarded = true;
        PoseValue = value;
    }

    public void Dispose()
    {
        HasBeenRewarded = false;
    }

    private void Damage(int DamageDone, SoundType soundType) 
    {
        CurrentHealth -= DamageDone;
        PlaySound(soundType);
    }

    private void Spark(Transform position)
    {
        Instantiate(SparkParticles, position);
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
            int i = UnityEngine.Random.Range(0, SoundLibrary.Length);
            SoundSource.clip = SoundLibrary[i];
            SoundSource.Play();
        }
    }

    public int GetPoints()
    {
        var mass = (int) GetComponent<Rigidbody>().mass;
        return mass * (CurrentHealth / StartHealth);
    }
}
