import {useEffect, useState } from "react";
import {getAuthorDetails} from "../../api.tsx";
import {handleError} from "../../Helpers/ErrorHandler.tsx";

interface Props {
	author: string;
}

const AuthorDetail: React.FC<Props> = ({ author }: Props): JSX.Element => {
	const [authorBooks, setAuthorBooks] = useState<Book[]>([]);

	useEffect(() => {
		const getAuthorBooks = async () => {
			const result = await getAuthorDetails(author);
			if (typeof result === "string") {
				handleError(result);
			} else if (typeof result?.data === "object") {
				setAuthorBooks(result?.data.books);
			}
		};
		getAuthorBooks();
	}, []);
	
	return (
		<>
			{authorBooks.length > 0 ? (
				authorBooks.map((book) => {
					return <img alt="book image" src={book.image} className="max-w-80 max-h-80 m-5 p-3" />
				})
			) : (
				"No other books found"
			)}
		</>
	);
};

export default AuthorDetail;
