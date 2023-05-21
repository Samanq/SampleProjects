# Sass Sample 
Sass stands for "Syntactically Awesome Style Sheets." It is a preprocessor scripting language that extends the capabilities of CSS (Cascading Style Sheets). Sass provides additional features and functionality that make it easier to write and maintain CSS code.

Here are some key features of Sass:

1. **Variables**: Sass allows you to define and use variables to store values. This makes it easy to reuse values throughout your stylesheets and quickly make global changes.

2. **Nesting**: With Sass, you can nest CSS selectors inside one another, which helps organize your code and makes it more readable. It also reduces repetition by inheriting properties from parent selectors.

3. **Mixins**: Mixins are reusable blocks of code that can be included in multiple selectors. They are similar to functions and allow you to define a set of styles and apply them wherever needed.

4. **Functions**: Sass provides built-in functions that can be used to perform calculations, manipulate colors, and more. You can also create your custom functions to extend the functionality further.

5. **Partials and Importing**: Sass allows you to split your stylesheets into smaller modular files called partials. These partials can be imported into other Sass files, making it easier to manage and organize your styles.

6. **Inheritance**: Sass supports the concept of inheritance, where selectors can inherit properties from other selectors. This can help reduce redundancy and make your code more maintainable.

7. **Operators**: Sass includes various mathematical and logical operators that can be used to perform calculations and conditional operations within your stylesheets.

To use Sass, you <mark>write your stylesheets using Sass syntax and then compile them into regular CSS files that can be interpreted by web browsers.</mark> There are several ways to compile Sass, including using **command-line tools**, build** systems**, or using **online services**.

---

## Setting up the environment
1. Install **Live Sass Compiler** extension by Glenn Marks in vsCode.

2. In vs code press **ctrl + shift + p** to open command pallet and open for preferencces: Open settings.

3. Add this to the endof the settings.json in order to save the css files into the dist folder
```json
,
"liveSassCompile.settings.formats": [
    {
        "format": "expanded",
        "extensionName": ".css",
        "savePath": "/dist",
        "savePathReplacementPairs": null
    }
]
```
4. Now we should restart the vsCode and the extensions should be ready, we can press the **watch Sass** button at buttom of the page to compile the scss files.

---

## Variables 
 CSS variables are a native feature of CSS that provide a way to define and reuse values in a global scope, while SCSS variables are a feature of the SCSS syntax, offering additional capabilities like math operations and scoping within SCSS files.

For Defining css variable we should use -- syntax and for defining scss variables we use $.

**CSS:**
```css
// Defining a variable
:root{
    --primary-colord: #43877c;
}

// Using a variable
.primary-btn{
    background-color: var(--primary-colord);
}
```
**SCSS:**
```scss
// Defining a variable
$secondary-color : #6fefa0;

// Defining a list variable that contains multiple values.
$red-colors : (
    "dark-red" : #5b0a0a,
    "normal-red" : #da1414,
    "light-red" : #ff8787
);

// Using a scss variable
.primary-btn{
    background-color: $secondary-color;
}

// Getting a value from variable with multiple values
.danger-lable {
  background-color: map-get($red-colors, "dark-red");
}
```
---

## Nesting
