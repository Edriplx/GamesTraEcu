using System.Collections;
using UnityEngine;
using Photon.Pun;
using Unity.XR.CoreUtils;

public class MonedaDetectorSimplificado : MonoBehaviourPun
{
    public XROrigin xrOriginHost;
    public XROrigin xrOriginClient;
    public Transform hostRespawn;  // Respawn para el host
    public Transform clientRespawn; // Respawn para el cliente

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MonedaCirculo"))
        {
            // Determinar si fue golpeado por Moneda1 o Moneda2
            if (photonView.IsMine)
            {
                if (CompareTag("Moneda1") || CompareTag("Moneda2"))
                {
                    StartCoroutine(HandleMonedaExit());
                }
            }
        }
    }

    private IEnumerator HandleMonedaExit()
    {
        // Esperar 2 segundos antes de teletransportar
        yield return new WaitForSeconds(2f);

        // Teletransportar al jugador y a la moneda al respawn correspondiente
        if (CompareTag("Moneda1"))
        {
            xrOriginHost.transform.position = hostRespawn.position;
            transform.position = hostRespawn.position; // Mover la moneda al respawn
        }
        else if (CompareTag("Moneda2"))
        {
            xrOriginClient.transform.position = clientRespawn.position;
            transform.position = clientRespawn.position; // Mover la moneda al respawn
        }
    }
}
