using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace MediaQuery
{
    public class BlockHisto
    {
        private Bitmap m_bitmap;
        private Histogram[,] m_histogramBlocks;       // 44 wide by 36 tall array of histograms for 8x8 blocks 
        private int m_binSize1, m_binSize2, m_binSize3;
        private const int WIDTH = 352;
        private const int HEIGHT = 288;
        private int m_blockSize;                        // ie: 8 for an 8x8 block
        private int m_numBlocksAcross;
        private int m_numBlocksDown;

        public BlockHisto(Bitmap b, int binSize1, int binSize2, int binSize3)
        {
            m_blockSize = 8;
            m_numBlocksAcross = WIDTH / m_blockSize;
            m_numBlocksDown = HEIGHT / m_blockSize;

            m_bitmap = b;
            m_histogramBlocks = new Histogram[m_numBlocksAcross, m_numBlocksDown];
            m_binSize1 = binSize1;
            m_binSize2 = binSize2;
            m_binSize3 = binSize3;

            BuildHistogramBlocks();
        }

        public Histogram GetHistogramAtBlock(int x, int y)
        {
            return m_histogramBlocks[x, y];
        }

        public int NumBlocksAcross
        {
            get { return m_numBlocksAcross; }
        }

        public int NumBlocksDown
        {
            get { return m_numBlocksDown; }
        }

        public int BlockSize
        {
            get { return m_blockSize; }
        }

        private void BuildHistogramBlocks()
        {
            // initialize to zero
            for (int y = 0; y < m_numBlocksDown; y++)
                for (int x = 0; x < m_numBlocksAcross; x++)
                    m_histogramBlocks[x, y] = null;

            // build all block histograms
            for (int y = 0; y < HEIGHT; y += m_blockSize)
                for (int x = 0; x < WIDTH; x += m_blockSize)
                {
                    // Clone current NxN portion of the Bitmap object.
                    Rectangle cloneRect = new Rectangle(x, y, m_blockSize, m_blockSize);
                    PixelFormat format = m_bitmap.PixelFormat;
                    Bitmap smallBlock = m_bitmap.Clone(cloneRect, format);

                    Histogram h = CreateHistogram(smallBlock);
                    m_histogramBlocks[x / m_blockSize, y / m_blockSize] = h;
                }
        }

        private unsafe Histogram CreateHistogram(Bitmap bmp)
        {
            Histogram hist = new Histogram(m_binSize1 * m_binSize2 * m_binSize3);
            float total = 0;
            int idx = 0;

            UnsafeBitmap fastBitmap = new UnsafeBitmap(bmp);
            fastBitmap.LockBitmap();
            Point size = fastBitmap.Size;
            BGRA* pPixel;

            for (int y = 0; y < size.Y; y++)
            {
                pPixel = fastBitmap[0, y];
                for (int x = 0; x < size.X; x++)
                {
                    //get the bin index for the current pixel colour
                    idx = GetSingleBinIndex(m_binSize1, m_binSize2, m_binSize3, pPixel);

                    if (idx < hist.Data.Length - 1 && idx != 0)     // disregard black and white
                    {
                        hist.Data[idx]++;
                        total++;
                    }

                    //increment the pointer
                    pPixel++;
                }
            }

            fastBitmap.UnlockBitmap();

            //normalise
            if (total > 0)
                hist.Normalise(total);

            return hist;
        }

        private unsafe int GetSingleBinIndex(int binCount1, int binCount2, int binCount3, BGRA* pixel)
        {
            int idx = 0;

            //find the index                
            int i1 = GetBinIndex(binCount1, (float)pixel->red, 255);
            int i2 = GetBinIndex(binCount2, (float)pixel->green, 255);
            int i3 = GetBinIndex(binCount3, (float)pixel->blue, 255);
            idx = i1 + i2 * binCount1 + i3 * binCount1 * binCount2;

            return idx;
        }

        private int GetBinIndex(int binCount, float colourValue, float maxValue)
        {
            int idx = (int)(colourValue * (float)binCount / maxValue);
            if (idx >= binCount)
                idx = binCount - 1;

            return idx;
        }
    }
}