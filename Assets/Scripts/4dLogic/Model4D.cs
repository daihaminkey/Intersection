/*
 * First approach of 4D model representation.
 * Independent from Unity.
 * Just a mock yet, still in development
 * 
 * @daihaminkey, 31.01.2019
 */

using System;

namespace Intersection
{

    public class Model4D
    {
		/// <summary>
		///		Array of 4D lines, that converts to points on intersection
		/// </summary>
        private Edge4D[] interPoints;

		/// <summary>
		///		Sequences of 3 numbers, used to describe polygons
		/// </summary>
        public int[] interTriangles;

		/// <summary>
		///		Last intersected W-coordinate
		/// </summary>
        private float lastW;

		/// <summary>
		///		Last intersection, generated on <see cref="lastW"/>
		/// </summary>
        private Vertex4D[] lastIntersection = null;


		/// <summary>
		///		This class, hipoteticly, will describe any 4D model
		///		But now is just mick with simple interdimensional square
		/// </summary>
		public Model4D()
        {
            //Square, located at w = 0
            Vertex4D v00 = new Vertex4D(0, 0, 0, 0);
            Vertex4D v01 = new Vertex4D(-1, 1, 0, 0);
            Vertex4D v02 = new Vertex4D(1, 1, 0, 0);
            Vertex4D v03 = new Vertex4D(0, 2, 0, 0);

            //Large square, located at w = 1
            Vertex4D v10 = new Vertex4D(0, 0, 0, 1);
            Vertex4D v11 = new Vertex4D(-2, 2, 0, 1);
            Vertex4D v12 = new Vertex4D(2, 2, 0, 1);
            Vertex4D v13 = new Vertex4D(0, 4, 0, 1);

			
			//4D edges, that on intersection generate square at custom w
			Edge4D e0 = new Edge4D(v00, v10);
            Edge4D e1 = new Edge4D(v01, v11);
            Edge4D e2 = new Edge4D(v02, v12);
            Edge4D e3 = new Edge4D(v03, v13);


            interPoints = new Edge4D[] { e0,e1,e2,e3 };

            interTriangles = new int[]
            {
                0, 1, 2,
                2, 1, 3
            };

        }

		/// <summary>
		///		<para>Generates new intersection, or returns last generated, depends on value of w</para>
		///		<para>Return tuple with vertices and update status (new or old vertices used)</para>
		/// </summary>
		/// <param name="w">depth of W</param>
		/// <returns>
		///		<para>Tuple</para>
		///		<para>.vertices - array with vertices</para>
		///		<para>.updated - is vertices old, or newly generated</para>
		/// </returns>
		public (Vertex4D[] vertices, bool updated) GetIntersection(float w)
        {
	        bool update = lastIntersection == null || w != lastW;
	        if (update)
	        {
		        lastW = w;
				lastIntersection = new Vertex4D[this.interPoints.Length];

		        for (int i = 0; i < this.interPoints.Length; ++i)
			        lastIntersection[i] = this.interPoints[i].Intersect(w);
	        }

	        return (lastIntersection, update);
        }
    }
}