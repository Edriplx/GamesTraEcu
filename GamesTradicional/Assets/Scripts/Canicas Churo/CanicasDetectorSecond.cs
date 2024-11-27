using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class CanicasDetectorSecond : MonoBehaviour
{
    public Transform hostRespawnPoint; // Punto de respawn para el host
    public Transform clientRespawnPoint; // Punto de respawn para el cliente

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Canica5") || other.CompareTag("Canica6"))
        {
            Debug.Log($"{other.name} ha entrado en el área del detector.");

            // Iniciar la corrutina para esperar 4 segundos y teletransportar
            StartCoroutine(TeletransportarJugador(other.gameObject));
        }
    }

    private IEnumerator TeletransportarJugador(GameObject canica)
    {
        yield return new WaitForSeconds(4f);

        XROrigin xrOrigin = null;
        Transform respawnPoint = null;

        if (canica.CompareTag("Canica5"))
        {
            xrOrigin = FindObjectOfType<XROrigin>(); // Encuentra la XROrigin del host
            respawnPoint = hostRespawnPoint;
            Debug.Log("Teletransportando al jugador del host...");
        }
        else if (canica.CompareTag("Canica6"))
        {
            xrOrigin = FindObjectOfType<XROrigin>(); // Encuentra la XROrigin del cliente
            respawnPoint = clientRespawnPoint;
            Debug.Log("Teletransportando al jugador del cliente...");
        }

        if (xrOrigin != null && respawnPoint != null)
        {
            xrOrigin.transform.position = respawnPoint.position;
            Debug.Log($"Jugador teletransportado a la posición de respawn: {respawnPoint.position}");
        }
        else
        {
            Debug.LogError("XR Origin o punto de respawn no asignado correctamente.");
        }
    }
}
