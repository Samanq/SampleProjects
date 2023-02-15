namespace XUnitSample.Tests.ExternalTestData;

public class ExternalNumbersTestData
{
    public static IEnumerable<object[]> SumTestData
    {
        get
        {
            string[] csvLines = File.ReadAllLines(@"ExternalTestData\SumTestCases.csv");

            var testCases = new List<object[]>();

            foreach (var csvLine in csvLines)
            {
                IEnumerable<int> values = csvLine.Split(',').Select(int.Parse);

                object[] testCase = values.Cast<object>().ToArray();

                testCases.Add(testCase);
            }

            return testCases;
        }
    }
}
