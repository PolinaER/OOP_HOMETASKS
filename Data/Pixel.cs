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

		double delta { get => maxC-minC; }
		double maxC { get => Math.Max(R, Math.Max(G, B)); }
		double minC { get => Math.Min(R, Math.Min(G, B)); }
		public double H { 
			get {
				if (delta == 0)
					return 0;
				else if (maxC == R)
					return ToHue((G-B)/delta*60);
				else if (maxC == G)
					return ToHue((2+(B-R)/delta)*60);
				else
					return ToHue((4+(R-G)/delta)*60);
			}
		}
		public double S { 
			get 
            {
				if (0.5 * (maxC + minC)<= 0)
					return delta/(maxC+minC);
				else 
					return delta / (2-maxC-minC);
			}
		}
		public double L { 
			get 
            {
				return 0.5 * (maxC + minC);
            }

		}

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
		public static double ToHue(double val)
        {
			if (val < 0)
				return val + 360;
			else
				return val;
        }
	}
}
