import { useState } from "react";
import { FaRegHeart } from "react-icons/fa";
import { FaHeart } from "react-icons/fa6";

const LikeButton = () => {
  const [liked, setLiked] = useState(false);
  return (
    <div onClick={() => setLiked(!liked)}>
      {liked ? <FaHeart color="red" size={30} /> : <FaRegHeart size={30} />}
    </div>
  );
};

export default LikeButton;
