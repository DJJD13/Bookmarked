import { SyntheticEvent } from "react";

interface Props {
    searchIsbn: string | undefined;
    onSearchIsbnSubmit: (e: SyntheticEvent) => void;
    handleSearchIsbnChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
};

const Search: React.FC<Props> = ({ searchIsbn, onSearchIsbnSubmit, handleSearchIsbnChange }: Props): JSX.Element => {
    return (
        <>
            <section className="relative bg-gray-100">
                <div className="max-w-4xl mx-auto p-6 space-y-6">
                    <form
                        className="form relative flex flex-col w-full p-10 space-y-4 bg-darkBlue rounded-lg md:flex-row md:space-y-0 md:space-x-3"
                        onSubmit={onSearchIsbnSubmit}
                    >
                        <input
                            className="flex-1 p-3 border-2 rounded-lg placeholder-black focus:outline-none"
                            id="search-isbn-input"
                            placeholder="Search via ISBN"
                            value={searchIsbn}
                            onChange={handleSearchIsbnChange}
                        ></input>
                    </form>
                </div>
            </section>
        </>
    );
}

export default Search;