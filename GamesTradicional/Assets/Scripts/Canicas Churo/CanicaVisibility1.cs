using UnityEngine;
using Photon.Pun;

public class CanicaVisibility1 : MonoBehaviourPun
{
    private Renderer canicaRenderer;

    void Start()
    {
        canicaRenderer = GetComponent<Renderer>();
    }

    [PunRPC]
    void SetCanicaVisibility(bool isVisible)
    {
        canicaRenderer.enabled = isVisible;
    }

    public void UpdateVisibility(bool isVisible)
    {
        photonView.RPC("SetCanicaVisibility", RpcTarget.AllBuffered, isVisible);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain2"))
        {
            // Actualiza la visibilidad de la canica para todos los jugadores
            UpdateVisibility(true);
        }
    }

    // Aseg�rate de que cuando la canica sea agarrada nuevamente, vuelva a ser visible
    void OnSelectEntered()
    {
        UpdateVisibility(true);
    }
}
