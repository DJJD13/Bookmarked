import { SyntheticEvent } from "react";
import DeleteBookshelf from "../DeleteBookshelf/DeleteBookshelf";
import { Link } from "react-router-dom";

interface Props {
    bookshelfValue: string;
    onBookshelfDelete: (e: SyntheticEvent) => void;
}

const CardBookshelf: React.FC<Props> = ({ bookshelfValue, onBookshelfDelete }: Props): JSX.Element => {
    return (<div className="flex flex-col w-full p-8 space-y-4 text-center rounded-lg shadow-lg md:w-1/3">
        <Link to={`/book/${bookshelfValue}`} className="pt-6 text-xl font-bold">{bookshelfValue}</Link>
        <DeleteBookshelf bookshelfValue={bookshelfValue} onBookshelfDelete={onBookshelfDelete} />
    </div>);
}

export default CardBookshelf;