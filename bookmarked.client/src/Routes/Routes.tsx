import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import HomePage from "../Pages/HomePage/HomePage";
import SearchPage from "../Pages/SearchPage/SearchPage";
import BookPage from "../Pages/BookPage/BookPage";
import BookDetail from "../Components/BookDetail/BookDetail";
import AuthorDetail from "../Components/AuthorDetail/AuthorDetail";
import DesignPage from "../Pages/DesignPage/DesignPage";

export const router = createBrowserRouter([
	{
		path: "/",
		element: <App />,
		children: [
			{ path: "", element: <HomePage /> },
			{ path: "search", element: <SearchPage /> },
			{ path: "design-guide", element: <DesignPage /> },
			{
				path: "book/:isbn",
				element: <BookPage />,
				children: [
					{ path: "book-details", element: <BookDetail /> },
					{ path: "author-details", element: <AuthorDetail /> },
				],
			},
		],
	},
]);
