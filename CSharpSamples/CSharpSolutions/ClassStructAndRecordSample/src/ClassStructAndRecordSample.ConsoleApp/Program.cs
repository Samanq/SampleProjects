MyClass myClass = new() { Number = 1 };
MyRecord myRecord = new(Name: "John", Number: 1);
MyRecordStruct myRecordStruct = new(Number: 1);
MyStruct myStruct = new() { Number = 1 };


Console.WriteLine($"MyClass : {myClass.Number} ");
Console.WriteLine($"MyRecord : {myRecord.Number} ");
Console.WriteLine($"MyRecordStruct : {myRecordStruct.Number} ");
Console.WriteLine($"MyStruct : {myStruct.Number} \n");

ChangeClassNumber(myClass);
ChangeStructNumber(myStruct);

Console.WriteLine($"MyClass : {myClass.Number} * The value of the Number has changed because class is a reference type.");
Console.WriteLine($"MyStruct : {myStruct.Number} * The value of the Number has NOT changed because struct is a value type.\n");

// A with expression produces a copy of its operand with the specified properties and fields modified.
var newMyRecordAnotherNumber = myRecord with { Number = 2 };
Console.WriteLine($"Name: {newMyRecordAnotherNumber.Name}, Number: {newMyRecordAnotherNumber.Number}\n");


// Method 1
// string localName;
// int localNumber;
// (localName,localNumber) = myRecord;

// Method 2
//(string localName, int localNumber) = myRecord;

// Method 3
// Deconstructing a record
var (localName, localNumber) = myRecord;

Console.WriteLine($"LocalName: {localName}, LocalNumber: {localNumber}");

static void ChangeClassNumber(MyClass myClass)
{
    myClass.Number += 2;
}

static void ChangeRecordNumber(MyRecord myRecord)
{
    //myRecord.Number += 2; Error: this object is immutable and we can't change the value
}

static void ChangeStructNumber(MyStruct myStruct)
{
    myStruct.Number += 2;
}

// Defining some class, struct and record objects.
public class MyClass
{
    public int Number { get; set; }
}

public struct MyStruct
{
    public int Number { get; set; }
}

public record MyRecord
(
    string Name,
    int Number
);

public record struct MyRecordStruct
(
    int Number
);
public record class MyRecordClass
(
    int Number
);