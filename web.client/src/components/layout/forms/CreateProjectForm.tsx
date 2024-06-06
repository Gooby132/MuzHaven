import { useState } from "react";
import { ProjectDto } from "services/project/contracts";
import styled from "styled-components";
import { TextInput } from "../../atoms/form/TextInput";
import { NumberInput } from "../../atoms/form/NumberInput";
import { KeyPicker } from "../../atoms/form/KeyPicker";
import { ScalePicker } from "../../atoms/form/ScalePicker";
import { SubmitInput } from "../../atoms/form/SubmitInput";

const Container = styled.form`
  >* {
    margin-top: .2em;
  }
`;

type Props = {
  onSubmit: (project: ProjectDto) => void;
};

export const CreateProjectForm = ({ onSubmit }: Props) => {
  const [createProjectFields, setCreateProjectFields] = useState<ProjectDto>({
    title: "",
    album: "",
    description: undefined,
    releaseInUtc: undefined,
    beatsPerMinute: undefined,
    musicalProfile: undefined,
  });

  return (
    <Container onSubmit={(e) => { 
      e.preventDefault()
      onSubmit(createProjectFields)
      }}>
      <TextInput
        name="title"
        text="Title"
        onChange={(val) => {
          setCreateProjectFields((prev) => {
            return {
              ...prev,
              title: val,
            };
          });
        }}
      />
      <TextInput
        name="album"
        text="Album"
        onChange={(val) => {
          setCreateProjectFields((prev) => {
            return {
              ...prev,
              album: val,
            };
          });
        }}
      />
      <TextInput
        name="description"
        text="Description"
        onChange={(val) => {
          setCreateProjectFields((prev) => {
            return {
              ...prev,
              description: val,
            };
          });
        }}
      />
      <NumberInput
        name="beatsPerMinute"
        text="BPM"
        onChange={(val) => {
          setCreateProjectFields((prev) => {
            return {
              ...prev,
              beatsPerMinute: val,
            };
          });
        }}
      />
      <KeyPicker
        onChange={(val) => {
          setCreateProjectFields((prev) => {
            return {
              ...prev,
              musicalProfile: {
                ...prev.musicalProfile,
                key: val,
              },
            };
          });
        }}
      />
      <ScalePicker
        onChange={(val) => {
          setCreateProjectFields((prev) => {
            return {
              ...prev,
              musicalProfile: {
                ...prev.musicalProfile,
                scale: val,
              },
            };
          });
        }}
      />
      <SubmitInput text="Create" />
    </Container>
  );
};
