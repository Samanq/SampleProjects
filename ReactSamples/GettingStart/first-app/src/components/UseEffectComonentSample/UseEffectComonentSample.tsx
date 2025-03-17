import { useEffect } from "react";

const UseEffectComponentSample = () => {
  useEffect(() => {
    document.title = "EffectComponentSample";
  }, []);

  return (
    <div>
      <h1>UseEffectComponentSample</h1>
    </div>
  );
};

export default UseEffectComponentSample;
