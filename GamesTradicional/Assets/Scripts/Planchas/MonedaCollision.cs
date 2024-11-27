using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedaCollision : MonoBehaviour
{
    public string moneda1Tag = "Moneda1";
    public string moneda2Tag = "Moneda2";
    public float forceMagnitude = 3f; // Magnitud de la fuerza aplicada, puedes ajustar este valor

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(moneda1Tag) || collision.gameObject.CompareTag(moneda2Tag))
        {
            // Dirección del impacto
            Vector3 impactDirection = collision.relativeVelocity.normalized;

            // Generar una dirección aleatoria
            Vector3 randomDirection = Random.insideUnitSphere;
            randomDirection.y = 0; // Asegurarse de que el movimiento sea solo en el plano XZ

            // Asegurarse de que la dirección aleatoria no sea similar a la dirección del impacto
            while (Vector3.Dot(impactDirection, randomDirection) > 0.5f)
            {
                randomDirection = Random.insideUnitSphere;
                randomDirection.y = 0;
            }

            // Aplicar fuerza a la moneda en la dirección aleatoria
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(randomDirection * rb.mass * forceMagnitude, ForceMode.Impulse); // Ajusta la magnitud de la fuerza
        }
    }
}
