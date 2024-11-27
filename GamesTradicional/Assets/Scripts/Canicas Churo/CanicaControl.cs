using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class CanicaControl : MonoBehaviourPun
{
    public string terrainTag = "Terrain2"; // El tag del terreno
    public string detectorTag = "Detectorinicio"; // Tag del cubo detector
    private XRGrabInteractable grabInteractable;
    private bool hasTouchedTerrain = false;
    private bool hasTouchedDetector = false;
    private Vector3 initialPosition;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        initialPosition = transform.position; // Guardar la posición inicial
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(terrainTag) && !hasTouchedTerrain && !hasTouchedDetector)
        {
            hasTouchedTerrain = true;
            StartCoroutine(ReturnToInitialPosition());
        }
    }

    IEnumerator ReturnToInitialPosition()
    {
        // Esperar 3 segundos
        yield return new WaitForSeconds(3f);

        // Regresar a la posición inicial si no ha tocado el detector
        if (!hasTouchedDetector)
        {
            transform.position = initialPosition;
            hasTouchedTerrain = false; // Resetear para permitir otra detección de terreno
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(detectorTag))
        {
            hasTouchedDetector = true;

            // Desactivar la interacción con la canica
            grabInteractable.enabled = false;
        }
    }
}
