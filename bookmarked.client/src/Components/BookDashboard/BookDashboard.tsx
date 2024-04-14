import BookDetail from "../BookDetail/BookDetail.tsx";
import AuthorDetail from "../AuthorDetail/AuthorDetail.tsx";
import {formatAuthorName} from "../../Helpers/StringHelpers.tsx";

interface Props {
	children: React.ReactNode;
	book: Book;
}

const BookDashboard: React.FC<Props> = ({ children, book }: Props): JSX.Element => {
	return (
		<div className="relative bg-blueGray-100 w-full">
			<div className="relative pt-20 pb-32 bg-lightBlue-500">
				<div className="px-4 md:px-6 mx-auto w-full">
					<div>
						<div className="flex flex-wrap">{children}</div>
						<div className="flex flex-wrap">{<BookDetail book={book}/>}</div>
						<h4 className="my-3 font-bold">Other Books by {formatAuthorName(book.authors[0])}</h4>
						<div className="flex flex-wrap">{<AuthorDetail author={book.authors[0]}/>}</div>
					</div>
				</div>
			</div>
		</div>
	);
};

export default BookDashboard;
