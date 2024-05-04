import { Link } from "react-router-dom";
import logo from "./logo-placeholder.png";
import { useAuth } from "../../Context/useAuth";
import { GiBookshelf } from "react-icons/gi";
import { FaSearch } from "react-icons/fa";
import { AiOutlineFieldNumber } from "react-icons/ai";

const NavBar: React.FC = (): JSX.Element => {
    const { isLoggedIn, user, logout } = useAuth();
    return (
        <nav className="relative container mx-auto p-6">
            <div className="flex items-center justify-between">
                <div className="flex items-center space-x-20">
                    <Link to="/">
                        <img src={logo} height="50" width="200" alt="Logo" />
                    </Link>
                    <div className="hidden font-bold lg:flex">
                        <Link to="/search" className="flex items-center hover:text-darkBlue mr-5">
                            <FaSearch className={"mr-1"} /> <span>Search</span>
                        </Link>
                        {isLoggedIn() ? (
                            <>
                                <Link to="add-isbn" className={"flex items-center text-black hover:text-darkBlue mr-5"}>
                                    <AiOutlineFieldNumber className={"mr-1"} /> <span>Add via ISBN</span>
                                </Link>
                                <Link to="/bookshelf" className="flex items-center hover:text-darkBlue">
                                    <GiBookshelf className={"mr-1"} /> <span>My Bookshelf</span>
                                </Link>
                            </>
                        ) : "" }
                    </div>
                </div>
                {isLoggedIn() ? (
                    <div className="hidden lg:flex items-center space-x-6 text-back">
                        <div className="hover:text-darkBlue">Welcome {user?.userName}</div>
                        <a
                            onClick={logout}
                            className="px-8 py-3 font-bold rounded text-white bg-lightGreen hover:opacity-70"
                        >
                            Logout
                        </a>
                    </div>

                ): (
                    <div className="hidden lg:flex items-center space-x-6 text-back">
                        <Link to="/login" className="hover:text-darkBlue">Login</Link>
                        <Link
                            to="/register"
                            className="px-8 py-3 font-bold rounded text-white bg-lightGreen hover:opacity-70"
                        >
                            Signup
                        </Link>
                    </div>
                )}
            </div>
        </nav>
    );
}

export default NavBar;