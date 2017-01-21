using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrahedral : MonoBehaviour {

    public Material material;
	void Start ()
    {

        gameObject.AddComponent<MeshFilter>();
        var renderer = gameObject.AddComponent<MeshRenderer>();
        GetComponent<MeshFilter>().mesh = TetrahedralFactory.mesh;
        renderer.material = material;
    }
}
