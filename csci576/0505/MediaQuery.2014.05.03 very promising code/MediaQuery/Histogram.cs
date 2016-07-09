using System;
using System.Collections.Generic;
using System.Text;

namespace MediaQuery
{
    public class Histogram
    {
        private float[] m_data;

        public Histogram(int binCount)
        {
            m_data = new float[binCount];
        }

        public int Size
        {
            get { return m_data.Length; }
        }

        public void SetElement(int i, float value)
        {
            m_data[i] = value;
        }

        public float GetElement(int i)
        {
            return m_data[i];
        }

        public float[] Data
        {
            get { return m_data; }
        }

        public void Normalise(float total)
        {
            for (int i = 0; i < m_data.Length; i++)
            {
                m_data[i] = m_data[i] / total;
            }
        }

        
    }
}
