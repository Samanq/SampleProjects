using System.Reflection;
using Xunit.Sdk;

namespace XUnitSample.Tests.TestDataAttributes;

public class NumbersSumDataAttribute : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        yield return new object[] { 1, 2, 3 };
        yield return new object[] { 2, 1, 3 };
        yield return new object[] { -1, -1, -2 };
    }
}
