import BookComment from "../BookComment/BookComment";
import {formatAuthorName, removeHTMLTags} from "../../Helpers/StringHelpers.tsx";

interface Props {
    book: Book;
}

const BookDetail: React.FC<Props> = ({ book }: Props): JSX.Element => {
    return (<>
        <h3 className="font-bold my-2 p-2 text-4xl">{book.title}</h3>
        <h5 className="my-2 p-2 text-2xl">{formatAuthorName(book.authors[0])}</h5>
        <p className="my-2 p-2">
            {(book.synopsis && book.synopsis.length > 0) ? removeHTMLTags(book.synopsis) : "No Synopsis Found"}
        </p>
        <p className="font-light p-2 text-gray-600">Pages: {book.pages}, {book.binding}</p>
        <p className="p-2 text-gray-600">{book.date_published}</p>
        <p className={"p-2 text-gray-600 text-sm mb-4"}>ISBN: {book.isbn13} (ISBN10: {book.isbn10})</p>
        <BookComment isbn={book.isbn} />
    </>);
};

export default BookDetail;
