using System.Collections;
using UnityEngine;
using Photon.Pun;
using Unity.XR.CoreUtils;
using System.Collections.Generic;

public class CanicaDetector : MonoBehaviourPun
{
    public Transform hostRespawnPoint; // Punto de respawn para el host
    public Transform clientRespawnPoint; // Punto de respawn para el cliente
    private List<GameObject> canicasDentro = new List<GameObject>();
    private float tiempoDentro = 0f;
    private const float tiempoLimite = 10f; // Tiempo en segundos

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Canica1") || other.CompareTag("Canica2"))
        {
            // Añadir la canica a la lista de canicas dentro del área
            if (!canicasDentro.Contains(other.gameObject))
            {
                canicasDentro.Add(other.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Canica1") || other.CompareTag("Canica2"))
        {
            // Quitar la canica de la lista de canicas dentro del área
            canicasDentro.Remove(other.gameObject);

            if (canicasDentro.Count == 0)
            {
                tiempoDentro = 0f;
            }
        }
    }

    void Update()
    {
        if (canicasDentro.Count > 0)
        {
            tiempoDentro += Time.deltaTime;

            if (tiempoDentro >= tiempoLimite)
            {
                foreach (GameObject canica in canicasDentro)
                {
                    XROrigin xrOrigin = null;
                    Transform respawnPoint = null;

                    if (canica.CompareTag("Canica1"))
                    {
                        xrOrigin = FindObjectOfType<XROrigin>(); // Encuentra la XROrigin del host
                        respawnPoint = hostRespawnPoint;
                    }
                    else if (canica.CompareTag("Canica2"))
                    {
                        xrOrigin = FindObjectOfType<XROrigin>(); // Encuentra la XROrigin del cliente
                        respawnPoint = clientRespawnPoint;
                    }

                    if (xrOrigin != null && respawnPoint != null)
                    {
                        xrOrigin.transform.position = respawnPoint.position;
                    }
                }

                tiempoDentro = 0f;
                canicasDentro.Clear();
            }
        }
    }
}