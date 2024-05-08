using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocarconArboles : MonoBehaviour
{
    List<GameObject> treeColliders;

    void Start() {
        // Inicializa la lista de colliders de árboles
        treeColliders = new List<GameObject>();
    }

    void Update() {
    // Radio de detección alrededor del jugador
    float detectionRadius = 10.0f;

    // Obtiene todos los árboles en el radio de detección
    TreeInstance[] trees = Terrain.activeTerrain.terrainData.treeInstances;
    foreach (TreeInstance tree in trees) {
        Vector3 treeWorldPos = Vector3.Scale(tree.position, Terrain.activeTerrain.terrainData.size) + Terrain.activeTerrain.transform.position;
        if (Vector3.Distance(transform.position, treeWorldPos) < detectionRadius) {
            // Si el árbol está dentro del radio de detección, agrega un collider
            GameObject treeCollider = new GameObject("TreeCollider");
            treeCollider.transform.position = treeWorldPos;
            CapsuleCollider collider = treeCollider.AddComponent<CapsuleCollider>();
            collider.radius = 1.0f; // Ajusta el radio y la altura según sea necesario
            collider.height = 5.0f;
        }
    }
}

}
