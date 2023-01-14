// Variable with type any
let temp;

temp = 10;
temp = "some texts"

console.log(temp)
//----------------------------------------------//
// Array
let even_numbers: number[] = [2,4,6];
let odd_numbers = [1,3,5];
let keywords: string[] = ['first', 'second', 'third'];
//----------------------------------------------//
// Tuple
let user: [number, string] = [1, "John"]

console.log("Id: " + user[0])
console.log("Name: " + user[1])
//----------------------------------------------//
// Enum
const samll = 0;
const medium = 1;
const large = 2;

// Enums shoud follow Pacal naming convention
enum Size{Samll, Medium, Large} // Default values are 0,1,2
enum SecondSize{Samll = 10, Medium = 50, Large = 100}
enum ThirdSize{Samll = 's', Medium = 'm', Large = 'l'}

let mySize = ThirdSize.Medium