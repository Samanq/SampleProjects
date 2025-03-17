# Getting Start with React

## Summary

React is a **JavaScript library** for building dynamic and reactive user interfaces.<br/>
Since React is a **library** and not a framework like VUE or Angular, we need other tools for routing, state management, from validation, etc.

There are three important concepts in react.

### Components

- Components are like functions, they receive objects and return an UI.
- Components are reusable.
- Components can contain other components.
- Can be used as regular html element like this <Component / >
- Components can have a **private state** that can hold data that may change over the life cycle of the component.
- component's name must start with **capital letter**.

There are two types of components.

- **Function Component**
- **Class Component**

### Reactive Updates

- When the input of a component changes, the output could be changes as well.

### Virtual views in memory

- Generating HTML using JavaScript.
- React uses the virtual dom to compare versions of the UI in memory before it acts on them.

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
function ListGroup() {
  return <h1>Hello World</h1>;
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

> **Hooks** can only be called at the top level of components.

> **State variables**, unlike local variables in a function, stay in **memory** as long as the
component is **visible on the screen**. This is because state is tied to the component
instance, and React will destroy the component and its state when it is removed from
the screen.

> React updates state in an **asynchronous** manner, so updates are not applied
**immediately**. Instead, they’re batched and applied at once after all event handlers have
finished execution. Once the state is updated, React re-renders our component.

> A **pure function** is one that always returns the same result given the same input. Pure
functions **should not** modify objects outside of the function.

> React expects our function components to be **pure**. A pure component should always
return the same JSX given the same input.

> To keep our components pure, we should avoid making changes during the render
phase.

> **Strict mode** helps us catch potential problems such as impure components. Starting
from React 18, it is enabled by default. It renders our components twice in development
mode to detect any potential side effects.

> When updating objects or arrays, we should treat them as **immutable** objects. Instead of
mutating them, we should create new objects or arrays to update the state.

> **Immer** is a library that can help us update objects and arrays in a more concise and
mutable way.

> To share state between components, we should lift the state up to the closest parent
component and pass it down as **props** to **child components**.

> The component that holds some state should be the one that updates it. If a child
component needs to update some state, it should notify the parent component using a
callback function passed down as a prop.


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
### Adding Item to state
...
### Removing Item from state
...
### Editing Item in state
...

### Sharing State between components
...

---
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
      <Alert type="error">This is a error alert</Alert>
    </div>
  );
}
export default App;
```

## Styling

### Inline CSS
We can write inline css with `style` just like HTML
```tsx
const InlineCustomButton = ({children} : Props) => {
    return <button style={{backgroundColor: 'blue', color:'white'}}>{children}</button>
}
```
---
### Vanilla CSS
We can write Vanilla (plain) css for the components.

1. Create a `.css` file with the same name as the component and place the css and component in the same folder.
2. Import the `.css` into the component, and now you ues it.

```tsx
import { ReactNode } from "react";
// Import the styles
import "./VanillaCustomButton.css";

interface CustomButtonProps {
  children: ReactNode;
}

const CustomButton = ({ children }: CustomButtonProps) => {
  return <button className="vanillaButton">{children}</button>;
};

export default CustomButton;
```

> Styles can be overridden by other styles.
---
### CSS Modules
To prevent overridden the component styles with other styles, we can use modules.

1. Create a `.css` file with same name as the component, followed by .module.css. e.g: `MyButton.module.css` and place the css and the component in the same folder.
2. Import the css module into the component

```tsx
import { ReactNode } from "react";
// Importing styles from the module
import styles from "./CustomButton.module.css";

interface Props {
  children: ReactNode;
}

const CustomButton = ({ children }: Props) => {
  return <button className={styles.customButton}>{children}</button>;
};

export default CustomButton;
```

If the class name contains `-`, then you need use it like this 
```tsx
const CustomButton = ({children} : Props) => {
    return <button className={styles['style-red']}>{children}</button>
}
```
We can use multiple classes with `join(' ')`
```tsx
const CustomButton = ({children} : Props) => {
    return <button className={[styles.customButton, styles.green].join(' ')}>{children}</button>
}
```
---
### CSS-IN-JS

I will write the description later.

---


### Icons
To use Icons we can install `react-icons` library
```npm
npm install react-icons
```
You can find the list of icons in the official website https://react-icons.github.io/react-icons/
<br>
To use the icons, first we need to import them into the component and use is as a react component.
```tsx
import { ReactNode } from "react";
// Import the styles
import "./VanillaCustomButton.css";
// Import the icon
import { BsLockFill } from "react-icons/bs";

interface Props {
  children: ReactNode;
}

const VanillaCustomButton = ({ children }: Props) => {
  return (
    <button className="vanillaButton">
      {children} <BsLockFill color="green" size={30} /> 
    </button>
  );
};

export default VanillaCustomButton;

```