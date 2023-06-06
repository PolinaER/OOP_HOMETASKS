﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PhotoRed
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения. 
        /// </summary>
        [STAThread]  
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = new MainForm();

            mainForm.AddFilter(new PixelFilter<LightningParameters>(
                "Осветление/Затемнение",
                (pixel, parametrs) => pixel * parametrs.Coefficient));

            mainForm.AddFilter(new PixelFilter<EmpryParameters>(
                "Оттенки серого",
                (pixel, parameters) => {
                    var lightness = (0.3 * pixel.R + 0.6 * pixel.G + 0.1 * pixel.R) / 3;
                    return new Pixel(lightness, lightness, lightness);
                }));

            mainForm.AddFilter(new PixelFilter<GammaCorrectionParameters>(
                "Гамма-коррекция",
                (pixel, parameters) =>
                {
                    var l = 0.5 * (Math.Max(Math.Max(pixel.R, pixel.G), pixel.B) + Math.Min(Math.Min(pixel.R, pixel.G), pixel.B));
                    var c = Math.Pow(l, (2 / parameters.Gamma));
                    return Convertors.HSLToPixel(Convertors.PixelToHSL(pixel), c);
                }));

            mainForm.AddFilter(new TransformFilter(
                "Отражение по вертикали",
                size => size,
                (newPoint, oldSize) => new Point(oldSize.Width - newPoint.X - 1, newPoint.Y)
                ));

            mainForm.AddFilter(new TransformFilter(
                "Повернуть против часовой стрелки",
                oldSize => new Size(oldSize.Height, oldSize.Width),
                (newPoint, oldSize) => new Point(oldSize.Width - newPoint.Y - 1, oldSize.Height - newPoint.X - 1)
                //newPoint.Y, oldSize.Height - newPoint.X - 1)
                //есть координата новой точки надо показать какую точку старой картинки поместить сюда
                ));

            mainForm.AddFilter(new TransformFilter<RotationParameters>(
                "Поворот на произвольный угол",
                new RotateTransformer()
                ));

            mainForm.AddFilter(new TransformFilter(
                "Отражение относительно побочной диагонали",
                 oldSize => new Size(oldSize.Height, oldSize.Width),
                (newPoint, oldSize) => new Point(oldSize.Width - newPoint.Y - 1, oldSize.Height - newPoint.X - 1)
                ));

            mainForm.AddFilter(new TransformFilter<UpperParameters>(
                "Сдвиг вверх",
                new UpperTransformer()
                ));

            Application.Run(mainForm);
        }
    }
}
