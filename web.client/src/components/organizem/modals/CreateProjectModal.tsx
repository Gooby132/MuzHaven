import { BasicButton } from "components/atoms/buttons/BasicButton";
import { ModalTitle } from "components/atoms/texts/ModalTitle";
import { CreateProjectForm } from "components/layout/forms/CreateProjectForm";
import { BasicModalLayout } from "components/layout/modals/BasicModalLayout";
import React from "react";
// @ts-ignore
import Modal from "react-modal";
import { ProjectDto } from "services/project/contracts";

type Props = {
  showCreateModal: boolean,
  closeModalClicked: () => void,
  onSubmit: (project: ProjectDto) => void;
};

export const CreateProjectModal = ({showCreateModal, closeModalClicked, onSubmit}: Props) => {
  return (
    <Modal isOpen={showCreateModal}>
      <BasicModalLayout
        headerChildren={[<ModalTitle key={0} text="Create Project" />]}
        footerChildren={[
          <BasicButton
            key={1}
            onClick={() => closeModalClicked()}
          >
            Close
          </BasicButton>,
        ]}
      >
        <CreateProjectForm onSubmit={onSubmit} />
      </BasicModalLayout>
    </Modal>
  );
};
