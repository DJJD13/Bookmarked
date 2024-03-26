
interface Props {
    searchTitle: string;
    handleChangeTitle: (e: React.ChangeEvent<HTMLInputElement>) => void;
    handleClickTitle: () => void;
};

const SearchTitle: React.FC<Props> = ({ searchTitle, handleChangeTitle, handleClickTitle}: Props): JSX.Element => {
    return (
        <div>
            <input value={searchTitle} onChange={(e) => handleChangeTitle(e)}></input>
            <button onClick={() => handleClickTitle()} />
        </div>
    );
}

export default SearchTitle;