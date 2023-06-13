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

		private double h;
		public double H { get => h; set => h = CheckHue(value); }

		private double s;
		public double S { get => s; set => s = CheckVal(value); }

		private double l;
		public double L { get => l; set => l = CheckVal(value); }


		public Pixel(double red, double green, double blue) : this()
		{
			R = red;
			G = green;
			B = blue;
		}
		public Pixel(double red, double green, double blue, double h, double s, double l) : this()
		{
			R = red;
			G = green;
			B = blue;
			H = h;
			S = s;
			L = l;
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
		private double CheckVal(double val)
		{
			if (val < 0 || val > 1)
				throw new ArgumentException("Неверное значение S/L");
			return val;
		}

		private double CheckHue(double hue)
        {
			if (hue < 0 || hue > 360)
				throw new ArgumentException("Неверное значение Hue");
			return hue;
		}

		private static double Trim(double lightness) 
		{
			if (lightness > 1)
				return 1;

			return lightness;
		}
    }
}
