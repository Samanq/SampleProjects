# JS Cheat Sheet

## Getting elements

### Get by Tag
```JS
const paragraphs = document.getElementsByTagName("p");
```
### Get element by ID
```JS
const button = document.getElementById("myButton");
```

### Get elements by Class
```JS
const buttons = document.getElementsByClassName("myButton");
```

### Get element(s) by Query Selector
```JS
const button = document.querySelector('.my-button');

const buttons = document.querySelectorAll(".myButton");

// Get By specific attribute
const disabledButton = document.querySelector("button:disabled");

// Get By mixed attributes
// Get the disabled button with the class 'primary'
const disabledPrimaryButton = document.querySelector("button.primary:disabled");

// Get an input with a specific type
const firstCheckbox = document.querySelector("input[type='checkbox']");
```

### Get By Element
```JS
// Get all forms in the document
const forms = document.forms;

// Get all images in the document
const images = document.images;

// Get all links in the document
const links = document.links;

// Get all named anchors in the document
const anchors = document.anchors;
```
---

## Get Document info
### Get Current URL
```JS
// Get the full URL of the current page
const currentUrl = window.location.href;
```

### Get Query Strings (URL Parameters)
```JS
// Assuming "?post=1234&action=edit" is the query string
const params = new URLSearchParams(window.location.search);

const post = params.get("post"); // is the string "1234"
const action = params.get("action"); // is the string "edit"

// Search for a parameter
if(params.has("post")) {
    let result = params.get("post");
    console.log(post);
}

// Iterate the params
params.forEach((value, key) => {
    console.log(`${key}: ${value}`);
});
```
---
## Add Attributes
### Add event Listener
```JS
const button = document.getElementById("myButton");

button.addEventListener("click", () => {
    alert("Button was clicked!");
});
```

## Loops
### Foreach
```JS
// Get all button elements by their class name using getElementsByClassName
const buttonsByClassName = document.getElementsByClassName("myButton");

// Convert the HTMLCollection to an array
const buttonArray = Array.from(buttonsByClassName);

// Iterate the array and add event listeners
.forEach(button => {
    button.addEventListener("click", () => {
        alert("Button was clicked!");
    });
});
```