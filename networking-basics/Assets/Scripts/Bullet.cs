using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : NetworkBehaviour
{
    [SerializeField]
    private float destroyAfter = 2f;

    private Rigidbody rb;

    private GameObject owner;

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 15f, ForceMode.VelocityChange);
    }

    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }

    public void SetOwner(GameObject _owner)
    {
        owner = _owner;
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != owner)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<Health>().CmdTakeDamage();
            }
            NetworkServer.Destroy(gameObject);
        }
    }
}
