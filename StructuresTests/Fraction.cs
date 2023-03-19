using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuresTests
{
    public struct Fraction
    {
        public int Numerator { get; set; }

        int denumenator;
        public int Denumerator
        {
            get { return denumenator; }
            set
            {
                if (value > 0)
                    denumenator = value;
                else
                    throw new ArgumentException("Positivw denumenator!");
            }
        }

        public Fraction(int numerator, int denumerator) : this()
        {
            Numerator = numerator;
            Denumerator = denumerator;
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denumerator}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Fraction myFraction)
                return Numerator * myFraction.Denumerator == Denumerator * myFraction.Numerator;

            throw new ArgumentException("Fraction and fraction only!");
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            return new Fraction(
                a.Numerator * b.Denumerator + a.Denumerator * b.Numerator, 
                a.Denumerator * b.Denumerator);
        }

    }
}
