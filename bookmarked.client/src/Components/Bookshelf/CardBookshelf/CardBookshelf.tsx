import { SyntheticEvent } from "react";
import DeleteBookshelf from "../DeleteBookshelf/DeleteBookshelf";
import { Link } from "react-router-dom";
import { BookshelfGet } from "../../../Models/Bookshelf";

interface Props {
    bookshelfValue: BookshelfGet;
    onBookshelfDelete: (e: SyntheticEvent) => void;
}

const CardBookshelf: React.FC<Props> = ({ bookshelfValue, onBookshelfDelete }: Props): JSX.Element => {
    return (<div className="flex flex-col p-3 space-y-4 text-center rounded-xl shadow-lg md:w-1/3">
        <Link to={`/book/${bookshelfValue.isbn}/book-details`}>
            <img src={bookshelfValue.coverImage} alt="Book cover" className="h-100 w-75 object-cover rounded-t-xl" />
        </Link>
        <Link to={`/book/${bookshelfValue.isbn}/book-details`} className="pt-6 text-xl font-bold">{bookshelfValue.title}</Link>
        <DeleteBookshelf bookshelfValue={bookshelfValue.isbn} onBookshelfDelete={onBookshelfDelete} />
    </div>);
}

export default CardBookshelf;