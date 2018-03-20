using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Parametric_Classification
{
    class Classifier
    {

        //TODO: Transform calculatemean to matrix reutnrs

        /// <summary>
        /// Takes data class by class basis, calculates means for a single class.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Matrix<double> Mean(List<Data> data)
        {
            //Records values for each attribute
            double[] attributeValues = new double[data[0].Attributes.Count];

            


            foreach (Data d in data)
            {
                for (int i = 0; i < attributeValues.Length; i++)
                {
                    attributeValues[i] += d.Attributes[i];
                }
            }

            for (int i = 0; i < attributeValues.Length; i++)
            {
                attributeValues[i] = attributeValues[i] / data.Count;
            }

            Matrix<double> Mean = Matrix<double>.Build.Dense(1, 2);
            Mean[0, 0] = attributeValues[0];
            Mean[0, 1] = attributeValues[1];

            return Mean;
        }


        /// <summary>
        /// Computes Covariance matrix per class basis, calculates covariance matrix for a single class
        /// </summary>
        /// <param name="data"></param>
        public static Matrix<double> Covariance(List<Data> data)
        {

            //TODO: Change values to accomidate 4 class ones
            //double[,] covMatrix = new double[data.Count, data[0].Attributes.Count];

            Matrix<double> Covariance = Matrix<double>.Build.Dense(2,2);

            double meanAtt1 = Mean(data)[0,0];
            double meanAtt2 = Mean(data)[0,1];

            foreach (Data d in data)
            {
                Matrix<double> m = Matrix<double>.Build.Dense(2, 1);
                m[0, 0] = (d.Attributes[0] - meanAtt1); //value for x1
                m[1, 0] = (d.Attributes[1] - meanAtt2); //value for x2

                var m_transpose = m.Transpose();
                var mult = m * m_transpose;

                Covariance = Covariance + mult;
            }
            return Covariance;
        }


        /// <summary>
        /// Calculates Gi for a class per class basis, calculates Gi(x) for a single class
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public static Matrix<double> Gi(List<Data> data, int rows, int cols)
        {
            Matrix<double> M = Matrix<double>.Build.Dense(rows, cols);

            var t = (-0.5) * ((M - Mean(data)).Transpose()) * (Covariance(data).Inverse()) - Math.Log(Math.PI * 2) - 0.5* Math.Log(Covariance(data).Determinant()) + Math.Log(0.5);

            return t;
        }


    }
}
