using System.Collections;
using UnityEngine;
using Photon.Pun;
using Unity.XR.CoreUtils;

public class TeleportAfterCollision2 : MonoBehaviourPun
{
    public Terrain terreno;
    public XROrigin xrOriginHost;
    public XROrigin xrOriginClient;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain2"))
        {
            if (photonView.IsMine)
            {
                Debug.Log($"Collision detected with Terrain by {gameObject.name}");

                if (CompareTag("Canica3"))
                {
                    Debug.Log("Canica1 detected, starting teleport sequence for host");
                    StartCoroutine(StopAndTeleport(xrOriginHost));
                }
                else if (CompareTag("Canica4"))
                {
                    Debug.Log("Canica2 detected, starting teleport sequence for client");
                    StartCoroutine(StopAndTeleport(xrOriginClient));
                }
            }
        }
    }

    IEnumerator StopAndTeleport(XROrigin xrOrigin)
    {
        yield return new WaitForSeconds(2);

        // Frenar la esfera
        if (rb != null)
        {
            Debug.Log("Stopping sphere");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        yield return new WaitForSeconds(1); // Esperar un segundo adicional para asegurar que la esfera esté completamente quieta

        // Teletransportar al jugador
        if (xrOrigin != null)
        {
            xrOrigin.transform.position = transform.position;
            Debug.Log("Player teleported to: " + transform.position);
        }
        else
        {
            Debug.LogError("XR Origin reference is missing.");
        }
    }
}
