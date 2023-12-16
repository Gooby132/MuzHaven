import { TextInput } from "components/atoms/form/TextInput";
import React from "react";
import { ProjectDto } from "services/user/contracts";
import styled from "styled-components";

const Container = styled.div`
  display:grid;
  grid-template-columns: 1fr 1fr;
`;

type Props = {
  project: ProjectDto;
};

export const DetailsTab = ({ project }: Props) => {
  return (
    <Container>
      <TextInput
        name="title"
        text={"Title"}
        initialValue={project.title}
        onChange={() => {}}
      />
      <TextInput
        name="album"
        text={"Album"}
        initialValue={project.album}
        onChange={() => {}}
      />
      <TextInput
        name="description"
        text={"Description"}
        initialValue={project.description}
        onChange={() => {}}
      />
      
    </Container>
  );
};
