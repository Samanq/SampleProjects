import { ReactNode } from "react";
// Importing styles from the module
import styles from "./CustomButton.module.css";

interface Props {
    children: ReactNode;
}

const CustomButton = ({children} : Props) => {
    return <button className={[styles.customButton, styles.green].join(' ')}>{children}</button>
}

export default CustomButton;