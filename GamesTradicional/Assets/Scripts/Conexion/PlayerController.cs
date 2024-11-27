using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    void Update()
    {
        // Solo el jugador local puede controlar su avatar
        if (photonView.IsMine)
        {
            ProcessInputs();
        }
    }

    void ProcessInputs()
    {
        // Aquí va la lógica de control del jugador
        // Ejemplo: mover el jugador usando el teclado
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * Time.deltaTime * 5f);
    }
}
