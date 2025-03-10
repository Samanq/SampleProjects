import { Children } from "react";

interface ButtonProps {
  children: string;
  buttonType?: "primary" | "secondary" | "success" | "danger";
  onClick?: () => void;
}

const Button = ({ children, buttonType = "primary", onClick }: ButtonProps) => {
  return (
    <button type="button" className={"btn btn-" + buttonType} onClick={onClick}>
      {children}
    </button>
  );
};

export default Button;
