import { SyntheticEvent, useState } from "react";
import Search from "../../Components/Search/Search";
import SearchTitle from "../../Components/SearchTitle/SearchTitle";
import CardList from "../../Components/CardList/CardList";
import { getBookByISBN, searchByTitle } from "../../api";
import { bookshelfAddAPI } from "../../Services/BookshelfService";
import { toast } from "react-toastify";

interface Props {
}

const SearchPage: React.FC<Props> = (): JSX.Element => {
    const [searchIsbn, setSearchIsbn] = useState<string>("");
    const [searchTitle, setSearchTitle] = useState<string>("");
    const [searchIsbnResult, setSearchIsbnResult] = useState<Book>();
    const [searchTitleResult, setSearchTitleResult] = useState<BookTitleSearch>({ total: 0, books: [] });
    const [serverError, setServerError] = useState<string>("");

    const onBookshelfCreate = (e: any) => {
        e.preventDefault();
        bookshelfAddAPI(e.target[0].value).then((res) => {
            if (res?.status == 204) {
                toast.success("Book added to Bookshelf!");
            }
        }).catch((e) => toast.warning("Could not add book to bookshelf!"));
    }

    const handleSearchIsbnChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchIsbn(e.target.value);
    }
    
    const handleSearchTitleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchTitle(e.target.value);
    }

    const onSearchIsbnSubmit = async (e: SyntheticEvent) => {
        e.preventDefault();
        const result = await getBookByISBN(searchIsbn);
        if (typeof result === "string") {
            setServerError(result);
        } else if (typeof result.data === "object") {
            setSearchIsbnResult(result.data.book);
        }
    }

    const onSearchTitleSubmit = async (e: SyntheticEvent) => {
        e.preventDefault();
        const result = await searchByTitle(searchTitle);
        if (typeof result === "string") {
            setServerError(result);
        } else if (typeof result.data === "object") {
            setSearchTitleResult(result.data);
        }
    }

    return (
        <div className="App">
            <Search onSearchIsbnSubmit={onSearchIsbnSubmit} searchIsbn={searchIsbn} handleSearchIsbnChange={handleSearchIsbnChange} />
            <SearchTitle onSearchTitleSubmit={onSearchTitleSubmit} searchTitle={searchTitle} handleSearchTitleChange={handleSearchTitleChange} />
            {serverError && <h1>{serverError}</h1>}
            <CardList searchResults={searchTitleResult} onBookshelfCreate={onBookshelfCreate} />
        </div>
    );
}

export default SearchPage;