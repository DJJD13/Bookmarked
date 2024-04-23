import { SyntheticEvent } from "react";

interface Props {
    pagesRead: number;
    totalPages: number;
    handlePagesReadChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
    onSubmitPagesRead: (e: SyntheticEvent) => void;
}
const ReadingProgress: React.FC<Props> = ({ pagesRead, totalPages, handlePagesReadChange, onSubmitPagesRead }: Props): JSX.Element => {
    return (
        <>
           <div className={"flex flex-row items-center "}>
               <form className={"my-2 p-1"} onSubmit={onSubmitPagesRead}>
                   <input className={"w-14 p-1 border-2 border-blue-200 rounded-md"} value={pagesRead} onChange={handlePagesReadChange}></input>
               </form>
               <div>
                   / 
               </div>
               <div>
                   {totalPages}
               </div>
           </div> 
        </>
    );
};

export default ReadingProgress;