import RatioList from "../RatioList/RatioList";
import BookComment from "../BookComment/BookComment";
import {formatAuthorName} from "../../Helpers/StringHelpers.tsx";

interface Props {
    book: Book;
}

const tableConfig = [
    {
        label: "Title",
        render: (book: Book) => book.title
    },
    {
        label: "Author",
        render: (book: Book) => formatAuthorName(book.authors[0])
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

const BookDetail: React.FC<Props> = ({ book }: Props): JSX.Element => {
    return (<>
        <RatioList data={book} config={tableConfig} />
        <BookComment isbn={book.isbn} />
    </>);
};

export default BookDetail;
