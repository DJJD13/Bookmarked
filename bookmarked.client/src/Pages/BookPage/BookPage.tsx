import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getBookByISBN } from "../../api";
import BookDashboard from "../../Components/BookDashboard/BookDashboard";
import {handleError} from "../../Helpers/ErrorHandler.tsx";

interface Props {}

const BookPage: React.FC<Props> = (): JSX.Element => {
	const { isbn } = useParams();
	const [bookDetails, setBookDetails] = useState<Book | null>(null);

	useEffect(() => {
		const getBookInit = async () => {
			const result = await getBookByISBN(isbn!);
			if (typeof result === "string") {
				handleError(result);
			} else if (typeof result?.data === "object") {
				setBookDetails(result?.data);
			}
		};
		getBookInit();
	}, []);
	return (
		<>
			{bookDetails ? (
				<div className="w-full px-10 py-5 mx-auto relative flex ct-docs-disable-sidebar-content overflow-x-hidden">
					<BookDashboard book={bookDetails}>
					</BookDashboard>
				</div>
			) : (
				<div>Book not found</div>
			)}
		</>
	);
};

export default BookPage;
