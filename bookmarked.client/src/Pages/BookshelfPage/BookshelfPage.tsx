import { useEffect, useState } from "react";
import { bookshelfDeleteAPI, bookshelfGetAPI } from "../../Services/BookshelfService";
import { BookshelfGet } from "../../Models/Bookshelf";
import { toast } from "react-toastify";
import ListBookshelf from "../../Components/Bookshelf/ListBookshelf/ListBookshelf";

interface Props { }

const BookshelfPage: React.FC<Props> = (): JSX.Element => {
    const [bookshelfValues, setBookshelfValues] = useState<BookshelfGet[] | null>([]);

    useEffect(() => {
        getBookshelf();
    }, []);

    const getBookshelf = () => {
        bookshelfGetAPI().then((res) => {
            if (res?.data) {
                setBookshelfValues(res?.data);
            }
        }).catch(() => toast.warning("Could not get bookshelf books!"))
    };

    const onBookshelfDelete = (e: any) => {
        e.preventDefault();
        bookshelfDeleteAPI(e.target[0].value).then((res) => {
            if (res?.status == 200) {
                toast.success("Book removed from bookshelf");
                getBookshelf();
            }
        }).catch(() => toast.warning("Could not remove book from bookshelf"));
    }

    return (
        <ListBookshelf bookshelfValues={bookshelfValues!} onBookshelfDelete={onBookshelfDelete} />
    );
};

export default BookshelfPage;