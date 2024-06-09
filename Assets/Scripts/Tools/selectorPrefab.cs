using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class selectorPrefab : MonoBehaviour
{
    // Lista de prefabs disponibles
    public List<GameObject> prefabsDisponibles = new List<GameObject>();
    // Índice del prefab seleccionado
    public int indicePrefabSeleccionado = 0;
    // Método para cambiar el prefab seleccionado
    public void CambiarPrefab(int nuevoIndice)
    {
        // Verificamos si el nuevo índice está dentro de los límites de la lista
        if (nuevoIndice >= 0 && nuevoIndice < prefabsDisponibles.Count)
        {
            indicePrefabSeleccionado = nuevoIndice;
            // Llamamos a la función para actualizar la visualización del objeto
            ActualizarVisualizacion();
        }
    }

    // Método para actualizar la visualización del objeto con el prefab seleccionado
    [ContextMenu("Actualizar")]
    public void ActualizarVisualizacion()
    {
        // Eliminamos el objeto actual
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Instanciamos el nuevo prefab seleccionado
        GameObject nuevoObjeto = Instantiate(prefabsDisponibles[indicePrefabSeleccionado], transform.position, Quaternion.identity);
        // Lo colocamos como hijo del objeto actual para que se renderice en su posición
        nuevoObjeto.transform.parent = transform;
    }

    // Método llamado al iniciar la escena
    private void Start()
    {
        // Llamamos a la función para actualizar la visualización del objeto al inicio del juego
        ActualizarVisualizacion();
    }
}
