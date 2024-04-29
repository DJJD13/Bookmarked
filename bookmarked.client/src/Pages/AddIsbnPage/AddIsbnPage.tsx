import { SyntheticEvent, useState } from "react";
import { getBookByISBN } from "../../api.tsx";
import { handleError } from "../../Helpers/ErrorHandler.tsx";
import Search from "../../Components/Search/Search.tsx";
import Card from "../../Components/Card/Card.tsx";
import {bookshelfAddAPI} from "../../Services/BookshelfService.tsx";
import { toast } from "react-toastify";

const AddIsbnPage: React.FC = () : JSX.Element => {
    const [searchIsbn, setSearchIsbn] = useState<string>("");
    const [searchIsbnResult, setSearchIsbnResult] = useState<Book>();
    
    const addToBookshelf = (e: any) => {
        e.preventDefault();
        bookshelfAddAPI(e.target[0].value).then((res) => {
            if (res?.status == 204) {
                toast.success("Book added to Bookshelf!");
            }
        }).catch(() => toast.warning("Could not add book to bookshelf!"));
    }

    const handleSearchIsbnChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchIsbn(e.target.value);
    }

    const onSearchIsbnSubmit = async (e: SyntheticEvent) => {
        e.preventDefault();
        const result = await getBookByISBN(searchIsbn);
        if (typeof result === "string") {
            handleError(result);
        } else if (typeof result?.data === "object") {
            setSearchIsbnResult(result?.data);
        }
    }
    return (
        <>
            <Search onSearchIsbnSubmit={onSearchIsbnSubmit} searchIsbn={searchIsbn} handleSearchIsbnChange={handleSearchIsbnChange} />
            <div className="flex flex-col items-center justify-center">
                {searchIsbnResult ? (
                    <Card id={searchIsbnResult.isbn13} searchResult={searchIsbnResult!} onBookshelfCreate={addToBookshelf} />
                ) : (
                    "No book found"
                )}
            </div>
        </>
        
    );
};

export default AddIsbnPage;