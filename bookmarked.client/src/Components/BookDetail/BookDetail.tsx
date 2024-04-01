import { useEffect, useState } from "react";
import { useOutletContext } from "react-router-dom";
import { getBookByISBN } from "../../api";
import RatioList from "../RatioList/RatioList";
import Spinner from "../Spinner/Spinner";

interface Props {}

const tableConfig = [
    {
        label: "Title",
        render: (book: Book) => book.title
    },
    {
        label: "Author",
        render: (book: Book) => book.authors[0]
    },
    {
        label: "Title Long",
        render: (book: Book) => book.title_long
    },
    {
        label: "Edition",
        render: (book: Book) => book.edition
    },
    {
        label: "Binding",
        render: (book: Book) => book.binding
    },
    {
        label: "Date Published",
        render: (book: Book) => book.date_published
    },
    {
        label: "ISBN",
        render: (book: Book) => book.isbn
    },
    {
        label: "ISBN10",
        render: (book: Book) => book.isbn10
    },
    {
        label: "ISBN13",
        render: (book: Book) => book.isbn13
    },
    {
        label: "MSRP",
        render: (book: Book) => book.msrp?.toString()
    },
    {
        label: "Pages",
        render: (book: Book) => book.pages?.toString()
    }
];

const BookDetail: React.FC<Props> = (props: Props): JSX.Element => {
    const isbn = useOutletContext<string>();
    const [bookData, setBookData] = useState<Book>();
    useEffect(() => {
        const getBookDetails = async () => {
            const result = await getBookByISBN(isbn!);
            if (typeof result === "string") {
                console.log(result);
            } else if (typeof result.data === "object") {
                setBookData(result.data.book);
            }
        };
        getBookDetails();
    }, [])
    return (<>
        {bookData ? (
            <RatioList data={bookData} config={tableConfig} />    
        ): (
            <Spinner />
            ) 
        }
    </>);
};

export default BookDetail;
