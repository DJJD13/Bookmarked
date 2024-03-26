import { useState } from 'react';
import './App.css';
import CardList from './Components/CardList/CardList';
import Search from './Components/Search/Search';
import { searchByISBN, searchByTitle } from './api';
import SearchTitle from './Components/SearchTitle/SearchTitle';


function App() {
    const [search, setSearch] = useState<string>("");
    const [searchTitle, setSearchTitle] = useState<string>("");
    const [searchResult, setSearchResult] = useState<Book>();
    const [searchTitleResult, setSearchTitleResult] = useState<BookTitleSearch>({ total: 0, books: []});
    const [serverError, setServerError] = useState<string>("");

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearch(e.target.value);
        console.log(e);
    }

    const handleChangeTitle = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchTitle(e.target.value);
        console.log(e);
    }

    const handleClick = async () => {
        const result = await searchByISBN(search);
        if (typeof result === "string") {
            setServerError(result);
        } else if (typeof result.data === "object") {
            setSearchResult(result.data);
        }
        console.log(searchResult);
    }

    const handleClickTitle = async () => {
        const result = await searchByTitle(searchTitle);
        if (typeof result === "string") {
            setServerError(result);
        } else if (typeof result.data === "object") {
            setSearchTitleResult(result.data);
        }
        console.log(searchTitleResult);
    }

    return (
        <div className="App">
            <Search onClick={handleClick} search={search} handleChange={handleChange} />
            <SearchTitle handleClickTitle={handleClickTitle} searchTitle={searchTitle} handleChangeTitle={handleChangeTitle} />
            {serverError && <h1>{serverError}</h1>}
            <CardList searchResults={searchTitleResult} />
        </div>
    );
}

export default App;