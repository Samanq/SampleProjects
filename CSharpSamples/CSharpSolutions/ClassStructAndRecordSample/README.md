# Class, Struct and Record
**Classes** are usually used to model complex objects. <br>

**Structs** are usually used to represent simple and light objects. <br>

**Records** are usually used to represent immutable objects.<br>

**Class** is a reference type but, **Struct** is a value type.

**Record** by default is a reference type, however it can be defined as a **record struct**.
```C#
MyClass myClass = new() { Number = 1 };
MyRecord myRecord = new(Number: 1);
MyStruct myStruct = new() { Number = 1 };

Console.WriteLine($"MyClass : {myClass.Number} ");
Console.WriteLine($"MyRecord : {myRecord.Number} ");
Console.WriteLine($"MyStruct : {myStruct.Number} \n");

ChangeClassNumber(myClass);
ChangeStructNumber(myStruct);

Console.WriteLine($"MyClass : {myClass.Number} * The value of the Number has changed because class is a reference type.");
Console.WriteLine($"MyStruct : {myStruct.Number} * The value of the Number has NOT changed because struct is a value type.");

static void ChangeClassNumber(MyClass myClass)
{
    myClass.Number += 2;
}

static void ChangeRecordNumber(MyRecord myRecord)
{
    // myRecord.Number += 2; Error: this object is immutable and we can't change the value.
}

static void ChangeStructNumber(MyStruct myStruct)
{
    myStruct.Number += 2;
}

// Defining a class, struct and record objects.
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
    int Number
);
```
Result:
```
MyClass : 1
MyRecord : 1
MyStruct : 1

MyClass : 3 * The value of the Number has changed because class is a reference type.
MyStruct : 1 * The value of the Number has NOT changed because struct is a value type.
```

## Record
Records are usually used to represent **immutable** data structure. <br>
Record introduced in C# 9. <br>
A **with** expression produces a copy of its operand with the specified properties and fields modified.<br>

```C#
MyRecord myRecord = new(Name: "John", Number: 1);

// A with expression produces a copy of its operand with the specified properties and fields modified.
var newMyRecordAnotherNumber = myRecord with { Number = 2 };

public record MyRecord
(
    string Name,
    int Number
);
Console.WriteLine($"Name: {myRecord.Name}, Number: {myRecord.Number}");
Console.WriteLine($"Name: {newMyRecordAnotherNumber.Name}, Number: {newMyRecordAnotherNumber.Number}");
```
It's possible to define a record as **struct**. (ValueType).<br>
By default, record is a **record class**. (reference Type)
```C#
public record struct MyRecordStruct
(
    int Number
);

public record class MyRecordClass
(
    int Number
);
```
---

### Deconstructing a record. 
We can also deconstruct a record like a tuple 
```C#
MyRecord myRecord = new(Name: "John", Number: 1);

string localName;
int localNumber;
(localName,localNumber) = myRecord;

Console.WriteLine($"LocalName: {localName}, LocalNumber: {localNumber}");


public record MyRecord(string Name, int Number);
```
```C#
MyRecord myRecord = new(Name: "John", Number: 1);

(string localName, int localNumber) = myRecord;

Console.WriteLine($"LocalName: {localName}, LocalNumber: {localNumber}");

public record MyRecord(string Name, int Number);
```
```C#
MyRecord myRecord = new(Name: "John", Number: 1);

// Deconstructing a record
var (localName, localNumber) = myRecord;

Console.WriteLine($"LocalName: {localName}, LocalNumber: {localNumber}");

public record MyRecord(string Name,int Number);
```
---
