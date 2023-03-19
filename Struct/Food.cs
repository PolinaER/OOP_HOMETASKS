using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct
{
    public struct Food
    {
        int weight;
        public int Weight
        {
            get { return weight; }
            set
            {
                if (value > 0)
                    weight = value;
                else
                    throw new ArgumentException("Positive weight!");
            }
        }

        double calorie;
        public double Calorie
        {
            get { return calorie; }
            set
            {
                if (value > 0)
                    calorie = Math.Round(value, 1);
                else
                    throw new ArgumentException("Positive calorie!");
            }
        }
        public double Value
        {
            get { return Weight * Calorie * 0.01; }
        }


        public Food(int weight, double calorie) : this()
        {
            Weight = weight;
            Calorie = calorie;
        }
        public override string ToString()
        {
            return $"{Weight} г калорийности {Calorie} Ккал/100 г";
        }
        public override bool Equals(object obj)
        {
            if (obj is Food myFood)
                return Weight == myFood.Weight && Calorie == myFood.Calorie;

            throw new ArgumentException("Food and food only!");
        }
        public override int GetHashCode() => ToString().GetHashCode();
        public static Food operator +(Food a, Food b)
        {
            if (a.Calorie == b.Calorie)
                return new Food(a.Weight + b.Weight, a.Calorie);
            else
                throw new ArgumentException("Different calories!");
        }
        public static Food operator -(Food a, Food b)
        {
            if (a.Calorie == b.Calorie && a.Weight >= b.Weight)
                return new Food(a.Weight - b.Weight, a.Calorie);
            else
                throw new ArgumentException("Same calories and not that big second weight!");
        }
    }
}
