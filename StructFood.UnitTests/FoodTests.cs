using NUnit.Framework;

namespace Struct
{
    [TestFixture]
    public class FoodTests
    {
        [Test]
        public void ConstructorTest() 
        { 
            var f = new Food(230,123.4); 
            Assert.That(f.Weight, Is.EqualTo(230)); 
            Assert.That(f.Calorie, Is.EqualTo(123.4)); 
            Assert.That(f.Value, Is.EqualTo(230*123.4*0.01)); 
        }

        [Test]
        public void WeightSet_NegativeValue_ArgumentException() 
        { 
            var f = new Food();
            var a = -30;

            Assert.That(() => f.Weight = a, Throws.ArgumentException); 
        }

        [Test]
        public void CalorieSet_NegativeValue_ArgumentException()
        {
            var f = new Food();
            var a = -30;

            Assert.That(() => f.Calorie = a, Throws.ArgumentException);
        }

        [TestCase(123.34, 123.3)]
        [TestCase(123.39, 123.4)]
        public void CalorieSet_MoreThanOneSignRoundValue_OneSign(double c, double result)
        {
            var f = new Food();

            f.Calorie = c;

            Assert.That(f.Calorie, Is.EqualTo(result));
        }

        [Test]
        public void ToStringTest()
        {
            var f = new Food(230, 123.4);

            Assert.That(f.ToString(), Is.EqualTo("230 г калорийности 123,4  кал/100 г"));
        }

        [TestCase(230, 123.4, 230, 24, false)]
        [TestCase(230, 123.4, 230, 123.4, true)]
        [TestCase(230, 123.4, 223, 123.4, false)]
        [TestCase(234, 13, 123, 1, false)]

        public void Equals_ToFoods_Result(int weight1, double calorie1, int weight2, double calorie2, bool result)
        {
            var a = new Food(weight1, calorie1);
            var b = new Food(weight2, calorie2);

            Assert.That(a.Equals(b), Is.EqualTo(result));
        }

        [Test]
        public void Equals_ToObject_ArgumentException()
        {
            var f = new Food(123, 456.2);
            var obj = new object();

            Assert.That(() => f.Equals(obj), Throws.ArgumentException);
        }

        [TestCase(100, 1.5, 1.5)]
        [TestCase(1, 1, 0.01)] 
        [TestCase(1, 3, 0.03)]
        public void ValueInSecondsTest(int weight, double calorie, double result)
        {
            var f = new Food(weight, calorie); 
            
            Assert.That(f.Value, Is.EqualTo(result)); 
        }

        [Test] 
        public static void GetHashCodeTest() 
        { 
            var f1 = new Food(200, 120); 
            var f2 = new Food(200, 120); 
            var f3 = new Food(220, 12.2);

            Assert.That(f1.Equals(f2), Is.True); 
            Assert.That(f1.Equals(f3), Is.False); 
        }

        [TestCase(200, 100, 200, 100, 400, 100)]
        [TestCase(200, 100.73, 150, 100.7, 350, 100.7)]
        public void AdditionalTest(int w1, double c1, int w2, double c2, int resultW, double resultC)
        {
            var f1 = new Food(w1, c1);
            var f2 = new Food(w2, c2);
            var resultF = new Food(resultW, resultC);

            Assert.That(f1 + f2, Is.EqualTo(resultF));
        }

        [Test]
        public void Addition_DifferentCalories_ArgumentException()
        {
            var f1 = new Food(123, 456.2);
            var f2 = new Food(123, 123);

            Assert.That(() => f1 + f2, Throws.ArgumentException);
        }

        [TestCase(200, 100, 55, 100, 145, 100)]
        [TestCase(200, 100.400, 100, 100.4, 100, 100.4)]
        public void SubtractionTest(int w1, double c1, int w2, double c2, int resultW, double resultC)
        {
            var f1 = new Food(w1, c1);
            var f2 = new Food(w2, c2);
            var resultF = new Food(resultW, resultC);

            Assert.That(f1 - f2, Is.EqualTo(resultF));
        }

        [TestCase(200, 100, 150, 14)]
        [TestCase(200, 100.7, 400, 100.7)]
        [TestCase(200, 123, 744, 454)]
        [TestCase(200, 100.73, 200, 100.7)]
        public void Subtraction__DifferentCaloriesOrTooHIghtWeight_ArgumentException(int w1, double c1, int w2, double c2)
        {
            var f1 = new Food(w1, c1);
            var f2 = new Food(w2, c2);

            Assert.That(() => f1 - f2, Throws.ArgumentException);
        }
    }
}