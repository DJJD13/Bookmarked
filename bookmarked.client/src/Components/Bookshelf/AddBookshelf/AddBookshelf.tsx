import { SyntheticEvent } from "react";

interface Props {
    onBookshelfCreate: (e: SyntheticEvent) => void;
    isbn: string;
}

const AddBookshelf: React.FC<Props> = ({ onBookshelfCreate, isbn}: Props): JSX.Element => {
    return <form onSubmit={onBookshelfCreate}>
        <input readOnly={true} hidden={true} value={isbn} />
        <button type="submit">Add</button>
    </form>;
}

export default AddBookshelf;