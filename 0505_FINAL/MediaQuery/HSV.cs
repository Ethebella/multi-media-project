using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaQuery
{
    public class HSV
    {
        private float m_hue;
        private float m_saturation;
        private float m_value;

        public HSV(float hue, float saturation, float value)
        {
            m_hue = hue;
            m_saturation = saturation;
            m_value = value;
        }

        public float Hue
        {
            get { return m_hue; }
            set { m_hue = value; }
        }

        public float Saturation
        {
            get { return m_saturation; }
            set { m_saturation = value; }
        }

        public float Value
        {
            get { return m_value; }
            set { m_value = value; }
        }
    }
}
