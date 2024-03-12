using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BSCalcLib
{
    public class LinearEquationsSystem
    {
        public LinearEquationsSystem(int _N)
        {
            m_N = _N;
            m_pMatrix = new Row[_N];
            m_nErrorIndex = _N;

            for (int i = 0; i < m_N; ++i)
            {
                m_pMatrix[i] = new Row();
                m_pMatrix[i].adCells = new double[m_N];
                m_pMatrix[i].nIndex = i;
            }
        }

        public void SetMatrixCell(int _i, int _j, double _dCell)
        {
            Debug.Assert(_i < m_N && _j < m_N);
            m_pMatrix[_j].adCells[_i] = _dCell;
        }
        public double GetMatrixCell(int _i, int _j)
        {
            Debug.Assert(_i < m_N && _j < m_N);
            return m_pMatrix[_j].adCells[_i];
        }

        public void SetFreeVector(int _j, double _dCell)
        {
            Debug.Assert(_j < m_N);
            m_pMatrix[_j].dFreeCell = _dCell;
        }
        public double GetFreeVector(int _j)
        {
            Debug.Assert(_j < m_N);
            return m_pMatrix[_j].dFreeCell;
        }

        // Fill the matrix and the free vector by zeros.
        public void SetZero()
        {
            for (int i = 0; i < m_N; ++i)
            {
                for (int j = 0; j < m_N; ++j)
                    SetMatrixCell(i, j, 0);
                SetFreeVector(i, 0);
            }
        }

        // Implement Gauss' method.
        // Call of the function destroys data in the matrix and in the free vector and constructs data for Solution().
        // Return false on error; you may call GetErrorIndex() to determine index of the equation linearly dependent from equations located above it in the system.
        public bool Solve(double _dMinValue = 1e-20)
        {
            m_nErrorIndex = m_N;

            // The first pass: convert the matrix to upper triangle one.
            for (int k = 0; k < m_N; ++k)
            {
                // Find max element in the k-th column.
                double dMaxValue = 0;
                int nMaxIndex = k;
                for (int j = k; j < m_N; ++j)
                {
                    double fValue = GetMatrixCell(k, j);
                    if (Math.Abs(fValue) > Math.Abs(dMaxValue))
                    {
                        dMaxValue = fValue;
                        nMaxIndex = j;
                    }
                }

                if (Math.Abs(dMaxValue) < Math.Abs(_dMinValue))
                {
                    // Report about degeneration error.
                    m_nErrorIndex = m_pMatrix[nMaxIndex].nIndex;
                    return false;
                }

                if (nMaxIndex != k)
                    SwapRows(nMaxIndex, k);

                // Now, Matrix(k,k) is max cell in k-th right bottom submatrix.
                // Make first column of this submatrix zero (except first element).
                // NOTE: all the cells are untouched indeed as they will not make influence on the further calculations.
                for (int j = k + 1; j < m_N; ++j)
                {
                    double a = GetMatrixCell(k, j) / dMaxValue;
                    for (int i = k + 1; i < m_N; ++i)
                        m_pMatrix[j].adCells[i] -= GetMatrixCell(i, k) * a;
                    m_pMatrix[j].dFreeCell -= GetFreeVector(k) * a;
                }
            }

            // The second pass (backward).
            for (int k = m_N - 1; k >= 0; --k)
            {
                for (int i = k + 1; i < m_N; ++i)
                    m_pMatrix[k].dFreeCell -= GetFreeVector(i) * GetMatrixCell(i, k);
                m_pMatrix[k].dFreeCell /= GetMatrixCell(k, k);
            }

            return true;
        }

        // Solutions are in the free vector.
        public double Solution(int _i) { return GetFreeVector(_i); }
        // Return index of first encountered degenerating equation.
        public int ErrorIndex { get { return m_nErrorIndex; } }

        // The dimension of the system.
        private int m_N;
        public int N { get { return m_N; } }

        private class Row
        {
            public double[] adCells;
            public double dFreeCell;
            public int nIndex;
        };
        private Row[] m_pMatrix;

        private int m_nErrorIndex;
        private void SwapRows(int _j1, int _j2)
        {
            Row tmp = m_pMatrix[_j1];
            m_pMatrix[_j1] = m_pMatrix[_j2];
            m_pMatrix[_j2] = tmp;
        }
    }
}
