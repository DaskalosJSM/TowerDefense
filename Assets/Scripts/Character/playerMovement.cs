using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static Vector3 currentTilePosition;
    public Vector3 currentTile;
    // Asegúrate de asignar la capa de los tiles en el inspector
    // Variable para almacenar la información del Raycast
    private RaycastHit hit;
    public float RayDistance = 2;

    void Update()
    {
        DetectTileUnderPlayer();
        currentTile = currentTilePosition;
    }

    void DetectTileUnderPlayer()
    {
        Ray ray = new Ray(transform.position, Vector3.down*RayDistance);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Transform tileTransform = hit.transform;
            currentTilePosition = tileTransform.position;
        }
    }

    void OnDrawGizmos()
    {
        // Si estamos en el modo de juego, dibujar el Raycast
        
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * RayDistance);

            // Dibujar una esfera en el punto de impacto
            if (hit.collider != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(hit.point, 0.2f);
            }
    }
}
