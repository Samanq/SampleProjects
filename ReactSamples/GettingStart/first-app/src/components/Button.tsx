import { Children } from "react";

interface ButtonProps {
  children: string;
  buttonType?: "primary" | "secondary" | "success" | "danger";
}

const Button = ({ children, buttonType = "primary" }: ButtonProps) => {
  return (
    <button type="button" className={"btn btn-" + buttonType}>
      {children}
    </button>
  );
};

export default Button;
