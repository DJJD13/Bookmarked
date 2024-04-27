import Hero from "../../Components/Hero/Hero";

interface Props { }

const HomePage: React.FC<Props> = (): JSX.Element => {
    return <div>
        <Hero />
    </div>;
}

export default HomePage;