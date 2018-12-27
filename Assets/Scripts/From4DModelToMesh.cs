/*
 * Another row and temporary Unity scripts. It contains data about simple 4D model, 
 * converts it to 3D model (yet without intersection, because Edge-based triangle generator isn't exist),
 * and renders it as a Unity Mesh (for the sake of simplisity - each frame) 
 * 
 * Also it contains first approach of Universal -> Unity format Vertex4D conversion,
 * done with extension methods
 * 
 * @daihaminkey, 27.12.2018
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Intersection
{

    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class From4DModelToMesh : MonoBehaviour
    {

        private Vector3[] vertices;

        private Mesh mesh;

        Model4D tempModel = new Model4D();

        void Start()
        {
            
        }

        void Update()
        {
            Generate();
        }

        private void Awake()
        {
            Generate();
        }

        private void Generate()
        {
            GetComponent<MeshFilter>().mesh = mesh = new Mesh();
            mesh.name = "Procedural Grid";

            mesh.vertices = tempModel.vertices.Get3DVertices();
            mesh.triangles = tempModel.triangles;
            mesh.RecalculateNormals();
        }

    }

    public static class Unity4DConvertors
    {
        public static Vector3 GetVector3(this Vertex4D v4d)
        {
            return new Vector3(v4d.x, v4d.y, v4d.z);
        }

        public static Vector3[] Get3DVertices(this Vertex4D[] arr)
        {
            Vector3[] ret = new Vector3[arr.Length];

            for (int i = 0; i < arr.Length; ++i)
                ret[i] = arr[i].GetVector3(); 

            return ret;
        }
    }
}