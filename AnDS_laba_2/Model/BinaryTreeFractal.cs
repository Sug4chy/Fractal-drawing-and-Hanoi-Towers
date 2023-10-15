using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Point = System.Windows.Point;

namespace AnDS_laba_2.Model;

public static class BinaryTreeFractal
{
    private const double LengthScale = 0.75;
    private const double DeltaTheta = Math.PI / 5;
    
    public static async Task Draw(Canvas canvas, int depth)
    {
        await DrawBinaryTree(canvas, depth, new Point(canvas.Width / 2, 0.83 * canvas.Height), 
            0.125 * canvas.Width, -Math.PI / 2);
    }

    private static async Task DrawBinaryTree(Panel canvas, int depth, Point pt, double length, double theta)
    {
        var x1 = pt.X + length * Math.Cos(theta);
        var y1 = pt.Y + length * Math.Sin(theta);
        var line = new Line
        {
            Stroke = Brushes.Black,
            X1 = pt.X,
            Y1 = pt.Y,
            X2 = x1,
            Y2 = y1
        };
        canvas.Children.Add(line);
        await Task.Delay(10);

        if (depth <= 1)
        {
            return;
        }
        await DrawBinaryTree(canvas,depth - 1,
            new Point(x1, y1),
            length * LengthScale, theta + DeltaTheta);
        await DrawBinaryTree(canvas, depth - 1,
            new Point(x1, y1),
            length * LengthScale, theta - DeltaTheta);
    }
}