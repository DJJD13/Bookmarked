import { SyntheticEvent } from "react";
import CardBookshelf from "../CardBookshelf/CardBookshelf";
import { BookshelfGet } from "../../../Models/Bookshelf";

interface Props {
    bookshelfValues: BookshelfGet[];
    onBookshelfDelete: (e: SyntheticEvent) => void;
}


const ListBookshelf: React.FC<Props> = ({ bookshelfValues, onBookshelfDelete }: Props): JSX.Element => {
    return (
        <>
            <section id="bookshelf">
                <h2 className="mb-3 mt-3 text-3xl font-semibold text-center md:text-4xl">
                    My Bookshelf
                </h2>
                <div className="p-1 flex flex-wrap items-center justify-center">
                    <>
                        {bookshelfValues.length > 0 ? (
                            bookshelfValues.map((bookshelfValue) => {
                                return <CardBookshelf key={bookshelfValue.isbn} bookshelfValue={bookshelfValue} onBookshelfDelete={onBookshelfDelete} />;
                            })
                        ) : (
                            <h3 className="mb-3 mt-3 text-xl font-semibold text-center md:text-xl">
                                Your bookshelf is empty.
                            </h3>
                        )}
                    </>
                </div>
            </section>
            
        </>
    );
}

export default ListBookshelf;