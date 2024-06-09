import { TextInput } from "components/atoms/form/TextInput";
import { ProjectDto } from "services/user/contracts";
import styled from "styled-components";

const Container = styled.div`
  display: flex;
  flex-wrap: wrap;
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
