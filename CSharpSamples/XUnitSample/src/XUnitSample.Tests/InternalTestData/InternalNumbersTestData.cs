namespace XUnitSample.Tests.InternalTestData;

public class InternalNumbersTestData
{
    public static IEnumerable<object[]> SumTestData
    {
        get
        {
            yield return new object[] { 1, 2, 3 };
            yield return new object[] { 2, 1, 3 };
            yield return new object[] { -1, -1, -2 };
        }
    }
}
