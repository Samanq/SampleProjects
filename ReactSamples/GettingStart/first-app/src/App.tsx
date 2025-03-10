import { useState } from "react";
import "./App.css";
import Alert from "./components/Alert";
import Button from "./components/Button";
import ListGroup from "./components/ListGroup";

function App() {
  const [showError, setShowError] = useState(false);

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

  const onClickError = () => {
    setShowError(true);
  };

  return (
    <div>
      {/* passing the items, heading and the click function props to the ListGroup component */}
      <ListGroup items={cities} heading="Cities" onClickItem={onClickItem} />
      <Alert type="success">
        <span>This is a success alert</span>
      </Alert>
      {showError && <Alert type="error">This is an error alert</Alert>}
      <Button buttonType="danger" onClick={onClickError}>
        Click me!
      </Button>
    </div>
  );
}
export default App;
