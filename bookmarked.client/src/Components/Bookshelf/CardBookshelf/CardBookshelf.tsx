import { SyntheticEvent, useState } from "react";
import DeleteBookshelf from "../DeleteBookshelf/DeleteBookshelf";
import { Link } from "react-router-dom";
import { BookshelfGet } from "../../../Models/Bookshelf";
import ReadingStatus from "../../ReadingStatus/ReadingStatus.tsx";
import {bookshelfUpdateStatusAPI} from "../../../Services/BookshelfService.tsx";
import { toast } from "react-toastify";
import ReadingProgress from "../../ReadingProgress/ReadingProgress.tsx";

interface Props {
    bookshelfValue: BookshelfGet;
    onBookshelfDelete: (e: SyntheticEvent) => void;
}

const CardBookshelf: React.FC<Props> = ({ bookshelfValue, onBookshelfDelete }: Props): JSX.Element => {
    const [readingStatus, setReadingStatus] = useState<number>(bookshelfValue.readingStatus);
    const [pagesRead, setPagesRead] = useState<number>(bookshelfValue.pagesRead);

    const handlePagesReadInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setPagesRead(parseInt(e.target.value));
    }
    
    const handleReadingStatusChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        let readingStatusNum = parseInt(e.target.value);
        bookshelfUpdateStatusAPI(bookshelfValue.isbn, readingStatusNum, pagesRead)
            .then((res) => {
                if (res?.status == 200) {
                    toast.success(`Reading Status for ${bookshelfValue.title} updated!`);
                    setReadingStatus(readingStatusNum);
                }
            }).catch(() => toast.warning("Could not update reading status"))
    }
    
   const onSubmitPagesRead = (e: SyntheticEvent) => {
        e.preventDefault();
        bookshelfUpdateStatusAPI(bookshelfValue.isbn, readingStatus, pagesRead)
            .then((res) => {
                if (res?.status == 200) {
                    toast.success(`Pages read for ${bookshelfValue.title} updated!`);
                    setPagesRead(pagesRead);
                }
            }).catch(() => toast.warning("Could not update reading status"))
   } 
    
    
    return (
        <div className="flex flex-col items-center w-72 bg-white shadow-md 
                        rounded-xl p-5 duration-500 hover:scale-105 hover:shadow-xl m-5">
            <Link to={`/book/${bookshelfValue.isbn}`}>
                <img src={bookshelfValue.coverImage} alt="Book cover" className="h-100 w-75 object-cover rounded-t-xl" />
                <h4 className={"pt-6 text-xl font-bold"}>{bookshelfValue.title}</h4>
            </Link>
            <ReadingStatus readingStatus={readingStatus} handleReadingStatusChange={handleReadingStatusChange} />
            { readingStatus == 1 &&
                <ReadingProgress pagesRead={pagesRead} 
                                 totalPages={bookshelfValue.totalPages} 
                                 handlePagesReadChange={handlePagesReadInputChange} 
                                 onSubmitPagesRead={onSubmitPagesRead} />
            }
            <DeleteBookshelf bookshelfValue={bookshelfValue.isbn} onBookshelfDelete={onBookshelfDelete} />
        </div>
    );
}

export default CardBookshelf;