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
