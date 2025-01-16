using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public Material VisionConeMaterial;
    public float VisionRange;
    public float VisionAngle;
    public LayerMask VisionObstructingLayer;
    public int VisionConeResolution = 120;
    Mesh VisionConeMesh;
    MeshFilter MeshFilter_;
    void Start()
    {
        transform.AddComponent<MeshRenderer>().material = VisionConeMaterial;
        MeshFilter_ = transform.AddComponent<MeshFilter>();
        VisionConeMesh = new Mesh();
        VisionAngle *= Mathf.Deg2Rad;
    }

    
    void Update()
    {
        DrawVisionCone();
    }

    void DrawVisionCone()
    {
        int[] triangles = new int[(VisionConeResolution - 1) * 3];
        Vector3[] vertices = new Vector3[VisionConeResolution + 1];
        vertices[0] = Vector3.zero; // Base du cône à l'origine (espace local)

        float currentAngle = -VisionAngle / 2;
        float angleIncrement = VisionAngle / (VisionConeResolution - 1);

        for (int i = 0; i < VisionConeResolution; i++)
        {
            // Calcul direction du rayon en espace monde
            float sine = Mathf.Sin(currentAngle);
            float cosine = Mathf.Cos(currentAngle);
            Vector3 raycastDirection = (transform.forward * cosine) + (transform.right * sine);

            // Effectuer le raycast
            if (Physics.Raycast(transform.position, raycastDirection, out RaycastHit hit, VisionRange, VisionObstructingLayer))
            {
                // Convertir en espace local
                vertices[i + 1] = transform.InverseTransformPoint(hit.point);
            }
            else
            {
                // Si pas d'obstacle, placer au bout du cône (en espace local)
                Vector3 endPoint = transform.position + raycastDirection * VisionRange;
                vertices[i + 1] = transform.InverseTransformPoint(endPoint);
            }

            currentAngle += angleIncrement;
        }

        // Générer les triangles du mesh
        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }

        // Mise à jour du mesh
        VisionConeMesh.Clear();
        VisionConeMesh.vertices = vertices;
        VisionConeMesh.triangles = triangles;
        VisionConeMesh.RecalculateNormals();
        MeshFilter_.mesh = VisionConeMesh;
    }
}