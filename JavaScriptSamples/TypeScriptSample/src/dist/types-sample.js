"use strict";
let temp;
temp = 10;
temp = "some texts";
console.log(temp);
let even_numbers = [2, 4, 6];
let odd_numbers = [1, 3, 5];
let keywords = ['first', 'second', 'third'];
let user = [1, "John"];
console.log("Id: " + user[0]);
console.log("Name: " + user[1]);
const samll = 0;
const medium = 1;
const large = 2;
var Size;
(function (Size) {
    Size[Size["Samll"] = 0] = "Samll";
    Size[Size["Medium"] = 1] = "Medium";
    Size[Size["Large"] = 2] = "Large";
})(Size || (Size = {}));
var SecondSize;
(function (SecondSize) {
    SecondSize[SecondSize["Samll"] = 10] = "Samll";
    SecondSize[SecondSize["Medium"] = 50] = "Medium";
    SecondSize[SecondSize["Large"] = 100] = "Large";
})(SecondSize || (SecondSize = {}));
var ThirdSize;
(function (ThirdSize) {
    ThirdSize["Samll"] = "s";
    ThirdSize["Medium"] = "m";
    ThirdSize["Large"] = "l";
})(ThirdSize || (ThirdSize = {}));
let mySize = ThirdSize.Medium;
//# sourceMappingURL=types-sample.js.map