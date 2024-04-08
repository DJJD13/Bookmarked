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
        <h2 className="flex items-center justify-center font-bold">Count: {searchResults.total}</h2>
        <div className="p-1 flex flex-wrap items-center justify-center">
            {searchResults.books.length > 0 ? (
                searchResults.books.map((book) => {
                    return <Card id={book.isbn} key={uuidv4()} searchResult={book} onBookshelfCreate={onBookshelfCreate} />
                })
            ): (
                <p className="mb-3 mt-3 text-xl font-semibold text-center md:text-xl">
                    No results!
                </p>
            )}
        </div>
    </>;
}

export default CardList;