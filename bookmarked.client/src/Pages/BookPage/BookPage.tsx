import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getBookByISBN } from "../../api";
import BookDashboard from "../../Components/BookDashboard/BookDashboard";
import Tile from "../../Components/Tile/Tile";
import {formatAuthorName} from "../../Helpers/StringHelpers.tsx";

interface Props {}

const BookPage: React.FC<Props> = (): JSX.Element => {
	const { isbn } = useParams();
	const [bookDetails, setBookDetails] = useState<Book | null>(null);

	useEffect(() => {
		const getBookInit = async () => {
			const result = await getBookByISBN(isbn!);
			if (typeof result === "string") {
				console.log(result);
			} else if (typeof result?.data === "object") {
				setBookDetails(result?.data.book);
				console.log(bookDetails);
			}
		};
		getBookInit();
	}, []);
	return (
		<>
			{bookDetails ? (
				<div className="w-full relative flex ct-docs-disable-sidebar-content overflow-x-hidden">
					<img src={bookDetails.image} alt="Book cover" className="max-w-80 max-h-80 m-5" />
					<BookDashboard book={bookDetails}>
						<Tile title="Book Title" subtitle={bookDetails.title} />
						<Tile
							title="Author"
							subtitle={
								bookDetails.authors[0]
									? formatAuthorName(bookDetails.authors[0])
									: "[Unknown]"
							}
						/>
						<Tile
							title="Date Published"
							subtitle={bookDetails.date_published}
						/>
						<Tile title="ISBN" subtitle={bookDetails.isbn} />
					</BookDashboard>
				</div>
			) : (
				<div>Book not found</div>
			)}
		</>
	);
};

export default BookPage;
