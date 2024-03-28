import { SyntheticEvent } from "react";

interface Props {
    onBookshelfCreate: (e: SyntheticEvent) => void;
    isbn: string;
}

const AddBookshelf: React.FC<Props> = ({ onBookshelfCreate, isbn}: Props): JSX.Element => {
    return <div className="flex flex-col items-center justify-center flex-1 space-x-4 space-y-2 md:flex-row md:space-y-0">
        <form onSubmit={onBookshelfCreate}>
            <input readOnly={true} hidden={true} value={isbn} />
            <button
                type="submit"
                className="p-2 px-8 text-white bg-darkBlue rounded-lg hover:opacity-70 focus:outline-none"
            >
                Add
            </button>
        </form>
    </div>
}

export default AddBookshelf;