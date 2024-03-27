import { SyntheticEvent } from "react";

interface Props {
    onBookshelfDelete: (e: SyntheticEvent) => void;
    bookshelfValue: string;
}

const DeleteBookshelf: React.FC<Props> = ({ onBookshelfDelete, bookshelfValue}: Props): JSX.Element => {
    return (
        <form onSubmit={onBookshelfDelete}>
            <input readOnly={true} hidden={true} value={bookshelfValue} />
            <button type="submit">X</button>
        </form> 
    );
}

export default DeleteBookshelf;