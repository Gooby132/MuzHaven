import { useEffect, useState } from "react";
import styled from "styled-components";
import { PageTitle } from "components/atoms/texts/PageTitle";
import { SprededRow } from "components/layout/rows/SprededRow";
import { PageSizeButton } from "components/atoms/buttons/PageSizeButton";
import {
  CompleteProjectDto,
  ProjectDto,
} from "services/project/contracts";
import { fetchProjects } from "services/project/projectServiceClient";
import { useSelector } from "react-redux";
import { RootState } from "redux/store";
import { ProjectRow } from "components/molecules/projects/ProjectRow";
import { CreateProjectModal } from "components/organizem/modals/CreateProjectModal";
import Seperator from "components/atoms/layouts/Seperator";
import { createProject } from "services/user/userServiceClient";

const Container = styled.div`
  position: relative;
  color: ${({ theme }) => theme.text};
`;

type Props = {};

export const Projects = ({}: Props) => {
  const user = useSelector((state: RootState) => state.user);
  const [showCreateModal, setShowCreateModal] = useState<boolean>(false);
  const [createdProjects, setCreatedProjects] =
    useState<CompleteProjectDto[]>();

  const onSubmit = async (project: ProjectDto) => {
    const result = await createProject({
      token: user.token,
      project: project,
    });
  };

  useEffect(() => {
    const fetchCreatorProjects = async () => {
      if (user.token === undefined) return;

      const projects = await fetchProjects({
        token: user.token,
      });

      if (!projects.isError) setCreatedProjects(projects.result!.projects);
    };
    fetchCreatorProjects();
  }, []);

  return (
    <Container>
      <PageTitle text="Projects" />
      <SprededRow>
        <PageSizeButton onClick={() => setShowCreateModal((prev) => !prev)}>
          Create Project
        </PageSizeButton>
      </SprededRow>

      <div className="projects">
        {createdProjects?.map((project) => (
          <>
            <Seperator />
            <ProjectRow key={project.id} project={project} />
          </>
        ))}
      </div>

      <CreateProjectModal
        showCreateModal={showCreateModal}
        closeModalClicked={() => setShowCreateModal((prev) => !prev)}
        onSubmit={onSubmit}
      />
    </Container>
  );
};

export default Projects;
