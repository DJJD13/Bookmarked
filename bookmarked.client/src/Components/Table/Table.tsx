import { testBookData } from "./testdata";

const data = testBookData;

interface Props { }

type BookDetails = (typeof data)[0];




const Table: React.FC<Props> = (props: Props): JSX.Element => {
    const renderedRow = data.map((book) => {
        return (
            <tr key={book.isbn}>
                {configs.map((val: any) => {
                    return (
                        <td className="p-4 whitespace-nowrap text-sm font-normal text-gray-900">
                            {val.render(book)}
                        </td>
                    )
                }) }
                
            </tr>
        )
    })
    const renderedHeaders = configs.map((config: any) => {
        return (
            <th className="p-4 text-left text-xs font-medium text-gray-500 uppercase tracking-wider" key={config.label}>{config.label}</th>
        )
    })
    return <div className="bg-white shadow rounded-lg p-4 sm:p-6 xl:p-8">
        <table>
            <thead className="min-w-full divide-y divide=gray-200 m-5">{renderedHeaders}</thead>
            <tbody>{renderedRow}</tbody>
        </table>
    </div>;
}

export default Table;