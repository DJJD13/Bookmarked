import Card from "../Card/Card";
import { v4 as uuidv4 } from "uuid";
import "./CardList.css"
import { SyntheticEvent } from "react";

interface Props {
    searchResults: BookTitleSearch;
    onBookshelfCreate: (e: SyntheticEvent) => void;
}

const CardList: React.FC<Props> = ({searchResults, onBookshelfCreate}: Props) : JSX.Element => {
    return <>
        <h2>Count: {searchResults.total}</h2>
        <div className="card-list">
            {searchResults.books.length > 0 ? (
                searchResults.books.map((book) => {
                    return <Card id={book.isbn} key={uuidv4()} searchResult={book} onBookshelfCreate={onBookshelfCreate} />
                })
            ): (
                <h1>No Results</h1>
                )}
        </div>
    </>;
}

export default CardList;