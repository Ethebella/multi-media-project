using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MediaQuery
{
    public class Image : ICloneable
    {
        private const int WIDTH = 352;              // image width
        private const int HEIGHT = 288;             // image height
        private const int ALPHA_ON = 255;           // initial value for all alpha pixels
        private Bitmap m_bitmapImage;               // bitmap representation for the GUI to use
        private Color[,] m_argbImage;               // 2-D array of the image's ARGB channels
        private HSV[,] m_hsvImage;                  // 2-D array of the image's HSV channels
        private HSV[,] m_hsvSaturated;

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        public void SuperRed()
        {
            for (int j = 0; j < HEIGHT; j++)
                for (int i = 0; i < WIDTH; i++)
                {
                    float hue = m_hsvImage[i, j].Hue;
                    float saturation = m_hsvImage[i, j].Saturation;
                    float value = m_hsvImage[i, j].Value;

                    if (hue > 10)           // stay in the 0-10 degrees of red
                        continue;

                    if (value <= 0.1)       // if it's very dark, don't apply the filter
                        continue;

                    m_hsvImage[i, j].Saturation = 1;
                    m_hsvImage[i, j].Value = 1;

                    Color c = ColorFromHSV(hue, 1, 1);
                    m_bitmapImage.SetPixel(i, j, c);
                }
        }

        public Image(string inputFile, string inputAlphaFile = "")
        {
            m_bitmapImage = new Bitmap(WIDTH, HEIGHT, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            m_argbImage = new Color[WIDTH, HEIGHT];
            m_hsvImage = new HSV[WIDTH, HEIGHT];
            m_hsvSaturated = new HSV[WIDTH, HEIGHT];

            ReadRGBChannels(inputFile);                     // take in the input file
            if (!string.IsNullOrEmpty(inputAlphaFile))      // if there's an associated alpha channel, take it in
                ReadAlphaChannel(inputAlphaFile);

            GenerateHSV();                                  // generate the HSV representation of the image

            PopulateBitmap();                               // populate the bitmap for the GUI to use
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public Bitmap BitmapImage
        {
            get { return m_bitmapImage; }
            set { m_bitmapImage = value; }
        }

        public int Width
        {
            get { return WIDTH; }
        }

        public int Height
        {
            get { return HEIGHT; }
        }

        public bool HasAlphaMask(int x, int y)
        {
            return m_argbImage[x, y].A == 0 ? true : false;
        }

        public HSV GetHSVPixel(int x, int y)
        {
            return m_hsvImage[x, y];
        }

        public float GetAvgBrightnessForBlock(int xStart, int yStart, int width, int height)
        {
            float numPixelsCounted = 0;
            float totalValue = 0;
            for (int y = yStart; y < yStart + height; y++)
            {
                if (y >= HEIGHT)
                    continue;

                for (int x = xStart; x < xStart + width; x++)
                {
                    if (x >= WIDTH)
                        continue;

                    totalValue += m_hsvImage[x, y].Value;
                    numPixelsCounted++;
                }
            }

            return totalValue / numPixelsCounted;
        }


        // print crosshairs on the GUI's bitmap, crossing at point {x, y} (need to update the GUI after this call)
        public void PrintCrosshairs(int x, int y)
        {
            Color c = Color.Red;
            for (int i=0; i<WIDTH; i++)                 // print the horizontal bar
                m_bitmapImage.SetPixel(i, y, c);
            for (int j = 0; j < HEIGHT; j++)            // print the vertical bar
                m_bitmapImage.SetPixel(x, j, c);
        }

        public void PrintCorner(int x, int y, int width, int height)
        {
            Color c = Color.Red;
            for (int i = x; i < x+width; i++)                 // print the horizontal bar
                m_bitmapImage.SetPixel(i, y, c);
            for (int j = y; j < y+height; j++)            // print the vertical bar
                m_bitmapImage.SetPixel(x, j, c);
        }

        public void PrintSolidSquare(int x, int y, int width, int height, Brush b)
        {
            using (Graphics g = Graphics.FromImage(m_bitmapImage))
            {
                g.FillRectangle(b, x, y, width, height);
            }
        }



        public void PrintSolidSquare(int x, int y, int width, int height)
        {
            using (Graphics g = Graphics.FromImage(m_bitmapImage))
            {
                g.FillRectangle(Brushes.Green, x, y, width, height);
            }
        }

        public void PrintRectangle(int x, int y, int width, int height)
        {
            using (Graphics g = Graphics.FromImage(m_bitmapImage))
            {
                g.DrawRectangle(Pens.Blue, x, y, width, height);
            }
        }

        public void PrintCircle(Point center, int radius)
        {
            using (Graphics g = Graphics.FromImage(m_bitmapImage))
            {
                g.DrawEllipse(Pens.White, center.X - radius, center.Y - radius, radius + radius, radius + radius);
            }
        }

        private void ReadRGBChannels(string inputFile)
        {
            int len = WIDTH * HEIGHT;
            try
            {
                FileStream fs = new FileStream(inputFile, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                byte[] red = br.ReadBytes(len);
                byte[] green = br.ReadBytes(len);
                byte[] blue = br.ReadBytes(len);
                br.Close();
                fs.Close();

                // set the 1-D input file values to our 2-D arrays
                int x = 0;                          // grows to the right
                int y = 0;                          // grows down            
                for (int i = 0; i < len; i++)
                {
                    Color c = Color.FromArgb(ALPHA_ON, red[i], green[i], blue[i]);
                    m_argbImage[x, y] = c;
 
                    x++;
                    if (x == WIDTH)                 // see if we've reached the row's max width, if so, drop down to the next row
                    {
                        x = 0;
                        y++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading RGB image.\n\n" + "Full message: " + ex.ToString());
            }
        }

        private void ReadAlphaChannel(string inputFile)
        {
            int len = WIDTH * HEIGHT;
            try
            {
                FileStream fs = new FileStream(inputFile, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                byte[] alpha = br.ReadBytes(len);
                br.Close();
                fs.Close();

                // set the 1-D input file values to our 2-D array
                int x = 0;                          // grows to the right
                int y = 0;                          // grows down            
                for (int i = 0; i < len; i++)
                {
                    Color c = m_argbImage[x, y];       // get the existing val for the pixel

                    if (alpha[i] == 0)              // if we need to turn the alpha OFF
                        m_argbImage[x, y] = Color.FromArgb(0, c.R, c.G, c.B);             

                    x++;
                    if (x == WIDTH)                 // see if we've reached the row's max width, if so, drop down to the next row
                    {
                        x = 0;
                        y++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading alpha image.\n\n" + "Full message: " + ex.ToString());
            }
        }

        private void GenerateHSV()
        {
            for (int y = 0; y < HEIGHT; y++)
                for (int x = 0; x < WIDTH; x++)
                    m_hsvImage[x, y] = RGBtoHSV(x, y);
        }

        public void GenerateSaturatedHSV()
        {
            SaturateHSV tempObject = new SaturateHSV(); ;
            for (int y = 0; y < HEIGHT; y++)
                for (int x = 0; x < WIDTH; x++)
                    m_hsvSaturated[x, y] = tempObject.getSaturated(RGBtoHSV(x, y));

            for (int j = 0; j < HEIGHT; j++)
                for (int i = 0; i < WIDTH; i++)
                {
                    float hue = m_hsvSaturated[i, j].Hue;
                    float saturation = m_hsvSaturated[i, j].Saturation;
                    float value = m_hsvSaturated[i, j].Value;

                    Color c = ColorFromHSV(hue, saturation, value);
                    m_bitmapImage.SetPixel(i, j, c);
                }
        }

        

        private HSV RGBtoHSV(int x, int y)
        {
            float hue = m_argbImage[x, y].GetHue();
            float saturation = m_argbImage[x, y].GetSaturation();
            float value = m_argbImage[x, y].GetBrightness();

            return new HSV(hue, saturation, value);
        }

        // used for setting the Bitmap values so the GUI can display the images
        private void PopulateBitmap()
        {
            for (int y = 0; y < HEIGHT; y++)
                for (int x = 0; x < WIDTH; x++)
                    m_bitmapImage.SetPixel(x, y, m_argbImage[x, y]);
        }

        
    }
}
