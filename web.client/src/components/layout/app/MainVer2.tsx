import styled, { ThemeProvider } from "styled-components";
import { Sidebar } from "../../organizem/Sidebar";
import { useSelector } from "react-redux";
import { RootState } from "../../../redux/store";
import { Footer } from "../../organizem/Footer";
import { Outlet } from "react-router-dom";
import { Header } from "../../organizem/Header";
import { WordMark } from "../../atoms/texts/WordMark";
import { HeaderLayout } from "../headers/HeaderLayout";
import { Searchbar } from "components/atoms/form/Searchbar";
import { ProjectsLinkGroup } from "components/organizem/projects/ProjectsLinkGroup";
import { SidebarLink } from "components/atoms/links/SidebarLink";
import { SlimSidebarLink } from "components/atoms/links/SlimSidebarLink";
import { useState } from "react";
import { MuzHavenTheme } from "themes/theme";
import { darkTheme } from "themes/darkTheme";

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
  const [theme] = useState<MuzHavenTheme>(darkTheme);

  return (
    <ThemeProvider theme={theme}>
      <Container className="app-layout">
        <Sidebar
          header={ <WordMark text="MuzHaven" /> }
          links={[
            <ProjectsLinkGroup
              key={"projectLinkGroup"}
              projects={projects.slice(0, 3).map((project, i) => (
                <SidebarLink
                  key={project.id}
                  project={project}
                  to={`/project/${project.id}`}
                  isActive={false}
                />
              ))}
            />,
          ]}
          footer={[
            <SlimSidebarLink
              key="create-project"
              to="/projects"
              text="Projects"
              isActive={true}
            />,
            <SlimSidebarLink
              key="profile"
              to="/profile"
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
