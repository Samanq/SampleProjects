import { useState } from "react";

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
    <>
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
    </>
  );
}

export default ListGroup;
