using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public struct Pixel
    {
		private double r;
		public double R { get => r; set => r = CheckValue(value); }

		private double g;
		public double G { get => g; set => g = CheckValue(value); }

		private double b;
		public double B { get => b; set => b = CheckValue(value); }


		public double H;
		public double S;
		public double L;

        public Pixel(double red, double green, double blue) : this()
		{
			R = red;
			G = green;
			B = blue;
		}

		public static Pixel operator *(double k, Pixel p) 
		{
			Pixel result = new Pixel();
			result.r = Trim(p.r * k);
			result.g = Trim(p.g * k);
			result.b = Trim(p.b * k);

			return result;
		}
		public static Pixel operator *(Pixel p, double k) => k * p;

		private double CheckValue(double val)
		{
			if (val < 0 || val > 1)
				throw new ArgumentException("Неверное значение яркости канала");
			return val;
		}
		private static double Trim(double lightness) 
		{
			if (lightness > 1)
				return 1;

			return lightness;
		}
    }
}
