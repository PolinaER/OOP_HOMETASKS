using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public class Photo   //Структуры это тип значения, класс это тип ссылки.
                         //При возможности не использовать их,
                         //здесь это экономит память - храниться инфо про объект там же, а не ссылки + инфо
                         //Копирует данные и не меняет исходные данные, класс-объект передает ссылки и исходник меняется
                         //используя ключевое слово ref передается ссылка на структуру - и меняется исходник
    {
        private Pixel[,] data;
        public int Width { get => data.GetLength(0); } //сколько пикселей по горизонтали
        public int Height { get => data.GetLength(1); } //сколько пикселей по вертикале

        public Pixel this[int x, int y]  //Индексаторы
        {
            get => data[x, y];
            set => data[x, y] = value;
        }

        public Photo(int width, int height) 
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Размеры должны быть положительные");

            data = new Pixel[width, height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    data[x, y] = new Pixel();
        }
    }
}
