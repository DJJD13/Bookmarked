import { toast } from "react-toastify";
import { commentGetAPI, commentPostAPI } from "../../Services/CommentService";
import BookCommentForm from "./BookCommentForm/BookCommentForm";
import { CommentGet } from "../../Models/Comment";
import { useEffect, useState } from "react";
import BookCommentList from "../BookCommentList/BookCommentList";
import Spinner from "../Spinner/Spinner";

interface Props {
    isbn: string;
}

type CommentFormInputs = {
    title: string;
    content: string;
}

const BookComment: React.FC<Props> = ({ isbn }: Props): JSX.Element => {
    const [comments, setComments] = useState<CommentGet[] | null>(null);
    const [loading, setLoading] = useState<boolean>(false);

    useEffect(() => {
        getComments();
    }, [])

    const handleComment = (e: CommentFormInputs) => {
        commentPostAPI(e.title, e.content, isbn).then((res) => {
            if (res) {
                toast.success("Comment created successfully!");
                getComments();
            }
        }).catch((e) => toast.warning(e));
    }

    const getComments = () => {
        setLoading(true);
        commentGetAPI(isbn).then((res) => {
            setLoading(false);
            // eslint-disable-next-line @typescript-eslint/no-non-null-asserted-optional-chain
            setComments(res?.data!);
        })
    }
    return (
        <div className="flex flex-col">
            {loading ? <Spinner /> : <BookCommentList comments={comments!} /> }
            <BookCommentForm isbn={isbn} handleComment={handleComment} />
        </div>

    );
};

export default BookComment;