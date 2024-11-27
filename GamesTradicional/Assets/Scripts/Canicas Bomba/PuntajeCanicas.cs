using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PuntajeCanicas : MonoBehaviourPun
{
    public TextMeshProUGUI textHostScore;
    public TextMeshProUGUI textClientScore;
    private int puntajeHost = 0;
    private int puntajeCliente = 0;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MonedaCirculo"))
        {
            // Detectar si fue la Canica1 o Canica2 la que sacó la canica del círculo
            if (PhotonNetwork.IsMasterClient && other.CompareTag("Canica1"))
            {
                puntajeHost++;
                textHostScore.text = "Host: " + puntajeHost;
                Debug.Log("Punto para el Host");
            }
            else if (!PhotonNetwork.IsMasterClient && other.CompareTag("Canica2"))
            {
                puntajeCliente++;
                textClientScore.text = "Cliente: " + puntajeCliente;
                Debug.Log("Punto para el Cliente");
            }

            photonView.RPC("ActualizarPuntaje", RpcTarget.AllBuffered, puntajeHost, puntajeCliente);
        }
    }

    [PunRPC]
    void ActualizarPuntaje(int nuevoPuntajeHost, int nuevoPuntajeCliente)
    {
        puntajeHost = nuevoPuntajeHost;
        puntajeCliente = nuevoPuntajeCliente;

        textHostScore.text = "Host: " + puntajeHost;
        textClientScore.text = "Cliente: " + puntajeCliente;
    }
}
