import { SyntheticEvent } from "react";
import AddBookshelf from "../Bookshelf/AddBookshelf/AddBookshelf";
import "./Card.css"
interface Props {
    id: string;
    searchResult: Book;
    onBookshelfCreate: (e: SyntheticEvent) => void;
}

const Card: React.FC<Props> = ({ id, searchResult, onBookshelfCreate }: Props) : JSX.Element => {
    return (
        <div className="card">
            <img src={searchResult.image} alt="Book cover" />
            <div className="details">
                <h1>{searchResult.title}</h1>
                <h4>Author: {searchResult.authors[0]}</h4>
                <h5>Publication Date: {searchResult.date_published}</h5>
                <h5>ISBN: {id}</h5>
                <hr />
                <p className="summary">{searchResult.synopsis}</p>
                <AddBookshelf onBookshelfCreate={onBookshelfCreate} isbn={searchResult.isbn} />
            </div>
        </div>
    )
};

export default Card;