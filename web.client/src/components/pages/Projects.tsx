import React, { useState } from "react";
import styled from "styled-components";
import { PageTitle } from "../atoms/texts/PageTitle";
import { SprededRow } from "../layout/rows/SprededRow";
import { PageSizeButton } from "../atoms/buttons/PageSizeButton";
// @ts-ignore
import Modal from "react-modal";
import { BasicButton } from "../atoms/buttons/BasicButton";
import { BasicModalLayout } from "../layout/modals/BasicModalLayout";
import { ModalTitle } from "../atoms/texts/ModalTitle";
import { TextInput } from "../atoms/form/TextInput";
import { ProjectDto } from "../../services/project/Contracts";
import { CreateProjectForm } from "../layout/forms/CreateProjectForm";

const Container = styled.div`
  position: relative;
`;

type Props = {};

export const Projects = ({}: Props) => {
  const [showCreateModal, setShowCreateModal] = useState<boolean>(false);

  const onSubmit = (project: ProjectDto) => {}

  return (
    <Container>
      <PageTitle text="Projects" />
      <SprededRow>
        <PageSizeButton onClick={() => setShowCreateModal((prev) => !prev)}>
          Create Project
        </PageSizeButton>
      </SprededRow>
      <Modal isOpen={showCreateModal}>
        <BasicModalLayout
            headerChildren={[
              <ModalTitle text="Create Project" />
            ]}
          footerChildren={[
            <BasicButton onClick={() => setShowCreateModal((prev) => !prev)} >
              Close
            </BasicButton>,
          ]}
        >
          <CreateProjectForm onSubmit={onSubmit} />
        </BasicModalLayout>
      </Modal>
    </Container>
  );
};

export default Projects;
