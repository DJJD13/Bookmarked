import { Outlet } from "react-router";

interface Props {
	children: React.ReactNode;
	isbn: string;
}

const BookDashboard: React.FC<Props> = ({ children, isbn }: Props): JSX.Element => {
	return (
		<div className="relative md:ml-64 bg-blueGray-100 w-full">
			<div className="relative pt-20 pb-32 bg-lightBlue-500">
				<div className="px-4 md:px-6 mx-auto w-full">
					<div>
						<div className="flex flex-wrap">{children}</div>
						<div className="flex flex-wrap">{<Outlet context={isbn} />}</div>
					</div>
				</div>
			</div>
		</div>
	);
};

export default BookDashboard;
