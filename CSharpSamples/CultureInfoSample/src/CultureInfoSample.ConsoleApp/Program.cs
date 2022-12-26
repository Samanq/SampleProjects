using CultureInfoSample.ConsoleApp;
using Newtonsoft.Json;
using System.Globalization;

var cultureInfoModels = new List<CultureInfoModel>();


var cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures);

// Getting all cultures.
foreach (var culterInfo in CultureInfo.GetCultures(CultureTypes.NeutralCultures))
{
    cultureInfoModels.Add(
        new CultureInfoModel
        {
            DisplayName = culterInfo.Name,
            EnglishName = culterInfo.EnglishName,
            Name = culterInfo.Name,
            ShortDatePattern = culterInfo.DateTimeFormat.ShortDatePattern
        });
}


// Converting to json
var jsonResult = JsonConvert.SerializeObject(cultureInfoModels, Formatting.Indented);

// Saving the result into a file.
var executingPath = Environment.CurrentDirectory;
File.WriteAllText(executingPath + "\\jsonResult.json", jsonResult);
Console.WriteLine(jsonResult);
