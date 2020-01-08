using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomClasses
{
    public class MeshCreator : MonoBehaviour
    {
        void Start()
        {
            Mesh mesh = new Mesh();

            Vector3[] vertices = new Vector3[8];
            Vector2[] uv = new Vector2[8];
            int[] triangles = new int[36];

            //vertices[0] = new Vector3(0, 0);
            //vertices[1] = new Vector3(0, 1);
            //vertices[2] = new Vector3(1, 1);
            //vertices[3] = new Vector3(1, 0);

            vertices[0] = new Vector3(-0.5f, -0.5f, -0.5f);
            vertices[1] = new Vector3(-0.5f, 0.5f, -0.5f);
            vertices[2] = new Vector3(0.5f, 0.5f, -0.5f);
            vertices[3] = new Vector3(0.5f, -0.5f, -0.5f);

            vertices[7] = new Vector3(0.5f, -0.5f, 0.5f);
            vertices[6] = new Vector3(0.5f, 0.5f, 0.5f);
            vertices[5] = new Vector3(-0.5f, 0.5f, 0.5f);
            vertices[4] = new Vector3(-0.5f, -0.5f, 0.5f);


            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(0, 1);
            uv[2] = new Vector2(1, 1);
            uv[3] = new Vector2(1, 0);

            uv[4] = new Vector2(0, 0);
            uv[5] = new Vector2(0, 1);
            uv[6] = new Vector2(1, 1);
            uv[7] = new Vector2(1, 0);


            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            triangles[3] = 0;
            triangles[4] = 2;
            triangles[5] = 3;

            triangles[6] = 7;
            triangles[7] = 6;
            triangles[8] = 5;
            triangles[9] = 7;
            triangles[10] = 5;
            triangles[11] = 4;

            triangles[6] = 7;
            triangles[7] = 6;
            triangles[8] = 5;
            triangles[9] = 7;
            triangles[10] = 5;
            triangles[11] = 4;

            triangles[12] = 4;
            triangles[13] = 5;
            triangles[14] = 1;
            triangles[15] = 4;
            triangles[16] = 1;
            triangles[17] = 0;

            triangles[18] = 3;
            triangles[19] = 2;
            triangles[20] = 6;
            triangles[21] = 3;
            triangles[22] = 6;
            triangles[23] = 7;

            triangles[24] = 1;
            triangles[25] = 5;
            triangles[26] = 6;
            triangles[27] = 1;
            triangles[28] = 6;
            triangles[29] = 2;

            triangles[30] = 3;
            triangles[31] = 7;
            triangles[32] = 4;
            triangles[33] = 3;
            triangles[34] = 4;
            triangles[35] = 0;


            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}