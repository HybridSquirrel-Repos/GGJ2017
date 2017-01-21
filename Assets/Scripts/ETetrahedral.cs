using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETetrahedral : MonoBehaviour {

	void Start ()
    {
        GetComponent<MeshFilter>().mesh = TetrahedralFactory.mesh;
    }
}
