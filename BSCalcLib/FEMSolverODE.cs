using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BSCalcLib
{
                
    public class FEMSolverODE
    {
        public List<double> X 
        { 
            set 
            { 
                m_X = value.ToArray(); 
                NPoints = m_X.Length;
                NElem = NPoints - 1;
            } 
        }
  
                private static int NPoints;
                private static int NElem;
                private double[] m_X = new double[NPoints];
        private const int ElPoints = 2;
                double[,] STE = new double[ElPoints, ElPoints];
                double[,] ST = new double[NPoints, NPoints];
                double[] RHS = new double[NPoints];
        static FEMSolverODE()
        {
            NPoints = 7;
            NElem = NPoints-1;
        }
                private void InitToZeros()
        {
            for (int i = 0; i < NPoints; i++) 
            {
                RHS[i] = 0;
                for (int j = 0; j<NPoints; j++)
                {
                    ST[i, j] = 0;                    
                }
            }
        }
                private void InitSTMatrix()
        {
            for (int i = 0; i < NElem; i++)
            {
                double coef = m_X[i + 1] - m_X[i];
                STE[0,0] = coef/3.0 + 1.0/coef;
                STE[0,1] = coef/6.0 - 1.0 / coef;
                STE[1, 0] = STE[0, 1];
                STE[1,1] = STE[0, 0];
                
                ST[i, i] += STE[0, 0];
                ST[i, i+1] += STE[0, 1];
                ST[i+1, i] += STE[1, 0];
                ST[i+1, i+1] += STE[1, 1];
            }
        }
                private void InitBC()
        {
            List<int> bounds = new List<int>() {0, NPoints-1 } ;
            foreach (int i in bounds) 
            {
                for (int j = 0; j < NPoints; j++)
                {
                    ST[i, j] = 0.0;
                }
                ST[i, i] = 1.0;
                RHS[i] = Math.Exp(m_X[i]);
            }
            
        }
        public List<double> Run()
        {
            InitToZeros();
            InitSTMatrix();
            InitBC();
            LinearEquationsSystem system = new LinearEquationsSystem(NPoints);
            for (int j=0; j< NPoints; j++)
                system.SetFreeVector(j, RHS[j]);
            for (int i = 0; i < NPoints; i++)
                for (int j = 0; j < NPoints; j++)
                    system.SetMatrixCell(j, i, ST[i, j]);
            system.Solve();
            List<double> Z = new List<double>();
            for (int i = 0; i < NPoints; i++)
                Z.Add(system.Solution(i));
            return Z;
        }
        public static List<double> RunFromCode()
        {
            List<double> _X = new List<double> {0.0, 0.1, 0.3, 0.6, 1.0, 1.5, 2.0 };
            FEMSolverODE solverODE = new FEMSolverODE();
            solverODE.X = _X;
            var Z = solverODE.Run();
            return Z;
        }
      
    }
}
