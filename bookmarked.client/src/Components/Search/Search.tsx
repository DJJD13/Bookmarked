
interface Props {
    search: string | undefined;
    onClick: (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => void;
    handleChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
};

const Search: React.FC<Props> = ({ search, onClick, handleChange }: Props): JSX.Element => {
    return (
        <div>
            <input value={search} onChange={(e) => handleChange(e)}></input>
            <button onClick={(e) => onClick(e)} />
        </div>
    );
}

export default Search;