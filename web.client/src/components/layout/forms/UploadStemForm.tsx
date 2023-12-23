import { FileInput } from "components/atoms/form/FileInput";
import { SubmitInput } from "components/atoms/form/SubmitInput";
import { TextInput } from "components/atoms/form/TextInput";
import React, { useState } from "react";
import { StemDto } from "services/stem/contracts";
import styled from "styled-components";

const Container = styled.form``;

type Props = {
  onSubmit: (stem: StemDto, file: FileList) => void;
  projectId: string;
  creatorId: string;
};

export const UploadStemForm = ({ onSubmit, projectId, creatorId }: Props) => {
  const [stem, setStem] = useState<StemDto>({
    creatorId: creatorId,
    projectId: projectId,
    name: "",
    instrument: "",
    file: undefined,
    description: "",
    comments: []
  });

  return (
    <Container
      onSubmit={(e) => {
        e.preventDefault();

        if (stem.file === undefined) return;

        onSubmit(
          {
            creatorId: creatorId,
            projectId: projectId,
            instrument: stem.instrument,
            name: stem.name,
            file: stem.file,
            description: stem.description,
            comments: []
          },
          stem.file
        );
      }}
    >
      <TextInput
        name="name"
        onChange={(value) =>
          setStem((prev) => {
            return {
              ...prev,
              name: value,
            };
          })
        }
        text="Name"
      />
      <TextInput
        name="instrument"
        onChange={(value) =>
          setStem((prev) => {
            return {
              ...prev,
              instrument: value,
            };
          })
        }
        text="Instrument"
      />
      <TextInput
        name="description"
        onChange={(value) =>
          setStem((prev) => {
            return {
              ...prev,
              description: value,
            };
          })
        }
        text="Description"
      />
      <FileInput
        name="stem-file"
        text="file"
        onChange={(val) => {
          setStem((prev) => {
            return {
              ...prev,
              file: val,
            };
          });
        }}
      />
      <SubmitInput text="Upload" />
    </Container>
  );
};
