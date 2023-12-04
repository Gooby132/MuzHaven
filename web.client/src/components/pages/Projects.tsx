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
import { ProjectDto } from "../../services/project/contracts";
import { CreateProjectForm } from "../layout/forms/CreateProjectForm";
import { createProject } from "../../services/project/projectServiceClient";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "redux/store";

const Container = styled.div`
  position: relative;
`;

type Props = {};

export const Projects = ({}: Props) => {
  const dispatcher = useDispatch()
  const user = useSelector((state: RootState) => state.user)
  const [showCreateModal, setShowCreateModal] = useState<boolean>(false);

  const onSubmit = async (project: ProjectDto) => {
    const result = await createProject({
      token: user.token,
      project: project
    })
  }

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
              <ModalTitle key={0} text="Create Project" />
            ]}
          footerChildren={[
            <BasicButton key={1} onClick={() => setShowCreateModal((prev) => !prev)} >
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
