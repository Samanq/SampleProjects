import { ReactNode } from "react";

interface AlertProps {
  // Define children prop as ReactNode
  children: ReactNode;
  type: "success" | "error" | "warning" | "info";
}

const Alert = ({ children, type }: AlertProps) => {
  const getClassName = () => {
    switch (type) {
      case "success":
        return "alert alert-success";
      case "error":
        return "alert alert-danger";
      case "warning":
        return "alert alert-warning";
      case "info":
      default:
        return "alert alert-primary";
    }
  };

  return (
    <>
      <div className={getClassName()} role="alert">
        {children}
      </div>
    </>
  );
};

export default Alert;
