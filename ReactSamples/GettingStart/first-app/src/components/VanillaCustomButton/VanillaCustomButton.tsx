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
