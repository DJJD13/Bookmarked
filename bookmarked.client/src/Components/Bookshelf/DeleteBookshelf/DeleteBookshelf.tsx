import { SyntheticEvent } from "react";

interface Props {
    onBookshelfDelete: (e: SyntheticEvent) => void;
    bookshelfValue: string;
}

const DeleteBookshelf: React.FC<Props> = ({ onBookshelfDelete, bookshelfValue}: Props): JSX.Element => {
    return (
        <form onSubmit={onBookshelfDelete}>
            <input readOnly={true} hidden={true} value={bookshelfValue} />
            <button className="block w-full py-3 text-white duration-200 border-2 rounded-lg bg-red-500 hover:text-red-500 hover:bg-white border-red-500">
                X
            </button>
        </form> 
    );
}

export default DeleteBookshelf;