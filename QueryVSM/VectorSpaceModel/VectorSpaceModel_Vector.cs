using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryVSM
{
    partial class VectorSpaceModel
    {
        // Function: Calculate cosine form two given vector
        private double vector_cos(double[] v1, double[] v2)
        {
            // Checking
            if (v1.Length != v2.Length)
            {
                throw new Exception("Vector size is not match!");
            }

            return this.vector_dot(v1, v2) / (this.vector_abs(v1) * this.vector_abs(v2));
        }

        // Function: Calculate dot operation with given vectors
        private double vector_dot(double[] v1, double[] v2)
        {
            // Checking
            if (v1.Length != v2.Length)
            {
                throw new Exception("Vector size is not match!");
            }

            // Calculate
            double dot = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                dot += v1[i] * v2[i];
            }

            return dot;
        }

        // Function: Calculate absolute value with given vectors
        private double vector_abs(double[] v)
        {
            return Math.Sqrt(this.vector_dot(v, v));
        }
    }
}
