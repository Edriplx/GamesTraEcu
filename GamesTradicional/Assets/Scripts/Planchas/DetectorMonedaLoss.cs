using System.Collections;
using UnityEngine;
using Photon.Pun;
using Unity.XR.CoreUtils;

public class DetectorMonedaLoss : MonoBehaviourPun
{
    public Transform respawnPointLossHost;   // Respawn para el host
    public Transform respawnPointLossClient; // Respawn para el cliente
    private bool isDetected = false;
    private float timeInDetector = 0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moneda1") || other.CompareTag("Moneda2"))
        {
            isDetected = true;
            timeInDetector = 0f; // Reinicia el contador de tiempo
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Moneda1") || other.CompareTag("Moneda2"))
        {
            isDetected = false;
            timeInDetector = 0f; // Reinicia el contador si la moneda sale del área
        }
    }

    void Update()
    {
        if (isDetected)
        {
            timeInDetector += Time.deltaTime;

            if (timeInDetector >= 5f)
            {
                // Teletransporta al jugador y la moneda
                TeleportPlayerAndMoneda();
                timeInDetector = 0f; // Reinicia el tiempo después del teletransporte
            }
        }
    }

    private void TeleportPlayerAndMoneda()
    {
        GameObject moneda = null;
        XROrigin xrOrigin = null;

        if (PhotonNetwork.IsMasterClient)
        {
            moneda = GameObject.FindGameObjectWithTag("Moneda1");
            xrOrigin = FindObjectOfType<XROrigin>(); // Encuentra la XROrigin del host
        }
        else
        {
            moneda = GameObject.FindGameObjectWithTag("Moneda2");
            xrOrigin = FindObjectOfType<XROrigin>(); // Encuentra la XROrigin del cliente
        }

        if (moneda != null && xrOrigin != null)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                xrOrigin.transform.position = respawnPointLossHost.position;
                moneda.transform.position = respawnPointLossHost.position;
            }
            else
            {
                xrOrigin.transform.position = respawnPointLossClient.position;
                moneda.transform.position = respawnPointLossClient.position;
            }
        }
        else
        {
            Debug.LogError("No se encontró la moneda o el XROrigin.");
        }
    }
}
