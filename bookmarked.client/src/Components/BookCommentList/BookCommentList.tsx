import { CommentGet } from "../../Models/Comment";
import BookCommentListItem from "../BookCommentListItem/BookCommentListItem";

interface Props {
    comments: CommentGet[];
}

const BookCommentList: React.FC<Props> = ({ comments }: Props): JSX.Element => {
    return (
        <>
            {comments ? comments.map((comment) => {
                return <BookCommentListItem comment = {comment} />
            }) : "" }
        </>
    );
};

export default BookCommentList;
