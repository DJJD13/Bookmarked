import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import HomePage from "../Pages/HomePage/HomePage";
import SearchPage from "../Pages/SearchPage/SearchPage";
import BookPage from "../Pages/BookPage/BookPage";
import BookDetail from "../Components/BookDetail/BookDetail";
import AuthorDetail from "../Components/AuthorDetail/AuthorDetail";
import DesignPage from "../Pages/DesignPage/DesignPage";
import LoginPage from "../Pages/LoginPage/LoginPage";
import RegisterPage from "../Pages/RegisterPage/RegisterPage";
import ProtectedRoute from "./ProtectedRoute";

export const router = createBrowserRouter([
	{
		path: "/",
		element: <App />,
		children: [
			{ path: "", element: <HomePage /> },
			{ path: "login", element: <LoginPage /> },
			{ path: "register", element: <RegisterPage /> },
			{ path: "search", element: <ProtectedRoute><SearchPage /></ProtectedRoute> },
			{ path: "design-guide", element: <DesignPage /> },
			{
				path: "book/:isbn",
				element: <ProtectedRoute><BookPage /></ProtectedRoute>,
				children: [
					{ path: "book-details", element: <BookDetail /> },
					{ path: "author-details", element: <AuthorDetail /> },
				],
			},
		],
	},
]);
