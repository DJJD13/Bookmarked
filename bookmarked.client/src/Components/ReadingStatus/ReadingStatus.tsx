interface Props {
    handleReadingStatusChange: (e: React.ChangeEvent<HTMLSelectElement>) => void;
    readingStatus: number;
}

const ReadingStatus : React.FC<Props> = ({ readingStatus, handleReadingStatusChange }: Props) : JSX.Element => {
    return (
        <>
            <select value={readingStatus} onChange={handleReadingStatusChange} className={"my-2 p-2"}>
                <option value={0}>Want to Read</option>
                <option value={1}>Reading</option>
                <option value={2}>Finished</option>
            </select>
        </>
    );
};

export default ReadingStatus;