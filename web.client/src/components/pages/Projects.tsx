import { useEffect, useState } from "react";
import styled from "styled-components";
import { PageSizeButton } from "components/atoms/buttons/PageSizeButton";
import { CompleteProjectDto, ProjectDto } from "services/project/contracts";
import { useSelector } from "react-redux";
import { RootState } from "redux/store";
import { ProjectItem } from "components/molecules/projects/ProjectItem";
import { CreateProjectModal } from "components/organizem/modals/CreateProjectModal";
import { PageBase } from "components/layout/pages/PageBase";
import { useFetchCreatorProjects } from "hooks/useFetchCreatorProjects";
import { useCreateProject } from "hooks/useCreateProject";
import { useDeleteProject } from "hooks/useDeleteProject";

const Container = styled.div`
  position: relative;

  .page-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 1em;
  }

  .projects > * {
    margin-top: 1em;
  }
`;

type Props = {};

export const Projects = ({}: Props) => {
  const { projects } = useSelector((state: RootState) => state.project);
  const [showCreateModal, setShowCreateModal] = useState<boolean>(false);
  const [selectedProject, setSelectedProject] = useState<CompleteProjectDto>();
  const [fetchProjects, fetchProjectsResponse] = useFetchCreatorProjects();
  const [create, createResponse] = useCreateProject();
  const [deleteProject, deleteResponse] = useDeleteProject();

  const onSubmit = async (project: ProjectDto) => {
    create(project);
    setShowCreateModal(false);
  };

  useEffect(() => {
    fetchProjects();
  }, [createResponse, deleteResponse]);

  return (
    <PageBase>
      <Container>
        <div className="page-header">
          <h1>Projects</h1>
          <PageSizeButton onClick={() => setShowCreateModal((prev) => !prev)}>
            Create Project
          </PageSizeButton>
        </div>

        <div className="projects">
          {projects.map((project) => (
            <ProjectItem
              key={project.id}
              highlight={selectedProject?.id === project.id}
              onClick={(id) =>
                setSelectedProject(projects.filter((p) => p.id === id)[0])
              }
              project={project}
              deleteRequested={id => {
                deleteProject(id)
              }}
            />
          ))}
        </div>

        <CreateProjectModal
          showCreateModal={showCreateModal}
          closeModalClicked={() => setShowCreateModal((prev) => !prev)}
          onSubmit={onSubmit}
        />
      </Container>
    </PageBase>
  );
};

export default Projects;
