using NUnit.Framework;

namespace StructuresTests.UnitTests
{
    [TestFixture]
    public class FractionTests
    {
        Fraction f;

        [SetUp]
        public void Setup()
        {
            f = new Fraction(-2, 3);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.That(f.Numerator, Is.EqualTo(-2));
            Assert.That(f.Denumerator, Is.EqualTo(3));
        }

        [Test]
        public void ToStringTest()
        {
            Assert.That(f.ToString(), Is.EqualTo("-2/3"));
        }

        [TestCase(1,2,2,4,true)]
        [TestCase(1,2,2,3,false)]
        public void Equals_ToFractions_Result(
            int numerator1,int denumenator1, int numerator2,int denumenator2,bool result)
        {
            var a = new Fraction(numerator1, denumenator1);
            var b = new Fraction(numerator2, denumenator2);

            Assert.That(a.Equals(b), Is.EqualTo(result));
        }

        [Test]
        public void Equals_ToObject_ArgumentException()
        {
            var obj = new object();
            Assert.That(() => f.Equals(obj), Throws.ArgumentException);
        }
    }
}