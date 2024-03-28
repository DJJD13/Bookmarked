import { Link } from "react-router-dom";
import logo from "./logo-placeholder.png";
interface Props { }

const NavBar: React.FC<Props> = (props: Props): JSX.Element => {
    return (
        <nav className="relative container mx-auto p-6">
            <div className="flex items-center justify-between">
                <div className="flex items-center space-x-20">
                    <Link to="/">
                        <img src={logo} height="50" width="200" alt="Logo" />
                    </Link>
                    <div className="hidden font-bold lg:flex">
                        <Link to="/search" className="text-black hover:text-darkBlue">
                            Search
                        </Link>
                    </div>
                </div>
                <div className="hidden lg:flex items-center space-x-6 text-back">
                    <div className="hover:text-darkBlue">Login</div>
                    <a
                        href=""
                        className="px-8 py-3 font-bold rounded text-white bg-lightGreen hover:opacity-70"
                    >
                        Signup
                    </a>
                </div>
            </div>
        </nav>
    );
}

export default NavBar;