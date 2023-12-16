import { UploadStemForm } from "components/layout/forms/UploadStemForm";
import { BasicModalLayout } from "components/layout/modals/BasicModalLayout";
import { ModalTitle } from "components/atoms/texts/ModalTitle";
import { BasicButton } from "components/atoms/buttons/BasicButton";

// @ts-ignore
import Modal from "react-modal";
import { StemDto } from "services/stem/contracts";

type Props = {
  projectId: string;
  creatorId: string;
  showModal: boolean;
  closeModalClicked: () => void;
  onSubmit: (stem: StemDto, file: FileList) => void;
};

export const UploadStemModal = ({
  onSubmit,
  showModal,
  creatorId,
  projectId,
  closeModalClicked,
}: Props) => {
  return (
    <Modal isOpen={showModal}>
      <BasicModalLayout
        headerChildren={[<ModalTitle key={0} text="Upload Stem" />]}
        footerChildren={[
          <BasicButton key={1} onClick={() => closeModalClicked()}>
            Close
          </BasicButton>,
        ]}
      >
        <UploadStemForm onSubmit={onSubmit} projectId={projectId} creatorId={creatorId} />
      </BasicModalLayout>
    </Modal>
  );
};
