using System.Collections;
using UnityEngine;
using Photon.Pun;
using Unity.XR.CoreUtils;

public class TeleportClientCanica : MonoBehaviourPun
{
    public Terrain terreno;
    public XROrigin xrOriginClient;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            if (photonView.IsMine)
            {
                StartCoroutine(StopAndTeleport(xrOriginClient));
            }
        }
    }

    IEnumerator StopAndTeleport(XROrigin xrOrigin)
    {
        yield return new WaitForSeconds(6);

        // Frenar la esfera
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        yield return new WaitForSeconds(1); // Esperar un segundo adicional para asegurar que la esfera esté completamente quieta

        // Teletransportar al jugador
        if (xrOrigin != null)
        {
            xrOrigin.transform.position = transform.position;
        }
    }
}
