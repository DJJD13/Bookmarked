import { SyntheticEvent, useEffect, useState } from "react";
import Search from "../../Components/Search/Search";
import SearchTitle from "../../Components/SearchTitle/SearchTitle";
import ListBookshelf from "../../Components/Bookshelf/ListBookshelf/ListBookshelf";
import CardList from "../../Components/CardList/CardList";
import { getBookByISBN, searchByTitle } from "../../api";
import { BookshelfGet } from "../../Models/Bookshelf";
import { bookshelfAddAPI, bookshelfDeleteAPI, bookshelfGetAPI } from "../../Services/BookshelfService";
import { toast } from "react-toastify";

interface Props {
}

const SearchPage: React.FC<Props> = (props: Props): JSX.Element => {
    const [searchIsbn, setSearchIsbn] = useState<string>("");
    const [searchTitle, setSearchTitle] = useState<string>("");
    const [bookshelfValues, setBookshelfValues] = useState<BookshelfGet[] | null>([]);
    const [searchIsbnResult, setSearchIsbnResult] = useState<Book>();
    const [searchTitleResult, setSearchTitleResult] = useState<BookTitleSearch>({ total: 0, books: [] });
    const [serverError, setServerError] = useState<string>("");

    useEffect(() => {
        getBookshelf();
    }, []);

    const onBookshelfCreate = (e: any) => {
        e.preventDefault();
        bookshelfAddAPI(e.target[0].value).then((res) => {
            if (res?.status == 204) {
                toast.success("Book added to Bookshelf!");
                getBookshelf();
            }
        }).catch((e) => toast.warning("Could not add book to bookshelf!"));
    }

    const onBookshelfDelete = (e: any) => {
        e.preventDefault();
        bookshelfDeleteAPI(e.target[0].value).then((res) => {
            if (res?.status == 200) {
                toast.success("Book removed from bookshelf");
                getBookshelf();
            }
        }).catch((e) => toast.warning("Could not remove book from bookshelf"));
    }

    const handleSearchIsbnChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchIsbn(e.target.value);
        console.log(e);
    }

    const getBookshelf = () => {
        bookshelfGetAPI().then((res) => {
            if (res?.data) {
                setBookshelfValues(res?.data);
            }
        }).catch((e) => toast.warning("Could not get bookshelf books!"))
    }

    const handleSearchTitleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchTitle(e.target.value);
        console.log(e);
    }

    const onSearchIsbnSubmit = async (e: SyntheticEvent) => {
        e.preventDefault();
        const result = await getBookByISBN(searchIsbn);
        if (typeof result === "string") {
            setServerError(result);
        } else if (typeof result.data === "object") {
            setSearchIsbnResult(result.data.book);
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
            <ListBookshelf bookshelfValues={bookshelfValues!} onBookshelfDelete={onBookshelfDelete} />
            {serverError && <h1>{serverError}</h1>}
            <CardList searchResults={searchTitleResult} onBookshelfCreate={onBookshelfCreate} />
        </div>
    );
}

export default SearchPage;