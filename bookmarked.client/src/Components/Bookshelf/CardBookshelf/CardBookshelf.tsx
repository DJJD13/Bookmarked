import { SyntheticEvent, useState } from "react";
import DeleteBookshelf from "../DeleteBookshelf/DeleteBookshelf";
import { Link } from "react-router-dom";
import { BookshelfGet } from "../../../Models/Bookshelf";
import ReadingStatus from "../../ReadingStatus/ReadingStatus.tsx";

interface Props {
    bookshelfValue: BookshelfGet;
    onBookshelfDelete: (e: SyntheticEvent) => void;
}

const CardBookshelf: React.FC<Props> = ({ bookshelfValue, onBookshelfDelete }: Props): JSX.Element => {
    const [readingStatus, setReadingStatus] = useState<number>(0);
    
    const handleReadingStatusChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        let readingStatusNum = parseInt(e.target.value);
        setReadingStatus(readingStatusNum);
    }
    
    return (<div className="flex flex-col items-center w-72 bg-white shadow-md rounded-xl p-5 duration-500 hover:scale-105 hover:shadow-xl m-5">
        <Link to={`/book/${bookshelfValue.isbn}`}>
            <img src={bookshelfValue.coverImage} alt="Book cover" className="h-100 w-75 object-cover rounded-t-xl" />
        </Link>
        <Link to={`/book/${bookshelfValue.isbn}`} className="pt-6 text-xl font-bold">{bookshelfValue.title}</Link>
        <ReadingStatus readingStatus={readingStatus} handleReadingStatusChange={handleReadingStatusChange} />
        <DeleteBookshelf bookshelfValue={bookshelfValue.isbn} onBookshelfDelete={onBookshelfDelete} />
    </div>);
}

export default CardBookshelf;