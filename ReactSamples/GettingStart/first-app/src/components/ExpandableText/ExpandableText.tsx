import { useState } from "react";

interface Props {
  children: string;
  maxLength: number;
}

const ExpandableText = ({ children, maxLength }: Props) => {
  const [length, setLength] = useState(maxLength);
  const [isExpanded, setExpanded] = useState(false);

  const handleExpand = () => {
    if (isExpanded) {
      setLength(maxLength);
    } else {
      setLength(children.length);
    }
    setExpanded(!isExpanded);
  };

  const text =
    children.length > maxLength ? children.substring(0, length) : children;
  const buttonText = isExpanded ? "Read less ..." : "Read more ...";

  return (
    <>
      <p>
        {text} <button onClick={handleExpand}>{buttonText}</button>
      </p>
    </>
  );
};

export default ExpandableText;
