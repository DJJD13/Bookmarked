import BookDetail from "../BookDetail/BookDetail.tsx";
import AuthorDetail from "../AuthorDetail/AuthorDetail.tsx";
import {formatAuthorName} from "../../Helpers/StringHelpers.tsx";

interface Props {
	book: Book;
}

const BookDashboard: React.FC<Props> = ({ book }: Props): JSX.Element => {
	return (
		<div className="relative bg-blueGray-100 w-full">
			<div className="relative pt-14 pb-24 bg-lightBlue-500">
				<div className="px-4 md:px-24 mx-auto">
					<div>
						<img src={book.image} alt="Book cover" className="max-w-96 max-h-96 mr-7 float-left"/>
						<div className="flex flex-col">{<BookDetail book={book}/>}</div>
						<h4 className="my-3 font-bold">Other Books by {formatAuthorName(book.authors[0])}</h4>
						<div className="flex flex-wrap">{<AuthorDetail author={book.authors[0]}/>}</div>
					</div>
				</div>
			</div>
		</div>
	);
};

export default BookDashboard;
