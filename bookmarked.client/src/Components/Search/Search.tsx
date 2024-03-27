import { SyntheticEvent } from "react";

interface Props {
    searchIsbn: string | undefined;
    onSearchIsbnSubmit: (e: SyntheticEvent) => void;
    handleSearchIsbnChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
};

const Search: React.FC<Props> = ({ searchIsbn, onSearchIsbnSubmit, handleSearchIsbnChange }: Props): JSX.Element => {
    return (
        <>
            <form onSubmit={onSearchIsbnSubmit}>
                <input value={searchIsbn} onChange={handleSearchIsbnChange} placeholder="ISBN here..." />
            </form>
        </>
    );
}

export default Search;