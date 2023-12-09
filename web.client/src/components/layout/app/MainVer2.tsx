import styled, { ThemeProvider } from "styled-components";
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
import { ProjectsLinkGroup }from "components/organizem/projects/ProjectsLinkGroup";
import { SidebarLink } from "components/atoms/links/SidebarLink";
import { SlimSidebarLink } from "components/atoms/links/SlimSidebarLink";
import { useState } from "react";
import { MuzHavenTheme } from "themes/theme";
import { darkTheme } from "themes/darkTheme";
import { PageBase } from "../pages/PageBase";

type Props = {};

const Container = styled.div`
  position: relative;
  display: flex;
  min-height: 100vh;
  background-color: ${({ theme }) => theme.primary};

  > nav {
    position: sticky;
    top: 0;
    min-width: 20%;
  }
  > .main-container {
    min-width: 80%;
  }
`;

export const MainVer2 = ({}: Props) => {
  const { projects } = useSelector((state: RootState) => state.project);
  const [theme, setTheme] = useState<MuzHavenTheme>(darkTheme);

  return (
    <ThemeProvider theme={theme}>
      <Container className="app-layout">
        <Sidebar
          header={<WordMark text="MuzHaven" />}
          links={[
            <ProjectsLinkGroup
              projects={projects.map((project) => (
                <SidebarLink
                  key={project.id}
                  to={`/project/${project.id}`}
                  isActive={false}
                  itemId={project.id}
                />
              ))}
            />,
          ]}
          footer={[
            <SlimSidebarLink
              to="/projects"
              itemId="create-project"
              text="Create Project"
              isActive={true}
            />,
            <SlimSidebarLink
              to="/profile"
              itemId="profile"
              text="Profile"
              isActive={false}
            />,
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
    </ThemeProvider>
  );
};
