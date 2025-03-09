# Getting Start with React

## Summary
React is a **JavaScript library** for building dynamic and reactive user interfaces.<br/>
Since React is a **library** and not a framework like VUE or Angular, we need other tools for routing, state management, from validation, etc. 

There are three important concepts in react.
### Components
* Components are like functions, they receive objects and return an UI.
* Components are reusable. 
* Components can contain other components.
* Can be used as regular html element like this <Component / >
* Components can have a **private state** that can hold data that may change over the life cycle of the component.
* component's name must start with **capital letter**.

There are two types of components.
* **Function Component**
* **Class Component**

### Reactive Updates
* When the input of a component changes, the output could be changes as well.

### Virtual views in memory
* Generating HTML using JavaScript.
* React uses the virtual dom to compare versions of the UI in memory before it acts on them.
---
## Installation
1. Download and install **Node.js** 
---
## Creating New Project
We can use `vite` to create a rect app. 
1. Open terminal, navigate to your project folder and run `npm create vite@latest` to create a React app.
2. Set a name for your project.
3. Select `React` as framework
4. Select `TypeScript` as language.
5. Navigate to your project folder which already created and run `npm install` to install te dependencies.
6. run `code .` to open the project in VSCode.
7. Open terminal and run `npm run dev` to run the application.
---
## Installing Packages
To install more packages we can navigate to our project folder where `package.json` file is located and run `npm install <packageName>`
```powershell
npm install bootstrap
```
---
## Components
Components are ...
Let's create a folder in our project folder and call it `components` to store all the components, then create a new components with `Pascal` naming convention.<br/>
Example (ListGroup.tsx)
```tsx
function ListGroup(){
    return(
        <h1>Hello World</h1>
    )
}

export default ListGroup;
```

Components cannot return more than 1 element.<br>
To use multiple element you need to all the elements into a `Fragment`.
```tsx
import { Fragment } from "react/jsx-runtime";

function ListGroup() {
  return (
    <Fragment>
      <h1>List Group</h1>
      <ul className="list-group">
        <li className="list-group-item">Cras justo odio</li>
      </ul>
    </Fragment>
  );
}

export default ListGroup;
```
Or shorter version
```tsx
function ListGroup() {
  return (
    <>
      <h1>List Group</h1>
      <ul className="list-group">
        <li className="list-group-item">Cras justo odio</li>
      </ul>
    </>
  );
}

export default ListGroup;

```

---
## Item Enumeration
We can enumerate arrays with the `map` function.<br>
Each item needs a unique `key`
```tsx
import { Fragment } from "react/jsx-runtime";

const cities = [
  { id: 1, name: "Tehran" },
  { id: 2, name: "Paris" },
  { id: 3, name: "Zagreb" },
  { id: 4, name: "Berlin" },
];
function ListGroup() {
  return (
    <Fragment>
      <h1>List Group</h1>
      <ul className="list-group">
        {cities.map((item) => (
          <li key={item.id} className="list-group-item">
            {item.name}
          </li>
        ))}
      </ul>
    </Fragment>
  );
}

export default ListGroup;

```
## Conditional rendering
...
```tsx
```

## Events
```tsx
import { Fragment } from "react/jsx-runtime";

const cities = [
  { id: 1, name: "Tehran" },
  { id: 2, name: "Paris" },
  { id: 3, name: "Zagreb" },
  { id: 4, name: "Berlin" },
];

// MouseOver Handler
const handleMouseOver = (event: React.MouseEvent<HTMLLIElement>) => {
  console.log(event.currentTarget.textContent);
  console.log(event);
};

function ListGroup() {
  return (
    <Fragment>
      <h1>List Group</h1>
      <ul className="list-group">
        {cities.map((item) => (
          <li
            key={item.id}
            className="list-group-item"
            onClick={() => console.log(item.name)}
            onMouseOver={handleMouseOver}
          >
            {item.name}
          </li>
        ))}
      </ul>
    </Fragment>
  );
}

export default ListGroup;

```
## States
To handle state of the component we need to import the `state` hook and use `useState` object.<br/>
> **Hooks** can only be called inside of the body of a function component.
```tsx
// Import useState
import { useState } from "react";
import { Fragment } from "react/jsx-runtime";

function ListGroup() {
  const cities = [
    { id: 1, name: "Tehran" },
    { id: 2, name: "Paris" },
    { id: 3, name: "Zagreb" },
    { id: 4, name: "Berlin" },
  ];

  // State Hook
  // selectIndex is the value of the state.
  // setSelectedIndex is a function to update the value of state.
  const [selectedIndex, setSelectedIndex] = useState(-1);

  return (
    <Fragment>
      <h1>List Group</h1>
      <ul className="list-group">
        {cities.map((item, index) => (
          <li
            key={item.id}
            className={
              selectedIndex === index
                ? "list-group-item active"
                : "list-group-item"
            }
            onClick={() => setSelectedIndex(index)}
          >
            {item.name}
          </li>
        ))}
      </ul>
    </Fragment>
  );
}

export default ListGroup;

```

## Props
With Props we can pass a model to a component.
First we need to define an interface as `props`, then add it to the our component parameters.
<br>
We can also pass `functions` as parameter to the component.
<br>
`Props` must be treated immutable.
```tsx
import { useState } from "react";
import { Fragment } from "react/jsx-runtime";

// Define Props Interface
interface Props {
  items: { id: number; name: string }[];
  heading: string;
  onClickItem: (item: string) => void;
}

// destructuring the props object
function ListGroup({ items, heading, onClickItem }: Props) {
  // State Hook
  const [selectedIndex, setSelectedIndex] = useState(0);

  // MouseOver Handler
  const handleMouseOver = (event: React.MouseEvent<HTMLLIElement>) => {
    console.log(event.currentTarget.textContent);
  };

  return (
    <Fragment>
      <h1>{heading}</h1>
      <ul className="list-group">
        {items.map((item, index) => (
          <li
            key={item.id}
            className={
              selectedIndex === index
                ? "list-group-item active"
                : "list-group-item"
            }
            onClick={() => {
              setSelectedIndex(index);
              onClickItem(item.name);
            }}
            onMouseOver={handleMouseOver}
          >
            {item.name}
          </li>
        ))}
      </ul>
    </Fragment>
  );
}

export default ListGroup;

```
Then, when we use the component, we have to pass the parameters.
```tsx
import "./App.css";
import ListGroup from "./components/ListGroup";

function App() {
  // Defining the items
  const cities = [
    { id: 1, name: "Tehran" },
    { id: 2, name: "Paris" },
    { id: 3, name: "Zagreb" },
    { id: 4, name: "Berlin" },
  ];

  // Defining the onClickItem function
  const onClickItem = (item: string) => {
    alert(item);
  };

  return (
    <div>
      {/* passing the items and heading props to the ListGroup component */}
      <ListGroup items={cities} heading="Cities" onClickItem={onClickItem} />
    </div>
  );
}
export default App;

```

## Passing Children To Component
We can pass `children` to component.<br>
For passing HTML elements as children, you have use `ReactNode` as the type of `children`
```tsx
import { ReactNode } from "react";

interface AlertProps {
  // Define children prop as ReactNode
  children: ReactNode;
  type: "success" | "error" | "warning" | "info";
}

const Alert = ({ children, type }: AlertProps) => {
  const getClassName = () => {
    switch (type) {
      case "success":
        return "alert alert-success";
      case "error":
        return "alert alert-danger";
      case "warning":
        return "alert alert-warning";
      case "info":
      default:
        return "alert alert-primary";
    }
  };

  return (
    <>
      <div className={getClassName()} role="alert">
        {children}
      </div>
    </>
  );
};

export default Alert;

```
To use the component we have to pass the children
```tsx
import "./App.css";
import Alert from "./components/Alert";

function App() {
  return (
    <div>
      <Alert type="success">
        <span>This is a success alert</span>
      </Alert>
      <Alert type="error">
        This is a error alert
      </Alert>
    </div>
  );
}
export default App;

```

## NN