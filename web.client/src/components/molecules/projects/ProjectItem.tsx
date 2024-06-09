import { CardTitle } from "components/atoms/texts/CardTitle";
import { DisplayLabel } from "components/atoms/texts/DisplayLabel";
import { ProjectCardLayout } from "components/layout/projects/ProjectCardLayout";
import { CompleteProjectDto } from "services/project/contracts";
import styled from "styled-components";
import { ShareIcon } from "components/atoms/icons/ShareIcon";
import { DeleteIcon } from "components/atoms/icons/DeleteIcon";
import { useNavigate } from "react-router-dom";

type StyledProps = {
  highlighted: boolean;
};

const ProjectItemBody = styled.div`
  display: grid;
  grid-template-columns: 3fr 1fr;
  > .action-buttons {
    display: flex;
    justify-content: space-around;
    align-items: center;
    > svg {
      max-height: 1.5em;
      cursor: pointer;
    }
  }
`;

const Container = styled.div<StyledProps>`
  padding: 0.5em;
  border: ${({ theme }) => theme.secondary} 1px solid;
  background: ${({ theme, highlighted }) => highlighted && theme.secondary};
`;

type Props = {
  project: CompleteProjectDto;
  onClick?: (id: string) => void;
  highlight?: boolean;
  deleteRequested?: (id: string) => void;
};

export const ProjectItem = ({
  project,
  onClick,
  highlight,
  deleteRequested,
}: Props) => {
  const navigate = useNavigate();

  return (
    <Container
      highlighted={highlight ?? false}
      onClick={() => {
        onClick && onClick(project.id);
      }}
    >
      <ProjectCardLayout
        header={<CardTitle text={project.title} />}
        body={
          <ProjectItemBody>
            <div>
              <DisplayLabel header="Album" text={project.album} />
              <DisplayLabel header="Description" text={project.description} />
            </div>
            <div className="action-buttons">
              <ShareIcon onClick={() => navigate(`/project/${project.id}`)} />
              <DeleteIcon
                onClick={() => deleteRequested && deleteRequested(project.id)}
              />
            </div>
          </ProjectItemBody>
        }
        footer={<p>BPM: {project.beatsPerMinute}</p>}
        footerNotes={
          project.musicalProfile && (
            <p>
              Profile: {project.musicalProfile?.key}{" "}
              {project.musicalProfile?.scale}
            </p>
          )
        }
      />
    </Container>
  );
};
