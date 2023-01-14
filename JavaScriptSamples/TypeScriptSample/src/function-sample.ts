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
