/*
 * Vector with 4 coordinates. Point in 4D space.
 * Independent from Unity.
 * 
 * @daihaminkey, 27.12.2018
 */

using System;

namespace Intersection
{

    /// <summary>
    /// Point in 4D space
    /// </summary>
    public class Vertex4D
    {
        public float x, y, z, w;

        /// <summary>
        /// Creates 4D vertex with zeroes in all coordinates
        /// </summary>
        public Vertex4D()
        {
            x = y = z = w = 0;
        }

        /// <summary>
        /// Creates 4D vertex
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        /// <param name="w">W coordinate</param>
        public Vertex4D(float x, float y, float z, float w = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }
}