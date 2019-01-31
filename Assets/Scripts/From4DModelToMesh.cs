/*
 * Another row and temporary Unity script. It contains data about simple 4D model, 
 * converts it to 3D model with respect to intersection, and renders it as a Unity Mesh 
 * 
 * Also it contains first approach of Universal -> Unity format Vertex4D conversion,
 * done with extension methods
 * 
 * @daihaminkey, 31.01.2019
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
	    [Range(0f, 1f)]
	    public float interW = 1f;

		private Vector3[] vertices;

        private Mesh mesh;

		//Should be loaded external, but now contains mock, so it's okay
        Model4D model = new Model4D();

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
			var intersect = model.GetIntersection(interW);

			if (intersect.updated)
			{
				GetComponent<MeshFilter>().mesh = mesh = new Mesh();
				mesh.name = "Procedural Grid";
				mesh.vertices = intersect.vertices.Get3DVertices();
				mesh.triangles = model.interTriangles;
				mesh.RecalculateNormals();
			}
        }

        private void OnDrawGizmos()
        {
            if (vertices == null)
            {
                return;
            }


            Gizmos.color = Color.black;
            foreach (var v in vertices)
	            Gizmos.DrawSphere(v, 0.1f);
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