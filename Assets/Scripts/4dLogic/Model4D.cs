/*
 * First approach of 4D model representation.
 * Independent from Unity.
 * Just a mock, still in development
 * 
 * @daihaminkey, 27.12.2018
 */

using System;

namespace Intersection
{

    public class Model4D
    {
        public Vertex4D[] vertices;
        public int[] triangles;

        /// <summary>
        /// This class, hipoteticly, will discribe 4D model
        /// But now is just mick with simple square
        /// </summary>
        public Model4D()
        {
            //Square, located at w = 0
            Vertex4D v0 = new Vertex4D(0, 0, 0, 0);
            Vertex4D v1 = new Vertex4D(-1, 1, 0, 0);
            Vertex4D v2 = new Vertex4D(1, 1, 0, 0);
            Vertex4D v3 = new Vertex4D(0, 2, 0, 0);

            //unused
            //TODO need to create some map from edges to triangles on 4D
            Edge4D e0 = new Edge4D(v0, v1);
            Edge4D e1 = new Edge4D(v0, v2);

            Edge4D e2 = new Edge4D(v2, v1);

            Edge4D e3 = new Edge4D(v2, v3);
            Edge4D e4 = new Edge4D(v1, v3);


            vertices = new Vertex4D[] { v0,v1,v2,v3 };

            triangles = new int[]
            {
                0, 1, 2,
                2, 1, 3
            };

        }
    }
}