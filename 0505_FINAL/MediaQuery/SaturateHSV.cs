using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaQuery
{
    class SaturateHSV
    {
        enum Colors { cBLACK = 0, cWHITE, cGREY, cRed, cRed_L, cWarmRed, cWarmRed_L, cOrange, cOrange_L, cWarmYellow, cWarmYellow_L, cYellow, cYellow_L, cCoolYellow, cCoolYellow_L, cYellowGreen, cYellowGreen_L, cWarmGreen, cWarmGreen_L, cMildGreen, cMildGreen_L, cCoolGreen, cCoolGreen_L, cGreenCyan, cGreenCyan_L, cWarmCyan, cWarmCyan_L, cMildCyan, cMildCyan_L, cCoolCyan, cCoolCyan_L, cBlueCyan, cBlueCyan_L, cCoolBlue, cCoolBlue_L, cMildBlue, cMildBlue_L, cWarmBlue, cWarmBlue_L, cVoilet, cVoilet_L, cCoolMagenta, cCoolMagenta_L, cMildMagenta, cMildMagenta_L, cWarmMagenta, cWarmMagenta_L, cRedMagenta, cRedMagenta_L, cCoolRed, cCoolRed_L, NUM_COLOR_TYPES };
        List<string> saturatedColor = new List<string>() { "Black", "White", "Grey", "Red", "Red_L", "Warm Red", "Warm Red_L", "Orange", "Orange_L", "WarmYellow", "WarmYellow_L", "Yellow", "Yellow_L", "CoolYellow", "CoolYellow_L", "YellowGreen", "YellowGreen_L", "WarmGreen", "WarmGreen_L", "MildGreen", "MildGreen_L", "CoolGreen", "CoolGreen_L", "GreenCyan", "GreenCyan_L", "WarmCyan", "WarmCyan_L", "MildCyan", "MildCyan_L", "CoolCyan", "CoolCyan_L", "BlueCyan", "BlueCyan_L", "CoolBlue", "CoolBlue_L", "MildBlue", "MildBlue_L", "WarmBlue", "WarmBlue_L", "Voilet", "Voilet_L", "CoolMagenta", "CoolMagenta_L", "MildMagenta", "MildMagenta_L", "WarmMagenta", "WarmMagenta_L", "RedMagenta", "RedMagenta_L", "CoolRed", "CoolRed_L" };
        List<int> saturatedHue = new List<int>() { 0, 0, 0, 0, 0, 14, 14, 30, 30, 44, 44, 60, 60, 74, 74, 90, 90, 104, 104, 120, 120, 134, 134, 150, 150, 164, 164, 180, 180, 194, 194, 210, 210, 224, 224, 240, 240, 254, 254, 270, 270, 284, 284, 300, 300, 314, 314, 330, 330, 360, 360 };//1
        List<float> saturatedSat = new List<float>() { 0, 0, 0, 1, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 0.5f };//2
        List<float> saturatedVal = new List<float>() { 0, 1.0f, 0.45f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };//0


        public HSV getSaturated(HSV inputHSV)
        {
            HSV outputHSV = new HSV(0, 0, 0);
            int ctype = getPixelColorType(inputHSV);

            outputHSV.Hue = saturatedHue[ctype];	// Hue
            outputHSV.Saturation = saturatedSat[ctype];	// Full Saturation (except for black & white)
            outputHSV.Value = saturatedVal[ctype];    // Full Brightness

            return outputHSV;
        }


        int getPixelColorType(HSV HSVPlanes)
        {
            float H = HSVPlanes.Hue;
            float S = HSVPlanes.Saturation;
            float V = HSVPlanes.Value;

            int color;
            if (V < 0.2)
                color = (int)Colors.cBLACK;
            else if (V > 0.9 && S < 0.1)
                color = (int)Colors.cWHITE;
            else if (S < 0.2 && V < 0.7)
                color = (int)Colors.cGREY;
            else
            {	// Is a color

                if (H <= 4)
                    color = (int)Colors.cRed;
                else if (H <= 10 && S < 0.5)
                    color = (int)Colors.cWarmRed_L;
                else if (H <= 10)
                    color = (int)Colors.cWarmRed;
                else if (H <= 32 && S < 0.5)
                    color = (int)Colors.cOrange_L;
                else if (H <= 32)
                    color = (int)Colors.cOrange;
                else if (H <= 48 && S < 0.5)
                    color = (int)Colors.cWarmYellow_L;
                else if (H <= 48)
                    color = (int)Colors.cWarmYellow;
                else if (H <= 64 && S < 0.5)
                    color = (int)Colors.cYellow_L;
                else if (H <= 64)
                    color = (int)Colors.cYellow;
                else if (H <= 80 && S < 0.5)
                    color = (int)Colors.cCoolYellow;
                else if (H <= 80)
                    color = (int)Colors.cCoolYellow;

                else if (H <= 100 && S < 0.5)
                    color = (int)Colors.cYellowGreen_L;
                else if (H <= 100)
                    color = (int)Colors.cYellowGreen;


                else if (H <= 120 && S < 0.5)
                    color = (int)Colors.cWarmGreen_L;

                else if (H <= 120)
                    color = (int)Colors.cWarmGreen;
                else if (H <= 130 && S < 0.5)
                    color = (int)Colors.cMildGreen_L;
                else if (H <= 130)
                    color = (int)Colors.cMildGreen;
                else if (H <= 150 && S < 0.5)
                    color = (int)Colors.cCoolGreen_L;
                else if (H <= 150)
                    color = (int)Colors.cCoolGreen;



                //else if (H <= 166 && S < 0.5)
                //   color = (int)Colors.cGreenCyan_L;

                else if (H <= 166)
                    color = (int)Colors.cGreenCyan;



                else if (H <= 180 && S < 0.5)

                    color = (int)Colors.cWarmCyan_L;


                else if (H <= 180)
                    color = (int)Colors.cWarmCyan;
                else if (H <= 196 && S < 0.5)
                    color = (int)Colors.cMildCyan_L;
                else if (H <= 196)
                    color = (int)Colors.cMildCyan;
                else if (H <= 210 && S < 0.5)
                    color = (int)Colors.cCoolCyan_L;
                else if (H <= 210)
                    color = (int)Colors.cCoolCyan;


                else if (H <= 226 && S < 0.5)
                    color = (int)Colors.cBlueCyan_L;
                else if (H <= 226)
                    color = (int)Colors.cBlueCyan;
                else if (H <= 240 && S < 0.5)
                    color = (int)Colors.cCoolBlue_L;
                else if (H <= 240)
                    color = (int)Colors.cCoolBlue;
                else if (H <= 256 && S < 0.5)
                    color = (int)Colors.cMildBlue_L;
                else if (H <= 256)
                    color = (int)Colors.cMildBlue;
                else if (H <= 270 && S < 0.5)
                    color = (int)Colors.cWarmBlue_L;
                else if (H <= 270)
                    color = (int)Colors.cWarmBlue;
                else if (H <= 286 && S < 0.5)
                    color = (int)Colors.cVoilet_L;
                else if (H <= 286)
                    color = (int)Colors.cVoilet;
                else if (H <= 300 && S < 0.5)
                    color = (int)Colors.cCoolMagenta_L;
                else if (H <= 300)
                    color = (int)Colors.cCoolMagenta;
                else if (H <= 316 && S < 0.5)
                    color = (int)Colors.cMildMagenta_L;
                else if (H <= 316)
                    color = (int)Colors.cMildMagenta;
                else if (H <= 330 && S < 0.5)
                    color = (int)Colors.cWarmMagenta_L;
                else if (H <= 330)
                    color = (int)Colors.cWarmMagenta;
                else if (H <= 346 && S < 0.5)
                    color = (int)Colors.cRedMagenta_L;
                else if (H <= 346)
                    color = (int)Colors.cRedMagenta;
                else if (H <= 360 && S < 0.5)
                    color = (int)Colors.cCoolRed_L;
                else if (H <= 360)
                    color = (int)Colors.cCoolRed;
                else
                    color = (int)Colors.cVoilet;

            }
            return color;
        }



    }
}
