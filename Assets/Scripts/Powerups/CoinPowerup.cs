using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinPowerup : BasePowerup
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        this.type = PowerupType.Coin;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void SpawnPowerup()
    {
        Debug.Log("Coin spawned");
        spawned = true;

        // play the sound
        AudioSource source = this.GetComponent<AudioSource>();
        source.PlayOneShot(source.clip);

    }

    // hide the base destroy method
    public new void DestroyPowerup()
    {
    }

    public override void ApplyPowerup(MonoBehaviour i)
    {
        // try
        GameManager manager;
        bool result = i.TryGetComponent<GameManager>(out manager);

        if (result)
        {
            manager.IncreaseScore(1);
        }
    }
}
