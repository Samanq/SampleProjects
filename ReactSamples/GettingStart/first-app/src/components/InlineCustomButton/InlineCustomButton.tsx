import { ReactNode } from "react";

interface Props {
    children: ReactNode;
}

const InlineCustomButton = ({children} : Props) => {
    return <button style={{backgroundColor: 'blue', color:'white'}}>{children}</button>
}

export default InlineCustomButton;