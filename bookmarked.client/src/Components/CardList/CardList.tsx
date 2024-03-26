import Card from "../Card/Card";
import { v4 as uuidv4 } from "uuid";
import "./CardList.css"

interface Props {
    searchResults: BookTitleSearch;
}

const CardList: React.FC<Props> = ({searchResults}: Props) : JSX.Element => {
    return <>
        <h2>Count: {searchResults.total}</h2>
        <div className="card-list">
            {searchResults.books.length > 0 ? (
                searchResults.books.map((book) => {
                    return <Card id={book.isbn} key={uuidv4()} searchResult={book} />
                })
            ): (
                <h1>No Results</h1>
                )}
        </div>
    </>;
}

export default CardList;