/*
 * 4D edge, that connects two 4D vertices, is the core data structure of the project
 * Independent from Unity.
 * Exactly here we compute intersections!
 * 
 * @daihaminkey, 27.12.2018
 */

using System;

namespace Intersection
{

    /// <summary>
    ///		Edge between two 4D vertices
    /// </summary>
    public class Edge4D
    {
        /// <summary>
        ///		Edge limiting 4D vertex. Has lower <see cref="Vertex4D.w"/>-value, than <see cref="B"/>
        /// </summary>
        public Vertex4D A;

        /// <summary>
        ///		Edge limiting 4D vertex. Has higher <see cref="Vertex4D.w"/>-value, than <see cref="A"/>
        /// </summary>
        public Vertex4D B;

        /// <summary>
        ///		Creates edge. <see cref="A"/> would have lower <see cref="Vertex4D.w"/>-value,
        /// then <see cref="B"/>, with no respect of parameters order
        /// </summary>
        /// <param name="A">First limiting edge</param>
        /// <param name="B">Second limiting edge</param>
        public Edge4D(Vertex4D A, Vertex4D B)
        {
			if (A == null)
                throw new ArgumentNullException("A must be not null");
            else if (B == null)
                throw new ArgumentNullException("B must be not null");

            if (A.w <= B.w)
            {
                this.A = A;
                this.B = B;
            }
            else
            {
                this.A = B;
                this.B = A;
            }
        }

        /// <summary>
        ///		Intersection of 4D edge in 3D space is a point
        /// </summary>
        /// <param name="w">Depth on forth dimention</param>
        /// <returns>4D point, located at given depth <see cref="w"/></returns>
        public Vertex4D Intersect(float w)
        {
            if (!IsIntersection(w))
                return null;

            if (A.w == w)
                return A;

            if (B.w == w)
                return B;

            // Here we do intersection: evaluating of equasion of the line with respect of parameter w

            float t = (w - A.w) / (B.w - A.w);
            /* Here we can't get division by zero. It appears only when B.w = A.w,
             * but in this case only w param, which isIntercection, would be w = B.w = A.w
             * and we return it in if-else statement
             */

            float x = A.x + (B.x - A.x) * t;
            float y = A.y + (B.y - A.y) * t;
            float z = A.z + (B.z - A.z) * t;

            return new Vertex4D(x, y, z, w);
        }

        /// <summary>
        ///		Checks, is <see cref="w"/> in interval between <see cref="A"/> and <see cref="B"/> of this edge
        /// </summary>
        /// <param name="w"></param>
        /// <returns>
        ///     <list type="bullet">
        ///        <item>
        ///            <description>true - inside interval</description>
        ///        </item>
        ///        <item>
        ///            <description>false - outside interval</description>
        ///        </item>
        ///     </list>
        /// </returns>
        public bool IsIntersection(float w)
        {
            return A.w <= w && w <= B.w;
        }
    }
}