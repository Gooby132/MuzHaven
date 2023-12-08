import styled, { useTheme } from "styled-components";
import { Sidebar } from "../../organizem/Sidebar";
import { CoreLink } from "../../atoms/links/CoreLink";
import { LinkGroup } from "../../molecules/LinkGroup";
import { useSelector } from "react-redux";
import { RootState } from "../../../redux/store";
import { Footer } from "../../organizem/Footer";
import { Outlet } from "react-router-dom";
import { Header } from "../../organizem/Header";
import { WordMark } from "../../atoms/texts/WordMark";
import { HeaderLayout } from "../headers/HeaderLayout";
import { Searchbar } from "components/atoms/form/Searchbar";
import ProjectsLinkGroup from "components/organizem/projects/ProjectsLinkGroup";

type Props = {};

const Container = styled.div`
  background-color: ${({ theme }) => theme.primary};
  display: flex;
  min-height: 100vh;
  > nav {
    min-width: 20%;
  }
  > .main-container {
    min-width: 80%;
  }
`;

export const MainVer2 = ({}: Props) => {
  const user = useSelector((state: RootState) => state.user);

  const loggedInRoutes = [
    <CoreLink key={0} text="profile" to="profile" />,
    <CoreLink key={1} text="projects" to="projects" />,
  ];

  const logInRoutes = [
    <CoreLink key={1} text="login" to="login" />,
    <CoreLink key={2} text="register" to="register" />,
  ];

  return (
    <Container className="app-layout">
      <Sidebar
        header={<WordMark text="MuzHaven" />}
        links={[
          <LinkGroup
            key={0}
            header="User"
            links={user.loggedIn ? loggedInRoutes : logInRoutes}
          />,
        ]}
        footer={[
          <ProjectsLinkGroup
          />
        ]}
      />
      <section className="main-container">
        <Header
          layout={
            <HeaderLayout
              search={<Searchbar onChange={(e) => {}} />}
              userIcon={<div></div>}
            />
          }
        />
        <Outlet />
        <Footer />
      </section>
    </Container>
  );
};
