import RatioList from "../../Components/RatioList/RatioList";
import Table from "../../Components/Table/Table";
import { testBookData } from "../../Components/Table/testdata";

interface Props { }

const tableConfig = [
    {
        label: "Title",
        render: (book: any) => book.title
    },
];

const DesignPage: React.FC<Props> = (props: Props) : JSX.Element => {
  return (
      <>
        <h1>Bookmarked Design Page</h1>
        <h2>This is Bookmarked's design page. This is where we will house various design aspects of the app</h2>
        <RatioList data={testBookData} config={tableConfig} />
        <Table />
      </> 
  );
}

export default DesignPage;