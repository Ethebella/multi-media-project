using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MediaQuery
{
    class Clumps
    {
        public List<List<Point>> ListOfShapes = new List<List<Point>>();
        public List<Point[]> BoundingRectForShape = new List<Point[]>();
        private List<string> ShapeDescription = new List<string>();

       public bool checkAdjacencyAndAdd(int x, int y)
        {
           /*
            Console.Write("adding point ");
            Console.Write(x);
            Console.Write(",");
            Console.Write(y);
            Console.WriteLine();
            * */
            if (ListOfShapes.Count == 0)
            {
                Console.WriteLine("First Object");
                Point tempPoint = new Point(x, y);
                List<Point> shape = new List<Point>();
                shape.Add(tempPoint);
                ListOfShapes.Add(shape);
                return false;
            }
            else
            {
                int i = 0;
                foreach (List<Point> shape in ListOfShapes)
                {
                    i++;
                    foreach (Point point in shape)
                    {
                        if (point.X == x && point.Y == y)
                        {
                            return true;
                        }
                        if (
                            (point.X == x + 8 && point.Y == y) ||
                            (point.X == x + 8 && point.Y == y + 8) ||
                            (point.X == x     && point.Y == y + 8) ||
                            (point.X == x - 8 && point.Y == y + 8) ||
                            (point.X == x - 8 && point.Y == y) ||
                            (point.X == x - 8 && point.Y == y - 8) ||
                            (point.X == x     && point.Y == y - 8) ||
                            (point.X == x + 8 && point.Y == y - 8)
                            )
                        {
                            Point tempPoint = new Point(x,y);
                            shape.Add(tempPoint);
                            Console.Write("adding point ");
                            Console.Write("x=");
                            Console.Write(x);
                            Console.Write("y=");
                            Console.Write(y);
                            Console.Write(" to shape ");
                            Console.WriteLine(i);
                            return true;
                        }
                            
                    }
                }
                // not found yet

                Point tempPoint1 = new Point(x, y);
                List<Point> shape1 = new List<Point>();
                shape1.Add(tempPoint1);
                ListOfShapes.Add(shape1);

                Console.Write("new shape");
                Console.Write("x=");
                Console.Write(x);
                Console.Write("y=");
                Console.Write(y);
                Console.WriteLine(i);

                return false;
            }
        }

       public int numberOfShapes()
       {
           return ListOfShapes.Count;
       }

       private bool mergeShapes(int gap)
       {
           bool found = false;
           for (int i = 0; i < ListOfShapes.Count;i++ )
           {
               found = false;
               for (int j = i+1; j < ListOfShapes.Count; j++)
               {
                   for (int k = 0; k < ListOfShapes[i].Count; k++)
                   {
                       for (int l = 0; l < ListOfShapes[j].Count; l++)
                       {

                           if ((ListOfShapes[i][k].X == ListOfShapes[j][l].X +  gap && ListOfShapes[i][k].Y == ListOfShapes[j][l].Y) ||
                                (ListOfShapes[i][k].X == ListOfShapes[j][l].X + gap && ListOfShapes[i][k].Y == ListOfShapes[j][l].Y + gap) ||
                                (ListOfShapes[i][k].X == ListOfShapes[j][l].X       && ListOfShapes[i][k].Y == ListOfShapes[j][l].Y + gap) ||
                                (ListOfShapes[i][k].X == ListOfShapes[j][l].X - gap && ListOfShapes[i][k].Y == ListOfShapes[j][l].Y + gap) ||
                                (ListOfShapes[i][k].X == ListOfShapes[j][l].X - gap && ListOfShapes[i][k].Y == ListOfShapes[j][l].Y) ||
                                (ListOfShapes[i][k].X == ListOfShapes[j][l].X - gap && ListOfShapes[i][k].Y == ListOfShapes[j][l].Y - gap) ||
                                (ListOfShapes[i][k].X == ListOfShapes[j][l].X       && ListOfShapes[i][k].Y == ListOfShapes[j][l].Y - gap) ||
                                (ListOfShapes[i][k].X == ListOfShapes[j][l].X + gap && ListOfShapes[i][k].Y == ListOfShapes[j][l].Y - gap)
                                )
                           {
                               found = true;
                               break;
                           }
                       }
                       if (found)
                       {
                           found = true;
                           break;
                       }
                    
                   }
                   if (found)
                   {
                       Console.WriteLine("merging shape "+i+ "with shape " + j);
                       foreach (Point point in ListOfShapes[j])
                       {
                           ListOfShapes[i].Add(point);
                       }
                       ListOfShapes.RemoveAt(j);
                       break;
                   }
               }
               if (found)
               {
                   i=-1;
               }
           }
               return true;
       }

       public bool keepTopX(int x)
       {
           while (ListOfShapes.Count > x)
           {
               ListOfShapes.RemoveAt(x);
               if (BoundingRectForShape.Count > x)
               {
                   BoundingRectForShape.RemoveAt(x);
                   ShapeDescription.RemoveAt(x);
               }
           }
           return true;
       }

       private void removeTinyHits(int x)
       {
           for (int i = 0; i < ListOfShapes.Count; )
           {
               if (ListOfShapes[i].Count < x)
               {
                   ListOfShapes.RemoveAt(i);
                   if (BoundingRectForShape.Count > i)
                   {
                       BoundingRectForShape.RemoveAt(i);
                       ShapeDescription.RemoveAt(i);
                   }
               }
               else
                   i++;
           }
       }

       private bool removeBadRatios(Point minPoint,Point maxPoint , float maxRatio)
       {
           int xdist=maxPoint.X-minPoint.X;
           int ydist=maxPoint.Y-minPoint.Y;
           float clumpRatio = Math.Max(ydist / (float)xdist, xdist / (float)ydist);
           if (clumpRatio > maxRatio)
               return true;
           return false;
       }

       public bool computeAllShapes()
       {
           mergeShapes(8);
           ListOfShapes = ListOfShapes.OrderByDescending(o => o.Count).ToList();
           //keepTopX(3);
           bool atleastABigImage = false;
           foreach (List<Point> tempShape in ListOfShapes)
           {
               if (tempShape.Count > 3)
               {
                   atleastABigImage = true;
                   break;
               }
           }
           if (atleastABigImage)
                removeTinyHits(4);
           mergeShapes(16);
           keepTopX(3);
           
           //keepTopX(3);
           
           for (int i=0; i <ListOfShapes.Count;)
           {
               List<Point> shape = ListOfShapes[i];
               int minX, minY, maxX, maxY;
               int countSquares=0;
               minX = minY = 500;
               maxX = maxY = -1;
               foreach (Point tempPoint in shape)
               {
                   countSquares++;
                   if (minX > tempPoint.X)
                       minX = tempPoint.X;
                   if (minY > tempPoint.Y)
                       minY = tempPoint.Y;
                   if (maxX < tempPoint.X)
                       maxX = tempPoint.X;
                   if (maxY < tempPoint.Y)
                       maxY = tempPoint.Y;
               }
               Point minPoint = new Point(minX,minY);
               Point maxPoint = new Point(maxX +8, maxY+8);
               if (removeBadRatios(minPoint, maxPoint, 3))
               {
                   ListOfShapes.RemoveAt(i);
                   continue;
               }
               Point[] rect = new Point[] {minPoint ,maxPoint };
               BoundingRectForShape.Add(rect);
               // describe shape

               int Area = (maxX +8 - minX) * (maxY +8 - minY);
               int AreaOccupiedByShape = countSquares * 8 * 8;
               float ratio = AreaOccupiedByShape/ (float)Area ;
               if (ratio <0.0)
               {
                   Console.WriteLine("adding circle");
                   ShapeDescription.Add("Circle");
               }
               else
               {
                   Console.WriteLine("adding rectangle");
                   ShapeDescription.Add("Rectangle");
               }
               Console.WriteLine("AreaOccupiedByShape= " + AreaOccupiedByShape + "Area= " + Area + "Ratio" + ratio);
               i++;
           }
           return true;
       }

       public bool drawBestBoundaries(int x, Image img)
       {
           // add check for -1
           if (x == -1)
               return true;
           for (int i = x; i < ShapeDescription.Count;)
           {
               Point[] extremes = BoundingRectForShape[i];
               if (ShapeDescription[i] == "Circle")
               {

                   Console.WriteLine("drawing circle");
                   int Radius = 0;
                   Point center = new Point();
                   center.X = (extremes[0].X + extremes[1].X) / 2;
                   center.Y = (extremes[0].Y + extremes[1].Y) / 2;
                   Radius = (int)Math.Sqrt(((extremes[0].X - center.X) * (extremes[0].X - center.X)) + ((extremes[0].Y - center.Y) * (extremes[0].Y - center.Y)));
                   Console.WriteLine("center =" + center + "radius=" + Radius);
                   img.PrintCircle(center, Radius);
               }
               else if (ShapeDescription[i] == "Rectangle")
               {
                   Console.WriteLine("drawing rect");
                   int width = 0;
                   int height = 0;
                   Point topLeft = new Point();
                   width = Math.Abs(extremes[0].X - extremes[1].X);
                   height = Math.Abs(extremes[0].Y - extremes[1].Y);
                   topLeft = extremes[0];
                   Console.WriteLine("left =" + topLeft + "width=" + width + "height=" + height);
                   img.PrintRectangle(topLeft.X, topLeft.Y, width, height);
               }
               break;
           }
           return true;
       }

       public bool drawBoundaries(Image img)
       {
           for (int i = 0; i < ShapeDescription.Count; i++)
           {
               Point[] extremes = BoundingRectForShape[i];
               if (ShapeDescription[i] == "Circle")
               {

                   Console.WriteLine("drawing circle");
                   int Radius=0;
                   Point center = new Point();
                   center.X=(extremes[0].X+extremes[1].X)/2;
                   center.Y=(extremes[0].Y+extremes[1].Y)/2;
                   Radius =(int) Math.Sqrt(((extremes[0].X - center.X) * (extremes[0].X - center.X)) + ((extremes[0].Y - center.Y) * (extremes[0].Y - center.Y)));
                   Console.WriteLine("center ="+center+"radius="+Radius);
                   img.PrintCircle(center, Radius);
               }
               else if (ShapeDescription[i] == "Rectangle")
               {
                   Console.WriteLine("drawing rect");
                   int width=0;
                   int height=0;
                   Point topLeft= new Point();
                   width = Math.Abs(extremes[0].X - extremes[1].X);
                   height= Math.Abs(extremes[0].Y - extremes[1].Y);
                   topLeft = extremes[0];
                   Console.WriteLine("left =" + topLeft+ "width=" + width +"height=" + height);
                   img.PrintRectangle(topLeft.X, topLeft.Y,width,height);
               }
           }
               return true;
       }
       
    }
}
