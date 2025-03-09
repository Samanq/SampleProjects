import "./App.css";
import Alert from "./components/Alert";
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
      {/* passing the items, heading and the click function props to the ListGroup component */}
      <ListGroup items={cities} heading="Cities" onClickItem={onClickItem} />
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
