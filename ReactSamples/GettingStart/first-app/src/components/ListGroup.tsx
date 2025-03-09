import { useState } from "react";
import { Fragment } from "react/jsx-runtime";

function ListGroup() {
  const cities = [
    { id: 1, name: "Tehran" },
    { id: 1, name: "Paris" },
    { id: 1, name: "Zagreb" },
    { id: 1, name: "Berlin" },
  ];

  // State Hook
  const [selectedIndex, setSelectedIndex] = useState(-1);

  // MouseOver Handler
  const handleMouseOver = (event: React.MouseEvent<HTMLLIElement>) => {
    console.log(event.currentTarget.textContent);
  };

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
