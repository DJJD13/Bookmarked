import { SyntheticEvent, useState } from "react";
import SearchTitle from "../../Components/SearchTitle/SearchTitle";
import CardList from "../../Components/CardList/CardList";
import { searchByTitle } from "../../api";
import { bookshelfAddAPI } from "../../Services/BookshelfService";
import { toast } from "react-toastify";
import { Link } from "react-router-dom";

interface Props {
}

const SearchPage: React.FC<Props> = (): JSX.Element => {
    const [searchTitle, setSearchTitle] = useState<string>("");
    const [searchTitleResult, setSearchTitleResult] = useState<BookTitleSearch>({ total: 0, books: [] });
    const [serverError, setServerError] = useState<string>("");

    const onBookshelfCreate = (e: any) => {
        e.preventDefault();
        bookshelfAddAPI(e.target[0].value).then((res) => {
            if (res?.status == 204) {
                toast.success("Book added to Bookshelf!");
            }
        }).catch(() => toast.warning("Could not add book to bookshelf!"));
    }

    const handleSearchTitleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchTitle(e.target.value);
    }

    const onSearchTitleSubmit = async (e: SyntheticEvent) => {
        e.preventDefault();
        const result = await searchByTitle(searchTitle);
        if (typeof result === "string") {
            setServerError(result);
        } else if (typeof result?.data === "object") {
            setSearchTitleResult(result?.data);
        }
    }

    return (
        <div className="App">
            <SearchTitle onSearchTitleSubmit={onSearchTitleSubmit} searchTitle={searchTitle} handleSearchTitleChange={handleSearchTitleChange} />
            <div className="flex flex-col items-center justify-center mb-3">
                <p className="text-gray-500 mb-2">Know your ISBN?</p>
                <Link to="/add-isbn">
                    <button
                        className="inline-flex items-center py-2.5 px-4 text-sm font-medium text-center text-white bg-lightGreen rounded-lg focus:ring-4 focus:ring-primary-200 dark:focus:ring-primary-900 hover:bg-primary-800">
                        Add it here!
                    </button>
                </Link>
            </div>
            {serverError && <h1>{serverError}</h1>}
            <CardList searchResults={searchTitleResult} onBookshelfCreate={onBookshelfCreate}/>
        </div>
    );
}

export default SearchPage;