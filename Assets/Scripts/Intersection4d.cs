/*
 * This Unity script is very raw proof-of-concept
 * It creates 4D figure with ability of 4D-intersection
 * It is really row, not clean at all, and we will absolutly delete it asap
 * But now it is the only workig prototype :(
 * Independent from other files, you can just drag-and-drop it on empty Unity object.
 * @daihaminkey, 27.12.2018
 */

using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Intersection4d: MonoBehaviour
{
    private Vector3[] vertices;

    private Mesh mesh;


    [Range(0f, 3f)]
    public float interW = 1f;

    [Range(0f, 6.27f)]
    public float rotate = 0f;

    void Start()
    {

    }

    void Update()
    {
        /*rotate += .03f;
        if (rotate > 6.26)
            rotate = 0;*/
        Generate();
    }

    

    private Vector3 IntersectX(Vector3 a, Vector3 b, float x)
    {

        float t = (x - a.x) / (b.x - a.x);

        float y = a.y + (b.y - a.y) * t;
        float z = a.z + (b.z - a.z) * t;

        Vector3 c = new Vector3(x, y, z);
        Debug.Log(c);
        return c;
    }

    private Vector4 IntersectW(Vector4 a, Vector4 b, float w)
    {

        float t = (w - a.w) / (b.w - a.w);

        float x = a.x + (b.x - a.x) * t;
        float y = a.y + (b.y - a.y) * t;
        float z = a.z + (b.z - a.z) * t;



        Vector4 c = new Vector4(x, y, z, w);
        //c.x = x * Mathf.Cos(rotate) - w * Mathf.Sin(rotate);
        //c.w = x * Mathf.Sin(rotate) + w * Mathf.Cos(rotate);

        float offset = Mathf.Abs(a.x - b.x) / 2;

        
        //c.w -= offset;


        //c.y = y * Mathf.Cos(rotate) - c.z * Mathf.Sin(rotate);
       // c.z = y * Mathf.Sin(rotate) + c.z * Mathf.Cos(rotate);

       // c.x -= 1;
        //c.y -= 1;
        //c.z -= 1;

        return c;
    }

    Vector4 Rotate(Vector4 vec)
    {
        float x = vec.x * Mathf.Cos(rotate) - vec.z * Mathf.Sin(rotate);
        float y = vec.y;
        float z = vec.x * Mathf.Sin(rotate) + vec.z * Mathf.Cos(rotate);
        float w = vec.w;

        return new Vector4(x,y,z,w);
    }

    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        Vector4 a11 = new Vector4(-3, -3, -3, 0);
        Vector4 a12 = new Vector4( 3, -3, -3, 0);
        Vector4 a13 = new Vector4(-3, -3,  3, 0);
        Vector4 a14 = new Vector4( 3, -3,  3, 0);

        Vector4 a21 = new Vector4(-3, 3, -3, 0);
        Vector4 a22 = new Vector4( 3, 3, -3, 0);
        Vector4 a23 = new Vector4(-3, 3,  3, 0);
        Vector4 a24 = new Vector4( 3, 3,  3, 0);

        Vector4 b1 = new Vector4(  -4,0,0,3);
        Vector4 b2 = new Vector4(4.6f,0,-5,3);
        Vector4 b3 = new Vector4(4.6f,0,5,3);

        Vector4 b4 = new Vector4(0,5,0,3);



        Vector4 c11 = IntersectW(b1, a11, interW);//0
        Vector4 c21 = IntersectW(b1, a21, interW);//1
        Vector4 c12 = IntersectW(b2, a12, interW);//2
        Vector4 c22 = IntersectW(b2, a22, interW);//3
        Vector4 c13 = IntersectW(b3, a13, interW);//4
        Vector4 c23 = IntersectW(b3, a23, interW);//5
        Vector4 c14 = IntersectW(b4, a14, interW);//6
        Vector4 c24 = IntersectW(b4, a24, interW);//7

        Vector4[] vertices4d = new Vector4[] 
        {
            //a11, a12, a13, a14,
            //a21, a22, a23, a24,
            //b11, b12, b13, b14,
           // b21, b22, b23, b24,
           // c11, c12, c13, c14,
           // c21, c22, c23, c24,
            c11, c12,
            c13, c14,
            c21, c22,
            c23, c24,
            //b1, b2, b3, b4,
        };

        vertices = (from Vector4 v4 in vertices4d
                   select v4.getVec3()).ToArray();

        mesh.vertices = vertices;

        int[] triangles = new int[]
        {
            0, 2, 4,
            2, 6, 4,

            5, 3, 1,
            5, 7, 3,

            0, 1, 2,
            3, 2, 1,

            //6, 5, 4,
            //5, 6, 7,
            4, 7, 5,
            7,4,6,

            5, 1, 0,
            5, 0, 4,

            2, 3, 7,
            2, 7, 6,
        };

        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }


        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }

    public class Vector4
    {
        public float x = 0,
                     y = 0,
                     z = 0,
                     w = 0;

        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static Vector4 operator+ (Vector4 self, float offset)
        {
            return new Vector4(self.x + offset, self.y + offset, self.z + offset, self.w);
        }

        public Vector3 getVec3()
        {
            return new Vector3(x,y,z);
        }
    }
}
