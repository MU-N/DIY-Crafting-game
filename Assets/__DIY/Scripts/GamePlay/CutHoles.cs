using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using System;

public class CutHoles : MonoBehaviour
{
    [SerializeField] int xSize = 20;
    [SerializeField] int zSize = 20;
    [Range(0.25f, 1f)]
    [SerializeField] float ceilSize = 1;


    private Mesh mesh;
    private Vector3[] vertices;
    private int[] trinagles;
    void Start()
    {
        mesh = new Mesh();
        mesh = GetComponent<MeshFilter>().mesh;
        CreateShape();
        UpdateMesh();
    }



    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                FindObjectOfType<AudioManager>().Play("Click");
                DeleteTrinagles(hit.triangleIndex);
                Debug.Log(hit.triangleIndex);

            }
        }
    }
    private void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int index = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                vertices[index] = new Vector3(x * ceilSize, 0, z * ceilSize);
                index++;
            }
        }

        trinagles = new int[xSize * zSize * 6];
        for (int ti = 0, vi = 0, z = 0; z < zSize; z++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                trinagles[ti] = vi;
                trinagles[ti + 3] = trinagles[ti + 2] = vi + 1;
                trinagles[ti + 4] = trinagles[ti + 1] = vi + xSize + 1;
                trinagles[ti + 5] = vi + xSize + 2;
            }
        }
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = trinagles;
        mesh.RecalculateNormals();
        gameObject.AddComponent<MeshCollider>();
    }
    private void DeleteTrinagles(int index)
    {
        Destroy(gameObject.GetComponent<MeshCollider>());
        mesh = GetComponent<MeshFilter>().mesh;
        int[] oldTringles = mesh.triangles;
        int[] newTringles = new int[mesh.triangles.Length - 3];
        int i = 0, j = 0;
        while (j < mesh.triangles.Length)
        {
            if (j != index * 3)
            {
                newTringles[i++] = oldTringles[j++];
                newTringles[i++] = oldTringles[j++];
                newTringles[i++] = oldTringles[j++];
            }
            else
            {
                j += 3;
            }
        }
        GetComponent<MeshFilter>().mesh.triangles = newTringles;
        gameObject.AddComponent<MeshCollider>();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}