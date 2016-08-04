using UnityEngine;
using System.Collections;

public class MeshAnalyzer : MonoBehaviour {

	void Start () {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.sharedMesh;
        Debug.Log("Mesh " + mesh.name + " consists of "  + mesh.subMeshCount + " submeshes");
        for (int i = 0; i < mesh.subMeshCount; i++) {
            Debug.Log("Submesh " + i + " has " + mesh.GetTriangles(i).Length);
        }
	}
	

	void Update () {
	
	}
}
