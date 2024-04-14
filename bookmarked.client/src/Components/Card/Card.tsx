import { SyntheticEvent } from "react";
import AddBookshelf from "../Bookshelf/AddBookshelf/AddBookshelf";
import "./Card.css"
import { Link } from "react-router-dom";
interface Props {
    id: string;
    searchResult: Book;
    onBookshelfCreate: (e: SyntheticEvent) => void;
}

const Card: React.FC<Props> = ({ id, searchResult, onBookshelfCreate }: Props) : JSX.Element => {
    return (
        <div className="flex flex-col w-72 bg-white shadow-md rounded-xl duration-500 hover:scale-105 hover:shadow-xl m-5">
            <Link to={`/book/${searchResult.isbn}`}>
                <img src={searchResult.image} alt="Book cover" className="h-100 w-75 object-cover rounded-t-xl" />
            </Link>
            <div className="px-4 py-3 w-75">
                <h1 className="text-lg font-bold text-black capitalize block">{searchResult.title}</h1>
                <h4><span className="font-bold text-black capitalize">Author:</span> {searchResult.authors[0]}</h4>
                <h5><span className="font-bold text-black capitalize">Publication Date: </span> {searchResult.date_published}</h5>
                <h5><span className="font-bold text-black uppercase">ISBN: </span> {id}</h5>
                <br />
                <hr />
            </div>
            <div className="mt-auto p-3 text-center">
                <AddBookshelf onBookshelfCreate={onBookshelfCreate} isbn={searchResult.isbn} />
            </div>
        </div>
    )
};

export default Card;