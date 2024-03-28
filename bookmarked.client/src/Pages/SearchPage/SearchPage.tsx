import { SyntheticEvent, useState } from "react";
import Search from "../../Components/Search/Search";
import SearchTitle from "../../Components/SearchTitle/SearchTitle";
import ListBookshelf from "../../Components/Bookshelf/ListBookshelf/ListBookshelf";
import CardList from "../../Components/CardList/CardList";
import { searchByISBN, searchByTitle } from "../../api";

interface Props {
}

const SearchPage: React.FC<Props> = (props: Props): JSX.Element => {
    const [searchIsbn, setSearchIsbn] = useState<string>("");
    const [searchTitle, setSearchTitle] = useState<string>("");
    const [bookshelfValues, setBookshelfValues] = useState<string[]>([]);
    const [searchIsbnResult, setSearchIsbnResult] = useState<Book>();
    const [searchTitleResult, setSearchTitleResult] = useState<BookTitleSearch>({ total: 0, books: [] });
    const [serverError, setServerError] = useState<string>("");

    const onBookshelfCreate = (e: any) => {
        e.preventDefault();
        const exists = bookshelfValues.find((value) => value === e.target[0].value);
        if (exists) return;
        const updatedBookshelf = [...bookshelfValues, e.target[0].value]
        setBookshelfValues(updatedBookshelf);
    }

    const onBookshelfDelete = (e: any) => {
        e.preventDefault();
        const removed = bookshelfValues.filter((value) => {
            return value !== e.target[0].value;
        });
        setBookshelfValues(removed);
    }

    const handleSearchIsbnChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchIsbn(e.target.value);
        console.log(e);
    }

    const handleSearchTitleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchTitle(e.target.value);
        console.log(e);
    }

    const onSearchIsbnSubmit = async (e: SyntheticEvent) => {
        e.preventDefault();
        const result = await searchByISBN(searchIsbn);
        if (typeof result === "string") {
            setServerError(result);
        } else if (typeof result.data === "object") {
            setSearchIsbnResult(result.data);
        }
        console.log(searchIsbnResult);
    }

    const onSearchTitleSubmit = async (e: SyntheticEvent) => {
        e.preventDefault();
        const result = await searchByTitle(searchTitle);
        if (typeof result === "string") {
            setServerError(result);
        } else if (typeof result.data === "object") {
            setSearchTitleResult(result.data);
        }
        console.log(searchTitleResult);
    }

    return (
        <div className="App">
            <Search onSearchIsbnSubmit={onSearchIsbnSubmit} searchIsbn={searchIsbn} handleSearchIsbnChange={handleSearchIsbnChange} />
            <SearchTitle onSearchTitleSubmit={onSearchTitleSubmit} searchTitle={searchTitle} handleSearchTitleChange={handleSearchTitleChange} />
            <ListBookshelf bookshelfValues={bookshelfValues} onBookshelfDelete={onBookshelfDelete} />
            {serverError && <h1>{serverError}</h1>}
            <CardList searchResults={searchTitleResult} onBookshelfCreate={onBookshelfCreate} />
        </div>
    );
}

export default SearchPage;