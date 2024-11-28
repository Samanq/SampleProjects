# Webpack Sample
Webpack is a static module bundler for modern JavaScript applications.<br/>
It takes modules with dependencies and generates static assets representing those modules.

## Setup Webpack
With these step we can easily setup webpack without any configuration.
1. Before everything make sure the Node.js is installed.

2. Navigate to your project where you want to install npm packages. and run the following command to create the **package.json**
    ```powershell
    npm init -y
    ```

3. Create a folder named **src** and create an empty JS file with the src folder and name it  **index.js**

4. Install the **Webpack** and **webpack-cli** by running the following command. You can also find the documentation here https://webpack.js.org/guides/installation/
    ```powershell
    npm install webpack webpack-cli --save-dev
    ```

5. Open **package.json** file and add a build command in the **scripts** object to run the webpack.
    ```json
    {
        "scripts": {
            "test": "echo \"Error: no test specified\" && exit 1",
            "build": "webpack ./src/index.js --output-path ../wwwroot/js --output-filename index.bundle.js"
        }
    }
    ```
6. If you're using a .NET project, you can open the .csproj file of your project and the these Pre-build commands.
    ```c#
    <Target Name="PreBuild" BeforeTargets="PreBuildEvent">
        <Exec Command="npm install" WorkingDirectory="npm" />
        <Exec Command="npm run build" WorkingDirectory="npm" />
    </Target>
    ```
7. To add the configuration file you need to create a file and name it <code>webpack.config.js</code>.<br/>You can use [createapp.dev](https://createapp.dev/webpack) website to create the config file or create it manually.<br/>You can also initialize the config file by the following command <code>npx webpack init</code>.<br/>
When we use a config  file we should remove the entry point and output path from the **build** command in the **package.json** <code>"build": "webpack"</code><br/>
Final result could be like this
    ```js
    const webpack = require('webpack');
    const path = require('path');

    const config = {
        entry: './src/index.js',
        mode: 'development',
        output: {
            path: path.resolve(__dirname, '../wwwroot/dist'),
            filename: 'bundle.js'
        }
    };

    module.exports = config;
    ```

## Core concepts

### Modules
A module in Webpack is a piece of code that encapsulates some functionality of your application.<br>
Modules can be JavaScript files, but they can also include other assets such as stylesheets, images, fonts, etc.<br>
These are individual files that encapsulate some functionality. Each module typically corresponds to a single file.

### Entry
This is the starting point of your application. Webpack will start from this file and build a dependency graph of all the modules your application needs.
[Documentation](https://webpack.js.org/concepts/#entry)

### Output
This is where Webpack will emit the bundled files, usually in a dist folder.
[Documentation](https://webpack.js.org/concepts/#output)

### Loaders
Loaders allow you to **pre-process** files as you import or load them. For example, you can use loaders to transpile TypeScript to JavaScript, or **load CSS files**.
[Documentation](https://webpack.js.org/concepts/#loaders)

### Plugins
[Documentation](https://webpack.js.org/concepts/#plugins)

### Mode
By setting the <code>mode</code> parameter to either <code>development</code>, <code>production</code> or <code>none</code>, you can enable webpack's built-in optimizations that correspond to each environment. The default value is <code>production</code>.<br/>

```js
// production for 
// Optimizes the bundle for performance and smaller size.
// Enables minification and other optimizations.
// Sets the process.env.NODE_ENV to "production".
module.exports = {
  mode: 'production',
};

// Or development for 
// Focuses on improving the development experience.
// Provides detailed error messages and useful warnings.
// Sets the process.env.NODE_ENV to "development".
// Outputs an unminified bundle for easier debugging.
module.exports = {
  mode: 'development',
};


// Or none for
// Disables all default optimizations.
// Useful if you want to have full control over the configuration and optimizations.
module.exports = {
  mode: 'none',
};
```



## Sample of usage
Lets install and use bootstrap in our project.

First we need to install the bootstrap package by running <code>npm install bootstrap</code> command.

Also install css-loader <code>npm install --save-dev css-loader</code> and <br/>
style-loader <code> npm install --save-dev style-loader</code>

Add a module in <code>webpack.config.js</code>
```js
module: {
    rules: [
        {
            test: /\.css$/, // Match any file ending in .css
            use: [
                'style-loader', // Injects CSS into the DOM
                'css-loader'    // Interprets `@import` and `url()` like `import/require()` and will resolve them
            ]
        }
    ]
}
```
Now open your entry point file, which in my case is, <code>index.js</code> and import the bootstrap css file
```js
import 'bootstrap/dist/css/bootstrap.min.css';
```
And now in our HTML file you can reference your output (bundle) file.
```HTML
<!DOCTYPE html>
<html lang="en">

<head>...</head>

<body>
    ...
    <script src="dist/bundle.js"></script>
</body>

</html>

```

The final <code>webpack.config.js</code> would be like this
```js
const webpack = require('webpack');
const path = require('path');

const config = {
    entry: './src/index.js',
    mode: 'development',
    output: {
        path: path.resolve(__dirname, '../wwwroot/dist'),
        filename: 'bundle.js'
    },
    module: {
        rules: [
            {
                test: /\.css$/,
                use: [
                    'style-loader',
                    'css-loader'
                ]
            }
        ]
    }
};

module.exports = config;

```
And the <code>package.json</code> like this
```JSON
{
  "name": "webpacksample",
  "version": "1.0.0",
  "main": "index.js",
  "scripts": {
    "build": "webpack"
  },
  "keywords": [],
  "author": "SamanQaydi",
  "license": "ISC",
  "description": "",
  "dependencies": {
    "bootstrap": "^5.3.3"
  },
  "devDependencies": {
    "css-loader": "^7.1.2",
    "style-loader": "^4.0.0",
    "webpack": "^5.96.1",
    "webpack-cli": "^5.1.4"
  }
}

```