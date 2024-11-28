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
### Get Cookies
```JS
console.log(document.cookie);
```
or
```JS
const cookies = document.cookie.split('; ').reduce((acc, cookie) => {
    const [name, value] = cookie.split('=');
    acc[name] = decodeURIComponent(value);
    return acc;
}, {});

console.log(cookies);
```

---
## Get Location info
### Get hash 
```JS
// Getting the value of the hash location
// If the URL is: https://example.com/#Article
console.log(window.location.hash); // The result of this command is "#Article"
```

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

## API

## Send Request to Server
### Fetch
```JS
const url = 'https://catfact.ninja/fact';

// Send a GET request using the fetch API
fetch(url)
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }
        return response.json();
    })
    .then(data => {
        console.log('Success:', data);
    })
    .catch(error => {
        console.error('There has been a problem with your fetch operation:', error);
    });
```
### XML Http Request
```JS
// Define the URL to which the GET request will be sent
const url = 'https://catfact.ninja/fact';

// Create a new XMLHttpRequest object
const xhr = new XMLHttpRequest();

// Configure it: GET-request for the URL
xhr.open('GET', url, true);

// Set up a function to handle the response data
xhr.onload = function() {
    if (xhr.status >= 200 && xhr.status < 300) {
        const data = JSON.parse(xhr.responseText);
        console.log('Success:', data);
    } else {
        console.error('Request failed. Returned status of ' + xhr.status);
    }
};

// Set up a function to handle any errors
xhr.onerror = function() {
    console.error('There has been a problem with your XMLHttpRequest operation.');
};

// Send the request
xhr.send();
```



## Working With String
### Slice
```JS
const sample = "#This is a test.";
const result = sample.slice(5,8); // Slicing from 5th character to 8th character.
const result2 = sample.slice(1); // The result is: "This is a test."
console.log(result); // The Result is : "is"
```
---
