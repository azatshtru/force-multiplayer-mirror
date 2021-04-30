using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Health : NetworkBehaviour
{
    public Slider healthSlider;

    float maxHealth = 100;

    [SerializeField][SyncVar(hook = nameof(UpdateHealthSlider))]
    private float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHealthSlider(float oldHealth, float newHealth)
    {
        healthSlider.value = newHealth;
    }

    public void CmdTakeDamage()
    {
        if (!isServer)
        {
            return;
        }

        health -= 25;

        if(health <= 0)
        {
            CmdDie();
        }
    }

    public void CmdDie ()
    {
        GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
    }
}
