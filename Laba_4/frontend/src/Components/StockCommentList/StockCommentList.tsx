import { CommentGet } from "../../Models/Comment.ts";
import StockCommentListItem from "../StockCommentListItem/StockCommentListItem.tsx";
import { v4 as uuidv4 } from "uuid";

type Props = {
  comments: CommentGet[];
};

const StockCommentList = ({ comments }: Props) => {
  return (
    <>
      {comments
        ? comments.map((comment) => {
            return (
              <StockCommentListItem
                key={uuidv4()}
                comment={comment}
              ></StockCommentListItem>
            );
          })
        : ""}
    </>
  );
};

export default StockCommentList;
