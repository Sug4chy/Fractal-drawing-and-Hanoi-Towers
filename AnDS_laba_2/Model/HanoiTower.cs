using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AnDS_laba_2.Model;

public static class HanoiTower
{
    public static void DrawRings(int ringsCount, double startWidth, StackPanel pole)
    {
        var random = new Random();
        var widthScale = (startWidth - 125) / ringsCount;
        for (var i = 0; i < ringsCount; i++)
        {
            var ring = new Rectangle
            {
                Height = 40,
                Width = startWidth - (i + 1) * widthScale,
                RadiusX = 15,
                RadiusY = 15,
                Fill = new SolidColorBrush(Color.FromRgb((byte)random.Next(1, 255), (byte)random.Next(1, 255),
                    (byte)random.Next(1, 255)))
            };
            pole.Children.Insert(0, ring);
        }
    }

    public static void MoveDisks(int start, int temp, int end, int disks, List<(int from, int to)> steps)
    {
        if (disks > 1)
            MoveDisks(start, end, temp, disks - 1, steps);
        steps.Add((start, end));
        if (disks > 1)
            MoveDisks(temp, start, end, disks - 1, steps);
    }
}