import { SyntheticEvent } from "react";
import DeleteBookshelf from "../DeleteBookshelf/DeleteBookshelf";

interface Props {
    bookshelfValue: string;
    onBookshelfDelete: (e: SyntheticEvent) => void;
}

const CardBookshelf: React.FC<Props> = ({ bookshelfValue, onBookshelfDelete }: Props): JSX.Element => {
    return (<>
        <h4>{bookshelfValue}</h4>
        <DeleteBookshelf bookshelfValue={bookshelfValue} onBookshelfDelete={onBookshelfDelete} />
    </>);
}

export default CardBookshelf;