import { useState } from "react";
import "./App.css";
import Alert from "./components/Alert";
import Button from "./components/Button";
import ListGroup from "./components/ListGroup";
import CustomButton from "./components/CustomButton/CustomButoon";
import VanillaCustomButton from "./components/VanillaCustomButton/VanillaCustomButton";
import InlineCustomButton from "./components/InlineCustomButton/InlineCustomButton";
import LikeButton from "./components/LikeButton/LikeButton";
import ExpandableText from "./components/ExpandableText/ExpandableText";
import UseEffectComponentSample from "./components/UseEffectComonentSample/UseEffectComonentSample";

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

      <CustomButton >
        I'm a custom button
      </CustomButton>

      <VanillaCustomButton >
        Vanilla Button
      </VanillaCustomButton>

      <InlineCustomButton >
        Inline Button
      </InlineCustomButton>

      <LikeButton />
      <ExpandableText maxLength={5}>
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam
          pulvinar risus non quam. Sed ultricies, purus sit amet volutpat
          sagittis, ipsum tortor viverra turpis, sit amet interdum felis turpis
          nec purus. Nulla facilisi. Sed non semper odio. Sed ut erat nec purus
          lacinia lacinia. Nulla facilisi. Sed non semper odio. Sed ut erat nec
          purus lacinia lacinia.
      </ExpandableText>

      <UseEffectComponentSample />
    </div>
  );
}
export default App;
