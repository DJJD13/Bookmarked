import { SyntheticEvent } from "react";
import CardBookshelf from "../CardBookshelf/CardBookshelf";

interface Props {
    bookshelfValues: string[];
    onBookshelfDelete: (e: SyntheticEvent) => void;
}


const ListBookshelf: React.FC<Props> = ({ bookshelfValues, onBookshelfDelete }: Props): JSX.Element => {
    return (
        <>
            <h3>My Bookshelf</h3>
            <ul>
                {bookshelfValues && 
                    bookshelfValues.map((bookshelfValue) => {
                        return <CardBookshelf bookshelfValue={bookshelfValue} onBookshelfDelete={onBookshelfDelete} />;
                    })
                }
            </ul>
        </>
    );
}

export default ListBookshelf;