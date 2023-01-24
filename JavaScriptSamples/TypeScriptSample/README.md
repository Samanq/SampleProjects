# TypeScript
TypeScript is a strongly typed programming language that builds on **JavaScript**, giving you better tooling at any scale.
 - TypeScript is JavaScript with **Type Checking**. It means we can define the type of variable when are declaring them.
 - Anything we can do with JavaScript, we can also do with TypeScript.
 - **.ts** files should compile to **.js** files with TypeScript compile. (Transpilation step)
---

## Installation
1. First Install nodejs from https://nodejs.org/en/
2. Then run this command to install TypeScript compiler globally.
```powershell
npm i -g typescript
```
or
```powershell
sudo npm i -g typescript
```
3. For getting the TypeScript installed compiler version run:
```powershell
tsc -v
```
---
## Compile **ts** files
1. in the src folder create new file named **index.ts**
```typescript
console.log("Hello world");
```
2. In terminal navigate to src folder and run this command to compile the **ts** file
```powershell
tsc index.ts
```
3. Now you should see index.js in the src folder and you can run it by node command
```powershell
node index.js
```
---
## Configuring the TypeScript compiler
Open terminal and navigate to src folder and run this command to create the compiler configuration file (tsconfig.json)
```powershell
tsc --init
```
### tsconfig properties
- **target** : is the version of JavaScript that the compiler use to compile the ts files.
- **module** : 
- **rootDir** : Specify the directory of source files.
- **outDir** : Specify the directory of compiled files.
- **removeComments** : Remove comments from compiled files.
- **noEmitOnError** : Compiler doesn't generate js files if there are error in ts codes. (Always enable it)
- **sourceMap** : It's a file that specifies how each line of TypeScript code maps to the generated JavaScript file.
- **noImplicitAny** : Compiler will show error for using implicit any type. (if you turn it off the compiler doesn't complain about using any type in functions) (it's not recommended to turn off this option)
- **noUnusedParameters** : Compailer raise an warning when a function parameter isn't read.
- **noImplicitReturns** : Compiler checks all path of functions return a value.
- **noUnusedLocals** : Compiler checks for unused local variables.

> Now you can run **tsc** command to compile all the **ts** files.

---

## Debugging
1. Set **sourceMap** to true in **tsconfig.json**
2. In VsCode open **debug panel** and create a lunch.json file and as a debugger select **Node.js** (This will create a file named launch.json)
3. Open **launch.json** and add this line after "program"
```json
"preLaunchTask": "tsc: build - tsconfig.json",
```
It shoudl be like this
```json
{
    // Use IntelliSense to learn about possible attributes.
    // Hover to view descriptions of existing attributes.
    // For more information, visit: https://go.microsoft.com/fwlink/?linkid=830387
    "version": "0.2.0",
    "configurations": [
        {
            "type": "node",
            "request": "launch",
            "name": "Launch Program",
            "skipFiles": [
                "<node_internals>/**"
            ],
            "program": "${workspaceFolder}\\debug-sample.ts",
            "preLaunchTask": "tsc: build - tsconfig.json",
            "outFiles": [
                "${workspaceFolder}/**/*.js"
            ]
        }
    ]
}
```
4. Open the debug panel again and press **Launch Program**
---
## Types in TypeScript
### JavaScript types
- **number**
```typescript
// let <variable name>: [<variable type>] = <value>
// You can use "_" as a digit seperator in numbers for having more readable code.
// You can also remove the variable type annotation, TypeScript can understand the type based on value.
let first_number: number = 1000;
let second_number: number = 10_000_000;
let third_number = 10;
```

- **string** 
```typescript
let title: string = 'Type Script Sample';
```

- **boolean**
```typescript
let is_published: boolean = true;
```

- **array**
```typescript
let even_numbers: number[] = [2,4,6];
let odd_numbers = [1,3,5];
let keywords: string[] = ['first', 'second', 'third'];
```
- **null**
- **undefined**
- **object** 
### TypeScript Extendent types
- **any**

    If you delcate variable and not specify the type and also not assigning any value to it, the type will be **any** which is a dynamic type and it's not a safe type variable anymore that means you can change it during the code.
```typescript
// Variable with type any
let temp;

temp = 10;
temp = "some texts"

console.log(temp)
```

- **unknown** 
- **never**
- **enum**

    If we use const for enum, compiler will generate more optimized code
```typescript
// Enums shoud follow Pacal naming convention
enum Size{Samll, Medium, Large} // Default values are 0,1,2
const enum SecondSize{Samll = 10, Medium = 50, Large = 100}
const enum ThirdSize{Samll = 's', Medium = 'm', Large = 'l'}

let mySize = ThirdSize.Medium
```
- **tuple**

    Tuple is a **fixed lenght** array where each element has a particular type
```typescript
let user: [number, string] = [1, "John"]

console.log("Id: " + user[0])
console.log("Name: " + user[1])
```
---
## Functions
For defining a function we can use this pattern.
function \<name>([<parameterName: parameterType>, <anotherParameterName: ParameterType>]): \<returnType>{}

```typescript
// Defining a function
// Third number is an optional parameter, if user doesn't send this argument default value will used.
function addNumbers(firstNumber: number, secondNumber: number, thirdNumber = 0) : number
{
    return firstNumber + secondNumber + thirdNumber;
}

// Calling a function
addNumbers(10, 20, 3);

// Calling a function without optional parameter
addNumbers(10, 20);

```
### Tips: 
 - JavaScripts by default always return **undefined** for functions if the function doesn't return a value.

 ---

 ## Objects
 First we should define the object, then initialize it and finally we can use it.
 ```Typescript
 let student: {
  readonly id: number; // This property is readonly and user cannot change it's value.
  name: string;
  nickName?: string; // This property is optional.
  study: (subjectTitle: string, duration: number) => void;
} = {
  id: 1,
  name: "john",
  nickName: "johny",
  study: (subjectTitle: string, duration: number) => {
    console.log("is studying " + subjectTitle + " for " + duration + " hours.");
  },
}; // Initializing the object.

// Using the object.
student.name = "John";
student.study("C#", 2);
 ```
 ---
