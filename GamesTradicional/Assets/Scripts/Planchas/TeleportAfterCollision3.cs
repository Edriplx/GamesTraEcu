using System.Collections;
using UnityEngine;
using Photon.Pun;
using Unity.XR.CoreUtils;

public class TeleportAfterCollision3 : MonoBehaviourPun
{
    public Terrain terreno;
    public XROrigin xrOriginHost;
    public XROrigin xrOriginClient;

    private bool isDetectedByDetectorMoneda = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DetectorMoneda"))
        {
            isDetectedByDetectorMoneda = true;
            Debug.Log($"{gameObject.name} detected by DetectorMoneda, teleportation disabled.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DetectorMoneda"))
        {
            isDetectedByDetectorMoneda = false;
            Debug.Log($"{gameObject.name} exited from DetectorMoneda, teleportation enabled.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain3") && !isDetectedByDetectorMoneda)
        {
            if (photonView.IsMine)
            {
                Debug.Log($"Collision detected with Terrain by {gameObject.name}");

                if (CompareTag("Moneda1"))
                {
                    Debug.Log("Moneda1 detected, starting teleport sequence for host");
                    StartCoroutine(TeleportPlayer(xrOriginHost));
                }
                else if (CompareTag("Moneda2"))
                {
                    Debug.Log("Moneda2 detected, starting teleport sequence for client");
                    StartCoroutine(TeleportPlayer(xrOriginClient));
                }
            }
        }
    }

    IEnumerator TeleportPlayer(XROrigin xrOrigin)
    {
        yield return new WaitForSeconds(2);

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
