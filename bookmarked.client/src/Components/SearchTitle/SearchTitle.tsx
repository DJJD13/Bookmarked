import { SyntheticEvent } from "react";

interface Props {
    searchTitle: string;
    handleSearchTitleChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
    onSearchTitleSubmit: (e: SyntheticEvent) => void;
};

const SearchTitle: React.FC<Props> = ({ searchTitle, handleSearchTitleChange, onSearchTitleSubmit}: Props): JSX.Element => {
    return (
        <>
            <form onSubmit={onSearchTitleSubmit}>
                <input value={searchTitle} onChange={handleSearchTitleChange} placeholder="Title here..." />
            </form>
        </>
    );
}

export default SearchTitle;