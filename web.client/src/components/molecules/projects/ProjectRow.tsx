import { CardTitle } from "components/atoms/texts/CardTitle";
import { DisplayLabel } from "components/atoms/texts/DisplayLabel";
import { ProjectCardLayout } from "components/layout/projects/ProjectCardLayout";
import React from "react";
import { MusicalProfileDto, ProjectDto } from "services/project/contracts";
import styled from "styled-components";

const Container = styled.div`
  padding: .5em;
`;

type Props = {
  project: ProjectDto;
};

export const ProjectRow = ({ project }: Props) => {
  return (
    <Container>
      <ProjectCardLayout
        header={<CardTitle text={project.title} />}
        body={
          <>
            <DisplayLabel header="Album" text={project.album} />
            <DisplayLabel header="Description" text={project.description} />
          </>
        }
        footer={<p>BPM: {project.beatsPerMinute}</p>}
        footerNotes={
          project.musicalProfile && (
            <p>
              Profile: {project.musicalProfile?.key} {project.musicalProfile?.scale}
            </p>
          )
        }
      />
    </Container>
  );
};
