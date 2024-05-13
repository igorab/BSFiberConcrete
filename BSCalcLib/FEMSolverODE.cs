using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSCalcLib
{

    /// <summary>
    /// Finite element method
    /// Solution of an ODE
    /// </summary>

    public class FEMSolverODE
    {
        public List<double> X { set { m_X = value.ToArray(); } }
  

        // total number of nodes
        private static int NPoints;
        // total number of elements
        private static int NElem;
        // x-coordinate of node I
        private double[] m_X = new double[NPoints];

        // the M-N-th element of the element X matrix, M and N being node identifiers
        double[,] STE = new double[NPoints, NPoints];

        // the I-J element of the assembled system K matrix
        double[,] ST = new double[NPoints, NPoints];

        //  Right-hand side matrix in the system matrix equasion
        double[,] RHS = new double[NPoints, NPoints];


        static FEMSolverODE()
        {
            NPoints = 3;
            NElem = 2;
        }

        //The system K matrix and the Right-hand side matrix are initilized to Zero
        private void InitSTRHS()
        {
            for (int i = 0; i < NPoints; i++) 
            {
                for (int j = 0; j<NPoints; j++)
                {
                    ST[i, j] = 0;
                    RHS[i, j] = 0;
                }
            }
        }

        // the element K Matrices atr obtained and assembled for all elements and the system k matrix is obtained
        private void InitSTEST()
        {
            for (int i = 0; i < NElem; i++)
            {
                double coef = m_X[i + 1] - m_X[i];

                STE[1,1] = coef/3.0 + 1.0/coef;
                STE[1,2] = coef/6.0 - 1.0 / coef;
                STE[2, 2] = STE[1, 1];
                STE[2, 1] = STE[1, 2];

                ST[i, i] += STE[1, 1];
                ST[i, i+1] += STE[1, 2];
                ST[i+1, i] += STE[2, 1];
                ST[i+1, i+1] += STE[2, 2];
            }
        }

        // the Dirichlet boundary conditions are inserted in the right-hand side matrix and the system k matrix is corrected
        private void InitBC()
        {
            for (int i=0; i < NPoints; i ++)
            {
                for (int j = 0; j < NPoints; j++)
                {
                    ST[i, j] = 0.0;
                }

                ST[i, i] = 1.0;
                RHS[i, 1] = Math.Exp(m_X[i]);
            }
        }

        public static void RunFromCode()
        {
            List<double> _X = new List<double> {0.0, 0.1, 0.3, 0.6, 1.0, 1.5, 2.0 };

            FEMSolverODE solverODE = new FEMSolverODE();
            solverODE.X = _X;
            solverODE.Run();
        }

        public void Run()
        {
            InitSTRHS();

            InitSTEST();

            InitBC();
        }
    }
}
