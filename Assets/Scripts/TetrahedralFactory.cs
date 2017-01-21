using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrahedralFactory : MonoBehaviour
{
    public static Mesh mesh;

    void Start()
    {
        mesh = new Mesh();
        Vector3 p0 = new Vector3(0, 0, 0);
        Vector3 p1 = new Vector3(1, 0, 0);
        Vector3 p2 = new Vector3(0.5f, 0, Mathf.Sqrt(0.75f));
        Vector3 p3 = new Vector3(0.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);

        mesh.Clear();
    
        mesh.vertices = new Vector3[] { p0, p1, p2, p3 };
        mesh.triangles = new int[] { 0,1,2,
                                     0,2,3,
                                     2,1,3,
                                     0,3,1 };

        // Optimize. //
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}